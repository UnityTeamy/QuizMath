using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

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

    public GameObject gamePanel;
    public GameObject introPanel;

    public TextMeshProUGUI timer;
    public TextMeshProUGUI ConnectionText;

    // Start is called before the first frame update
    void Start()
    {
        gamePanel.SetActive(false);
        introPanel.SetActive(true);
        Screen.SetResolution(960, 600, false);
        ConnectionText.text = "Thank you for visiting";
        //PhotonNetwork.ConnectUsingSettings();
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
        //ConnectionText.text = "Waiting..";
    }
    public override void OnJoinedRoom()
    {
        //PhotonNetwork.
        Debug.Log("Joined");
        log.text = "joined!";
        ConnectionText.text = "Waiting..";
        //Debug.LogError(PhotonNetwork.IsMasterClient);
        if (!PhotonNetwork.LocalPlayer.IsMasterClient) //islocal로 함 //MasterCl
                                                       //nt.으로 함
        {
            isconnected = true;
            //Startgame();
            //StartCoroutine(Wait(3));
            // 바보 휴지
            // Timer(3);
            StartCoroutine(Timer(3));
        }
        else
        {
            Debug.Log("connectexxx"); // 이게 실행됨
            targetnumber = Random.Range(0, 1000);
            // targetnumber는 뭐하는 애냐.
        }
        //if(PhotonNetwork.IsMasterClient)
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        isconnected = true;
        //Startgame();
        //GameManager.Instance.StartCoroutine(Wait(3));
        // Timer(3);
        Debug.Log("OnplayerEnteredRoom timer 작동");
        StartCoroutine(Timer(3));
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

    void function() // 누가 함수 이름을 함수로 짓냐..
    {
        PV.RPC("Changevalue", RpcTarget.Others);
    }

    public void Changetext(string A)
    {
        //ㅋㅋPV.RPC("Change", RpcTarget.Others, "A", "B");
        PV.RPC("Change", RpcTarget.Others, A);
    }
    
    public void retext(bool isfirst)
    {
        if (isconnected)
            PV.RPC("rt", RpcTarget.Others);
        else if(!isfirst)
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
    public void Startgame()
    {
        gamePanel.SetActive(true);
        introPanel.SetActive(false);
        //PhotonNetwork.ConnectUsingSettings();
    }
    public void EnterRoom(GameObject button)
    {
        //StartCoroutine(Wait(3));
        //SceneManager.LoadScene(0);
        PhotonNetwork.ConnectUsingSettings();
        button.gameObject.SetActive(false);
        ConnectionText.text = "Connecting..";
        //SceneManager.LoadScene(0);
    }
    
    IEnumerator Timer(int second)
    {
        //if (second != 100)
        //{
        ConnectionText.text = "Connected";
        for (int i = second; i > 0; i--)
        {
            timer.text = i.ToString();
            Debug.Log("Timer timer 작동");
            yield return new WaitForSecondsRealtime(second);
        }
        Startgame();
        //}
        /*else
        {
            //yield return new WaitForSeconds(3.0f);
            StartCoroutine(Wait(3));
            SceneManager.LoadScene(0);
        }*/
    }

    IEnumerator Wait(int time, bool restart)
    {
        yield return new WaitForSecondsRealtime(time);
        if(restart)
            SceneManager.LoadScene(0);
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
        //GameManager.Instance.OtherCount = 3;
        correct();
        targettext.text = "Lose..";
        //Time.timeScale = 0f;
        //wait(100);
        StartCoroutine(Wait(3,true));
        //SceneManager.LoadScene(0);
    }
}
