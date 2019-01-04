using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UniEventDispatcher;
public class Login : MonoBehaviour {

    private Button login;
    private Button sign;
	void Start () {

        NotificationCenter.Get().AddEventListener("Tips", Tips);
        login = GameObject.Find("Canvas/Login").GetComponent<Button>();
        sign = GameObject.Find("Canvas/Sign").GetComponent<Button>();

        login.onClick.AddListener(delegate () { this.OnUISend("Login"); });
        sign.onClick.AddListener(delegate () { this.OnUISend("Sign"); });
    }
    public void OnUISend(string content) {
       
        NotificationCenter.Get().DispatchEvent("Tips",content);
        
    }

	
	public  void Tips(Notification notification)
    {
        Debug.Log(notification.ToString());
        Debug.Log(notification.param);
    }

}
