using UnityEngine;
using System.Collections;

public class StaffBullet : MonoBehaviour {

    //lifetime of bullet if it doesn't collide with anything
    public float lifetime = 0.1f;
    public int damage = 2;

	// Use this for initialization
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        if (collision.gameObject.layer == enemyLayer)
        {
            Debug.Log("HIT!");
            collision.gameObject.GetComponent<Health>().Defend(damage);
        }
    }

}
