using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CreateObject : NetworkBehaviour {

    public GameObject doorPrefab;
    public Transform origin;
    //Based on the trigger enter and exit the below value will be changed from script - TriggerObjCreate.cs
    public static bool atPort = false;

    //the function gets called auto when the value of isAlive changes 
    [SyncVar(hook = "CreateObj")]
    bool isAlive = false;

    void Update()
    {
        if (!isServer)
            return;

        if (Input.GetKeyDown(KeyCode.K) && atPort == true){
            Debug.Log("isAlive set to true");
            isAlive = true;
        }
    }

    //Destroy the game object on trigger entry on server
    public void CreateObj(bool isObjAlive)
    {
        if (isObjAlive)
        {
            Debug.Log("Object created");
            GameObject child = Instantiate(doorPrefab);
            child.transform.position = origin.position;
        }
    }
}
