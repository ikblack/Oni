using System.Collections;
using System.Collections.Generic;
using UniEventDispatcher;
using UnityEngine;

public enum PLAYERSTEP
{

    NONE = -1,
    RUN = 0,        // 奔跑 游戏中
    STOP,           // 停止 显示
    JUMP
};

public class OniPlayerController : MonoBehaviour {
    // 初始位置
    public Vector3 ini_pos ;
    // 移动的速度
    public float run_speed = 1.0f;
    // 移动速度的最大值 [m/sec].
    public const float RUN_SPEED_MAX = 2.0f;
    // 移动速度的临时值 [m/sec].
    public  float RUN_TEMP_SPEED = .0f;
    //错过时的重力值[m / sec ^ 2].
    protected const float MISS_GRAVITY = 9.8f * 2.0f;
    // 移动速度的加速值 [m/sec^2].
    protected const float run_speed_add = 5f;
    // 移动速度的减速值 [m/sec^2].
    protected const float run_speed_sub = 2.50f * 1.0f;
    private float run_oriSpeed;
    public GameObject DieBox;
    public PLAYERSTEP step;
    public Animator animator;
    public bool bIsjump;
    protected bool is_running = true;

    public float ScoreRate = 0.05f;
    public string Score;
    void Start()
    {
        ini_pos = this.transform.localPosition;
        run_oriSpeed = run_speed;
        this.animator = this.GetComponentInChildren<Animator>();
        animator.SetTrigger("Run");
        step = PLAYERSTEP.NONE;
       
    }
    void InitPlayerState()
    {
        this.step = PLAYERSTEP.NONE;
        this.transform.localPosition = ini_pos;
        //this.transform.localEulerAngles
    }
    void Update()
    {
        switch (step)
        {
            case PLAYERSTEP.NONE:
                Vector3 vel = this.GetComponent<Rigidbody>().velocity;

              //  float height = 1.5f;

              //  vel.x = 2.5f;
               // vel.y = -Mathf.Sqrt(MISS_GRAVITY )/2;
              //  vel.z = 0.0f;

                this.GetComponent<Rigidbody>().velocity =vel;
                break;
            case PLAYERSTEP.RUN:
                /*****************************************/
                this.run_speed += run_speed_add * Time.deltaTime;
                if (RUN_SPEED_MAX> RUN_TEMP_SPEED)
                {
                    this.run_speed = Mathf.Clamp(this.run_speed, 0.0f, RUN_SPEED_MAX);
                }else
                    this.run_speed = Mathf.Clamp(this.run_speed, 0.0f, RUN_SPEED_MAX);

                if (this.run_speed>= RUN_TEMP_SPEED)
                {
                    RUN_TEMP_SPEED = 0;
                }

                Vector3 new_velocity = this.GetComponent<Rigidbody>().velocity;

                new_velocity.x = run_speed;

                if (new_velocity.y > 0.0f)
                {

                  //  new_velocity.y = 0.0f;
                }
                this.GetComponent<Rigidbody>().velocity = new_velocity;
                //Debug.Log("11111111");
                /*******************************************/
                break;
            case PLAYERSTEP.STOP:
                /*******************************************/

                /*******************************************/
                break;
            case PLAYERSTEP.JUMP:
                /*******************************************/
               
                Vector3 velocity = this.GetComponent<Rigidbody>().velocity;

                float jump_height = 2.5f;

                velocity.x = 2.5f;
                velocity.y = Mathf.Sqrt(MISS_GRAVITY * jump_height);
                velocity.z = 0.0f;

                this.GetComponent<Rigidbody>().velocity = velocity;
                step = PLAYERSTEP.NONE;
                Invoke("JumpEnd", 1f);
                bIsjump = false;
                /*******************************************/
                break;
            default:
                break;
        }
         Score = ((int)(ScoreRate * Time.time*this.transform.position.x)).ToString();
        NotificationCenter.Get().DispatchEvent("Score", Score);

        if (Input.GetKeyDown(KeyCode.Space)|bIsjump)
        {
            animator.SetTrigger("Roll");
            Vector3 pos = this.gameObject.transform.position;
            //    this.run_speed = 0;
            animator.SetTrigger("Run");
            this.transform.position = pos;
            this.step = PLAYERSTEP.JUMP;
        }

    }

    void JumpEnd()
    {
        step = PLAYERSTEP.RUN;
    }

    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Enemy")
        {
            DieBox.gameObject.SetActive(true);
            animator.SetTrigger("Die");
            GameObject.FindWithTag("Canvas").gameObject.GetComponent<GamePanel>().ShowMask();
        }
        this.run_speed = 0;
      
        //this.OnCollisionStay(other);
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            DieBox.gameObject.SetActive(true);
            animator.SetTrigger("Die");
        }
        this.run_speed = 0;
        StopRequest();
        //  this.is_running = true;

    }


    public void StopRequest()
    {
        this.is_running = false;
    }

    private void OnGUI()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            step = PLAYERSTEP.RUN;
        }
    }
}
