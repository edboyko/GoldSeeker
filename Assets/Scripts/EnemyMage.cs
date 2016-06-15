using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMage : MonoBehaviour {

    private bool playerIsNear;

    private Player player;
    private Enemy enemy;

    public GameObject missile;

    public float xAttackThreshold = 4;
    public float yAttackThreshold = 1;

    void Start () {
        player = GameObject.FindObjectOfType<Player>();
        enemy = GetComponent<Enemy>();
	}
	
	void Update () {
        if (PlayerWithinRange()[0] && PlayerWithinRange()[1])
        {
            enemy.CurrentSpeed = 0;
            playerIsNear = true;
            if(player.transform.position.x > transform.position.x)
            {
                enemy.Direction = 1;
            }
            else
            {
                enemy.Direction = -1;
            }
        }
        else
        {
            playerIsNear = false;
            enemy.CurrentSpeed = enemy.defaultSpeed;
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

    bool[] PlayerWithinRange()
    {
        bool xDistance = Mathf.Abs(player.transform.position.x - transform.position.x) <= xAttackThreshold;
        bool yDistance = Mathf.Abs(player.transform.position.y - transform.position.y) <= yAttackThreshold;
        return new bool[2] {xDistance, yDistance};
    }    
}
