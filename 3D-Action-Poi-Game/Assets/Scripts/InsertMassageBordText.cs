using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


public class InsertMassageBordText : MonoBehaviour
{
   
    //DataBase master = GameObject.Find ("Mater").GetComponent<DataBase>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Text MassegaeBord = GameObject.Find("MassageBordText").GetComponent<Text> ();
        MassegaeBord.text = DataBase.BordMassage +"\n"+ DataBase.BordMassage_Hit;
    }

}
