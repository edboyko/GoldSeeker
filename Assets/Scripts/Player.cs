using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float health, jumpForce, speed, playerDamage, batGravityScale;

    private float mana = 100;
    public float manaDrainRate = 20;
    public float manaRegen = 5;

    public GameObject dyingAnimation;
    
    private GameObject currentTarget;
    private Animator animator;
    private Slider manaSlider;
    private Slider healthSlider;    

    public bool isBat = false;

    public bool PlayerAttacking { get; set; }

    private Enemy victim;

    void Start()
    {
        animator = GetComponent<Animator>();
        manaSlider = GameObject.Find("ManaSlider").GetComponent<Slider>();
        healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
    }

    void Update()
    {
        if (isBat)
        {
            mana = mana - manaDrainRate * Time.deltaTime;
        }
        else if (!isBat && mana < 100)
        {
            mana = mana + manaRegen * Time.deltaTime;
        }

        manaSlider.value = mana;
        healthSlider.value = health;

        if (mana <= 0)
        {
            TransformToBat(false, 1);
        }
        if (PlayerAttacking)
        {
            victim.health -= playerDamage * Time.deltaTime;
        }
        DieIfHealthZero();
    }

    public void TransformToBat (bool trueOrFalse, float gravityScale)
    {
        animator.SetBool("isBat", trueOrFalse);
        isBat = trueOrFalse;
        GetComponent<Rigidbody2D>().gravityScale = gravityScale;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Enemy enemy = col.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            victim = enemy;
        }
    }
    void OnTriggerExit2D (Collider2D col)
    {
        Enemy enemy = col.gameObject.GetComponent<Enemy>();
        if(enemy)
        {
            victim = null;
        }
    }

    void DieIfHealthZero()
    {
        if(health <= 0)
        {
            GameObject animationInstance = Instantiate(dyingAnimation) as GameObject;
            animationInstance.GetComponent<DyingAnimation>().PlayerDestroyed = true;
            animationInstance.transform.position = transform.position;
            animationInstance.transform.localScale = transform.localScale;
            Destroy(gameObject);
        }
    }
}
