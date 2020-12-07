using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 
using System.Numerics;



public class PlayerMoving : MonoBehaviour
{
    void Awake() {   
        
    }

    Transform arml,armr,legl,legr,body,player,camera;
    Rigidbody playerRagid;

    public static class moving{

        public static int status = 0;
        public static class Speed{
            public static int status = 0;
            public static int Walk = 500;
            public static int Run = 2000;
            public static int jump = 2000;
            public static int gravity = -150;
            public static int w = 0;
            public static int s = 0;
            public static int a = 0;
            public static int d = 0;    
            public static int space = 0;
            public static int vertical = 0;
            public static int width = 0;    
            public static int height = 0;    
        }
        public static class Direction{
            public static int w = 0;
            public static int s = 0;
            public static int a = 0;
            public static int d = 0;
            public static int Lshift = 0;
            public static int space = 0;
            public static int vertical = 0;
            public static int width = 0;
            public static int height = 0;
        }
        //public static Complex DirectionVec = new Complex(0.0,0.0);
        //public static string line;



        public static void  InputAccSpd(int keyw,int keys, int keyd,int keya, int keyspace, int keyLShift){
            Direction.w = keyw - Speed.w;
            Direction.s = keys - Speed.s;
            Direction.d = keyd - Speed.d;
            Direction.a = keya - Speed.a;
            Direction.space = keyspace;
            Direction.Lshift = keyLShift;
            Direction.vertical += Direction.w - Direction.s;
            Direction.width += Direction.d - Direction.a;
            Direction.height = Direction.space*Speed.jump;

            if(Direction.Lshift==1){
                Speed.status = Speed.Run;
                RotateSpeed = Math.Sign(RotateSpeed) * RotateRunSpeed;
            }else{
                Speed.status = Speed.Walk;
                RotateSpeed = Math.Sign(RotateSpeed) * RotateWalkSpeed;
            }

            Speed.w += Direction.w;
            Speed.s += Direction.s;
            Speed.d += Direction.d;
            Speed.a += Direction.a;
            Speed.space = Direction.space;

        }

    }

    public static class attack{
        public static Color obj = GameObject.Find("Effect").GetComponent<Renderer>().material.color;
        public static Transform tra = GameObject.Find("Effect").transform;
        public static float EffectFlameMax = 1.0f;
        public static float EffectFlameDiff = 0.1f;
        public static int EffectFlameNow = Convert.ToInt32(EffectFlameMax/EffectFlameDiff);

        public static int EffectFlameNum = EffectFlameNow;

        public static bool EffectDoing = false;

        public static int AttackNum = 10;


        public static int FadeEffect(){

            if(
                EffectFlameNow == EffectFlameNum && 
                DataBase.AttackTo != null && 
                DataBase.AttackTo.gameObject.tag == "Enemy" && 
                DataBase.AttackTo.gameObject.GetComponent<Enemy>()
            ){
                
                
                GameObject.Find(DataBase.AttackTo.gameObject.name).GetComponent<Enemy>().HitAttacked(AttackNum);

                //aaa.HitAttacked(AttackNum) ;

                //DataBase.BordMassage_Hit = "\n攻撃:"+EffectFlameNow+":"+DataBase.AttackTo.gameObject.name;
                //Debug.Log(EffectFlameNow+":"+DataBase.AttackTo.gameObject.name);
            }else{
                //DataBase.BordMassage_Hit = "\n攻撃あたってないよ";
            }
            //DataBase.AttackTo = null;

            GameObject.Find("Effect").GetComponent<Renderer>().material.color = 
                new Color(obj.r,obj.g,obj.b ,EffectFlameNow*EffectFlameDiff);
            EffectFlameNow -= 1;
            if(EffectFlameNow <0){
                EffectFlameNow = EffectFlameNum;
                EffectDoing = false;
                return 1;
            }
            EffectDoing = true;
            return 0;
            
        }
    
    }



    //歩きモーション変数
    static int RotateWalkSpeed = 5;
    static int RotateRunSpeed = 20;

    static int RotateSpeed = RotateWalkSpeed;

    int RotatePosition = 0;

    int MouseSensitive =3;

    int RotateMax = 80;

    
    
