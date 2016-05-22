using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    // Moving variables
    private float destinationRight;
    private float destinationLeft;
    private float direction = 1;

    // Property variables
    public float health = 100;

    public float oneWayLength = 4;
    public float speed = 2;

    void Start()
    {
        destinationRight = transform.position.x + oneWayLength;
        destinationLeft = transform.position.x - oneWayLength;
    }
    
    void Update()
    {
        if (transform.position.x >= destinationRight)
        {
            direction = -1;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (transform.position.x <= destinationLeft)
        {
            direction = 1;
            transform.localScale = new Vector3(1, 1, 1);
        }
        transform.position += new Vector3(speed * Time.deltaTime * direction, 0, 0);
        transform.rotation = Quaternion.identity;
        Death();
    }

    void Death()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
