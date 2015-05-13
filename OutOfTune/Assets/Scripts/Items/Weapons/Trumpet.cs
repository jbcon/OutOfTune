using UnityEngine;
using System.Collections;

public class Trumpet : Weapon {



    public Trumpet(GameObject proj)
    {
        weaponType = WeaponType.FullAuto;
        projectile = proj;
        cooldown = 0.15f;
        weaponForce = 12f;
        bulletSpread = 7f;
        spin = true;
    }

    public override void Fire(Transform transform, AudioClip[] clipArray, AudioSource audioSource)
    {
        base.Fire(transform, clipArray, audioSource);
    }
	
}
