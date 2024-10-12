using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TextMeshProUGUI operText;
    public TextMeshProUGUI result;
    public float resultvalue;
    //public bool isint;
    string algebra;
    public bool isoperate;
    public Otherview otherview;
    public NetworkManager nw;
    // string operatingtext;
    public TextMeshProUGUI otheroperText;
    public TextMeshProUGUI otherresult;
    public bool isnumber;

    public TextMeshProUGUI Correctioncount;
    public TextMeshProUGUI OtherCorrectioncount;
    int Count;
    public int OtherCount;


    void Awake()
    {
        Instance = this;
        Reset();
        otheroperText.text = "";
        otherresult.text = "";
        isnumber = true;
        StartCoroutine("Wait");
        //Debug.Log("Waited");
        Count = OtherCount = 0;
        Correctioncount.text = OtherCorrectioncount.text = "Correction : 0";
    }

    public void ChangeOper(string op, bool isowner)
    {
        if (isowner)
        {
            //operText.text += op + " ";
            /*if (otherview.ismaster)
            {

            }//otherview.operatingstate += op + " ";
            else*/
            //otherview.otheroperatingstate += op + " ";
            //operatingtext += op + " ";
            if (isnumber)
            {
                operText.text += op + " ";
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
                isnumber = false;
            }
            else if(!isnumber)
            {
                operText.text += op + " ";
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
                isnumber = true;
            }
            //isint = !isint;
            nw.Changetext(op);
            //PV.RPC("settarget", RpcTarget.Others, op);
            /*if (otherview.ismaster)
                otherview.operatingstate = operText.text;
            else
                otherview.otheroperatingstate = operText.text;*/
        }
        else
        {
            otheroperText.text += op + " ";
            // Debug.LogError("false");
        }
        // Debug.LogError("false or true");
    }

    public void showotherresult(string result)
    {
        otherresult.text = " = " + result;
    }

    /*void OnClick()
    {
        isoperated = true;
    }*/

    // Update is called once per frame
    void Update()
    {
        if (isoperate)
        {
            result.text = " = " + resultvalue.ToString();
            if(resultvalue.ToString() == nw.targettext.text)
            {
                nw.targettext.text = "Correct!";
                StartCoroutine("Wait");
                //nw.Retarget();
                Count++;
                Correctioncount.text = "Correct : " + Count.ToString();
                if (Count >= 3)
                {
                    nw.targettext.text = "Win!";
                    nw.Win();
                    StartCoroutine("Stop");
                }
                else
                {
                    nw.Retarget();
                }
                //nw.targettext.text = "Correct!";
            }
            else
            {
                nw.targettext.text = "Wrong!";
                StartCoroutine("Wait");
                //Debug.Log("Waited");
                nw.targettext.text = nw.targetnumber.ToString();
                Reset();
            }
        }
            /*if (otherview.ismaster)
                otherview.Answer = resultvalue.ToString();
            else
                otherview.otherAnswer = resultvalue.ToString();*/
            isoperate = false;
        

    }

    public void Reset()
    {
        operText.text = "";
        resultvalue = 0;
        //isint = true;
        isnumber = true;
        algebra = "none";
        isoperate = false;
        result.text = "";
        //SceneManager.LoadScene(0);
        nw.retext();
    }

    public void Resettext()
    {
        otheroperText.text = "";
        //resultvalue = 0;
        //isint = true;
        //isnumber = true;
        //algebra = "none";
        //isoperate = false;
        otherresult.text = "";
        //SceneManager.LoadScene(0);
    }

    IEnumerable Wait()
    {
        yield return new WaitForSeconds(2.0f);
    }

    IEnumerable Stop()
    {
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0f;
    }
}
