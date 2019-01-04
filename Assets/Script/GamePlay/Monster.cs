using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Prop {

    // 移动的速度
    public float run_speed = 1.0f;

    // 移动速度的最大值 [m/sec].
    public const float RUN_SPEED_MAX = 2.0f;

    // 移动速度的加速值 [m/sec^2].
    protected const float run_speed_add = 0.50f;

    // 移动速度的减速值 [m/sec^2].
    protected const float run_speed_sub = 2.50f * 1.0f;

    private float run_oriSpeed;
    public override void Start()
    {
        base.Start();
        run_oriSpeed = run_speed;
    }
    public override void Update()
    {
        base.Update();
        this.run_speed += run_speed_add * Time.deltaTime;
        this.run_speed = Mathf.Clamp(this.run_speed, 0.0f, PlayerControl.RUN_SPEED_MAX);
        Vector3 new_velocity = this.GetComponent<Rigidbody>().velocity;

        new_velocity.x = run_speed;

        if (new_velocity.y > 0.0f)
        {

            new_velocity.y = 0.0f;
        }
        this.GetComponent<Rigidbody>().velocity = new_velocity;
    }

   void ReStart()
    {
        run_speed=run_oriSpeed;
    }
  
}
