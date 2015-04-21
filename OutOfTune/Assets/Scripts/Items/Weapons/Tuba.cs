using UnityEngine;
using System.Collections;

public class Tuba : Weapon {

    public Tuba(GameObject proj)
    {
        weaponType = WeaponType.SemiAuto;
        cooldown = 2f;
        weaponForce = 10f;
        spin = false;
        bulletSpread = 0f;
        projectile = proj;
    }

    public override void Fire(Transform tf)
    {
        //sends out a shockwave that expands at the rate of bulletForce
        //possibly use (S)LERP and an expanding collider (probably a BoxCollider2D)
        GameObject b = GameObject.Instantiate(projectile) as GameObject;
        b.transform.localPosition = tf.parent.transform.position;
    }
	
}
