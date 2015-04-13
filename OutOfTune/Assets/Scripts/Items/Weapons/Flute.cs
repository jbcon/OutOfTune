using UnityEngine;
using System.Collections;

public class Flute : Weapon {
    public Flute(GameObject proj) {
        weaponType = WeaponType.SemiAuto;
        cooldown = 5.0f;
        weaponForce = 20f;
        spin = false;
        bulletSpread = 0.0f;
        projectile = proj;
    }
}
