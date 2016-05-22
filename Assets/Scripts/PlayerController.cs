using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private bool isJumping = false;
    private Rigidbody2D playerRigidBody;
    private Animator playerAnimator;
    private Player player;
    private CircleCollider2D attackCollider;

    void Start ()
    {
        player = GetComponent<Player>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        attackCollider = GetComponent<CircleCollider2D>();
    }    
	
	void Update () {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * player.speed * Time.deltaTime;
            playerAnimator.SetBool("playerWalking", true);
            transform.localScale = new Vector3 (1,1,1);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            playerAnimator.SetBool("playerWalking", false);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * player.speed * Time.deltaTime;
            playerAnimator.SetBool("playerWalking", true);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            playerAnimator.SetBool("playerWalking", false);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping)
        {
            playerRigidBody.velocity = Vector3.up * player.jumpForce;
            playerAnimator.SetTrigger("jumped");
            isJumping = true;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            playerAnimator.SetBool("playerAttacking", true);
            attackCollider.enabled = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            playerAnimator.SetBool("playerAttacking", false);
            attackCollider.enabled = false;
        }
        transform.rotation = Quaternion.identity;
	}

    void OnCollisionEnter2D()
    {
        isJumping = false;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        Enemy enemy = col.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.health -= player.damage * Time.deltaTime;
        }
    }
}
