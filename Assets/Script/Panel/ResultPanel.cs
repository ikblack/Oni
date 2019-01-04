using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResultPanel : PanelBase {

    private Button startBtn;
    private Button quitBtn;
    private Button chooseBtn;
    #region 生命周期
    public override void Init(params object[] args)
    {
        base.Init(args);
        skinPath = "TitlePanel";
        layer = PanelLayer.Panel;
    }

    public override void OnShowing()
    {
        base.OnShowing();
        Transform skinTrans = skin.transform;
        startBtn = skinTrans.Find("StartBtn").GetComponent<Button>();
        quitBtn = skinTrans.Find("QuitBtn").GetComponent<Button>();
        chooseBtn = skinTrans.Find("ChooseBtn").GetComponent<Button>();


        startBtn.onClick.AddListener(OnStartClick);
        quitBtn.onClick.AddListener(OnQiutClick);
        chooseBtn.onClick.AddListener(OnChooseClick);
    }
    #endregion


    public void OnStartClick()
    {
        Close();
        GameController._gameInstance.StartGame();
    }

    public void OnQiutClick()
    {
        GameController._gameInstance.QiutGame();
    }

    public void OnChooseClick()
    {
        Close();
        PanelMgr.instance.OpenPanel<ChoosePanel>("");
    }
}
