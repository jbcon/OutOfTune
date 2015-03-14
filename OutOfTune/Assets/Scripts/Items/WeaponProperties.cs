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
        transform.LookAt(transform.position, direction);
        GameObject b = Instantiate(projectile) as GameObject;
        b.transform.position = GameObject.FindWithTag("Reticle").transform.position;
        b.transform.rotation = Quaternion.Euler(new Vector3(b.transform.rotation.x, 0, transform.rotation.z));
        b.GetComponent<Rigidbody2D>().AddForce(transform.right * weaponForce, ForceMode2D.Impulse);
    }
}
