using UnityEngine;
using System.Collections;

public class MeleeProperties : MonoBehaviour {

    public float swingInterval = 0.2f;
    public float swingCooldown = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        if (collision.gameObject.layer == enemyLayer)
        {
            Debug.Log("HIT!");
            collision.GetComponent<simpleAI>().Defend(2);
        }
    }

    public void Fire(Vector2 direction)
    {
        transform.LookAt(transform.position, direction);
        StartCoroutine(Swing());
    }

    public IEnumerator Swing()
    {
        Collider2D weaponCollider = GetComponent<Collider2D>();
        weaponCollider.enabled = true;
        Debug.Log("SWING");
        yield return new WaitForSeconds(swingInterval);
        weaponCollider.enabled = false;
        yield return new WaitForSeconds(swingCooldown);
    }
}
