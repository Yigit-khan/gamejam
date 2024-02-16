using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("PlayerHeal");
            collision.gameObject.GetComponent<Health>().TakeHeal();

            Destroy(gameObject);
        }
    }
}
