using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{

    [SerializeField] public GameObject player;
    List<Transform> respawnPoints = new List<Transform>();
    float upperLimit, bottomLimit, leftLimit, rightLimit;
    Vector3 playerPos = new Vector3();
    GameObject respawnPositions;
    Rigidbody2D playerRigidbody;
    BoxCollider2D playerHurtbox;
    PolygonCollider2D dummyHurtbox;
    AudioSource lostLifeSound, respawnSound;
    SpriteRenderer playerSprite;
    Color spawnProtectionColor = new Color(1f, 1f, 1f, 0.3f);
    Color normalPlayerColor = new Color(1f, 1f, 1f, 1f);
    int randomSpawnn;
    public int lives = 2;

    void Start()
    {
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        playerSprite = player.GetComponent<SpriteRenderer>();
        foreach (Transform child in player.transform)
        {
            if (LayerMask.LayerToName(child.gameObject.layer) == "Hurtbox")
            {
                playerHurtbox = child.gameObject.GetComponent<BoxCollider2D>();
                if (!playerHurtbox)
                {
                    dummyHurtbox = child.gameObject.GetComponent<PolygonCollider2D>();
                    spawnProtectionColor = new Color(1f, 0f, 0f, 0.3f);
                    normalPlayerColor = new Color(1f, 0f, 0f, 1f);
                }
                break;
            }
        }

        respawnPositions = GameObject.Find("RespawnPositions");
        if (respawnPoints != null)
        {
            foreach (Transform child in respawnPositions.transform)
            {
                respawnPoints.Add(child);
            }
        }

        upperLimit = GameObject.Find("EdgeLimits/UpperLimit").transform.position.y;
        bottomLimit = GameObject.Find("EdgeLimits/BottomLimit").transform.position.y;
        leftLimit = GameObject.Find("EdgeLimits/LeftLimit").transform.position.x;
        rightLimit = GameObject.Find("EdgeLimits/RightLimit").transform.position.x;

        lostLifeSound = GameObject.Find("Scenery/Sounds/LostLife").GetComponent<AudioSource>();
        respawnSound = GameObject.Find("Scenery/Sounds/Respawn").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (upperLimit != 0f && bottomLimit != 0f && leftLimit != 0f && rightLimit != 0f)
        {
            playerPos = player.transform.position;
            if ((playerPos.y < bottomLimit || playerPos.y > upperLimit || playerPos.x < leftLimit || playerPos.x > rightLimit) && lives > 0)
            {
                lives --;
                lostLifeSound.Play();
                StartCoroutine(RespawnPoint());
                Transform removeLives = GameObject.Find("Canvas").GetComponent<PlayerDmg>().playerProfile[gameObject.name].transform.Find("vida"+lives);
                Debug.Log(removeLives);

                Destroy(removeLives.gameObject);
            }
        }

        if (lives <= 0)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    IEnumerator RespawnPoint()
    {
        if (playerHurtbox)
        {
            playerHurtbox.enabled = false;
        }
        else
        {
            dummyHurtbox.enabled = false;
        }

        if (respawnPoints.Count > 0)
        {
            randomSpawnn = Random.Range(0, respawnPoints.Count);
            player.transform.position = respawnPoints[randomSpawnn].position;
        }
        else
        {
            player.transform.position = new Vector2(0, 10);
        }

        playerRigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
        yield return new WaitForSeconds(2.5f);
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        playerRigidbody.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        playerRigidbody.WakeUp();
        respawnSound.Play();
        yield return new WaitForSeconds(1);
        playerRigidbody.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        StartCoroutine(SpawnProtection());
    }

    IEnumerator SpawnProtection()
    {
        float count = 0;
        while (count < 4)
        {
            if (playerSprite.color == normalPlayerColor)
            {
                playerSprite.color = spawnProtectionColor;
            }
            else
            {
                playerSprite.color = normalPlayerColor;
            }
            count += 0.25f;
            yield return new WaitForSeconds(0.25f);
        }

        if (playerHurtbox)
        {
            playerHurtbox.enabled = true;
        }
        else
        {
            dummyHurtbox.enabled = true;
        }
    }
}
