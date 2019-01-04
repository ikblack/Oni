using UnityEngine;
using System.Collections;

public class Root : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        Application.runInBackground = true;
        PanelMgr.instance.OpenPanel<GamePanel>("");//打开登陆界面
	}

    void Update()
    {
     //   NetMgr.Update();
    }
}
