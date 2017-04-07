using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MoveCubeControl : NetworkBehaviour
{

    //when the boxColor value changes, it calls UpdateColor and sync across network
    [SyncVar(hook = "UpdateTransform")]
    Transform boxTransform;

    void Start()
    {
        boxTransform = this.transform;
        boxTransform.position = new Vector3(0.0f, 1.5f, 6.0f);
    }

    void FixedUpdate()
    {
        if (!isServer)
            return;

        UpdateTransform(boxTransform);
    }

    public void UpdateTransform(Transform newBoxTransform)
    {
        this.transform.position = newBoxTransform.position;
        this.transform.rotation = newBoxTransform.rotation;
    }
}
