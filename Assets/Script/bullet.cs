using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    //子弹速度
    private float speedOfBullet = 20.0f;
    Rigidbody2D rigBullet;
    Collider2D collider2D;

    private void Awake()
    {
        //获取刚体
        rigBullet = GetComponent<Rigidbody2D>();
        //获取碰撞器
        collider2D = GetComponent<Collider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //子弹移动
        rigBullet.velocity = transform.right * speedOfBullet;
        //一定时间后摧毁物体
        Destroy(gameObject, 1);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

}
