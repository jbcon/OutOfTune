using UnityEngine;
using System.Collections;

public class NoteBullet : MonoBehaviour {

    //lifetime of bullet if it doesn't collide with anything
    public float lifetime = 0.1f;
    public int damage = 2;

	// Use this for initialization
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        if (collision.gameObject.layer == enemyLayer)
        {
            Debug.Log("HIT!");
            collision.gameObject.GetComponent<Health>().Defend(damage);
        }
        Destroy(gameObject);
    }

}
