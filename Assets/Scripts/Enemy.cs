using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    // Moving variables
    private float destinationRight;
    private float destinationLeft;

    public float Direction { get; set; }
    public float CurrentSpeed { get; set; }

    private Animator animator;

    // Property variables
    public float health = 100;

    public float oneWayLength = 4;
    public float defaultSpeed = 2;
    public float damage = 15;

    void Start()
    {
        CurrentSpeed = defaultSpeed;
        animator = GetComponent<Animator>();
        destinationRight = transform.position.x + oneWayLength;
        destinationLeft = transform.position.x - oneWayLength;
        Direction = 1;
    }
    
    void Update()
    {
        if (transform.position.x >= destinationRight)
        {
            Direction = -1;
        }
        if (transform.position.x <= destinationLeft)
        {
            Direction = 1;
        }
        transform.position += new Vector3(CurrentSpeed * Time.deltaTime * Direction, 0, 0);
        transform.localScale = new Vector3(Direction, 1, 1);
        transform.rotation = Quaternion.identity;
        DieIfHealthZero();
    }

    void DieIfHealthZero()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        Player player = col.gameObject.GetComponent<Player>();
        if (player)
        {
            player.health -= damage * Time.deltaTime;
            animator.SetBool("attacking", true);
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        BoxCollider2D playerCol = col.gameObject.GetComponent<BoxCollider2D>();
        Player player = col.gameObject.GetComponent<Player>();
        if (player && playerCol)
        {
            animator.SetBool("attacking", false);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject == GameObject.FindGameObjectWithTag("Wall"))
        {
            Direction = Direction * -1;
        }
    }
}
