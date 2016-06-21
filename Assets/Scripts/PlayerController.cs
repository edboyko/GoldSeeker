using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D playerRigidBody;
    private Animator playerAnimator;
    private Player player;

    public float Direction { get; set; }
    public bool grounded = true;

    void Start ()
    {
        Direction = 1;
        player = GetComponent<Player>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }    
	
	void Update () {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Direction = 1;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                Direction = -1;
            }
            transform.localScale = new Vector3(Direction, 1, 1);
            transform.position += Vector3.right * Direction * player.speed * Time.deltaTime;
            if(!player.isBat)
            {
                playerAnimator.SetBool("playerWalking", true);
            }
            else
            {
                playerAnimator.SetBool("batFlying", true);
            }
        }
        else if(Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if(!player.isBat)
            {
                playerAnimator.SetBool("playerWalking", false);
            }
            else
            {
                playerAnimator.SetBool("batFlying", false);
            }
        }

        if (!player.isBat)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)
            {
                playerRigidBody.velocity = Vector3.up * player.jumpForce;
                playerAnimator.SetTrigger("jumped");
                grounded = false;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                player.PlayerAttacking = true;
                playerAnimator.SetBool("playerAttacking", true);
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                player.PlayerAttacking = false;
                playerAnimator.SetBool("playerAttacking", false);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                playerAnimator.SetBool("batFlying", true);
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    transform.position += Vector3.up * player.speed * Time.deltaTime;
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    transform.position += Vector3.down * player.speed * Time.deltaTime;
                }
            }
            else if(Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                playerAnimator.SetBool("batFlying", false);
            }
        }

        if (Input.GetKeyDown(KeyCode.B) && grounded)
        {
            if(!player.isBat)
            {
                player.TransformToBat(true, player.batGravityScale);
            }
            else
            {
                player.TransformToBat(false, 1);
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            player.CastProjectile();
        }

        transform.rotation = Quaternion.identity;
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -30, 5), 0);
	}

    void OnCollisionEnter2D()
    {
        grounded = true;
    }
}
