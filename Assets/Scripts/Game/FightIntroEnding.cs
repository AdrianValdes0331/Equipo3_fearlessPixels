using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Photon.Pun;
using Photon.Realtime;
using DG.Tweening;
using TMPro;

public class FightIntroEnding : MonoBehaviourPunCallbacks
{
    public Image ready, fight, winner, tie; //BF5959 FFFFFF
    List<GameObject> Drivers = new List<GameObject>();
    List<GameObject> Players = new List<GameObject>();
    List<GameObject> HUDElements = new List<GameObject>();
    List<GameObject> PlayerImages = new List<GameObject>();
    List<GameObject> LifeImages = new List<GameObject>();
    Color fadedTextColor = new Color(1f, 1f, 1f, 0f);
    Color normalTextColor = new Color(1f, 1f, 1f, 1f);
    Color opaqueLifeColor = new Color(0.5f, 0.3f, 0.3f);
    Color brightLifeColor = new Color(1f, 1f, 1f);
    DynamicCamera cameraScript;
    Timer timerScript;
    bool isOnlineB;
    TimerOnline timerOnlineScript;
    public AudioSource readySound, fightSound, winSound, win2Sound, win3Sound, tieSound, backgroundMusic;
    float normalReadyDuration = 0.75f;
    float fastReadyDuration = 0.1f;
    float normalFightDuration = 0.25f;
    float fastFightDuration = 0.05f;
    float normalWinnerDuration = 0.75f;
    float fastWinnerDuration = 0.075f;
    float introDelay = 1f;
    float scaleHUDDelay = 0.15f;
    float changeColorHUDDelay = 1f;
    float fadeInHUDDuration = 5f;
    float scaleHUDDuration = 1.75f;
    float changeColorHUDDuration = 0.25f;
    int readyTimes = 4;
    int fightTimes = 5;
    int winnerTimes = 7;
    int lifeColorChangeTimes = 11;
    int livesNumber;
    private AudioSource[] allAudioSources;

    void Awake()
    {
        StopAllAudio();
    }

    void Start()
    {
        backgroundMusic = GameObject.Find("Scenery/Sounds/BackgroundMusic").GetComponent<AudioSource>();
        backgroundMusic.Play();
        int isOnline = PlayerPrefs.GetInt("isOnline");
        if (isOnline.Equals(1))
        {
            isOnlineB = true;
            if (PhotonNetwork.IsMasterClient)
            {
                photonView.RPC("ShowReadyFightOnline", RpcTarget.All);
            }
        }
        else
        {
            isOnlineB = false;
            ShowReadyFightOffline();
        }
    }

