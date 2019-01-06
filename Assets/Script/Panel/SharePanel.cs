using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SharePanel : PanelBase {
    private GameObject sharePanel;
    private Button returnBut;
    GameObject obj;
    #region 生命周期
    public override void Init(params object[] args)
    {
        base.Init(args);
        skinPath = "SharePanel";
        layer = PanelLayer.Panel;
    }

    public override void OnShowing()
    {
        base.OnShowing();
        Transform skinTrans = skin.transform;
        sharePanel = skin.transform.gameObject;
        returnBut = skin.transform.Find("ShareBut").GetComponent<Button>();
     //   sharePanel.SetActive(false);
        returnBut.onClick.AddListener(Return);
    }
    #endregion

    public void Return()
    {
        GameObject.FindWithTag("Canvas").gameObject.GetComponent<StartPanel>().Show();
        Close();
     //   sharePanel.SetActive(false);
     
    }
    public void Show()
    {
     //   sharePanel.SetActive(true);
    }

}
