using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    Animator animator;
    public float health = 10;

	// Use this for initialization
	void Start () {
        animator = gameObject.GetComponentInChildren<Animator>();
	}

    public void Defend(float dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            StartCoroutine("Die");
        }
    }

    private IEnumerator Die()
    {
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
}
