using UnityEngine;

public class EnemyMage : Enemy {
    
    public GameObject missile;

    override protected void Start () {
        base.Start();
	}

    override protected void Update ()
    {
        base.Update();
    }

    override protected void FindPlayer()
    {
        if (PlayerWithinRange()[0] && PlayerWithinRange()[1])
        {
            CurrentSpeed = 0;
            playerIsNear = true;
            if (player.transform.position.x > transform.position.x)
            {
                Direction = 1;
            }
            else
            {
                Direction = -1;
            }
        }
        else
        {
            playerIsNear = false;
            CurrentSpeed = defaultSpeed;
        }

        if (playerIsNear)
        {
            GetComponent<Animator>().SetBool("casting", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("casting", false);
        }
    }

    public void CastFireBalls ()
    {
        GameObject ball = Instantiate(missile, transform.position, Quaternion.identity) as GameObject;
        ball.transform.SetParent(transform);
    }

    override protected void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);
    }
}
