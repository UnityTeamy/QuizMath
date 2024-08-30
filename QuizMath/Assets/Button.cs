using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Button : MonoBehaviour
{
    TextMeshProUGUI textUI;
    string text;
    // Start is called before the first frame update
    void Awake()
    {
        textUI = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        text = textUI.text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
