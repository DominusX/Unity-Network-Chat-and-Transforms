////////////////////////////////////////////////////////////////////////////////////////
//Date: Sep 04 2016
//Update the 3D text for local player with received messages
////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class UpdateUIText : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject chatMsg = GameObject.Find("NetworkChat");
        chatMsg.GetComponent<ChatMessage>().setupClient();
    }

    void Update()
    {
        this.transform.LookAt(Camera.main.transform.position);
        this.transform.Rotate(new Vector3(0, 180, 0));
    }
}
