using System.Collections;
using System.Collections.Generic;
using UniEventDispatcher;
using UnityEngine;
using UnityEngine.UI;
public class GamePanel :PanelBase
{

    private Button FlashButton;
    private Button RollButton;
    private Button ReturnButton;
    private Button RestartButton;
    private GameObject CanvasPanel;
    private GameObject MaskP;
    private Image StartMask;
    private Text Score;
    private float CurrentTimer;
    private float LastTimer;
    private float TimerVal;
    #region 生命周期
    public override void Init(params object[] args)
    {
        base.Init(args);
        skinPath = "GamePanel";
        layer = PanelLayer.Panel;
    }

    public override void OnShowing()
    {
        base.OnShowing();
        Transform skinTrans = skin.transform;
        CanvasPanel = skin.transform. gameObject;
        FlashButton = skinTrans.Find("FlashButton").GetComponent<Button>();
        RollButton = skinTrans.Find("RollButton").GetComponent<Button>();
        ReturnButton = skinTrans.Find("ReturnButton").GetComponent<Button>();
        Score = skinTrans.Find("Score").GetChild(0).GetComponent<Text>();
        MaskP= skinTrans.Find("Mask").gameObject;
        RestartButton = skinTrans.Find("Mask/RestartBtn").GetComponent<Button>();
        StartMask = skinTrans.Find("StartMask").GetComponent<Image>();


        CanvasPanel.gameObject.SetActive(true);
        NotificationCenter.Get().AddEventListener("Score", ShowScore);

        FlashButton.onClick.AddListener(OnFlash);
        RollButton.onClick.AddListener(OnRoll);
        ReturnButton.onClick.AddListener(OnReturn);
        RestartButton.onClick.AddListener(Restart);
            
        MaskP.SetActive(false);
    }
    #endregion
    public void ShowScore(Notification notification)
    {
        if (Score)
        {
            Score.text = notification.param.ToString();
        }
       
    }
    public void InitScore()
    {
        Score.text = "0";
    }
    public void OnStartClick()
    {
        // Debug.Log("Start");
        GameObject.Find("Load").gameObject.GetComponent<AsyncLoadScene>().FinishLoad();
       
    }
    public void OnFlash()
    {
        TagMark.instance.Player.gameObject .GetComponent<OniPlayerController>().RUN_TEMP_SPEED = 5.0f;
        FlashButton.enabled = false;
        Invoke("OnFlashReset", Util.FlashCD);
    }
    public void OnRoll()
    {
        TagMark.instance.Player.gameObject.GetComponent<OniPlayerController>().bIsjump=true;
        RollButton.enabled = false;
        Invoke("OnRollReset",Util.RollCD);
    }
    public void OnReturn()
    {
        // Close();
        CanvasPanel.gameObject.SetActive(false);
      
        PanelMgr.instance.OpenPanel<StartPanel>("");
        GameObject.FindWithTag("Canvas").gameObject.GetComponent<StartPanel>().Show();
        GameController._gameInstance.RestartGame();
        TagMark.instance.Player.gameObject.GetComponent<OniPlayerController>().GetComponent<OniPlayerController>().step = PLAYERSTEP.STOP;
        GameController._gameInstance.MonsterStop();
        // Camera.main.gameObject.GetComponent<CameraControl>().enabled = false;
    }
    public void Show()
    {
        CanvasPanel.gameObject.SetActive(true);
    }

    public void OnRollReset()
    {
        RollButton.enabled = true;
    }

    public void OnFlashReset()
    {
        FlashButton.enabled = true;
    }
    public void ShowMask()
    {
        MaskP.SetActive(true);
        FlashButton.interactable = false;
        RollButton.interactable = false;
    }

    public void HideMask()
    {
        StartMask.gameObject. SetActive(false);
        MaskP.SetActive(false) ;
        FlashButton.interactable = true;
        RollButton.interactable = true;
    }

    public void Restart()
    {
        GameController._gameInstance.RestartGame();
        FadeControl.get().ui_image = StartMask;
        FadeControl.get().fade(1.0f, new Color(0.0f, 0.0f, 0.0f, 0.0f), new Color(0.0f, 0.0f, 0.0f, 1.0f));
        GameObject.FindWithTag("Player").gameObject.GetComponent<OniPlayerController>().Run();
        GameObject.FindWithTag("Player").gameObject.GetComponent<OniPlayerController>().step = PLAYERSTEP.STOP;
       // GameController._gameInstance.MonsterStop();
        Invoke("Delay",1.25f);

    }
    public void Delay()
    {
        
        FadeControl.get().gameObject.SetActive(false);
        GameObject.FindWithTag("Player").gameObject.GetComponent<OniPlayerController>().run_speed = GameObject.FindWithTag("Player").gameObject.GetComponent<OniPlayerController>().run_oriSpeed;
        GameObject.FindWithTag("Player").gameObject.GetComponent<OniPlayerController>().step = PLAYERSTEP.RUN;
        GameObject.FindWithTag("Player").gameObject.GetComponent<OniPlayerController>().Run();
        HideMask();
        GameController._gameInstance.MonsterRun();
    }
    public void DelayRun()
    {
        GameObject.FindWithTag("Player").gameObject.GetComponent<OniPlayerController>().step = PLAYERSTEP.RUN;
       
    }
}