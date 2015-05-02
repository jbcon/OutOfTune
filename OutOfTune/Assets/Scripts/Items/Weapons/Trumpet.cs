using UnityEngine;
using System.Collections;

public class Trumpet : Weapon {



    public Trumpet(GameObject proj)
    {
        weaponType = WeaponType.FullAuto;
        projectile = proj;
        cooldown = 0.07f;
        weaponForce = 13f;
        bulletSpread = 5f;
        spin = true;
    }

    public override void Fire(Transform transform, AudioClip[] clipArray, AudioSource audioSource)
    {
        base.Fire(transform, clipArray, audioSource);
    }
	
}
