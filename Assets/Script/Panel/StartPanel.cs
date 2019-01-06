using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : PanelBase
{
    private GameObject startPanel;
    private Button startBtn;
    private Button shareBtn;
    private Button setBtn;
    GameObject obj;
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
        startPanel = skin.transform.gameObject; 
        startBtn = skinTrans.Find("StartBtn").GetComponent<Button>();
        shareBtn = skinTrans.Find("ShareBtn").GetComponent<Button>();
        setBtn = skinTrans.Find("SetBtn").GetComponent<Button>();
        if (GameObject.Find("Load"))
        {
            obj = GameObject.Find("Load").gameObject;
        }
   

        startBtn.onClick.AddListener(OnStartClick);
        shareBtn.onClick.AddListener(OnShareClick);
        setBtn.onClick.AddListener(OnSetClick);
    }
    #endregion

    public void OnStartClick()
    {
        if (obj)
        {
            obj.GetComponent<AsyncLoadScene>().FinishLoad();
        }
        else
        {
            GameObject.FindWithTag("Canvas").gameObject.GetComponent<GamePanel>().Show();
            startPanel.SetActive(false);
        }
        if (RoleLoad.instance)
        {
            RoleLoad.instance.Load();
            Camera.main.gameObject.GetComponent<CameraControl>().enabled = true;
        }

    }

    public void OnShareClick()
    {
        PanelMgr.instance.OpenPanel<SharePanel>("");
        //GameObject.FindWithTag("Canvas").gameObject.GetComponent<SharePanel>().Show();
        startPanel.SetActive(false);
    }
    public void OnSetClick()
    {
        // obj.GetComponent<AsyncLoadScene>().FinishLoad();
        PanelMgr.instance.OpenPanel<ChoosePanel>("");
        startPanel.SetActive(false);
    }
   public void Show()
    {
        startPanel.SetActive(true);
    }
}