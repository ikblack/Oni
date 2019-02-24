using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour {
    public PropController propcon;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider col)
    {
        if (col.tag=="Player")
        {
            //col.gameObject.SetActive(false);
            propcon.GetComponent<PropController>(). SpawnProp(propcon.monster);
            propcon.GetComponent<PropController>().SpawnProp(propcon.mushroom);
        }
       
    }
}
