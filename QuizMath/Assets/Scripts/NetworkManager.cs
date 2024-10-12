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

    public TextMeshProUGUI targettext;

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
        //Debug.LogError(PhotonNetwork.IsMasterClient);
        if(!PhotonNetwork.LocalPlayer.IsMasterClient) //islocal로 함 //MasterClient.으로 함
        {
            isconnected = true;
        }
        else
        {
            targetnumber = Random.Range(0, 1000);
        }
        //if(PhotonNetwork.IsMasterClient)
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
        targettext.text = targetnumber.ToString();
        //settarget(targetnumber.ToString(), false);
        PV.RPC("settarget", RpcTarget.Others, targetnumber.ToString(), false);
    }
    
    public void OnClick()
    {
        function();
        //Debug.LogError(PhotonNetwork.MasterClient.IsMasterClient);
        //Debug.LogError(PhotonNetwork.IsMasterClient);
        //Debug.LogError(PhotonNetwork.LocalPlayer.IsMasterClient);
    }

    void function()
    {
        PV.RPC("Changevalue", RpcTarget.Others);
    }

    public void Changetext(string A)
    {
        //ㅋㅋPV.RPC("Change", RpcTarget.Others, "A", "B");
        PV.RPC("Change", RpcTarget.Others, A);
    }
    
    public void retext()
    {
        if (isconnected)
            PV.RPC("rt", RpcTarget.Others);
        else
            Debug.LogError("not connected");
        //Debug.LogError("operated");
    }

    public void Retarget()
    {
        if(PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            targetnumber = Random.Range(0, 1000);
            targettext.text = targetnumber.ToString();
            //settarget(targetnumber.ToString(), false);
            PV.RPC("settarget", RpcTarget.Others, targetnumber.ToString(), false);
        }
        else
        {
            //settarget("notarget", true);
            PV.RPC("settarget", RpcTarget.Others, "notarget", true);
        }
        PV.RPC("correct", RpcTarget.Others);
    }

    public void Win()
    {
        PV.RPC("win", RpcTarget.Others);
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
        //GameManager.Instance.showotherresult(GameManager.Instance.resultvalue.ToString());
        //showresult(GameManager.Instance.resultvalue.ToString());
        PV.RPC("showresult", RpcTarget.Others, GameManager.Instance.resultvalue.ToString());
    }

    [PunRPC]
    void rt()
    {
        GameManager.Instance.Resettext();
    }

    [PunRPC]
    void settarget(string target, bool ishost)
    {
        if (!ishost)
        {
            targettext.text = target;
            //Debug.LogError("targeted");
            targetnumber = int.Parse(target);
        }
        else
        {
            targetnumber = Random.Range(0, 1000);
            targettext.text = targetnumber.ToString();
            //settarget(targetnumber.ToString(), false);
            PV.RPC("settarget", RpcTarget.Others, targetnumber.ToString(), false);
        }
    }

    [PunRPC]
    void showresult(string result)
    {
        GameManager.Instance.otherresult.text = " = " + result;
    }

    /*[PunRPC]
    void set()
    {

    }*/

    [PunRPC]
    void correct()
    {
        GameManager.Instance.OtherCount++;
        GameManager.Instance.OtherCorrectioncount.text = "Correction : " + GameManager.Instance.OtherCount.ToString();
    }

    [PunRPC]
    void win()
    {
        targettext.text = "Lose..";
        Time.timeScale = 0f;
    }
}
