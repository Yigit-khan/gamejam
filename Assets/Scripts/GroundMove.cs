using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMove : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveDuration;
    [SerializeField] private float direction = 1;

    private void Start()
    {
        StartCoroutine(ChangeDirection());
    }

    private void Update()
    {
        transform.Translate(new Vector2(moveSpeed * Time.deltaTime * direction,0));
    }

    IEnumerator ChangeDirection()
    {
        while(true)
        {
            yield return new WaitForSeconds(moveDuration);
            {
                direction *= -1;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            transform.DetachChildren();
        }
    }
}