    void StopAllAudio()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Stop();
        }
    }

    void ShowReadyFightOffline()
    {
        StartCoroutine(WaitForPlayers());
        cameraScript = GameObject.Find("Main Camera").GetComponent<DynamicCamera>();
        timerScript = GameObject.Find("TimerOffline").GetComponent<Timer>();
        ready.color = fadedTextColor;
        fight.color = fadedTextColor;
        winner.color = fadedTextColor;
        tie.color = fadedTextColor;
        StartCoroutine(PlayFightIntro());
        StartCoroutine(StartingHUDAnimation());
    }

    [PunRPC]
    void ShowReadyFightOnline()
    {
        StartCoroutine(WaitForPlayersOnline());
        cameraScript = GameObject.Find("Main Camera").GetComponent<DynamicCamera>();
        timerOnlineScript = GameObject.Find("TimerOnline").GetComponent<TimerOnline>();
        ready.color = fadedTextColor;
        fight.color = fadedTextColor;
        winner.color = fadedTextColor;
        tie.color = fadedTextColor;
        StartCoroutine(PlayFightIntro());
        StartCoroutine(StartingHUDAnimation());
    }

    IEnumerator WaitForPlayersOnline()
    {
        yield return new WaitForSeconds(0.205f);
    }

    IEnumerator WaitForPlayers()
    {
        yield return new WaitForSeconds(0.205f);
        Drivers.AddRange(GameObject.FindGameObjectsWithTag("Driver"));
        Players.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        for (int i = 0; i < Drivers.Count; i++)
        {
            PlayerInput currentPlayerInput = Drivers[i].GetComponent<PlayerInput>();
            if (currentPlayerInput)
            {
                currentPlayerInput.enabled = false;
            }
        }
        FreezePlayers(Drivers);
    }

    public void CheckForWinner()
    {
        List<GameObject> AlivedPlayers = new List<GameObject>();
        AlivedPlayers.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        if (AlivedPlayers.Count - 1 == 1)
        {
            if (isOnlineB)
            {
                timerOnlineScript.startTimer = false;
            } else
            {
                timerScript.TimerOn = false;
            }
            FreezePlayers(AlivedPlayers);
            cameraScript.FocusWinner(null);
            StartCoroutine(PlayEnding(true));
        }
    }

    public void CheckForWinnerWhenTimeUp()
    {
        List<GameObject> AlivedPlayers = new List<GameObject>();
        AlivedPlayers.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        FreezePlayers(AlivedPlayers);
        livesNumber = AlivedPlayers[0].GetComponent<Respawn>().startingNumberLives;
        int[] livesOfPlayers = new int[AlivedPlayers.Count];
        int[] livesCount = new int[livesNumber];
        for (int i = 0; i < AlivedPlayers.Count; i++)
        {
            int currentPlayerLives = AlivedPlayers[i].GetComponent<Respawn>().lives;
            livesOfPlayers[i] = currentPlayerLives;
            livesCount[currentPlayerLives - 1]++;
        }

        for (int i = livesNumber - 1; i >= 0; i--)
        {
            if (livesCount[i] == 1)
            {
                cameraScript.FocusWinner(AlivedPlayers[System.Array.IndexOf(livesOfPlayers, i + 1)]);
                StartCoroutine(PlayEnding(true));
                return;
            }
        }
        StartCoroutine(PlayEnding(false));
    }

    public void FreezePlayers(List<GameObject> AlivedPlayers)
    {
        for (int i = 0; i < AlivedPlayers.Count; i++)
        {
            PlayerInput currentPlayerInput = AlivedPlayers[i].GetComponent<PlayerInput>();
            if (currentPlayerInput)
            {
                currentPlayerInput.enabled = false;
            }
            else
            {
                Rigidbody2D playerRigidbody = AlivedPlayers[i].GetComponent<Rigidbody2D>();
                playerRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }

    public void PlayEndingSound(bool victory)
    {
        if (victory)
        {
            int randomVictorySound = Random.Range(0, 3);
            switch (randomVictorySound)
            {
                case 0:
                    winSound.Play();
                    break;
                case 1:
                    win2Sound.Play();
                    break;
                case 2:
                    win3Sound.Play();
                    break;
            }
        }
        else
        {
            tieSound.Play();
        }
    }

    //Corrutina para animar el HUD al comienzo de la partida
    IEnumerator StartingHUDAnimation()
    {
        //Se hace una peque�a pausa para darle tiempo a la instanciaci�n de elementos de la UI
        yield return new WaitForSeconds(introDelay);
        HUDElements.AddRange(GameObject.FindGameObjectsWithTag("HUD"));
        //Todos los elementos encontrados con la etiqueta "HUD" y sus hijos son mandados a hacer FadeIn
        foreach (GameObject hudElement in HUDElements)
        {
            Image elementImage = hudElement.GetComponent<Image>();
            if (elementImage)
            {
                //Se manda al padre a hacer FadeIn
                FadeInHUDImage(elementImage);
            }

            //Todos los hijos son mandados a hacer FadeIn
            FadeInChilds(hudElement.transform);
        }

        PlayerImages.AddRange(GameObject.FindGameObjectsWithTag("PlayerImage"));
        //Todos los elementos encontrados con la etiqueta "PlayerImage" (ilustraci�n de peleadores) son mandados a escalarse
        foreach (GameObject playerImage in PlayerImages)
        {
            ScaleHUDTransform(playerImage.transform, 1.25f, 1f);
        }

        LifeImages.AddRange(GameObject.FindGameObjectsWithTag("LifeImage"));
        //Todos los elementos encontrados con la etiqueta "LifeImage" (corazones de vidas) son mandados a escalarse y a cambiar de color
        foreach (GameObject lifeImage in LifeImages)
        {
            Image image = lifeImage.GetComponent<Image>();
            StartCoroutine(ChangeColorHUDImage(image));
            ScaleHUDTransform(lifeImage.transform, 0.2f, 0.08f);
        }
    }

    //Aplica el FadeIn a todos los hijos del Transform mandado y hace recursi�n para hacerlo extensivamente
    public void FadeInChilds(Transform element)
    {
        foreach (Transform child in element)
        {
            Image childImage = child.gameObject.GetComponent<Image>();
            if (childImage)
            {
                //Si se reconoce un componente Image en el elemento, se manda a llamar FadeIn con su respectiva funci�n
                FadeInHUDImage(childImage);
            }
            else
            {
                TextMeshProUGUI childText = child.gameObject.GetComponent<TextMeshProUGUI>();
                if (childText)
                {
                    //Si se reconoce un componente TextMeshProUGUI en el elemento, se manda a llamar FadeIn con su respectiva funci�n
                    FadeInHUDText(childText);
                }
            }
            //Aqu� se aplica la recursi�n mandando a llamar la funci�n nuevamente con cada hijo
            FadeInChilds(child);
        }
    }

    //Aplica un FadeIn de Image con DOTween - DOFade
    public void FadeInHUDImage(Image image)
    {
        /*Previamente, todos los elementos del HUD en escena son puestos con su alpha en 0f
        para lograr conseguir el efecto esperado. La transicion se hace con una curva OutQuart,
        comenzando con una velocidad media y finalizando con una lenta (tangente horizontalmente)*/
        image.DOFade(1f, fadeInHUDDuration).SetEase(Ease.OutQuart);
    }

    //Aplica un FadeIn de TextMeshPro con DOTween - DOFade
    public void FadeInHUDText(TextMeshProUGUI text)
    {
        //Las propiedades son iguales a las de FadeInHUDImage
        text.DOFade(1f, fadeInHUDDuration).SetEase(Ease.OutQuart);
    }

    //Aplica un Scale de Transform con DOTween - DOScale
    public void ScaleHUDTransform(Transform transform, float bigScale, float finalScale)
    {
        /*Previamente, los elementos a los que se aplica la transici�n son escalados con un tama�o menor
        al est�ndar final. La animaci�n comienza escalando el transform a un tama�o un poco mayor al deseado (bigScale),
        con una curva InQuad (cada vez m�s r�pida) y un peque�o delay. Luego, al completarse la transici�n pasada, 
        el elemento se escala a su tama�o final correcto (finalScale), con una curva OutExpo, comenzando con una 
        velocidad r�pida y finalizando con una lenta (tangente horizontalmente)*/
        transform.DOScale(bigScale, scaleHUDDuration).SetEase(Ease.InQuad).SetDelay(scaleHUDDelay).OnComplete(
                    () => transform.DOScale(finalScale, scaleHUDDuration / 2).SetEase(Ease.OutExpo)
                );
    }

    //Corrutina que aplica un Color Change de Image con DOTween - DOColor cierto n�mero de veces
    IEnumerator ChangeColorHUDImage(Image image)
    {
        //Se hace una peque�a pausa para aplicar el delay de animaci�n deseado
        yield return new WaitForSeconds(changeColorHUDDelay);
        //La animaci�n se realiza cierto n�mero de veces por medio de un ciclo for
        for (int i = 0; i < lifeColorChangeTimes; i ++)
        {
            /*Si el index del for es par, se hace la transici�n a un color brillante de la vida.
            si es impar, se hace la transici�n al color opaco de la vida*/
            if (i % 2 == 0)
            {
                /*La transici�n se realiza con una curva InOutCubic y una duraci�n corta*/
                image.DOColor(brightLifeColor, changeColorHUDDuration).SetEase(Ease.InOutCubic);
            }
            else
            {
                /*La transici�n se realiza con una curva OutQuart y una duraci�n corta*/
                image.DOColor(opaqueLifeColor, changeColorHUDDuration).SetEase(Ease.OutQuart);
            }
            //Se hace una pausa despu�s de cada vuelta en el for para darle tiempo a la transici�n en ejecuci�n de terminar antes de la siguiente
            yield return new WaitForSeconds(changeColorHUDDuration + 0.1f);
        }
    }

    //Corrutina que aplica un FadeIn y FadeOut intermitente de Image con DOTween - DOFade
    IEnumerator BlinkImage(Image textImage, float normalDuration, float fastDuration, int numberOfTimes)
    {
        //La animaci�n se realiza cierto n�mero de veces por medio de un ciclo for
        for (int i = 0; i < numberOfTimes; i++)
        {
            /*Previamente, la imagen es puesta con su alpha en 0f para lograr conseguir el efecto esperado.
            La transicion comienza haciendo FadeIn con una duracion r�pida y una curva InQuint. Al finalizar,
            la animaci�n contin�a haciendo FadeOut con una duraci�n corta y la misma curva InQuint*/
            textImage.DOFade(1f, fastDuration).SetEase(Ease.InQuint).OnComplete(
                    () => textImage.DOFade(0f, normalDuration).SetEase(Ease.InQuint)
                );
            //Se hace una pausa despu�s de cada vuelta en el for para darle tiempo a las 2 transiciones antes de repetirlas
            yield return new WaitForSeconds(normalDuration + fastDuration);
        }
    }

    IEnumerator PlayEnding(bool thereIsWinner)
    {
        if (thereIsWinner)
        {
            StartCoroutine(BlinkImage(winner, normalWinnerDuration, fastWinnerDuration, winnerTimes));
            PlayEndingSound(true);
            yield return new WaitForSeconds(winnerTimes * (normalWinnerDuration + fastWinnerDuration) + 0.1f);
            winner.color = normalTextColor;
        }
        else
        {
            StartCoroutine(BlinkImage(tie, normalWinnerDuration, fastWinnerDuration, winnerTimes));
            PlayEndingSound(false);
            yield return new WaitForSeconds(winnerTimes * (normalWinnerDuration + fastWinnerDuration) + 0.1f);
            tie.color = normalTextColor;
        }
        yield return new WaitForSeconds(3f);
        if (isOnlineB)
        {
            timerOnlineScript.ReturnToMainMenu();
        }
        else
        {
            timerScript.ReturnToMainMenu();
        }
    }

    IEnumerator PlayFightIntro()
    {
        yield return new WaitForSeconds(introDelay);
        //Se manda a hacer blink a la imagen de Ready!
        StartCoroutine(BlinkImage(ready, normalReadyDuration, fastReadyDuration, readyTimes));
        readySound.Play();
        //Se hace una pausa para darle tiempo al blink de Ready! de terminar
        yield return new WaitForSeconds(readyTimes * (normalReadyDuration + fastReadyDuration) + 0.1f);
        //Se manda a hacer blink a la imagen de Fight!
        StartCoroutine(BlinkImage(fight, normalFightDuration, fastFightDuration, fightTimes));
        fightSound.Play();
        //Se hace una pausa para darle tiempo al blink de Fight! de terminar
        yield return new WaitForSeconds(fightTimes * (normalFightDuration + fastFightDuration) + 0.1f);
        if (isOnlineB)
        {
            timerOnlineScript.startTimer = true;


        } else
        {
            timerScript.TimerOn = true;

            for (int i = 0; i < Drivers.Count; i++)
            {
                PlayerInput currentPlayerInput = Drivers[i].GetComponent<PlayerInput>();
                if (currentPlayerInput)
                {
                    currentPlayerInput.enabled = true;
                }
            }
            for (int i = 0; i < Players.Count; i++)
            {
                PlayerInput currentPlayerInput = Players[i].GetComponent<PlayerInput>();
                if (currentPlayerInput)
                {
                    currentPlayerInput.enabled = true;
                }
                else
                {
                    Rigidbody2D playerRigidbody = Players[i].GetComponent<Rigidbody2D>();
                    playerRigidbody.constraints &= ~RigidbodyConstraints2D.FreezeAll;
                }
            }
        }
       
    }
}
