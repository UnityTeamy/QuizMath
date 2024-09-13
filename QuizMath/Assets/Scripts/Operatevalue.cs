using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Operatevalue : MonoBehaviour
{
    public GameObject gm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Clicked()
    {
        gm.gameObject.GetComponent<GameManager>().isoperate = true;
    }
}
