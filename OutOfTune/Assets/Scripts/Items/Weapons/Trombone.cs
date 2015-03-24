using UnityEngine;
using System.Collections;

public class Trombone : Weapon {


    public uint numProjectiles = 5;

    public Trombone(GameObject proj)
    {
        weaponType = WeaponType.SemiAuto;
        delay = 1.0f;
        weaponForce = 6f;
        spin = true;
        bulletSpread = 30.0f;
        projectile = proj;
    }

    public override void Fire(Vector2 direction, Transform transform)
    {
        /* has three projectiles
         * each projectile has a different bullet spread range
         */
        for (int i = 0; i < numProjectiles; i++)
        {
            base.Fire(direction, transform);
        }
    }
	
}
