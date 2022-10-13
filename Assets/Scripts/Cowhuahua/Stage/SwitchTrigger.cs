using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTrigger : MonoBehaviour
{
    GameObject switchPivot, sewer;
    BoxCollider2D wholeFloorCollider, leftFloorCollider, rightFloorCollider;
    bool switchActivated = false;
    bool timerOn;
    Vector3 activatedSwitchRotation = new Vector3(0, 0, 27.5f);
    Vector3 disabledSwitchRotation = new Vector3(0, 0, -26.5f);
    Vector3 activatedSewerRotation = new Vector3(15f, 0, 0);
    Vector3 disabledSewerRotation = new Vector3(-90f, 0, 0);
    string path = "/Scenary/";
    public AudioSource switchUsageSound, sewerClosingSound;
    // Start is called before the first frame update
    void Start()
    {
        switchPivot = GameObject.Find(path + "SwitchPivot");
        sewer = GameObject.Find(path + "Sewer");
        wholeFloorCollider = GameObject.Find(path + "Floor/FloorWholeCollider").GetComponent<BoxCollider2D>();
        leftFloorCollider = GameObject.Find(path + "Floor/FloorLeftCollider").GetComponent<BoxCollider2D>(); ;
        rightFloorCollider = GameObject.Find(path + "Floor/FloorRightCollider").GetComponent<BoxCollider2D>();
        wholeFloorCollider.enabled = true;
        leftFloorCollider.enabled = false;
        rightFloorCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D element)
    {
        if (element.tag == "Player" && timerOn == false && !switchActivated) //element.CompareTag("Player"))
        {
            
            Debug.Log(gameObject.name);
            switchUsageSound.Play();
            switchPivot.transform.localEulerAngles = activatedSwitchRotation;
            sewer.transform.localEulerAngles = activatedSewerRotation;
            switchActivated = true;
            leftFloorCollider.enabled = true;
            rightFloorCollider.enabled = true;
            wholeFloorCollider.enabled = false;
            
            StartCoroutine(returnSwitchToNormal());
            timerOn = true;
        }
    }

    IEnumerator returnSwitchToNormal()
    {
        yield return new WaitForSeconds(5);
        timerOn = false;
        sewerClosingSound.Play();
        switchPivot.transform.localEulerAngles = disabledSwitchRotation;
        sewer.transform.localEulerAngles = disabledSewerRotation;
        switchActivated = false;
        wholeFloorCollider.enabled = true;
        leftFloorCollider.enabled = false;
        rightFloorCollider.enabled = false;
    }
}
