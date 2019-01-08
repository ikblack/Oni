using System.Collections;
using System.Collections.Generic;
using UniEventDispatcher;
using UnityEngine;

public enum PLAYERSTEP
{

    NONE = -1,
    RUN = 0,        // 奔跑 游戏中
    STOP,           // 停止 显示
    JUMP,
    DIE
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
    //跳跃高度
    public float jump_height = 1.5f;
    //跳跃高度
    public float jump_weight = 2.5f;

    public GameObject triggerJump;

    public float run_oriSpeed;
    public GameObject DieBox;
    public PLAYERSTEP step;
    public Animator animator;
    public bool bIsjump;
    protected bool is_running = true;

    public float ScoreRate = 0.005f;
    public string Score;
    void Start()
    {
        ini_pos = this.transform.localPosition;
        run_oriSpeed = run_speed;
        this.animator = this.GetComponentInChildren<Animator>();
        animator.SetTrigger("Run");
        step = PLAYERSTEP.RUN;
       
    }
   public  void InitPlayerState()
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
               
                animator.SetTrigger("Run");
                this.run_speed += run_speed_add * Time.deltaTime;
                if (RUN_SPEED_MAX> RUN_TEMP_SPEED)
                {
                    this.run_speed = Mathf.Clamp(this.run_speed, 0.0f, RUN_SPEED_MAX);
                }else
                    this.run_speed = Mathf.Clamp(this.run_speed, 0.0f, RUN_TEMP_SPEED);

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
              
                /*******************************************/
                break;
            case PLAYERSTEP.STOP:
                /*******************************************/

                this.GetComponent<Rigidbody>().velocity = Vector3.zero; ;
                /*******************************************/
                break;
            case PLAYERSTEP.JUMP:
                /*******************************************/
               
                //Vector3 velocity = this.GetComponent<Rigidbody>().velocity;

                //float jump_height = 1.5f;

                //velocity.x = 2.5f;
                //velocity.y = Mathf.Sqrt(MISS_GRAVITY * jump_height);
                //velocity.z = 0.0f;

                //this.GetComponent<Rigidbody>().velocity = velocity;
                step = PLAYERSTEP.NONE;
                Invoke("JumpEnd", 2f);
                bIsjump = false;
                /*******************************************/
                break;
            case PLAYERSTEP.DIE:
                animator.SetTrigger("Die");
                this.GetComponent<Rigidbody>().velocity = Vector3.zero;
                this.step = PLAYERSTEP.NONE;
                break;
            default:
                break;
        }
         Score = ((int)(ScoreRate * this.transform.position.x)).ToString();
        NotificationCenter.Get().DispatchEvent("Score", Score);

        if (Input.GetKeyDown(KeyCode.Space)|bIsjump)
        {
            animator.SetTrigger("Roll");
            Vector3 pos = this.gameObject.transform.position;
            //    this.run_speed = 0;
           // animator.SetTrigger("Run");
            this.transform.position = pos;
            this.step = PLAYERSTEP.JUMP;
        }

    }
    public void Run()
    {
        animator.SetBool("IsRun",true);
        animator.SetTrigger("Run");
       
    }
    void JumpEnd()
    {
        step = PLAYERSTEP.RUN;
    }

    void Roll(string msg)
    {
        Vector3 velocity = this.GetComponent<Rigidbody>().velocity;

        //triggerJump.SetActive(true);
        //this.gameObject. GetComponent<CapsuleCollider>().enabled = false;
        if (this.run_speed> jump_weight)
        {
            velocity.x = run_speed;
        }
        else
        {
            velocity.x = jump_weight;
        }
       
        velocity.y = Mathf.Sqrt(MISS_GRAVITY * jump_height);
        velocity.z = 0.0f;

        this.GetComponent<Rigidbody>().velocity = velocity;
    }
    void OnCapUp()
    {
        this.gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0, 1.6f, 0);
    }
    void OnCapDown()
    {
        this.gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0, 0.8f, 0);
    }
    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Enemy")
        {
             animator.SetBool("IsRun", false);
            // DieBox.gameObject.SetActive(true);
            /*animator.GetParameter(2)= false;*/
            this.step = PLAYERSTEP.DIE;
            //animator.SetTrigger("Die");
            GameObject.FindWithTag("Canvas").gameObject.GetComponent<GamePanel>().ShowMask();
            PlayerPrefs.SetInt("Dis",(int)(this.transform.localPosition.x));
            this.run_speed = 0;
            
        }
      
        //this.OnCollisionStay(other);
    }
    void OnTriggerEnter(Collider col)
    {
        /*if (col.gameObject.tag == "Enemy")
        {
           // DieBox.gameObject.SetActive(true);
           // animator.SetTrigger("Die");
        }
        this.run_speed = 0;
        StopRequest();
        //  this.is_running = true;*/

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
