using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D playerRigidBody;
    private Animator playerAnimator;
    private Player player;

    private float direction;
    public bool grounded = true;

    void Start ()
    {
        player = GetComponent<Player>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }    
	
	void Update () {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                direction = 1;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                direction = -1;
            }
            transform.localScale = new Vector3(direction, 1, 1);
            transform.position += Vector3.right * direction * player.speed * Time.deltaTime;
            if(player.isBat == false)
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
            if(player.isBat == false)
            {
                playerAnimator.SetBool("playerWalking", false);
            }
            else
            {
                playerAnimator.SetBool("batFlying", false);
            }
        }

        if (player.isBat == false)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)
            {
                playerRigidBody.velocity = Vector3.up * player.jumpForce;
                playerAnimator.SetTrigger("jumped");
                grounded = false;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                player.Attack(true);
                playerAnimator.SetBool("playerAttacking", true);
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                player.Attack(false);
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
            if(player.isBat == false)
            {
                player.TransformToBat(true, player.batGravityScale);
            }
            else
            {
                player.TransformToBat(false, 1);
            }
        }
        transform.rotation = Quaternion.identity;
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -5, 5), 0);
	}

    void OnCollisionEnter2D()
    {
        grounded = true;
    }


}
