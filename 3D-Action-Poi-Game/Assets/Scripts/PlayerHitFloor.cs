using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitFloor : MonoBehaviour
{
    void OnTriggerStay(Collider co) {
        //DataBase.BordMassage_Hit = "\n床にあたってるよ";
        Debug.Log(co.transform);

        DataBase.Onfloor = true;
    }

    void OnTriggerExit() {
        //DataBase.BordMassage_Hit = "";
        DataBase.Onfloor = false;
    }
    
    void Update(){
        //DataBase.BordMassage =  "";
    }

}
