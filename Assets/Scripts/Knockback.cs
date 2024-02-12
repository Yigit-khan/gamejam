using Unity.VisualScripting;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _animator;

    [Header("Settings")]
    [SerializeField] private float knockbackForce = 10f;
    [SerializeField] private float knockbackDuration = 0.5f;

    private Rigidbody2D rb;
    private float knockbackTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (knockbackTimer > 0)
        {
            knockbackTimer -= Time.deltaTime;
            if (knockbackTimer <= 0)
            {
                rb.velocity = Vector2.zero;
            }
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            TakeDamage(new Vector2(1,0));
            _animator.SetTrigger("Hit");
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            TakeDamage(new Vector2(-1,0));
            _animator.SetTrigger("Hit");
        }
    }

    public void TakeDamage(Vector2 knockbackDirection)
    {
        rb.velocity = knockbackDirection * knockbackForce;

        knockbackTimer = knockbackDuration;
    }
}