    // Start is called before the first frame update
    void Start()
    {
        arml = GameObject.Find("ArmLeft").transform;
        armr = GameObject.Find("ArmRight").transform;
        legl = GameObject.Find("LegLeft").transform;
        legr = GameObject.Find("LegRight").transform;
        body = GameObject.Find("Body").transform;
        player = GameObject.Find("Player").transform;
        playerRagid = GameObject.Find("Player").GetComponent<Rigidbody>();
        camera = GameObject.Find("Camera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        DataBase.BordMassage = "";

        //double rad_y = player.rotation.y;


        if (!attack.EffectDoing){


            double rad_y = player.localEulerAngles.y;
            
            string l = "";
            if(Input.GetKey(KeyCode.W)) l=l+"w";
            if(Input.GetKey(KeyCode.S)) l=l+"s";
            if(Input.GetKey(KeyCode.D)) l=l+"d";
            if(Input.GetKey(KeyCode.A)) l=l+"a";
            if(Input.GetKey(KeyCode.Space)) l=l+",Jump";
            if(Input.GetKey(KeyCode.LeftShift)) l=l+",Run";

            DataBase.BordMassage += l;
            
            moving.InputAccSpd(
                System.Convert.ToInt32(Input.GetKey(KeyCode.W)),
                System.Convert.ToInt32(Input.GetKey(KeyCode.S)),
                System.Convert.ToInt32(Input.GetKey(KeyCode.D)),
                System.Convert.ToInt32(Input.GetKey(KeyCode.A)),
                System.Convert.ToInt32(Input.GetKey(KeyCode.Space)),
                System.Convert.ToInt32(Input.GetKey(KeyCode.LeftShift))
            );
    




            
            //平面むき
            if( moving.Direction.vertical!=0 || moving.Direction.width!=0 ){
                int rad = Convert.ToInt32( 180 / Math.PI * ( Math.Atan2( moving.Direction.width, moving.Direction.vertical ) ));
                DataBase.BordMassage +=  "\n"+Convert.ToString(rad_y);

                body.rotation = UnityEngine.Quaternion.Euler(0, rad+(float)rad_y, 0);

            }

            //上下移動
            if(DataBase.Onfloor){
                
                if(moving.Direction.space==1){
                    moving.Speed.height = moving.Speed.jump;
                }else{
                    moving.Speed.height = 0;
                }
            }else{
                moving.Speed.height += moving.Speed.gravity;
                if(moving.Speed.height < moving.Speed.jump*-1){
                    moving.Speed.height = moving.Speed.jump*-1;
                }
            }
            
            
            ////移動モーション
            if( moving.Direction.vertical!=0 || moving.Direction.width!=0){
                
                arml.Rotate(RotateSpeed,0,0);
                armr.Rotate(-1*RotateSpeed,0,0);
                legl.Rotate(-1*RotateSpeed,0,0);
                legr.Rotate(RotateSpeed,0,0);

                RotatePosition += RotateSpeed;
                if (System.Math.Abs(RotatePosition) > RotateMax){
                    RotateSpeed *= -1;
                }
                
            }else{
                arml.Rotate(-1*RotatePosition,0,0);
                armr.Rotate(RotatePosition,0,0);
                legl.Rotate(RotatePosition,0,0);
                legr.Rotate(-1*RotatePosition,0,0);
                RotatePosition = 0;

            //DataBase.BordMassage = Convert.ToString(System.Math.Abs(RotatePosition)) +"\n"+ Convert.ToString(RotateMax);
            }



            //移動
            
            double moving_x =  
                (-1 * moving.Speed.status * moving.Direction.vertical) * Math.Sin(Math.PI*rad_y/180) +
                (-1 * moving.Speed.status * moving.Direction.width) * Math.Cos(Math.PI*rad_y/180)
            ;
            double moving_z = 
                (-1 * moving.Speed.status * moving.Direction.vertical) * Math.Cos(Math.PI*rad_y/180) + 
                ( moving.Speed.status * moving.Direction.width) * Math.Sin(Math.PI*rad_y/180)
                
            ;


            playerRagid.velocity = new UnityEngine.Vector3(
                (float)moving_x,
                moving.Speed.height,
                (float)moving_z
            );


            //マウス視点移動
            player.Rotate(0,3*Input.GetAxis("Mouse X")*MouseSensitive,0);
            camera.Rotate(Input.GetAxis("Mouse Y")*-1*MouseSensitive,0,0);
            
            
            //DataBase.BordMassage += 
            //    "posi:\n"+player.position.x +"\n,"+ player.position.y +"\n,"+ player.position.z+"\n"+
            //    "move:\n"+moving.Speed.width +"\n,"+ moving.Speed.height +"\n,"+ moving.Speed.vertical
            //;

            //
            if(Input.GetMouseButtonDown(0)){
                attack.FadeEffect();
            }
        }else{



            arml.Rotate(-1*RotatePosition,0,0);
            armr.Rotate(RotatePosition,0,0);
            legl.Rotate(RotatePosition,0,0);
            legr.Rotate(-1*RotatePosition,0,0);
            RotatePosition = 0;



            //上下移動
            if(DataBase.Onfloor){
                
                if(moving.Direction.space==1){
                    moving.Speed.height = moving.Speed.jump;
                }else{
                    moving.Speed.height = 0;
                }
            }else{
                moving.Speed.height += moving.Speed.gravity;
                if(moving.Speed.height < moving.Speed.jump*-1){
                    moving.Speed.height = moving.Speed.jump*-1;
                }
            }
            
            


            playerRagid.velocity = new UnityEngine.Vector3(
                0,
                moving.Speed.height,
                0
            );

            attack.FadeEffect();     
        }



    }

}
