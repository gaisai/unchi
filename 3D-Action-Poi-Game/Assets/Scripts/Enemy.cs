using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{

    public static int HP = 1000;
    // Start is called before the first frame update

    public static void HitAttacked(int attacknum){
        HP -= attacknum;
    }
    
}
