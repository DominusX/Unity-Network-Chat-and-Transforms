////////////////////////////////////////////////////////////////////////////////////////
//Date: Sep 04 2016 Updated on 10 Sep 2016
//Local player related activation and character controls
////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEngine.Networking;

public class ControlPlayer : NetworkBehaviour
{
    //The below attribute automatically synchronizes across network if located at server
    [SyncVar]
    public string chatString;

    private string chatLabelText;
    private GUILayout chatArea;

    void OnGUI()
    {
        if (isLocalPlayer)
        {
            GameObject chatMsg = GameObject.Find("NetworkChat");
            //The below instruction updates the chat string on server and syncs across the network
            chatString = GUI.TextField(new Rect(15, Screen.height - 40, 250, 30), chatString);
            if (GUI.Button(new Rect(270, Screen.height - 40, 80, 30), "Send"))
            {
                GUI.FocusControl("SendMsg");
                //If the client updates the chat string, than it is required to be sent it to server to sync across ntwrk
                CmdUpdateChatString(chatString);
                chatMsg.GetComponent<ChatMessage>().SendChatMsg(chatString);
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        //Local player specific activations during playtime
        if (isLocalPlayer)
        {
            //Update the peer type
            ChatMessage.isLocalServer = isServer ? true : false;
            GetComponent<MouseLook>().enabled = true;

            //Make the Camera stick to character movements
            Camera.main.transform.position = this.transform.position - this.transform.forward * 5 + this.transform.up * 4;
            Camera.main.transform.parent = this.transform;

        }
    }

    void Update()
    {
        //Update the 3d text with chat string
        this.GetComponentInChildren<TextMesh>().text = chatString;
    }

    //Function called by client and invokes the function on server with same values as of client
    [Command]
    public void CmdUpdateChatString(string msg)
    {
        chatString = msg;
    }
}
