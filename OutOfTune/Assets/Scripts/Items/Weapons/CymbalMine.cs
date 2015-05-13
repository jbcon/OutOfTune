using UnityEngine;
using System.Collections;

public class CymbalMine : MonoBehaviour
{

    public float damage = 5f;
    public float launchForce = 200f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int groundLayer = LayerMask.NameToLayer("Ground");
		int statuelayer = LayerMask.NameToLayer("statue");
        int handLayer = LayerMask.NameToLayer("Hand");
        if (collision.gameObject.layer == enemyLayer)
        {
            //Debug.Log("HIT!");
            collision.gameObject.GetComponent<Health>().Defend(damage);
            collision.rigidbody.AddForce(Vector2.up * launchForce, ForceMode2D.Impulse);
            AudioSource source = GetComponent<AudioSource>();
            source.PlayOneShot(source.clip);

            //destroy all the components so the clip finishes playing
            Destroy(gameObject.GetComponent<SpriteRenderer>());
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            Destroy(gameObject, source.clip.length);

        }
        if (collision.gameObject.layer == groundLayer)
        {
            //Debug.Log("Mine placed on ground");
        }
		if (collision.gameObject.layer == statuelayer){
			collision.gameObject.GetComponent<ReneeStatue>().OnReceiveDamage(1.0f);
		}
        if (collision.gameObject.layer == handLayer)
        {
            collision.gameObject.GetComponent<BossHandDamage>().InflictDamage(damage);
            AudioSource source = GetComponent<AudioSource>();
            source.PlayOneShot(source.clip);

            //destroy all the components so the clip finishes playing
            Destroy(gameObject.GetComponent<SpriteRenderer>());
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            Destroy(gameObject, source.clip.length);
        }
    }

}
