using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;


public class Player : MonoBehaviour
{
    [Header("动画")]
    //是否攻击
    bool isAttack = false;
    //是否死亡
    bool isDie = false;

    [Header("攻击")]
    //要生成的子弹
    public GameObject bulletpre;
    //开火速度(秒每发）
    public float fireRate = 0.8f;
    //上次开火的间隔
    public float lastFireTime = 0f;

    [Header("冲刺")]
    //冲刺距离
    float rashAmout = 5.0f;
    //冲刺瞬移量
    Vector3 rushPosition;
    //冲刺冷却时间
    float rushCD = 0.6f;
    //冲刺冷却倒计时
    float currentRushCD;
    //冲刺无敌的总时间
    float rushDuration = 0.3f;
    //冲刺无敌的倒计时
    float rushConter;

    [Header("状态")]
    private bool isWalk;


    [Header("引用组件")]
    //引用rigidbody2D
    private Rigidbody2D rigidbody2D;
    //引用Animator
    private Animator animator;

    [Header("速度")]
    //玩家标准速度
    private const float speed = 20.0f;
    //玩家速度
    private float moveSpeed;

    [Header("生命值")]
    //玩家最大生命值
    private int MaxHP = 20;
    //玩家当前生命值
    public int CurrentHP;

    [Header(" 无敌")]
    //玩家是否处于无敌状态
    private bool invulnerable;

    [Header("移动相关")]
    //移动变量
    private float moveX ;
    private float moveY ;
    //移动方向
    private Vector3 moveDir;
    //是否顺移
    private bool isDash;
    //记录移动方向
    private Vector3 lastMoveDir;
    
    
    private void Awake()
    {
        //引用Rigidbody2D
        rigidbody2D = GetComponent<Rigidbody2D>();
        //引用Animator
        animator = GetComponent<Animator>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
        //开始游戏时，玩家的血量等于最大生命值
        CurrentHP = MaxHP;
        //冲刺冷却初始化
        currentRushCD = 0;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void FixedUpdate()
    {
        //移动变量
      moveX = 0f;
      moveY = 0f;
    //玩家速度
    moveSpeed = speed;
        //getInput();
        move();
        //重置冲刺状态
        isDash = false;
        //移动方向
        moveDir = new Vector3(moveX, moveY).normalized;
        //记录移动方向
        if (moveX != 0f || moveY != 0f)
        {
            lastMoveDir = moveDir;
        }
        Dash();
        //Ainmator attack
        animator.SetBool("attack", isAttack);
        Attack();
        //Animator die
        Die();
    }


    //玩家移动方法
    private void move()
    {
        //float h = Input.GetAxisRaw("Horizontal");
        //float v = Input.GetAxisRaw("Vertical");

        //rigidbody2D.transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        //rigidbody2D.transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveY = +1.0f;
            
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveY = -1.0f;
            
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveX = +1.0f;
            
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveX = -1.0f;
            
        }
        

        rigidbody2D.velocity = moveDir * moveSpeed;
    }

    //玩家当前生命值变化方法
    public void CHPC()
    {
        
    }


    //玩家闪避机制
    public void Dash()
    {
        //是否在CD
        bool isNotInCD = currentRushCD <= 0;
        //冲刺距离
         rashAmout = 5.0f;
        //冲刺瞬移量
        rushPosition = transform.position + lastMoveDir * rashAmout;
        //冲刺冷却时间
         rushCD = 0.5f;
        //冲刺无敌的总时间
         rushDuration = 0.3f;

        if (isNotInCD)
        {
            //按下C键开始冲刺
            if (Input.GetKeyDown(KeyCode.C))
            {
                isDash = true;
            }
            //冲刺
            if (isDash)
            {

                //冲刺到目的地
                rigidbody2D.MovePosition(rushPosition);

                //重置冲刺冷却倒计时
                currentRushCD = rushCD;


                //启动无敌
                invulnerable = true;

                //重置冲刺无敌的倒计时
                rushConter = rushDuration;

                //倒计时
                rushConter -= Time.fixedDeltaTime;
                Debug.Log(rushConter);
                //无敌时间取消
                if (rushConter <= 0)
                {
                    invulnerable = false;
                }
            }
        }
        else
        {
            //冷却开始倒计时
            currentRushCD -= Time.fixedDeltaTime;

        }


    }

    //玩家无敌机制
    private void TriggerInvulnerable()
    {
        
    }

    //玩家的攻击方法
    private void Attack()
    {
        

        //按下X攻击
        if (Input.GetKey(KeyCode.X))
        {
            isAttack = true;

            //让子弹有发射间隔
            if (Time.time > lastFireTime)
            {
                lastFireTime = Time.time + fireRate;
               Instantiate(bulletpre, transform.position, Quaternion.identity);
            }

        }
        else
        {
            isAttack = false;
        }
    }

    //玩家死亡方法
    private void Die()
    {
        if(CurrentHP <= 0)
        {
            isDie = true;
            Debug.Log(isDie);
        }
        if (isDie)
        {
            Destroy(gameObject, 3.0f);
            animator.SetBool("die", isDie);
        }
    }
}
