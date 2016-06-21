using UnityEngine;

public class PlayerMissile : Missile {

    private Player player;
    private PlayerController playerController;

    override protected void Start()
    {
        player = GetComponentInParent<Player>();
        playerController = GetComponentInParent<PlayerController>();

        damage = player.spellDamage;
        speed = player.missileSpeed;

        missileDirection = playerController.Direction;

        base.Start();
    }

    override protected void Update()
    {
        base.Update();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Enemy enemy = col.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.health -= damage;
            Destroy(gameObject);
        }
    }
}
