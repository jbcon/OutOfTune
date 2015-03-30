using UnityEngine;
using System.Collections;

public class Trumpet : Weapon {

    public Trumpet(GameObject proj)
    {
        weaponType = WeaponType.FullAuto;
        projectile = proj;
        cooldown = 0.01f;
        weaponForce = 10f;
        bulletSpread = 10f;
        spin = true;
    }
	
}
