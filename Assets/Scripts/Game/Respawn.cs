using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Respawn : MonoBehaviour
{

    [SerializeField] public GameObject player;
    List<Transform> respawnPoints = new List<Transform>();
    float upperLimit, bottomLimit, leftLimit, rightLimit;
    Vector3 playerPos = new Vector3();
    GameObject respawnPositions;
    Rigidbody2D playerRigidbody;
    BoxCollider2D playerHurtbox;
    PlayerInput playerInput;
    PolygonCollider2D dummyHurtbox;
    AudioSource lostLifeSound, respawnSound;
    SpriteRenderer playerSprite;
    Hurtbox hurtboxScript;
    DynamicCamera cameraScript;
    Camera mainCamera;
    FightIntroEnding introEndingScript;
    Color spawnProtectionColor = new Color(1f, 1f, 1f, 0.3f);
    Color normalPlayerColor = new Color(1f, 1f, 1f, 1f);
    int randomSpawn;
    bool keepCoroutine;
    public int startingNumberLives;
    [HideInInspector] public int lives = 3;

    void Start()
    {
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        playerSprite = player.GetComponent<SpriteRenderer>();
        playerInput = player.GetComponent<PlayerInput>();
        startingNumberLives = lives;
        foreach (Transform child in player.transform)
        {
            if (LayerMask.LayerToName(child.gameObject.layer) == "Hurtbox")
            {
                playerHurtbox = child.gameObject.GetComponent<BoxCollider2D>();
                hurtboxScript = child.gameObject.GetComponent<Hurtbox>();
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

        cameraScript = GameObject.Find("Main Camera").GetComponent<DynamicCamera>();

        introEndingScript = GameObject.Find("Intro&EndingManager").GetComponent<FightIntroEnding>();
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
                hurtboxScript.dmgPercent = 0.0f;
                hurtboxScript.UpdateDmgPercentText();
                StartCoroutine(RespawnPoint());
                Transform removeLives = GameObject.Find("Canvas").GetComponent<PlayerDmg>().playerProfile[transform.parent.name].transform.Find("vida"+lives);
                Debug.Log(removeLives);

                Destroy(removeLives.gameObject);
            }
        }

        if (lives <= 0)
        {
            cameraScript.DecreasePlayersCountByOne();
            Destroy(transform.parent.gameObject);
            introEndingScript.CheckForWinner();
        }
    }

    IEnumerator RespawnPoint()
    {
        keepCoroutine = false;
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
            randomSpawn = Random.Range(0, respawnPoints.Count);
            player.transform.position = respawnPoints[randomSpawn].position;
        }
        else
        {
            player.transform.position = new Vector2(0, 10);
        }

        if (playerInput)
        {
            playerInput.enabled = false;
        }

        playerRigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
        yield return new WaitForSeconds(2.5f);
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        playerRigidbody.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        playerRigidbody.WakeUp();
        respawnSound.Play();
        yield return new WaitForSeconds(0.5f);

        if (playerInput)
        {
            playerInput.enabled = true;
        }

        keepCoroutine = true;
        StartCoroutine(SpawnProtection());
    }

    IEnumerator SpawnProtection()
    {
        float count = 0;

        while (count < 4 && keepCoroutine)
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

        playerSprite.color = normalPlayerColor;

        if (keepCoroutine)
        {
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
}
