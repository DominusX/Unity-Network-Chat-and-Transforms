////////////////////////////////////////////////////////////////////////////////////////
//Date: Sep 03 2016, Updated on 10 Sep 2016
//Functionalities to handle client server messages
////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEngine.Networking;

public class ChatMessage : NetworkBehaviour {

    NetworkClient myClient;
    NetworkManager netMan;
    //Get peer type from ControlPlayer script since ChatMessage does not have network identity
    public static bool isLocalServer;

    private string chatLabelText;

    //Use below delegate to update any UI elements involving procedures in future
    //public delegate void MsgReceived(object sender, string msg);
    //public event MsgReceived msgArrived;

    void OnGUI()
    {
        //Chat box at top right corner of game window
        GUI.Label(new Rect(Screen.width - 300, 0, 250, 100), chatLabelText);
    }

    public void setupClient()
    {
        netMan = GameObject.FindObjectOfType<NetworkManager>();
        //Register delegate if client is connected
        if (netMan.IsClientConnected())
        {
            myClient = netMan.client;

            //Register message delegates to handle chat message received on the network
            if (myClient.isConnected)
            {
                myClient.RegisterHandler(MyMsgType.iden, ReceiveChatMsg);
            }
        }
        //Register server delegate 
        NetworkServer.RegisterHandler(MyMsgType.iden, ReceiveChatMsg);
    }

    //User defined msg type must contain msgtype higher than system defined
    public class MyMsgType
    {
        public static short iden = MsgType.Highest + 1;
    }

    //Create user defined message structure and its attributes
    [System.Serializable]
    public class ChatMsg : MessageBase
    {
        public string senderName;
        public string msg;
        public bool fromServer;

        public override string ToString()
        {
            return senderName + ":" + msg;
        }
    }

    //Based on the peer type, send the received message across the network
    public void SendChatMsg(string msg)
    {
        ChatMsg newMsg = new ChatMsg();
        newMsg.senderName = Network.player.ipAddress;
        newMsg.msg = msg;

        //send message to server, if the local peer type is client
        if (myClient.isConnected && !isLocalServer)
        {
            newMsg.fromServer = isLocalServer;
            myClient.Send(MyMsgType.iden, newMsg);
        }
        //send message to server, if the local peer type is server
        else
        {
            newMsg.fromServer = isLocalServer;
            NetworkServer.SendToAll(MyMsgType.iden, newMsg);
        }
    }

    //Receive message on the network
    public void ReceiveChatMsg(NetworkMessage netMsg)
    {
        ChatMsg receivedMsg = netMsg.ReadMessage<ChatMsg>();
        Debug.Log(receivedMsg.senderName + " : " + receivedMsg.msg);
        //Update the chat box with the received message in latest-first order
        if (!receivedMsg.fromServer)
        {
            receivedMsg.fromServer = true;
            NetworkServer.SendToAll(MyMsgType.iden, receivedMsg);
        }
        //Display in chat box only when sent from server side so as to avoid duplicate msg entries in chat
        else
        {
            chatLabelText = receivedMsg.senderName + " : " + receivedMsg.msg + "\n" + chatLabelText;
        }
    }
}
