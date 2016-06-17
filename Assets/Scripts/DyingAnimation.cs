using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DyingAnimation : MonoBehaviour {

    public bool PlayerDestroyed { get; set; }
    public GameObject messagePanel;

    void Start()
    {
    }

	void DestroyAfterPlaying()
    {
        if (PlayerDestroyed)
        {
            Debug.Log("Game Over");
            GameObject panel = Instantiate(messagePanel);
            panel.GetComponent<RectTransform>().SetParent(GameObject.Find("Canvas").GetComponent<RectTransform>(), true);
            panel.GetComponent<RectTransform>().localPosition = Vector3.zero;
            panel.SetActive(true);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
