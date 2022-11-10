using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRock : MonoBehaviour
{
    float upperLimit, bottomLimit, leftLimit, rightLimit, originalYCoordinate;
    Vector3 rockPosition = new Vector3();
    Vector3 respawnRockPosition = new Vector3();
    Rigidbody rockRigidbody;
    RigidbodyConstraints originalConstraints;

    // Start is called before the first frame update
    void Start()
    {
        rockPosition = transform.position;
        originalYCoordinate = rockPosition.y;
        upperLimit = GameObject.Find("EdgeLimits/UpperLimit").transform.position.y;
        bottomLimit = GameObject.Find("EdgeLimits/BottomLimit").transform.position.y;
        leftLimit = GameObject.Find("EdgeLimits/LeftLimit").transform.position.x;
        rightLimit = GameObject.Find("EdgeLimits/RightLimit").transform.position.x;
        rockRigidbody = gameObject.GetComponent<Rigidbody>();
        respawnRockPosition = rockPosition;
        respawnRockPosition.y = upperLimit + 3;
        originalConstraints = rockRigidbody.constraints;
        StartCoroutine(CheckIfNeedsForce());
    }

    // Update is called once per frame
    void Update()
    {
        rockPosition = transform.position;
        if (rockPosition.y < bottomLimit || rockPosition.y > upperLimit + 5 || rockPosition.x < leftLimit || rockPosition.x > rightLimit)
        {
            StartCoroutine(RespawnAndFreezeRock());
        }
    }

    IEnumerator RespawnAndFreezeRock()
    {
        transform.position = respawnRockPosition;
        rockRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        yield return new WaitForSeconds(20f);
        rockRigidbody.constraints = originalConstraints;
        rockRigidbody.WakeUp();
        StartCoroutine(CheckIfNeedsForce());
    }

    IEnumerator CheckIfNeedsForce()
    {
        yield return new WaitForSeconds(40f);
        if (rockPosition.y < originalYCoordinate + 2 && rockPosition.y > originalYCoordinate - 2)
        {
            rockRigidbody.AddForce(Vector3.left * 200);
        }
    }
}
