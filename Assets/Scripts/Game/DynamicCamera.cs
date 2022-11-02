using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCamera : MonoBehaviour
{
    float cameraEulerX;
    Vector3 cameraPosition;
    private IEnumerator coroutine; 
    public FocusPoint FocusPoint;
    public List<GameObject> Players;
    public float depthUpdateSpeed = 5f;
    public float positionUpdateSpeed = 5f;
    public float maxDepth, minDepth;
    [HideInInspector] public int playersCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        Players.Add(FocusPoint.gameObject);
        coroutine = UpdatePlayersList(0.3f);
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CalculateCameraLocations();
        MoveCamera();
    }

    private void MoveCamera()
    {
        Vector3 position = gameObject.transform.position;
        if (position != cameraPosition)
        {
            Vector3 targetPosition = Vector3.zero;
            targetPosition.x = Mathf.MoveTowards(position.x, cameraPosition.x, positionUpdateSpeed * Time.deltaTime);
            targetPosition.y = Mathf.MoveTowards(position.y, cameraPosition.y, positionUpdateSpeed * Time.deltaTime);
            targetPosition.z = Mathf.MoveTowards(position.z, cameraPosition.z, depthUpdateSpeed * Time.deltaTime);
            gameObject.transform.position = targetPosition;
        }
    }

    private void CalculateCameraLocations()
    {
        Vector3 averageCenter = Vector3.zero;
        Vector3 totalPositions = Vector3.zero;
        Bounds playerBounds = new Bounds();

        for (int i = 0; i < Players.Count; i ++)
        {
            if (Players[i])
            {
                Vector3 playerPosition = Players[i].transform.position;

                if (!FocusPoint.focusBounds.Contains(playerPosition))
                {
                    float playerX = Mathf.Clamp(playerPosition.x, FocusPoint.focusBounds.min.x, FocusPoint.focusBounds.max.x);
                    float playerY = Mathf.Clamp(playerPosition.y, FocusPoint.focusBounds.min.y, FocusPoint.focusBounds.max.y);
                    float playerZ = Mathf.Clamp(playerPosition.z, FocusPoint.focusBounds.min.z, FocusPoint.focusBounds.max.z);
                    playerPosition = new Vector3(playerX, playerY, playerZ);
                }

                totalPositions += playerPosition;
                playerBounds.Encapsulate(playerPosition);

                averageCenter = (totalPositions / playersCount);

                float extents = (playerBounds.extents.x + playerBounds.extents.y);
                float lerpPercent = Mathf.InverseLerp(0, (FocusPoint.halfXBounds + FocusPoint.halfYBounds) / 2, extents);

                float depth = Mathf.Lerp(maxDepth, minDepth, lerpPercent);

                cameraPosition = new Vector3(averageCenter.x, averageCenter.y, depth);
            }
        }
    }

    private IEnumerator UpdatePlayersList(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Players.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        playersCount = Players.Count;
    }

    public void decreasePlayersCountByOne()
    {
        playersCount --;
    }
}
