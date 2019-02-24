using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropController : MonoBehaviour {
   public Mushroom mushroom;
    public Monster monster;
    // 距离玩家后方 appear_margin 出产生怪物
    public float appear_margin = -15.0f;
    float appear_front = 10.0f;
    public float[] SpawnNormalLenghth;
    public float[] SpawnEnemyLenghth;
    // public float[] TriggerLenght;       // 玩家触发
    // -------------------------------------------------------------------------------- //

    public const float INTERVAL_MIN = 20.0f;            // 怪物出现间隔的最小值
    public const float INTERVAL_MAX = 50.0f;            // 怪物出现间隔的最大值
    public const int PAWNINTERVAL_MAX = 5;
    public const int ENEMY_MAX = 3;
    // -------------------------------------------------------------------------------- //

    public float step_timer = 0.0f;     // 状态迁移后经过的时间
    public float step_timer_prev = 0.0f;

    public float CD;

    public int RandomMaxNum;
    public int RandomMinNum;

    public float TriggerLenghth;
    private float currentTime;
    
    private float lastTime;
    private int index;
    private GameObject Player;
   
  public  List<GameObject> monsterlist = new List<GameObject>();
    void Start()
    {

        SpawnProp(mushroom);
       
        //Debug.Log(Player.transform.localPosition.x);
    }
	void Update () {
        // Debug.Log(Player.transform.localPosition.x);
        Player = TagMark.instance.Player.gameObject;
        currentTime = Time.time;
        if (currentTime-lastTime>CD)
        {
          //  if (Player.transform.localPosition.x >= TriggerLenghth)
            {
              int rand= (int)(UnityEngine.Random.Range(RandomMinNum, RandomMaxNum));
                for (int i = 0; i < rand; i++)
                {
                    //  SpawnProp(mushroom);
                    //SpawnProp(monster);
                  
                }
            //    TriggerLenghth -= 22;
            }
            lastTime = currentTime;
        }
       
    }
   public void SpawnProp(Prop obj)
    {
        EType type = obj.eType;
        switch (type)
        {
            case EType.PROP:
               bool isMushRoom= obj.GetComponent<Mushroom>()==null;
                if (!isMushRoom)
                {
                  GameObject obj1=  SpawnMushroom(obj.GetComponent<Mushroom>().prop);
                  OutLineSpawnNormal(obj1);
                }
                break;

            case EType.ENEMY:
                if (monsterlist.Count<= ENEMY_MAX&&GameController._gameInstance.bisDead==false)
                {
                    GameObject obj2 = SpawnMonster();
                    OutLineSpawnEnemy(obj2);
                    monsterlist.Add(obj2);
                }
               
                break;
            default:
                break;
        }
        //MushroomPropType prop = MushroomPropType.NORMAL;
        //GameObject mush = SpawnMushroom(prop);
        //SetPostion(mush, Vector3.zero);
    }

    public void DestroyMonster()
    {
        foreach (GameObject item in monsterlist)
        {
            //monsterlist.Remove(item);
            Destroy(item.gameObject);
        }
        monsterlist.Clear();
    }
    public GameObject SpawnMushroom(MushroomPropType prop)
    {
        GameObject obj;
        switch (prop)
        {
            case MushroomPropType.NORMAL:
            obj = Instantiate(mushroom.gameObject) as GameObject;
                break;
            case MushroomPropType.UP:
            obj = Instantiate(mushroom.gameObject) as GameObject;
                break;
            case MushroomPropType.DOWN:
            obj = Instantiate(mushroom.gameObject) as GameObject;
                break;
            default:
                return null;
        }
        return obj;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="prop"></param>
    /// <returns></returns>
    public GameObject SpawnMonster()
    {
        GameObject obj = Instantiate(monster.gameObject) as GameObject;      
        return obj;
    }
    public void SetPostion(GameObject obj,Vector3 Pos)
    {
        obj.transform.localPosition = Pos;
    }

    public void LerpCalculate(GameObject obj,Vector3 orin,Vector3 end,float time)
    {
       // Vector3 LerpPos = Vector3.Lerp(orin,end,time);
        //obj.transform.localPosition = LerpPos;
    }

    public void OutLineSpawnNormal(GameObject obj)
    {
        Vector3 appear_position = GameObject.FindWithTag("Player").gameObject.transform.localPosition;

        // 在玩家前方，稍微在画面外的位置生成
        appear_front = SpawnNormalLenghth[(int)Random.Range(0, SpawnNormalLenghth.Length-1)];
        appear_position.x += appear_front;
        obj.transform.localPosition = appear_position;
        obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, -0.3f, -3);
    }
    public void OutLineSpawnEnemy(GameObject obj)
    {
        Vector3 appear_position = GameObject.FindWithTag("Player").gameObject.transform.localPosition;

        
       // appear_position.x += SpawnNormalLenghth[(int)Random.Range(0, SpawnNormalLenghth.Length - 1)];
        //if (appear_position.x> GameObject.FindWithTag("Player").gameObject.transform.localPosition.x)
        //{
            //obj.transform.localEulerAngles = new Vector3(0,-90,0);
            //obj.GetComponent<Monster>().RUN_SPEED_MAX = -5.0f;
            //obj.GetComponent<Monster>().run_speed_add = -0.5f;
            //obj.GetComponent<Monster>().bIsRun = true;
            //obj.GetComponent<Monster>().bIsLeft = false;
       // }
      //  else
        {
            obj.transform.localEulerAngles = new Vector3(0, 90, 0);
            obj.GetComponent<Monster>().RUN_SPEED_MAX = 20.0f;
            obj.GetComponent<Monster>().run_speed_add = 0.5f;
            obj.GetComponent<Monster>().bIsLeft = true;
        }
        obj.transform.localPosition = appear_position;
        obj.transform.localPosition = new Vector3(obj.transform.localPosition.x-22, -0.76f, -3);
    }
}
