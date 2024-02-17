using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PimbaaMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float patrolDuration;

    private Rigidbody2D rb;
    private float direction = 1;
    private Vector2 patrolLocation;
    private float patrolStartDirection;
    private bool isDead = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ChangeDirection());

        direction = transform.localScale.x;
        patrolLocation = transform.position;
        patrolStartDirection = transform.localScale.x;
    }
    void FixedUpdate()
    {
        if (!isDead)
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    IEnumerator ChangeDirection()
    {
        while(true)
        {
            yield return new WaitForSeconds(patrolDuration);
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            direction *= -1;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(patrolLocation, new Vector2(patrolLocation.x + (speed*patrolDuration*patrolStartDirection),patrolLocation.y));
    }

    public void DeathStatus()
    {
        isDead = true;
        rb.velocity = new Vector2 (0,0);
    }
}
