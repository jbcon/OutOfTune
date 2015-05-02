using UnityEngine;
using System.Collections;

public class NoteBullet : MonoBehaviour {

    //lifetime of bullet if it doesn't collide with anything
    public float lifetime = 0.1f;
    public float damage = 0.1f;

	// Use this for initialization
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
		int enemyLayer;
        int bossLayer;
		int statuelayer = LayerMask.NameToLayer("statue");
		//enemy layer
		if (gameObject.layer == 13){
			enemyLayer = LayerMask.NameToLayer("Player");
            bossLayer = LayerMask.NameToLayer("Boss");
		}else{
			enemyLayer = LayerMask.NameToLayer("Enemy");
            bossLayer = LayerMask.NameToLayer("Boss");
		}
        
        if (collision.gameObject.layer == enemyLayer || collision.gameObject.layer == bossLayer)
        {
            Debug.Log("HIT!");
            collision.gameObject.GetComponent<Health>().Defend(damage);
        }else if (collision.gameObject.layer == statuelayer){
			collision.gameObject.GetComponent<ReneeStatue>().OnReceiveDamage(1.0f);
			Debug.Log ("histting");
		}
        Destroy(gameObject);
    }

}
