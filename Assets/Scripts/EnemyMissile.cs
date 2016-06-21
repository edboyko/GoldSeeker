using UnityEngine;

public class EnemyMissile : Missile {

    private EnemyMage enemy;
    
    override protected void Start ()
    {
        enemy = GetComponentInParent<EnemyMage>();

        damage = enemy.damage;
        speed = enemy.missileSpeed;

        missileDirection = enemy.Direction;

        base.Start();
    }
	
	override protected void Update ()
    {
        base.Update();
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
