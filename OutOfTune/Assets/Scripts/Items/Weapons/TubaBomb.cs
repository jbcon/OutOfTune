using UnityEngine;
using System.Collections;

public class TubaBomb : MonoBehaviour {

    public float lifetime = 3.0f;
    public float damage = 3.0f;
    public float radius = 10f;
    public float bombForce = 10f;
    public ParticleSystem pSystem;

	// Use this for initialization
	void Start () {
        StartCoroutine("Tick");
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
		int statuelayer = LayerMask.NameToLayer("statue");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")
            || collision.gameObject.layer == LayerMask.NameToLayer("Boss")
            || collision.gameObject.layer == LayerMask.NameToLayer("Hand"))
        {
            Explode();
        }
		if (collision.gameObject.layer == statuelayer){
			collision.gameObject.GetComponent<ReneeStatue>().OnReceiveDamage(1.0f);
			Debug.Log ("histting");
			Explode();
		}
    }
    void Explode()
    {
        Collider2D[] explosionVictims = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D collider in explosionVictims)
        {
            Health h = collider.gameObject.GetComponent<Health>();
            if (h)
            {
                Vector2 explosionVector = collider.gameObject.transform.position - transform.position;
                //float distToEnemy = explosionVector.magnitude;
                //float dteRatioPlusModifier = 2.0f;
                h.Defend(damage);
                explosionVector.Normalize();
                collider.attachedRigidbody.AddForce(explosionVector * bombForce, ForceMode2D.Impulse);
            }
            BossHandDamage a = collider.gameObject.GetComponent<BossHandDamage>();
            if (a)
            {
                a.InflictDamage(damage);
            }
        }
        GetComponent<SpriteRenderer>().enabled = false;
        pSystem = Instantiate(pSystem, gameObject.transform.position, new Quaternion()) as ParticleSystem;
        Destroy(gameObject);
    }

    IEnumerator Tick()
    {
        yield return new WaitForSeconds(lifetime);
        Explode();
    }
}
