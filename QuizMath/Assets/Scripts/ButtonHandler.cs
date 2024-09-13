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
        GameManager.Instance.ChangeOper(text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
