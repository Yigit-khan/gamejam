using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform groundCheck_1;
    [SerializeField] private Transform groundCheck_2;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;

    [Header("Movement Settings")]
    [SerializeField] private float speed = 8f; 
    [SerializeField] private float jumpingForce = 16f;

    [Header("Knockback Settings")]
    [SerializeField] private float KBForce;
    [SerializeField] private float KBCounter;
    [SerializeField] private float KBTotalTime;

    private bool KnockFromRight;
    private Rigidbody2D rb;
    private float horizontalInput;
    private bool isFacingRight = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && (IsGrounded_1() || IsGrounded_2()))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingForce);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        animator.SetFloat("Horizontal Speed", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("Vertical Speed", rb.velocity.y);
        animator.SetBool("Is Grounded", (IsGrounded_1() || IsGrounded_2()));

        Flip();
    }

    private void FixedUpdate()
    {
        
        if(KBCounter <= 0)
        {
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        }
        else
        {
            if(KnockFromRight == true)
            {
                rb.velocity = new Vector2(-KBForce, KBForce);
            }
            if(KnockFromRight == false)
            {
                rb.velocity = new Vector2(KBForce, KBForce);
            }

            KBCounter -= Time.deltaTime;
        }
    }

    private bool IsGrounded_1()
    {
        return Physics2D.OverlapCircle(groundCheck_1.position, 0.2f, groundLayer);
    }

    private bool IsGrounded_2()
    {
        return Physics2D.OverlapCircle(groundCheck_2.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void Knockback()
    {
        KBCounter = KBTotalTime;
    }

    public void KnockbackDirection(bool direction)
    {
        KnockFromRight = direction;
    }
}
