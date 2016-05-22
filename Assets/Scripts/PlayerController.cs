using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D playerRigidBody;
    private Animator playerAnimator;
    private Player player;

    private float direction;
    private bool grounded = true;

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
            playerAnimator.SetBool("playerWalking", true);
            transform.localScale = new Vector3(direction, 1, 1);
            transform.position += Vector3.right * direction * player.speed * Time.deltaTime;
        }
        else if(Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            playerAnimator.SetBool("playerWalking", false);
        }

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
        transform.rotation = Quaternion.identity;
	}

    void OnCollisionEnter2D()
    {
        grounded = true;
    }


}
