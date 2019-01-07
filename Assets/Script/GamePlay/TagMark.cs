using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagMark : MonoBehaviour {

    public static TagMark instance;
    public OniPlayerController Player;
	void Awake () {
        instance = this;
        Player = GameObject.FindWithTag("Player").gameObject.GetComponent<OniPlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ChangPlayer()
    {
        Player = GameObject.FindWithTag("Player").gameObject.GetComponent<OniPlayerController>();
    }
}
