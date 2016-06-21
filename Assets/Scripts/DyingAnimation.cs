using UnityEngine;

public class DyingAnimation : MonoBehaviour {

    public bool PlayerDestroyed { get; set; }
    public GameObject messagePanel;

	void DestroyAfterPlaying()
    {
        if (PlayerDestroyed)
        {
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
