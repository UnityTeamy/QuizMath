using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonHandler : MonoBehaviour
{
    TextMeshProUGUI textUI;
    Button btn;
    string text;
    public bool isnumberbutton;
    // Start is called before the first frame update
    void Awake()
    {
        textUI = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        btn = gameObject.GetComponent<Button>();
        text = textUI.text;
        btn.onClick.AddListener(Click);
    }

    void Click()
    {
        if((isnumberbutton && GameManager.Instance.isnumber) || (!isnumberbutton && !GameManager.Instance.isnumber))
            GameManager.Instance.ChangeOper(text, true);
            //PV.RPC("settarget", RpcTarget.Others, targetnumber.ToString(), false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
