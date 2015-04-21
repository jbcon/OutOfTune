using UnityEngine;
using System.Collections;

public class Trumpet : Weapon {

    //when true, camera shakes when it fires
    bool shakyCam = true;

    public Trumpet(GameObject proj)
    {
        weaponType = WeaponType.FullAuto;
        projectile = proj;
        cooldown = 0.01f;
        weaponForce = 10f;
        bulletSpread = 10f;
        spin = true;
    }

    public override void Fire(Transform transform)
    {
        base.Fire(transform);
        if (shakyCam)
        {
            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            camera.transform.localPosition = new Vector3(Random.RandomRange(-0.2f, 0.2f), 
                                                Random.RandomRange(-0.2f, 0.2f), 
                                                camera.transform.localPosition.z);
        }
        
    }
	
}
