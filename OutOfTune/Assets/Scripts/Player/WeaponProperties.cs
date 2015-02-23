using UnityEngine;
using System.Collections;

public class WeaponProperties : MonoBehaviour {

    public GameObject projectile;
    public int weaponForce;

	// Use this for initialization
	void Start ()
    {
	
	}

    void Update()
    {

    }

    public void Fire(Vector2 direction)
    {
        Debug.Log(weaponForce);
        transform.LookAt(transform.position, direction);
        GameObject b = Instantiate(projectile) as GameObject;
        b.transform.position = GameObject.FindGameObjectWithTag("Reticle").transform.position;
        b.rigidbody2D.AddForce(transform.right * weaponForce, ForceMode2D.Impulse);
    }
}
