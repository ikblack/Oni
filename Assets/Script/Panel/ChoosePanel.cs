using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChoosePanel : PanelBase {

    private Button oneBtn;
    private Button twoBtn;
    private Button returnBtn;

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
        oneBtn = skinTrans.Find("OneBtn").GetComponent<Button>();
        twoBtn = skinTrans.Find("TwoBtn").GetComponent<Button>();
        returnBtn= skinTrans.Find("ReturnBtn").GetComponent<Button>();

        oneBtn.onClick.AddListener(OnOneClick);
        twoBtn.onClick.AddListener(OnTwoClick);
        returnBtn.onClick.AddListener(OnReturnClick);
    }
    #endregion


    public void OnOneClick()
    {
    }

    public void OnTwoClick()
    {
    }
    public void OnReturnClick()
    {
        Close();
        PanelMgr.instance.OpenPanel<TitlePanel>("");
    }
}

