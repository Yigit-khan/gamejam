using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteract : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Animator animator;

    private bool isDoorOpen = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            if(Input.GetKey(KeyCode.E) && !isDoorOpen)
            {
                animator.SetTrigger("Open");
                isDoorOpen = true;
                StartCoroutine(NextLevel());
            }
        }
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(.5f);
        gameManager.NextScene();
    }
}
