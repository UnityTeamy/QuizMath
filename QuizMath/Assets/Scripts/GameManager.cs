using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TextMeshProUGUI operText;

    void Awake()
    {
        Instance = this;
        operText.text = "";
    }

    public void ChangeOper(string op)
    {
        operText.text += op + " ";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
