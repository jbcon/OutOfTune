using UnityEngine;
using System.Collections;

public class WeaponProperties : MonoBehaviour {

    public GameObject projectile;
    public int weaponForce;

	// Use this for initialization
	void Start () {
	
	}

    public void Fire(Vector2 direction)
    {
        GameObject b = Instantiate(projectile) as GameObject;
        b.transform.position = transform.GetChild(0).position;
        b.rigidbody2D.AddForce(direction * weaponForce, ForceMode2D.Impulse);
        Debug.Log(direction.magnitude * weaponForce);
    }
}
