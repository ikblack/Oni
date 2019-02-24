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
    private GameObject Img1;
    private GameObject Img2;
    private GameObject Img3;
    private Image StartMask;
    private Text Score;
    private Text ScoreTipScore;
    private Text RewardText;
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
        ScoreTipScore= skinTrans.Find("Mask/ScoreTip/Text").GetComponent<Text>();
        RewardText= skinTrans.Find("Mask/Reward/Text").GetComponent<Text>();

        Img1 =skinTrans.Find("Score1").gameObject;
        Img2 = skinTrans.Find("Score2").gameObject;
        Img3= skinTrans.Find("Score3").gameObject;

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
            ScoreTipScore.text = Score.text;
            RewardText.text = "0";
        }
       int index= PlayerPrefs.GetInt("Role");
        if (index==0)
        {
            Img1.gameObject.SetActive(true);
            Img2.gameObject.SetActive(false);
            Img3.gameObject.SetActive(false);
        }
        else if (index==1)
        {
            Img1.gameObject.SetActive(false);
            Img2.gameObject.SetActive(true);
            Img3.gameObject.SetActive(false);
        }
        else if (index==2)
        {
            Img1.gameObject.SetActive(false);
            Img2.gameObject.SetActive(false);
            Img3.gameObject.SetActive(true);
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
        TagMark.instance.Player.gameObject .GetComponent<OniPlayerController>().RUN_TEMP_SPEED =Util.TempSpeed;
        
        if (FlashButton.enabled ==true)
        {
            Invoke("OnFlashReset", Util.FlashCD);
        }
        FlashButton.enabled = false;
    }
    public void OnRoll()
    {
        TagMark.instance.Player.gameObject.GetComponent<OniPlayerController>().bIsjump=true;
        
        if (RollButton.enabled ==true)
        {
            Invoke("OnRollReset", Util.RollCD);

        }
        RollButton.enabled = false;
    }
    public void OnReturn()
    {
        // Close();
        CanvasPanel.gameObject.SetActive(false);

        // PanelMgr.instance.OpenPanel<StartPanel>("");
        if (GameObject.FindWithTag("Canvas").gameObject.GetComponent<StartPanel>())
        {
            GameObject.FindWithTag("Canvas").gameObject.GetComponent<StartPanel>().Show();
        }
        else
            PanelMgr.instance.OpenPanel<StartPanel>("");
        GameController._gameInstance.RestartGame();
        TagMark.instance.Player.gameObject.GetComponent<OniPlayerController>().GetComponent<OniPlayerController>().step = PLAYERSTEP.STOP;
        GameController._gameInstance.MonsterStop();
        // Camera.main.gameObject.GetComponent<CameraControl>().enabled = false;
        HideMask();
    }
    public void Show()
    {
        CanvasPanel.gameObject.SetActive(true);
    }

    public void OnRollReset()
    {
        RollButton.enabled = true;
        //TagMark.instance.Player.gameObject.GetComponent<OniPlayerController>().bisEnd = true;
       // Debug.LogError("11");
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
        Invoke("Delay",2.5f);

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