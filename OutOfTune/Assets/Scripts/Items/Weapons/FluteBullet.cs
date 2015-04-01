using UnityEngine;
using System.Collections;

public class FluteBullet : MonoBehaviour {

    public float lifetime = 1.0f;
    public float damage = 4f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    //ricochets off walls and enemies, just dies after time
    void OnCollisionEnter2D(Collision2D collision)
    {
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        if (collision.gameObject.layer == enemyLayer)
        {
            collision.gameObject.GetComponent<Health>().Defend(damage);
        }
        //Destroy(gameObject);
    }
}
