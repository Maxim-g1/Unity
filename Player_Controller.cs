using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed;
    public float jumpHeight;
    public Transform isGround;
    bool isGrounded;
    Animator anim;
    public int maxHP = 3;
    int curHP;
    
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        curHP = maxHP;
    }

   
    void Update()
    {
        CheckGround();
        if (Input.GetAxis("Horizontal") == 0 && isGrounded)
            anim.SetInteger("State", 1);
        else
        {
            Flip();
            if (isGrounded)
                anim.SetInteger("State", 2);
        }
            
       
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal")*moveSpeed, rb.velocity.y);
        if (Input.GetKey(KeyCode.Space))
            rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
    }

    void Flip()
    {
        if (Input.GetAxis("Horizontal") > 0)
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        if (Input.GetAxis("Horizontal") < 0)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
    }
    void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(isGround.position, 0.2f);
        isGrounded = colliders.Length > 1;

        if (!isGrounded)
            anim.SetInteger("State", 3);
    }

    public void RecountHP(int deltaHP)
    {
        curHP = curHP + deltaHP;
        if (curHP <= 0)
            Debug.Log("ты помер");
    }
}
