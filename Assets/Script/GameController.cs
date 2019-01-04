using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum STEP
    {

        NONE = -1,

        START,                  // “开始”的文字出现
        GAME,                   // 游戏进行中
        ONI_VANISH_WAIT,        // 游戏结束后，等待画面上所有的怪物消失
        LAST_RUN,               // 怪物不再出现后的状态
        PLAYER_STOP_WAIT,       // 等待玩家停止

        GOAL,                   // 得分
        ONI_FALL_WAIT,          // 等待“怪物从上方落下”过程结束
        RESULT_DEFEAT,          // 显示斩杀数量的评价
        RESULT_EVALUATION,      // 显示击倒时机的评价
        RESULT_TOTAL,           // 综合评价

        GAME_OVER,              // 游戏结束
        GOTO_TITLE,             // 迁移到标题

        NUM,
    };




    public static GameController _gameInstance;
    public PlayerControl playerControl;
    public FloorControl[] floors;
    public CameraControl cameraCon;
    public SceneControl sceneCon;

   

    private GameObject Player;
    
   
    public void create()
    {
        // 为了在游戏开始后产生怪物，
        // 将生成位置初始化设置为处于玩家后方
       // this.oni_generate_line = this.player.transform.position.x - 1.0f;

    }
    void Start()
    {
        _gameInstance = this;
        //HideInStart();
       // InitGame();
       // ShowInStart();
        //PasuGame();
    }


    void Update()
    {

       Player= GameObject.FindGameObjectWithTag("Player").gameObject;
    
    }
    public void InitGame()
    {
        playerControl.gameObject.GetComponent<PlayerControl>().enabled=true;
        cameraCon.gameObject.GetComponent<CameraControl>().enabled = true;
        sceneCon.gameObject.GetComponent<SceneControl>().enabled = true;

        foreach (FloorControl item in floors)
        {
            item.gameObject.GetComponent<FloorControl>().enabled = true;
        }
    }
    public void StartGame()
    {
        InitGame();
        ShowInStart();
    }
    public void PasuGame()
    {
        playerControl.gameObject.GetComponent<PlayerControl>().enabled = false;
        cameraCon.gameObject.GetComponent<CameraControl>().enabled = false;
        sceneCon.gameObject.GetComponent<SceneControl>().enabled = true;

        foreach (FloorControl item in floors)
        {
            item.gameObject.GetComponent<FloorControl>().enabled = false;
        }
        AudioManager.instance.Pause();
    }

    public void HideInStart()
    {
        List<GameObject> objects = new List<GameObject>();
        objects.Add(playerControl.gameObject);
        //objects.Add(cameraCon.gameObject);
        foreach (FloorControl item in floors)
        {
            objects.Add(item.gameObject);
        }
        HideInGame(objects);
    }
    public void ShowInStart()
    {
        List<GameObject> objects = new List<GameObject>();
        objects.Add(playerControl.gameObject);
        //objects.Add(cameraCon.gameObject);
        foreach (FloorControl item in floors)
        {
            objects.Add(item.gameObject);
        }
        ShowInGame(objects);
    }

    public  void HideInGame(List<GameObject> gameObjects)
    {
        foreach (GameObject item in gameObjects)
        {
            item.gameObject.SetActive(false);
        }
    }
    public void ShowInGame(List<GameObject> gameObjects)
    {
        foreach (GameObject item in gameObjects)
        {
            item.gameObject.SetActive(true);
        }
    }
    public void QiutGame()
    {
        Application.Quit();
    }
}
