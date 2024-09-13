using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TextMeshProUGUI operText;
    public TextMeshProUGUI result;
    public float resultvalue;
    public bool isint;
    string algebra;
    public bool isoperate;

    void Awake()
    {
        Instance = this;
        operText.text = "";
        resultvalue = 0;
        isint = true;
        algebra = "none";
        isoperate = false;
        result.text = "";
    }

    public void ChangeOper(string op)
    {
        operText.text += op + " ";
        if (isint)
        {
            switch (algebra)
            {
                case "none":
                    resultvalue = int.Parse(op);
                    break;
                case "plus":
                    resultvalue += int.Parse(op);
                    break;
                case "minus":
                    resultvalue -= int.Parse(op);
                    break;
                case "multiply":
                    resultvalue *= int.Parse(op);
                    break;
                case "per":
                    resultvalue /= int.Parse(op);
                    break;
            }
        }
        else
        {
            switch(op)
            {
                case "+":
                    algebra = "plus";
                    break;
                case "-":
                    algebra = "minus";
                    break;
                case "X":
                    algebra = "multiply";
                    break;
                case "/":
                    algebra = "per";
                    break;
            }
        }
        isint = !isint;
    }

    /*void OnClick()
    {
        isoperated = true;
    }*/

    // Update is called once per frame
    void Update()
    {
        if(isoperate)
            result.text = " = " + resultvalue.ToString();
    }
}
