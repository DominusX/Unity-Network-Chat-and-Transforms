using UnityEngine;
using System.Collections;

public class TriggerObjCreate : MonoBehaviour {

    //when the player enters the volume trigger check and set value of atPort to true
    void OnTriggerEnter()
    {
        Debug.Log("OnTriggerEnter");
        CreateObject.atPort = true;
    }

    //when the player exits the volume trigger check and set value of atPort to false
    void OnTriggerExit()
    {
        Debug.Log("OnTriggerExit");
        CreateObject.atPort = false;
    }
}
