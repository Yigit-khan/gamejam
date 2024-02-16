using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform groundCheck_1;
    [SerializeField] private Transform groundCheck_2;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject temporaryObjects;

    [Header("Movement Settings")]
    [SerializeField] private float speed = 8f; 
    [SerializeField] private float jumpingForce = 16f;

    [Header("Knockback Settings")]
    [SerializeField] private float KBForce;
    [SerializeField] private float KBCounter;
    [SerializeField] private float KBTotalTime;

    [Header("Dash Settings")]
    [SerializeField] private bool dashIsEnabled;
    [SerializeField] private float dashingForce;
    [SerializeField] private float dashingTime;
    [SerializeField] private float dashingCooldown;

    [Header("Shooting Settings")]
    [SerializeField] private bool shootingEnabled;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float fireRate;

    private int maxAmmo = 9; //Shoot
    private int ammo = 0; //Shoot
    private float nextTimeToFire; //Shoot
    private float shootTime; //Shoot
    private bool canDash = true; //Dash
    private bool isDashing; //Dash
    private bool KnockFromRight; //Knockback
    private Rigidbody2D rb; //Rigidbody
    private float horizontalInput; //Movement
    private bool isFacingRight = true; //Flipping

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        nextTimeToFire = 1 / fireRate;
        shootTime = 0;
    }
    void Update()
    {
        shootTime += Time.deltaTime;

        horizontalInput = Input.GetAxisRaw("Horizontal");

        if(!isDashing)
        {
            JumpAndDash();

            if (Input.GetButtonDown("Fire1") && shootingEnabled && shootTime >= nextTimeToFire)
            {
                Shoot();
                shootTime = 0;
            }
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
            if (isDashing)
                return;
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

    void JumpAndDash()
    {
        //Jump
        if (Input.GetButtonDown("Jump") && (IsGrounded_1() || IsGrounded_2()))
        {
            FindObjectOfType<AudioManager>().Play("PlayerJump");
            rb.velocity = new Vector2(rb.velocity.x, jumpingForce);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        //Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && dashIsEnabled)
        {

            StartCoroutine(Dash());
        }
    }

    void Shoot()
    {
        animator.SetTrigger("Shoot");
        FindObjectOfType<AudioManager>().Play("PlayerShoot");
        GameObject newBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        newBullet.transform.SetParent(temporaryObjects.transform);
        newBullet.transform.localScale = new Vector2(transform.localScale.x, newBullet.transform.localScale.y);
        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed * transform.localScale.x, 0);
    }

    public void Knockback()
    {
        KBCounter = KBTotalTime;
    }

    public void KnockbackDirection(bool direction)
    {
        KnockFromRight = direction;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingForce, 0f);
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        trailRenderer.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
