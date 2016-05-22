using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float health, jumpForce, speed, playerDamage;

    private CircleCollider2D attackCollider;
    private bool playerAttacking = false;
    private GameObject currentTarget;

    void Start()
    {
        attackCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {

    }

    public void Attack (bool isAttacking)
    {
        attackCollider.enabled = isAttacking;
        playerAttacking = isAttacking;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject victim = col.gameObject;
        Enemy enemy = victim.GetComponent<Enemy>();
        if (enemy && playerAttacking)
        {
            Attack(victim);
        }
    }
    void OnTriggerExit2D (Collider2D col)
    {
        currentTarget = null;
    }

    void Attack(GameObject target)
    {
        currentTarget = target;
    }

    void DealDamage()
    {
        if (currentTarget)
        {
            currentTarget.GetComponent<Enemy>().health -= playerDamage;
        }
    }
}
