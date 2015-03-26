using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum WeaponType { FullAuto, SemiAuto };

public class Weapon
{
    public WeaponType weaponType;
    public GameObject projectile;
    public float cooldown;
    public float weaponForce;
    public float bulletSpread;
    public bool spin;

    public virtual void Fire(Vector2 direction, Transform transform)
    {
        GameObject b = GameObject.Instantiate(projectile) as GameObject;
        b.transform.position = GameObject.FindWithTag("Reticle").transform.position;
        
        //treat it as an angle
        float spreadModifier = Random.Range(-bulletSpread, bulletSpread);

        //rotates direction by amount of spread
        Vector3 spreadVector = Quaternion.Euler(0.0f, 0.0f, spreadModifier) * direction;
        
        b.GetComponent<Rigidbody2D>().AddForce(spreadVector * weaponForce, ForceMode2D.Impulse);
        if (spin)
            b.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-100, 100));       //put a spin on it so it looks nice
    }
}


public class WeaponManager : MonoBehaviour {

    public Weapon currentWeapon;
    public List<Weapon> weapons = new List<Weapon>();
    public GameObject[] projectiles = new GameObject[10];
    private bool canFire;

	// Use this for initialization
	void Start ()
    {
        canFire = true;
        weapons.Add(new Trombone(projectiles[0]));
        weapons.Add(new CymbalMineThrower(projectiles[1]));
        currentWeapon = weapons[0];
	}

    void Update()
    {
        SwapWeapons();
    }

    void SwapWeapons()
    {
        if (Input.GetButtonDown("Weapon1"))
        {
            Debug.Log("Weapon Switch: 1");
            currentWeapon = weapons[0];
        }
        if (Input.GetButtonDown("Weapon2"))
        {
            Debug.Log("Weapon Switch: 2");
            currentWeapon = weapons[1];
        }
        /*if (Input.GetButtonDown("Weapon2"))
        {
            Debug.Log("Weapon Switch: 2");
            currentWeapon = weapons[2];
        }*/
    }

    public void FireCurrentWeapon(Vector2 direction, bool spin)
    {
        transform.LookAt(transform.position, direction);

        if (canFire)
        {
            currentWeapon.Fire(direction, transform);
            StartCoroutine(Cooldown());
        }

    }

    IEnumerator Cooldown()
    {
        canFire = false;
        yield return new WaitForSeconds(currentWeapon.cooldown);
        canFire = true;
    }
}
