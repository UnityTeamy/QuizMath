using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    //public InputField field;
    public TextMeshProUGUI log;
    public TextMeshProUGUI list;
    public TextMeshProUGUI text;
    public int n;

    public PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(960, 600, false);
        PhotonNetwork.ConnectUsingSettings();
        n = 0;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = n.ToString();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 2 }, null);
    }
    public override void OnJoinedRoom()
    {
        //PhotonNetwork.
        Debug.Log("Joined");
        log.text = "joined!";
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        list.text = "connected";
        for(int i  = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            list.text += "\n";
            list.text += PhotonNetwork.PlayerList[i].NickName;
        }
    }
    
    public void OnClick()
    {
        function();
    }

    void function()
    {
        PV.RPC("Changevalue", RpcTarget.Others);
    }

    [PunRPC]
    void Changevalue()
    {
        n += 1;
    }
}
