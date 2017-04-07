using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ColorCubeControl : NetworkBehaviour
{

    //when the boxColor value changes, it calls UpdateColor and sync across network
    [SyncVar(hook = "UpdateColor")]
    Color boxColor;

    private int rotateX = 0;
    private int rotateY = 40;

    void FixedUpdate()
    {
        transform.Rotate(rotateX * Time.deltaTime, rotateY * Time.deltaTime, 0.0f);
    }

    //on trigger entry update a new random color for the material
    void OnTriggerEnter()
    {
        if (!isServer)
            return;

        boxColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        UpdateColor(boxColor);
    }

    public void UpdateColor(Color newColor)
    {
        this.GetComponent<Renderer>().material.color = newColor;
    }
}
