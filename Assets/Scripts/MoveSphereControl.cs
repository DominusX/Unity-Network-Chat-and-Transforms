using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MoveSphereControl : NetworkBehaviour
{
    //when the boxColor value changes, it calls UpdateColor and sync across network
    [SyncVar(hook = "UpdateTransform")]
    Transform sphereTransform;

    void Start()
    {
        sphereTransform = this.transform;
        sphereTransform.position = new Vector3(0.0f, 1.5f, 6.0f);
    }

    void FixedUpdate()
    {
        if (!isServer)
            return;

            UpdateTransform(sphereTransform);
    }

    //Update the sphere transform on network
    public void UpdateTransform(Transform newBoxTransform)
    {
        this.transform.position = newBoxTransform.position;
        this.transform.rotation = newBoxTransform.rotation;
    }
}
