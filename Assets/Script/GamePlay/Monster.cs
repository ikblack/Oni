using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Prop {

    // 移动的速度
    public float run_speed = 1.0f;

    // 移动速度的最大值 [m/sec].
    public  float RUN_SPEED_MAX = 10.0f;

    // 移动速度的加速值 [m/sec^2].
    public float run_speed_add = 0.50f;

    // 移动速度的减速值 [m/sec^2].
    protected const float run_speed_sub = 2.50f * 1.0f;

    public Vector3 init_pos;

    private float run_oriSpeed;

    public bool bIsRun=true;
    public bool bIsLeft=true;
    public override void Start()
    {
        base.Start();
        run_oriSpeed = run_speed;
        init_pos = this.gameObject.transform.localPosition;
    }
    public override void Update()
    {
        base.Update();
        if (bIsRun)
        {
            //Debug.Log("1");
          
            if (bIsLeft)
            {
                //Debug.Log("2");
                this.run_speed += run_speed_add * Time.deltaTime;
                this.run_speed = Mathf.Clamp(this.run_speed, 0.0f, RUN_SPEED_MAX);
                //Debug.Log(RUN_SPEED_MAX);
            }
            else
            {

             //    this.run_speed = - PlayerControl.RUN_SPEED_MAX;
            }
            Vector3 new_velocity = this.GetComponent<Rigidbody>().velocity;

            new_velocity.x = run_speed;

            if (new_velocity.y > 0.0f)
            {

                new_velocity.y = 0.0f;
               // Debug.Log("3");
            }
            this.GetComponent<Rigidbody>().velocity = new_velocity;
        }
        else
        {
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        
    }

   public void ReStart()
    {
        run_speed=run_oriSpeed;
        this.transform.localPosition = init_pos;
    }
    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "Player")
        {
            // PanelMgr.instance.OpenPanel<ResultPanel>("");
            Debug.Log("OnCollisionEnter");
            // GameObject obj = GameObject.FindWithTag("Player");//.transform.Find("DieBox").gameObject;//.GetComponent<CapsuleCollider>().enabled=true;
            // obj.SetActive(true);
            // bIsDeath = true;
            GameController._gameInstance.MonsterStop();
        }

        if (col.gameObject.tag == "FX_Hit")
        {
            if (col.gameObject.transform.parent)
            {
                Destroy(col.gameObject.transform.parent.gameObject);
            }
            else
            {
                Destroy(col.gameObject);
            }
        }
    }

}
