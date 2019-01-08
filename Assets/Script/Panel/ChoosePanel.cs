using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChoosePanel : PanelBase {

    private Button nextBut;
    private Button twoBtn;
    private Button returnBtn;
    private Button lastBtn;
    private Button playBtn;
    private Button btn2;
    private Button retuenChooseBtn;
    private GameObject broad;
    private GameObject btn2Show;
   
    int index=1;
    #region 生命周期
    public override void Init(params object[] args)
    {
        base.Init(args);
        skinPath = "ChoosePanel";
        layer = PanelLayer.Panel;
    }

    public override void OnShowing()
    {
        base.OnShowing();
        Transform skinTrans = skin.transform;
        nextBut = skinTrans.Find("NextBtn").GetComponent<Button>();
       // twoBtn = skinTrans.Find("ReturnBut").GetComponent<Button>();
        returnBtn= skinTrans.Find("ReturnBtn").GetComponent<Button>();
        lastBtn = skinTrans.Find("LastBtn").GetComponent<Button>();
        playBtn = skinTrans.Find("PlayBtn").GetComponent<Button>();
        broad= skinTrans.Find("Broad").gameObject;
       // broad = skinTrans.Find("Broad/Top/RetuenChooseBtn").gameObject;
        btn2Show =skinTrans.Find("Btn2Show").gameObject;
        btn2 = skinTrans.Find("Btn2").GetComponent<Button>();
        retuenChooseBtn = skinTrans.Find("Broad/Top/RetuenChooseBtn").GetComponent<Button>();

        nextBut.onClick.AddListener(OnNextClick);
        returnBtn.onClick.AddListener(OnReturnClick);
        lastBtn.onClick.AddListener(OnLastClick);
        playBtn.onClick.AddListener(OnStartClick);
        btn2.onClick.AddListener(OnBroardClick);
        retuenChooseBtn.onClick.AddListener(OnReturnChooseClick);
        ChoosePlayer.instance.ShowPlayer(0);
    }
    #endregion
    public void OnReturnChooseClick()
    {
        btn2Show.SetActive(false);
        btn2.gameObject.SetActive(true);
        broad.gameObject.SetActive(false);
    }
    public void OnStartClick()
    {
        GameObject.FindWithTag("Canvas").gameObject.GetComponent<GamePanel>().Show();
        if (RoleLoad.instance)
        {
            RoleLoad.instance.Load();
            GameController._gameInstance.MonsterRun();
            //Camera.main.gameObject.GetComponent<CameraControl>().enabled = true;
        }
        Close();
    }
    public void OnLastClick()
    {
        ChoosePlayer.instance.ShowPlayer(index);
        index--;
        if (index<0)
        {
            index = 2;
        }
    }
    public void OnNextClick()
    {
        ChoosePlayer.instance.ShowPlayer(index);
        index++;
    }
    public void OnBroardClick()
    {
        btn2Show.SetActive(true);
        btn2.gameObject. SetActive(false);
        broad.gameObject.SetActive(true);
    }


    public void OnReturnClick()
    {
        GameObject.FindWithTag("Canvas").gameObject.GetComponent<StartPanel>().Show();
        ChoosePlayer.instance.HideAllPlayer();
        Close();
    }
}

