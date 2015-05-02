using UnityEngine;
using System.Collections;

public class Maestro : MonoBehaviour {

    private Health health;
    private Animator animator;
    private bool stunned = false;

	// Use this for initialization
	void Start () {
        health = GetComponent<Health>();
        animator = GetComponentInChildren<Animator>();
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        //allows the boss to be injured by flying hi-hats
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            health.Defend(20);
            collision.gameObject.GetComponent<Health>().health = 0;
        }
    }

    void OnReceiveDamage(float dmg)
    {
        animator.SetTrigger("Hurt");
        health.health -= dmg;
        if (health.health > 0)
        {
            StartCoroutine("Stun");
        }
    }


    IEnumerator Stun()
    {
        foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
            sr.material.color = new Vector4(0.5f, 0.0f, 0.0f, 1.0f);
        stunned = true;
        yield return new WaitForSeconds(1.0f);
        foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
            sr.material.color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        stunned = false;
    }
}
