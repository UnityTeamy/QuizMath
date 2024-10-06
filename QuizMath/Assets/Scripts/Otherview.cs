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
    public TextMeshProUGUI state;
    public bool issend;

    // Start is called before the first frame update
    void Start()
    {
        issend = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (ismaster)
        {
            os.text = operatingstate;
            otheros.text = otheroperatingstate;
            an.text = Answer;
            otheran.text = otherAnswer;
        }
        else
        {
            otheros.text = operatingstate;
            os.text = otheroperatingstate;
            otheran.text = Answer;
            an.text = otherAnswer;
        }
        if (target.text == "0")
            target.text = targetnumber.ToString();
        //else
            //target.text = "what?";
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
        if(stream.IsWriting && issend)
        //if(ismaster)
        {
            //targetnumber = (int)stream.ReceiveNext();
            //operatingstate = (string)stream.ReceiveNext();
            ///*otheroperatingstate = (string)stream.ReceiveNext();
            //Answer = (string)stream.ReceiveNext();
            //otherAnswer = (string)stream.ReceiveNext();*/
            if (ismaster)
            {
                stream.SendNext(targetnumber);
                stream.SendNext(operatingstate);
                //stream.SendNext(otheroperatingstate);
                stream.SendNext(Answer);
                //stream.SendNext(otherAnswer);
                //stream.
                //issend = !isreading;
                issend = false;
            }
            state.text = "writing";

        }
        else
        {
            //stream.IsReading && 
            if (!ismaster && !issend)
            {
                targetnumber = (int)stream.ReceiveNext();
                operatingstate = (string)stream.ReceiveNext();
                //otheroperatingstate = (string)stream.ReceiveNext();
                Answer = (string)stream.ReceiveNext();
                //otherAnswer = (string)stream.ReceiveNext();
                //stream.SendNext(targetnumber);
                //stream.SendNext(operatingstate);
                /*stream.SendNext(otheroperatingstate);
                //stream.SendNext(Answer);
                stream.SendNext(otherAnswer);*/
                //isreading = !isreading;
                issend = true;
            }
        }
        if (stream.IsReading && !issend)
        {
            if (ismaster)
            {
                otheroperatingstate = (string)stream.ReceiveNext();
                otherAnswer = (string)stream.ReceiveNext();
                //isreading = !isreading;
                issend = true;
            }
            //target.text += "reading";
            state.text = "reading";
        }
        else if(issend)
        {
            if (!ismaster)
            {
                stream.SendNext(otheroperatingstate);
                stream.SendNext(otherAnswer);
                issend = false;
                //isreading = !isreading;
            }
            //target.text += "not reading";
        }
    }

}
