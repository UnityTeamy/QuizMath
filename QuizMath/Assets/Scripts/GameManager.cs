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
    public Otherview otherview;

    void Awake()
    {
        Instance = this;
        Reset();
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
                    resultvalue = (int)resultvalue;
                    break;
            }
        }
        else
        {
            switch (op)
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
        if (otherview.ismaster)
            otherview.operatingstate = operText.text;
        else
            otherview.otheroperatingstate = operText.text;
    }

    /*void OnClick()
    {
        isoperated = true;
    }*/

    // Update is called once per frame
    void Update()
    {
        if (isoperate)
            result.text = " = " + resultvalue.ToString();
            if (otherview.ismaster)
                otherview.Answer = resultvalue.ToString();
            else
                otherview.otherAnswer = resultvalue.ToString();
            isoperate = false;
        

    }

    public void Reset()
    {
        operText.text = "";
        resultvalue = 0;
        isint = true;
        algebra = "none";
        isoperate = false;
        result.text = "";
    }
}
