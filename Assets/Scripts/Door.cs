using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
    
    void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.gameObject.GetComponent<Player>();
        if (player)
        {
            GetComponent<Animator>().SetBool("doorOpened", true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        Player player = col.gameObject.GetComponent<Player>();
        if (player)
        {
            GetComponent<Animator>().SetBool("doorOpened", false);
        }
    }

}
