using UnityEngine;

public class Missile : MonoBehaviour {

    private Enemy enemy;
    private float spawnTime;

    public float damage = 5;
    public float speed = 15;
    public float livingTime = 3;

    
	void Start () {
        enemy = GetComponentInParent<Enemy>();
        spawnTime = Time.time;
	}
    
    void Update()
    {
        transform.position += new Vector3(speed, 0) * Time.deltaTime * enemy.Direction;
        DestroyAfter(livingTime);
    }

    void DestroyAfter(float flyingTime)
    {
        if (Time.time - spawnTime >= flyingTime)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.gameObject.GetComponent<Player>();
        if (player)
        {
            player.health -= damage;
            Destroy(gameObject);
        }
    }
}
