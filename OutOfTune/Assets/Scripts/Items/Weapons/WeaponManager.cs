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
    //when true, camera shakes when it fires
    public bool shakyCam = true;

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
        if (shakyCam)
        {
            /*GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            camera.transform.localPosition = new Vector3(Random.RandomRange(-1f, 1f),
                                                Random.RandomRange(-1f, 1f),
                                                camera.transform.localPosition.z);*/
            /*float backwardsForce = .01f;
            GameObject cm = GameObject.FindGameObjectWithTag("CameraManager");
            CameraManager cmScript = cm.GetComponentInParent<CameraManager>();
            PlayerController pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            Vector2 feedback;
            Vector3 cameraTarget = cmScript.getTarget();
            if (pc.facingRight)
            {
                feedback = new Vector2(cameraTarget.x + backwardsForce, 0.0f);
            }
            else
            {
                feedback = new Vector2(cameraTarget.x - backwardsForce, 0.0f);
            }
            cm.transform.position = feedback;
             */
        }

        if (spin)
            b.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-100, 100));       //put a spin on it so it looks nice

        //play sound
        int clipIndex = Random.Range(0, 3);
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
    private int weaponIndex;

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
		weaponname = "Trumpet";
        currentClipArray = trumpetClips;
        weaponIndex = 0;
	}

    void Update()
    {
        GetInput();
        SwapWeapons();
    }

    void GetInput()
    {
        float scrollWheelMovement = Input.GetAxis("ScrollWheel");
        if (scrollWheelMovement > 0)
        {
            weaponIndex--;
            if (weaponIndex < 0)
                weaponIndex = 4;
        }
        else if (scrollWheelMovement < 0)
        {
            weaponIndex++;
            if (weaponIndex > 4)
                weaponIndex = 0;
        }
        else
        {
            if (Input.GetButtonDown("Weapon1"))
            {
                weaponIndex = 0;
            }
            if (Input.GetButtonDown("Weapon2"))
            {
                weaponIndex = 1;
            }
            if (Input.GetButtonDown("Weapon3"))
            {
                weaponIndex = 2;
            }
            if (Input.GetButtonDown("Weapon4"))
            {
                weaponIndex = 3;
            }
            if (Input.GetButtonDown("Weapon5"))
            {
                weaponIndex = 4;
            }
        }
    }

    void SwapWeapons()
    {
        if (weaponIndex == 0)
        {
            Debug.Log("Weapon Switch: 1");
            currentWeapon = weapons[0];
            renderer.sprite = sprites[0];
			weaponname = "Trumpet";		//everytime change weapon must also change the name of the selected weapon
            currentClipArray = trumpetClips;
        }
        if (weaponIndex == 1)
        {
            Debug.Log("Weapon Switch: 2");
            currentWeapon = weapons[1];
            renderer.sprite = sprites[1];
			weaponname = "trombone_alone";
            currentClipArray = tromboneClips;
        }
        if (weaponIndex == 2)
        {
            Debug.Log("Weapon Switch: 3");
            currentWeapon = weapons[2];
            renderer.sprite = sprites[2];
            currentClipArray = cymbalClips;
			weaponname = "HHGoomba_TopHat";
        }
        if (weaponIndex == 3)
        {
            Debug.Log("Weapon Switch: 4");
            currentWeapon = weapons[3];
            renderer.sprite = sprites[3];
			weaponname = "Tuba";			//tuba weapon must change the name of the selected weapon
            currentClipArray = tubaClips;
        }
        if (weaponIndex == 4)
        {
            Debug.Log("Weapon Switch: 5");
            currentWeapon = weapons[4];
            renderer.sprite = sprites[4];
			weaponname = "Flute";
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
