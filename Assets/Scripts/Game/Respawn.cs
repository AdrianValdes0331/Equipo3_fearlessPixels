using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Respawn : MonoBehaviour
{

    public GameObject player, hurtbox;
    List<Transform> respawnPoints = new List<Transform>();
    float upperLimit, bottomLimit, leftLimit, rightLimit;
    Vector3 playerPos = new Vector3();
    Vector3 childPos = new Vector3();
    GameObject respawnPositions;
    Rigidbody2D playerRigidbody;
    BoxCollider2D playerHurtbox;
    PlayerInput playerInput;
    PolygonCollider2D dummyHurtbox;
    AudioSource lostLifeSound, respawnSound;
    SpriteRenderer playerSprite;
    NHurtbox hurtboxScript;
    DynamicCamera cameraScript;
    Camera mainCamera;
    FightIntroEnding introEndingScript;
    Color spawnProtectionColor;
    Color normalPlayerColor;
    int randomSpawn;
    bool keepCoroutine;
    public int startingNumberLives;
    [HideInInspector] public int lives = 3;

    void Start()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
        playerInput = gameObject.GetComponent<PlayerInput>();
        playerSprite = player.GetComponent<SpriteRenderer>();
        playerHurtbox = hurtbox.GetComponent<BoxCollider2D>();
        hurtboxScript = hurtbox.GetComponent<NHurtbox>();
        startingNumberLives = lives;

        if(!playerRigidbody)
        {
            playerRigidbody = player.GetComponent<Rigidbody2D>();
        }

        if (!playerHurtbox)
        {
            dummyHurtbox = hurtbox.GetComponent<PolygonCollider2D>();
        }

        if (playerSprite.color == new Color(1f, 1f, 1f, 1f))
        {
            normalPlayerColor = new Color(1f, 1f, 1f, 1f);
            spawnProtectionColor = new Color(1f, 1f, 1f, 0.3f);
        }
        else
        {
            normalPlayerColor = new Color(1f, 0f, 0f, 1f);
            spawnProtectionColor = new Color(1f, 0f, 0f, 0.3f);
        }

        respawnPositions = GameObject.Find("RespawnPositions");
        if (respawnPositions != null)
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

        GameObject fightIntroEnding = GameObject.Find("Intro&EndingManager");
        if (fightIntroEnding)
        {
            introEndingScript = fightIntroEnding.GetComponent<FightIntroEnding>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (upperLimit != 0f && bottomLimit != 0f && leftLimit != 0f && rightLimit != 0f)
        {
            playerPos = transform.position;
            childPos = transform.GetChild(0).position;
            if ((playerPos.y < bottomLimit || playerPos.y > upperLimit || playerPos.x < leftLimit || playerPos.x > rightLimit || childPos.y < bottomLimit || childPos.y > upperLimit || childPos.x < leftLimit || childPos.x > rightLimit) && lives > 0)
            {
                StartCoroutine(RespawnPoint());
                lives--;
                lostLifeSound.Play();
                hurtboxScript.dmgPercent = 0.0f;
                hurtboxScript.UpdateDmgPercentText();
                Transform lifeLost = GameObject.Find("Canvas").GetComponent<PlayerDmg>().playerProfile[transform.name].transform.Find("vida" + lives);
                Destroy(lifeLost.gameObject);
            }
        }

        if (lives <= 0)
        {
            cameraScript.DecreasePlayersCountByOne();
            Destroy(gameObject);
            if (introEndingScript)
            {
                introEndingScript.CheckForWinner();
            }
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

        randomSpawn = Random.Range(0, respawnPoints.Count);
        if (playerHurtbox)
        {
            transform.position = respawnPoints[randomSpawn].position;
        }
        else
        {
            transform.GetChild(0).transform.position = respawnPoints[randomSpawn].position;
        }

        if (playerInput)
        {
            playerInput.enabled = false;
        }

        playerRigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
        yield return new WaitForSeconds(2.5f);

        if (playerInput)
        {
            playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            playerRigidbody.constraints &= ~RigidbodyConstraints2D.FreezeAll;
        }

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
