﻿using UnityEngine;
using System.Collections;

public class StaffBullet : MonoBehaviour {

    //lifetime of bullet if it doesn't collide with anything
    public float lifetime = 0.1f;
    public float damage = 2;
    public float force = 10f;

    private Rigidbody2D rb;

	// Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
		int statuelayer = LayerMask.NameToLayer("statue");
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int bossLayer = LayerMask.NameToLayer("Boss");
        int handLayer = LayerMask.NameToLayer("Hand");
        if (collision.gameObject.layer == enemyLayer || collision.gameObject.layer == bossLayer)
        {
            Debug.Log("HIT!");
            collision.gameObject.GetComponent<Health>().Defend(damage);
            //impart force upon the enemy
            if (collision.attachedRigidbody)
            {
                collision.attachedRigidbody.AddForce(transform.rotation * Vector2.right * force, ForceMode2D.Impulse);
			}

		}else if (collision.gameObject.layer == statuelayer){
			collision.gameObject.GetComponent<ReneeStatue>().OnReceiveDamage(1.0f);
        }
        else if (collision.gameObject.layer == handLayer)
        {
            collision.gameObject.GetComponent<BossHandDamage>().InflictDamage(damage);
        }
    }

}
