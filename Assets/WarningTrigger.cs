using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningTrigger : MonoBehaviour {
    public GameObject obj;
    public Animation anim;
    // Use this for initialization
    void Start () {
        this.gameObject.SetActive(true);
        obj.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            obj.SetActive(true);
            this.gameObject.SetActive(false);
            anim.Play();
        }
    }
}
