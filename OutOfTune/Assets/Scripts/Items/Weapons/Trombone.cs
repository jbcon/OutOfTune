using UnityEngine;
using System.Collections;

public class Trombone : Weapon {


    public uint numProjectiles = 3;

    public Trombone(GameObject proj)
    {
        weaponType = WeaponType.SemiAuto;
        cooldown = 1.0f;
        weaponForce = 6f;
        spin = false;
        bulletSpread = 30.0f;
        projectile = proj;
    }

    public override void Fire(Transform transform, AudioClip[] clipArray, AudioSource audioSource)
    {
        /* has three projectiles
         * each projectile has a different bullet spread range
         */
        for (int i = 0; i < numProjectiles; i++)
        {
            base.Fire(transform, clipArray, audioSource);
        }
    }
	
}
