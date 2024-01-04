using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float Speed = 2;
    [SerializeField]
    private float JumpPower = 2;

    private bool isJump = true;
    private int Jump = 1;

    Rigidbody2D rigid;
    SpriteRenderer sprite;
    Animator anim;
    
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isJump == true)
        {
            rigid.AddForce(Vector3.up * JumpPower, ForceMode2D.Impulse);
            anim.SetTrigger("Jump");
            StartCoroutine(JumpSystem());
        }
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        if (h > 0)
        {
            sprite.flipX = false;
            transform.Translate(new Vector3(Speed * Time.fixedDeltaTime, 0, 0));
        }
        else if (h < 0)
        {
            sprite.flipX = true;
            transform.Translate(new Vector3(-Speed * Time.fixedDeltaTime, 0, 0));
        }
        
    }

    IEnumerator JumpSystem()
    {
        isJump = false;
        yield return new WaitForSeconds(0.5f);
        if (Jump == 1)
        {
            Jump--;
            isJump = true;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            Jump = 1;
            isJump = true;
            anim.SetTrigger("Idle");
        }
    }
}
