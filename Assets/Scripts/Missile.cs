using UnityEngine;

public class Missile : MonoBehaviour {

    private float spawnTime;
    private float livingTime = 3;

    protected float missileDirection;

    protected float damage;
    protected float speed;
    
	virtual protected void Start ()
    {
        spawnTime = Time.time;
        transform.SetParent(GameObject.FindGameObjectWithTag("MissileContainer").transform);
    }
    
    virtual protected void Update()
    {
        DestroyAfter(livingTime);
        transform.position += new Vector3(speed, 0) * Time.deltaTime * missileDirection;
    }

    void DestroyAfter(float flyingTime)
    {
        if (Time.time - spawnTime >= flyingTime)
        {
            Destroy(gameObject);
        }
    }
}
