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
        gameObject.GetComponent<Rigidbody2D>().velocity = (new Vector2(moveSpeed * Time.deltaTime * direction,0));
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
}
