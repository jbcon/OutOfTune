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

    public bool canFire = true;

    public virtual void Fire(Vector2 direction, Transform transform)
    {
        GameObject b = GameObject.Instantiate(projectile) as GameObject;
        b.transform.position = GameObject.FindWithTag("Reticle").transform.position;
        
        //treat it as an angle
        float spreadModifier = Random.Range(-bulletSpread, bulletSpread);

        //rotates direction by amount of spread
        Vector3 spreadVector = Quaternion.Euler(0.0f, 0.0f, spreadModifier) * direction;
        
        b.GetComponent<Rigidbody2D>().AddForce(spreadVector * weaponForce, ForceMode2D.Impulse);
        b.transform.rotation = Quaternion.LookRotation(Vector3.forward, 
                                Quaternion.Euler(0f, 0f, 90f) * spreadVector);
        if (spin)
            b.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-100, 100));       //put a spin on it so it looks nice
    }

    public IEnumerator Cooldown()
    {
        canFire = false;
        yield return new WaitForSeconds(cooldown);
        canFire = true;
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
        weapons.Add(new Trumpet(projectiles[0]));
        weapons.Add(new Trombone(projectiles[1]));
        weapons.Add(new CymbalMineThrower(projectiles[2]));
        weapons.Add(new Tuba(projectiles[3]));
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
        if (Input.GetButtonDown("Weapon3"))
        {
            Debug.Log("Weapon Switch: 3");
            currentWeapon = weapons[2];
        }
        if (Input.GetButtonDown("Weapon4"))
        {
            Debug.Log("Weapon Switch: 4");
            currentWeapon = weapons[3];
        }
    }

    public void FireCurrentWeapon(Vector2 direction, bool spin)
    {
        transform.LookAt(transform.position, direction);

        if (currentWeapon.canFire)
        {
            currentWeapon.Fire(direction, transform);
            StartCoroutine(currentWeapon.Cooldown());
        }

    }
}
