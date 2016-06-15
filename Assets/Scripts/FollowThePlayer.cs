using UnityEngine;

public class FollowThePlayer : MonoBehaviour {

    private Player player;
    
	void Start () {
        player = GameObject.FindObjectOfType<Player>();
	}
	
	void Update () {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
	}
}
