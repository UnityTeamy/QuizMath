using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Otherview2 : MonoBehaviourPunCallbacks, IPunObservable
{
    public Otherview otherview;
    public bool issend;
    // Start is called before the first frame update
    void Start()
    {
        issend = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //if (stream.IsWriting)
        //if(otherview.ismaster)
        if(stream.IsReading && issend)
        {
            if (otherview.ismaster)//!used!!
            {
                otherview.otheroperatingstate = (string)stream.ReceiveNext();
                otherview.otherAnswer = (string)stream.ReceiveNext();
                //isreading = !isreading;
            }
            //stream.SendNext(targetnumber);
            //stream.SendNext(operatingstate);
            //stream.SendNext(Answer);

        }
        else
        {
            //targetnumber = (int)stream.ReceiveNext();
            //operatingstate = (string)stream.ReceiveNext();
            //Answer = (string)stream.ReceiveNext();
            //stream.IsWriting && 
            if (!otherview.ismaster && !issend)//!not used !!
            {
                stream.SendNext(otherview.otheroperatingstate);
                stream.SendNext(otherview.otherAnswer);
                //isreading = !isreading;
            }
        }
        if (otherview.ismaster)//!used!!
        {
            otherview.otheroperatingstate = (string)stream.ReceiveNext();
            otherview.otherAnswer = (string)stream.ReceiveNext();
        }
        if (!otherview.ismaster)//!not used !!
        {
            stream.SendNext(otherview.otheroperatingstate);
            stream.SendNext(otherview.otherAnswer);
        }
    }
}
