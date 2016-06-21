using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    protected bool playerIsNear;

    public float xAttackThreshold = 1.5f;
    public float yAttackThreshold = 1;

    // Moving variables
    protected float destinationRight;
    protected float destinationLeft;

    public float Direction { get; set; }
    public float CurrentSpeed { get; set; }

    // Component variables
    protected Animator animator;
    protected Slider healthSlider;

    // Property variables
    public float health = 100;
    public float oneWayLength = 4;
    public float defaultSpeed = 2;
    public float damage = 15;

    public GameObject dyingAnimation;

    protected Player target;

    protected Player player;
    
    protected virtual void Start()
    {
        player = GameObject.FindObjectOfType<Player>();

        animator = GetComponent<Animator>();
        healthSlider = GetComponentInChildren<Slider>();

        CurrentSpeed = defaultSpeed;
        Direction = 1;

        destinationRight = transform.position.x + oneWayLength;
        destinationLeft = transform.position.x - oneWayLength;
    }
    
    virtual protected void Update()
    {
        healthSlider.value = health;

        if (transform.position.x >= destinationRight)
        {
            Direction = -1;
        }
        if (transform.position.x <= destinationLeft)
        {
            Direction = 1;
        }

        EnemyMove();

        FindPlayer();

        DieIfHealthZero();
    }

    void EnemyMove()
    {
        transform.position += new Vector3(CurrentSpeed * Time.deltaTime * Direction, 0, 0);
        transform.localScale = new Vector3(Direction, 1, 1);
        transform.rotation = Quaternion.identity;
    }

    protected virtual void FindPlayer()
    {
        if (PlayerWithinRange()[0] && PlayerWithinRange()[1])
        {
            if (player.transform.position.x > transform.position.x)
            {
                Direction = 1;
            }
            else
            {
                Direction = -1;
            }
        }
    }

    protected bool[] PlayerWithinRange()
    {
        if (player)
        {
            bool xDistance = Mathf.Abs(player.transform.position.x - transform.position.x) <= xAttackThreshold;
            bool yDistance = Mathf.Abs(player.transform.position.y - transform.position.y) <= yAttackThreshold;
            return new bool[2] { xDistance, yDistance };
        }
        else
        {
            return new bool[2] { false, false };
        }
    }

    void DieIfHealthZero()
    {
        if (health <= 0)
        {
            GameObject animationInstance = Instantiate(dyingAnimation) as GameObject;
            animationInstance.transform.position = transform.position;
            animationInstance.transform.localScale = transform.localScale;
            Destroy(gameObject);
        }
    }

    public void EnemyDealDamage()
    {
        if (target)
        {
            target.health -= damage;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        Player player = col.gameObject.GetComponent<Player>();
        if (player)
        {
            target = null;
            animator.SetBool("attacking", false);
            CurrentSpeed = defaultSpeed;
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.gameObject.GetComponent<Player>();
        if (col.gameObject == GameObject.FindGameObjectWithTag("Wall"))
        {
            Direction = Direction * -1;
        }
        if (player)
        {
            target = player;
            CurrentSpeed = 0;
            animator.SetBool("attacking", true);
        }
    }
}
