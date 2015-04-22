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

    public virtual void Fire(Transform transform, AudioClip[] clipArray, AudioSource audioSource)
    {
        GameObject b = GameObject.Instantiate(projectile) as GameObject;
        GameObject reticle = GameObject.FindGameObjectWithTag("Reticle");
        b.transform.position = reticle.transform.position;
        
        //treat it as an angle
        float spreadModifier = Random.Range(-bulletSpread, bulletSpread);

        //rotates direction by amount of spread
        Vector3 spreadVector = Quaternion.Euler(0.0f, 0.0f, spreadModifier) * transform.right;
        
        b.GetComponent<Rigidbody2D>().AddForce(spreadVector * weaponForce, ForceMode2D.Impulse);
        b.transform.rotation = Quaternion.LookRotation(Vector3.forward, 
                                Quaternion.Euler(0f, 0f, 90f) * spreadVector);
        if (spin)
            b.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-100, 100));       //put a spin on it so it looks nice

        //play sound
        int clipIndex = Random.Range(0, 2);
        audioSource.PlayOneShot(clipArray[clipIndex]);
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
	public string weaponname;// added this to identify it for ui manager
    public List<Weapon> weapons = new List<Weapon>();
    public GameObject[] projectiles = new GameObject[10];
    public Sprite[] sprites = new Sprite[10];
    private SpriteRenderer renderer;

    /* SOUND EFFECTS
     * these vary by level
     * */

    public AudioClip[] trumpetClips = new AudioClip[3];
    public AudioClip[] tromboneClips = new AudioClip[3];
    public AudioClip[] cymbalClips = new AudioClip[3];
    public AudioClip[] tubaClips = new AudioClip[3];
    public AudioClip[] fluteClips = new AudioClip[3];

    private AudioClip[] currentClipArray;

    private AudioSource audioSource;

	// Use this for initialization
	void Start ()
    {
        renderer = GetComponentInChildren<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        weapons.Add(new Trumpet(projectiles[0]));
        weapons.Add(new Trombone(projectiles[1]));
        weapons.Add(new CymbalMineThrower(projectiles[2]));
        weapons.Add(new Tuba(projectiles[3]));
        weapons.Add(new Flute(projectiles[4]));
        currentWeapon = weapons[0];
        renderer.sprite = sprites[0];
		weaponname = "bigtrumpet";
        currentClipArray = trumpetClips;
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
            renderer.sprite = sprites[0];
			weaponname = "bigtrumpet";		//everytime change weapon must also change the name of the selected weapon
            currentClipArray = trumpetClips;
        }
        if (Input.GetButtonDown("Weapon2"))
        {
            Debug.Log("Weapon Switch: 2");
            currentWeapon = weapons[1];
            renderer.sprite = sprites[1];
			weaponname = "trombone";
            currentClipArray = tromboneClips;
        }
        if (Input.GetButtonDown("Weapon3"))
        {
            Debug.Log("Weapon Switch: 3");
            currentWeapon = weapons[2];
            renderer.sprite = sprites[2];
            currentClipArray = cymbalClips;
        }
        if (Input.GetButtonDown("Weapon4"))
        {
            Debug.Log("Weapon Switch: 4");
            currentWeapon = weapons[3];
            renderer.sprite = sprites[3];
			weaponname = "tuba";			//tuba weapon must change the name of the selected weapon
            currentClipArray = tubaClips;
        }
        if (Input.GetButtonDown("Weapon5"))
        {
            Debug.Log("Weapon Switch: 5");
            currentWeapon = weapons[4];
            renderer.sprite = sprites[4];
			weaponname = "flute";
            currentClipArray = fluteClips;
        }
    }

    public void FireCurrentWeapon(bool spin)
    {
        if (currentWeapon.canFire)
        {
            currentWeapon.Fire(transform, currentClipArray, audioSource);
            StartCoroutine(currentWeapon.Cooldown());
        }

    }
}
