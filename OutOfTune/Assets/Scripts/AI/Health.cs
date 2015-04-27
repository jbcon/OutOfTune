using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    Animator animator;
    public float health = 10;
    public ParticleSystem particles;

	// Use this for initialization
	void Start () {
        animator = gameObject.GetComponentInChildren<Animator>();
	}

    public void Defend(float dmg)
    {
        gameObject.SendMessage("OnReceiveDamage", dmg);
    }

    void Update()
    {
        if (health <= 0)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
			if (gameObject.GetComponent<Rigidbody2D>().fixedAngle == true){
				gameObject.GetComponent<Rigidbody2D>().fixedAngle = false;
			}
            StartCoroutine("Die");
        }
    }
    private IEnumerator Die()
    {
        if (animator)
        {
            animator.SetTrigger("Die");
            yield return new WaitForSeconds(1.0f);
        }
        particles = Instantiate(particles, gameObject.transform.position, new Quaternion()) as ParticleSystem;
        Destroy(gameObject);
    }
}
