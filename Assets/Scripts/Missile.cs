using UnityEngine;

public class Missile : MonoBehaviour {

    private Enemy enemy;
    private float spawnTime;

    public float damage = 5;
    public float speed = 15;

    
	void Start () {
        enemy = GetComponentInParent<Enemy>();
        spawnTime = Time.time;
	}
    
    void Update()
    {
        transform.position += new Vector3(speed, 0) * Time.deltaTime * enemy.Direction;
        if(Time.time - spawnTime >= 3)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.gameObject.GetComponent<Player>();
        if (player)
        {
            Destroy(gameObject);
            player.health -= damage;
        }
    }
}
