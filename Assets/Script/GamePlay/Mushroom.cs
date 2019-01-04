using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MushroomPropType
{
    NORMAL,
    UP,
    DOWN
}
public enum EType
{
  PROP,
  ENEMY
}
public class Mushroom : Prop
{
    public Animation anim;
    public MushroomPropType prop;
    public Vector3 initial_position;
    public Vector3 initial_rotation;
    private GameObject Player;
    public override void Start()
    {
        base.Start();
        prop = MushroomPropType.NORMAL;
        this.initial_position = this.transform.position;
        //anim.GetComponent<Animation>().Play();
        Player = GameObject.FindGameObjectWithTag("Player").gameObject;
    }


    public override void Update()
    {
        base.Update();
        if (Vector3.Distance(Player.transform.localPosition,this.transform.localPosition)>=50)
        {
            Destroy(this.gameObject);
        }
    }
    
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag=="Player")
        {
            //PanelMgr.instance.OpenPanel<ResultPanel>("");
            //GameObject.FindWithTag("Canvas").gameObject.GetComponent<GamePanel>().ShowMask();
        }
        Debug.Log("OnTriggerEnter");
       
    }
  
}
