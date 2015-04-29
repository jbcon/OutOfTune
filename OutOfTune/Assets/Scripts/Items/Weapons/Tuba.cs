using UnityEngine;
using System.Collections;

public class Tuba : Weapon {

    public Tuba(GameObject proj)
    {
        weaponType = WeaponType.SemiAuto;
        cooldown = 4f;
        weaponForce = 150f;
        spin = false;
        bulletSpread = 0f;
        projectile = proj;
    }

    public override void Fire(Transform transform, AudioClip[] clipArray, AudioSource audioSource)
    {
        GameObject b = GameObject.Instantiate(projectile) as GameObject;
        GameObject reticle = GameObject.FindGameObjectWithTag("TubaReticle");
        b.transform.position = reticle.transform.position;

        b.GetComponent<Rigidbody2D>().AddForce(transform.up * weaponForce, ForceMode2D.Impulse);
        b.transform.rotation = Quaternion.LookRotation(Vector3.forward,
                                Quaternion.Euler(0f, 0f, 90f) * transform.up);
        if (spin)
            b.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-100, 100));       //put a spin on it so it looks nice

        //play sound
        int clipIndex = Random.Range(0, 3);
        audioSource.PlayOneShot(clipArray[clipIndex]);
    }
	
}
