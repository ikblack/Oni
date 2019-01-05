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
    private GameObject CanvasPanel;
    private Image MaskP;
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
        MaskP= skinTrans.Find("Mask").GetChild(0).GetComponent<Image>();

       

        CanvasPanel.gameObject.SetActive(true);
        NotificationCenter.Get().AddEventListener("Score", ShowScore);

        FlashButton.onClick.AddListener(OnFlash);
        RollButton.onClick.AddListener(OnRoll);
        ReturnButton.onClick.AddListener(OnReturn);

        MaskP.enabled = false;
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
        // GameObject.FindWithTag("Player").gameObject.GetComponent<OniPlayerController>().bIsjump = true;
        FlashButton.enabled = false;

    }
    public void OnRoll()
    {
        GameObject.FindWithTag("Player").gameObject.GetComponent<OniPlayerController>().bIsjump=true;
        RollButton.enabled = false;
        Invoke("OnRollReset",Util.RollCD);
    }
    public void OnReturn()
    {
        // Close();
        CanvasPanel.gameObject.SetActive(false);
        PanelMgr.instance.OpenPanel<StartPanel>("");
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
        MaskP.enabled = true;
        FlashButton.interactable = false;
        RollButton.interactable = false;
    }

    public void HideMask()
    {
        MaskP.enabled = false;
        FlashButton.interactable = true;
        RollButton.interactable = true;
    }
}