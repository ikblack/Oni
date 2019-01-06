using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChoosePanel : PanelBase {

    private Button nextBut;
    private Button twoBtn;
    private Button returnBtn;

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

        nextBut.onClick.AddListener(OnNextClick);
        returnBtn.onClick.AddListener(OnReturnClick);

        ChoosePlayer.instance.ShowPlayer(0);
    }
    #endregion


    public void OnNextClick()
    {
        ChoosePlayer.instance.ShowPlayer(index);
        index++;
    }

   
    public void OnReturnClick()
    {
        GameObject.FindWithTag("Canvas").gameObject.GetComponent<StartPanel>().Show();
        ChoosePlayer.instance.HideAllPlayer();
        Close();
    }
}

