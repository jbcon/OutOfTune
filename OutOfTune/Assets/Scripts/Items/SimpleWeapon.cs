using UnityEngine;
using System.Collections;

public enum WeaponType { FullAuto, SemiAuto };

public class Weapon
{
    public WeaponType weaponType;
    public GameObject projectile;
    public float delay;
    public float weaponForce;
}


public class SimpleWeapon : MonoBehaviour {

    public WeaponType weaponType;
    public GameObject projectile;
    public float delay = 1f;
    public int weaponForce;
    public float bulletSpread = 0.05f;

    private bool canFire;

	// Use this for initialization
	void Start ()
    {
        canFire = true;
	}

    void Update()
    {

    }

    public void Fire(Vector2 direction, bool spin)
    {
        transform.LookAt(transform.position, direction);
        if (canFire)
        {
            GameObject b = Instantiate(projectile) as GameObject;
            b.transform.position = GameObject.FindWithTag("Reticle").transform.position;
            b.transform.rotation = Quaternion.Euler(new Vector3(b.transform.rotation.x, 0, transform.rotation.z));
            float spreadModifier = Random.Range(-weaponSpread, weaponSpread);
            Vector3 spreadVector = new Vector3(0.0f, spreadModifier, 0.0f);
            b.GetComponent<Rigidbody2D>().AddForce((transform.right + spreadVector)* weaponForce, ForceMode2D.Impulse);
            if (spin)
                b.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-100, 100));       //put a spin on it so it looks nice
            StartCoroutine(Cooldown());
        }

    }

    IEnumerator Cooldown()
    {
        canFire = false;
        yield return new WaitForSeconds(delay);
        canFire = true;
    }
}
