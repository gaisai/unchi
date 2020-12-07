using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitAttack : MonoBehaviour
{
    void OnTriggerStay(Collider co) {
        DataBase.BordMassage_Hit = "\n攻撃あたってるよ\n"+co.gameObject.name+":"+co.gameObject.tag+":"+Enemy.HP;
        Debug.Log(co.transform);

        DataBase.AttackTo = co;
    }

    void OnTriggerExit(Collider co) {
        DataBase.BordMassage_Hit = "\nだめ\n";
        Debug.Log(co.transform);

        DataBase.AttackTo = co;
    
    }

    void Update(){
        //DataBase.BordMassage =  "";
    }

}
