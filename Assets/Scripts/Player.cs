using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {

    public float health, jumpForce, speed, playerDamage, batGravityScale;

    private float mana = 100;
    public float manaDrainRate = 20;

    private CircleCollider2D attackCollider;
    private GameObject currentTarget;
    private Animator animator;
    private Slider manaSlider;

    private bool playerAttacking = false;

    public bool isBat = false;    

    void Start()
    {
        attackCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        manaSlider = GameObject.Find("ManaSlider").GetComponent<Slider>();
    }

    void Update()
    {
        if (isBat)
        {
            mana = mana - manaDrainRate * Time.deltaTime;
        }
        manaSlider.value = mana;
        if(mana <= 0)
        {
            TransformToBat(false, 1);
        }
    }

    public void TransformToBat (bool trueOrFalse, float gravityScale)
    {
        animator.SetBool("isBat", trueOrFalse);
        isBat = trueOrFalse;
        GetComponent<Rigidbody2D>().gravityScale = gravityScale;
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
