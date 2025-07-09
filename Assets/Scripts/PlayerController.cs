using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] Transform pointCheck;
    [SerializeField] float radiusCheck;
    float xInput;
    public LayerMask groundLayer;
    bool isGrounded;
    string currentAnnim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JUMP();
        }
    }
    private void FixedUpdate()
    {
        // ground
        isGrounded = CheckGround();
        Debug.Log(CheckGround());
        // move
        Movement();
        
    }

    private void JUMP()
    {
        rb.velocity = Vector2.up * jumpForce;
        changeAnim("jump");
        //rb.AddForce(Vector2.up * jumpForce);
    }

    private void Movement()
    {
        if (Mathf.Abs(xInput) > 0.01f)
        {
            rb.velocity = new Vector2(xInput * moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
            changeAnim("run");
            Flip();

        }
        else if (isGrounded)
        {
            rb.velocity = Vector2.zero;
            changeAnim("idle");

            //rb.velocity = new Vector2(xInput * moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
        }
    }

    private void Flip()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, xInput > 0 ? 0 : 180, 0));
    }

    private bool CheckGround()
    {
        Debug.DrawLine(pointCheck.position, pointCheck.position + Vector3.down * 1.5f, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(pointCheck.position, Vector2.down, 1.5f, groundLayer);
        return hit.collider != null;
    }
    void changeAnim(string animname)
    {
        if(currentAnnim != animname)
        {
            anim.ResetTrigger(animname);
            currentAnnim = animname;
            anim.SetTrigger(currentAnnim);
        }
    }

    

}
