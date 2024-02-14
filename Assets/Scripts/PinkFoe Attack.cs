using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkFoeAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform AttackPoint;
    [SerializeField] private float AttackRangeX;
    [SerializeField] private float AttackRangeY;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private int damage;

    private void Start()
    {
        StartCoroutine(RepeatAttack());
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(AttackPoint.position - new Vector3(0,0.2f), new Vector2(AttackRangeX, AttackRangeY), enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            if(enemy.gameObject.transform.tag == "Player")
                enemy.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }
    void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;
        Gizmos.DrawWireCube(AttackPoint.position - new Vector3(0,0.225f), new Vector2(AttackRangeX,AttackRangeY));
    }

    IEnumerator RepeatAttack()
    {
        while (true)
        {
            Attack();
            yield return new WaitForSeconds(2f);
        }
    }
}
