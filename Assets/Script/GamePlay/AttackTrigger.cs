using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

    // Use this for initialization
    public Monster monster;
    private bool bIsDeath;
    
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (bIsDeath)
        {
            monster.run_speed = 0;
        }
        
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            // PanelMgr.instance.OpenPanel<ResultPanel>("");
            Debug.Log("OnCollisionEnter");
           // GameObject obj = GameObject.FindWithTag("Player");//.transform.Find("DieBox").gameObject;//.GetComponent<CapsuleCollider>().enabled=true;
           // obj.SetActive(true);
            bIsDeath = true;
        }
    }
 
}
