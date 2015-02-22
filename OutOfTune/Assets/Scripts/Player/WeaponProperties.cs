using UnityEngine;
using System.Collections;

public class WeaponProperties : MonoBehaviour {

    public GameObject projectile;
    public float weaponForce;

	// Use this for initialization
	void Start () {
	
	}

    public void Fire(Vector2 direction)
    {
        GameObject b = Instantiate(projectile) as GameObject;
        b.transform.SetParent(transform);
        b.rigidbody2D.AddForce(direction*weaponForce, ForceMode2D.Impulse);
        b.SetActive(true);
		b.transform.localScale = new Vector3 (.5f, .5f, .5f);
    }
}
