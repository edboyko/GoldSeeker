using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    // Moving variables
    private float destinationRight;
    private float destinationLeft;
    private float direction = 1;
    private float currentSpeed;

    private Animator animator;

    // Property variables
    public float health = 100;

    public float oneWayLength = 4;
    public float defaultSpeed = 2;
    public float damage = 15;

    void Start()
    {
        currentSpeed = defaultSpeed;
        animator = GetComponent<Animator>();
        destinationRight = transform.position.x + oneWayLength;
        destinationLeft = transform.position.x - oneWayLength;
    }
    
    void Update()
    {
        if (transform.position.x >= destinationRight)
        {
            direction = -1;
        }
        if (transform.position.x <= destinationLeft)
        {
            direction = 1;
        }
        transform.position += new Vector3(currentSpeed * Time.deltaTime * direction, 0, 0);
        transform.localScale = new Vector3(direction, 1, 1);
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
            direction = direction * -1;
        }
    }
}
