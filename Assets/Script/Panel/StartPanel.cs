using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : PanelBase
{

    private Button startBtn;
    GameObject obj;
   // private Button quitBtn;
   // private Button chooseBtn;
    #region 生命周期
    public override void Init(params object[] args)
    {
        base.Init(args);
        skinPath = "StartPanel";
        layer = PanelLayer.Panel;
    }

    public override void OnShowing()
    {
        base.OnShowing();
        Transform skinTrans = skin.transform;
        startBtn = skinTrans.Find("StartBtn").GetComponent<Button>();
        // quitBtn = skinTrans.Find("QuitBtn").GetComponent<Button>();
        // chooseBtn = skinTrans.Find("ChooseBtn").GetComponent<Button>();
        if (GameObject.Find("Load"))
        {
            obj = GameObject.Find("Load").gameObject;
        }
   

        startBtn.onClick.AddListener(OnStartClick);
       // quitBtn.onClick.AddListener(OnQiutClick);
       // chooseBtn.onClick.AddListener(OnChooseClick);
    }
    #endregion

    public void OnStartClick()
    {
        // Debug.Log("Start");

        if (obj)
        {
            obj.GetComponent<AsyncLoadScene>().FinishLoad();
        }
        else
            Close();
        GameObject.FindWithTag("Canvas").gameObject.GetComponent<GamePanel>().Show();
    }
}