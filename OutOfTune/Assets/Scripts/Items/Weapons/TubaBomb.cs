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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")
            || collision.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
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
                float distToEnemy = explosionVector.magnitude;
                float dteRatioPlusModifier = distToEnemy / radius + 2.0f;
                h.Defend(dteRatioPlusModifier);
                explosionVector.Normalize();
                collider.attachedRigidbody.AddForce(explosionVector * bombForce, ForceMode2D.Impulse);
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
