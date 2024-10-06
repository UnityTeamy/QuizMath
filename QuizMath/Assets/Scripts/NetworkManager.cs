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
    public int targetnumber;
    public string operatingstate;
    public string Answer;
    bool isconnected;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(960, 600, false);
        PhotonNetwork.ConnectUsingSettings();
        n = 0;
        isconnected = false;
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
        if(!PhotonNetwork.MasterClient.IsLocal)
        {
            isconnected = true;
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        isconnected = true;
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

    public void Changetext(string A)
    {
        //¤»¤»PV.RPC("Change", RpcTarget.Others, "A", "B");
        PV.RPC("Change", RpcTarget.Others, A);
    }
    
    public void retext()
    {
        if(isconnected)
            PV.RPC("rt", RpcTarget.Others);
    }

    [PunRPC]
    void Changevalue()
    {
        n += 1;
    }

    [PunRPC]
    void Change(string A)
    {
        GameManager.Instance.ChangeOper(A, false);
        GameManager.Instance.showotherresult(GameManager.Instance.resultvalue.ToString());
    }

    [PunRPC]
    void rt()
    {
        GameManager.Instance.Resettext();
    }
}
