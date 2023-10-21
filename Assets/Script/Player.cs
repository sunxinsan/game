using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;


public class Player : MonoBehaviour
{
    [Header("����")]
    //�Ƿ񹥻�
    bool isAttack = false;
    //�Ƿ�����
    bool isDie = false;

    [Header("����")]
    //Ҫ���ɵ��ӵ�
    public GameObject bulletpre;
    //�����ٶ�(��ÿ����
    public float fireRate = 0.8f;
    //�ϴο���ļ��
    public float lastFireTime = 0f;

    [Header("���")]
    //��̾���
    float rashAmout = 5.0f;
    //���˲����
    Vector3 rushPosition;
    //�����ȴʱ��
    float rushCD = 0.6f;
    //�����ȴ����ʱ
    float currentRushCD;
    //����޵е���ʱ��
    float rushDuration = 0.3f;
    //����޵еĵ���ʱ
    float rushConter;

    [Header("״̬")]
    private bool isWalk;


    [Header("�������")]
    //����rigidbody2D
    private Rigidbody2D rigidbody2D;
    //����Animator
    private Animator animator;

    [Header("�ٶ�")]
    //��ұ�׼�ٶ�
    private const float speed = 20.0f;
    //����ٶ�
    private float moveSpeed;

    [Header("����ֵ")]
    //����������ֵ
    private int MaxHP = 20;
    //��ҵ�ǰ����ֵ
    public int CurrentHP;

    [Header(" �޵�")]
    //����Ƿ����޵�״̬
    private bool invulnerable;

    [Header("�ƶ����")]
    //�ƶ�����
    private float moveX ;
    private float moveY ;
    //�ƶ�����
    private Vector3 moveDir;
    //�Ƿ�˳��
    private bool isDash;
    //��¼�ƶ�����
    private Vector3 lastMoveDir;
    
    
    private void Awake()
    {
        //����Rigidbody2D
        rigidbody2D = GetComponent<Rigidbody2D>();
        //����Animator
        animator = GetComponent<Animator>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
        //��ʼ��Ϸʱ����ҵ�Ѫ�������������ֵ
        CurrentHP = MaxHP;
        //�����ȴ��ʼ��
        currentRushCD = 0;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void FixedUpdate()
    {
        //�ƶ�����
      moveX = 0f;
      moveY = 0f;
    //����ٶ�
    moveSpeed = speed;
        //getInput();
        move();
        //���ó��״̬
        isDash = false;
        //�ƶ�����
        moveDir = new Vector3(moveX, moveY).normalized;
        //��¼�ƶ�����
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


    //����ƶ�����
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

    //��ҵ�ǰ����ֵ�仯����
    public void CHPC()
    {
        
    }


    //������ܻ���
    public void Dash()
    {
        //�Ƿ���CD
        bool isNotInCD = currentRushCD <= 0;
        //��̾���
         rashAmout = 5.0f;
        //���˲����
        rushPosition = transform.position + lastMoveDir * rashAmout;
        //�����ȴʱ��
         rushCD = 0.5f;
        //����޵е���ʱ��
         rushDuration = 0.3f;

        if (isNotInCD)
        {
            //����C����ʼ���
            if (Input.GetKeyDown(KeyCode.C))
            {
                isDash = true;
            }
            //���
            if (isDash)
            {

                //��̵�Ŀ�ĵ�
                rigidbody2D.MovePosition(rushPosition);

                //���ó����ȴ����ʱ
                currentRushCD = rushCD;


                //�����޵�
                invulnerable = true;

                //���ó���޵еĵ���ʱ
                rushConter = rushDuration;

                //����ʱ
                rushConter -= Time.fixedDeltaTime;
                Debug.Log(rushConter);
                //�޵�ʱ��ȡ��
                if (rushConter <= 0)
                {
                    invulnerable = false;
                }
            }
        }
        else
        {
            //��ȴ��ʼ����ʱ
            currentRushCD -= Time.fixedDeltaTime;

        }


    }

    //����޵л���
    private void TriggerInvulnerable()
    {
        
    }

    //��ҵĹ�������
    private void Attack()
    {
        

        //����X����
        if (Input.GetKey(KeyCode.X))
        {
            isAttack = true;

            //���ӵ��з�����
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

    //�����������
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
