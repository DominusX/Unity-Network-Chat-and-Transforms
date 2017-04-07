using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DestroyObject : NetworkBehaviour
{
    //the function gets called auto when the value of isAlive changes 
    [SyncVar(hook = "DestroyObj")]
    bool isAlive = true;

    void OnTriggerEnter()
    {
        if (!isServer)
            return;

        isAlive = false;
        DestroyObj(isAlive);
    }

    //Destroy the game object on trigger entry on server
    public void DestroyObj(bool isObjAlive)
    {
        if (!isObjAlive)
        {
            Debug.Log("Object destroyed");
            Destroy(gameObject);
        }
    }
}
