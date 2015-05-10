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
		int statuelayer = LayerMask.NameToLayer("statue");
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int bossLayer = LayerMask.NameToLayer("Boss");
        if (collision.gameObject.layer == enemyLayer || collision.gameObject.layer == bossLayer)
        {
            collision.gameObject.GetComponent<Health>().Defend(damage);
		}else if (collision.gameObject.layer == statuelayer){
			collision.gameObject.GetComponent<ReneeStatue>().OnReceiveDamage(1.0f);
		}
        //Destroy(gameObject);
    }
}
