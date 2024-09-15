using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class Otherview : MonoBehaviourPunCallbacks, IPunObservable
{
    //[SerializeField]
    public int targetnumber; //host
    public string operatingstate;
    public string otheroperatingstate;
    public string Answer;
    public string otherAnswer;
    public PhotonView PV;
    public bool ismaster;
    public TextMeshProUGUI target;
    public TextMeshProUGUI os;
    public TextMeshProUGUI otheros;
    public TextMeshProUGUI an;
    public TextMeshProUGUI otheran;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        os.text = operatingstate;
        otheros.text = otheroperatingstate;
        an.text = Answer;
        otheran.text = otherAnswer;
    }

    public override void OnJoinedRoom()
    {
        ismaster = PhotonNetwork.LocalPlayer.IsMasterClient;
        if (ismaster)
        {
            //PV.ViewID = 2;
            targetnumber = Random.Range(1, 1000);
            
        }
        target.text = targetnumber.ToString();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            //targetnumber = (int)stream.ReceiveNext();
            //operatingstate = (string)stream.ReceiveNext();
            otheroperatingstate = (string)stream.ReceiveNext();
            //Answer = (string)stream.ReceiveNext();
            otherAnswer = (string)stream.ReceiveNext();
            stream.SendNext(targetnumber);
            stream.SendNext(operatingstate);
            //stream.SendNext(otheroperatingstate);
            stream.SendNext(Answer);
            //stream.SendNext(otherAnswer);

        }
        else
        {
            targetnumber = (int)stream.ReceiveNext();
            operatingstate = (string)stream.ReceiveNext();
            //otheroperatingstate = (string)stream.ReceiveNext();
            Answer = (string)stream.ReceiveNext();
            //otherAnswer = (string)stream.ReceiveNext();
            //stream.SendNext(targetnumber);
            //stream.SendNext(operatingstate);
            stream.SendNext(otheroperatingstate);
            //stream.SendNext(Answer);
            stream.SendNext(otherAnswer);
        }
    }
}
