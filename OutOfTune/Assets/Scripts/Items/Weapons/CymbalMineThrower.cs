using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CymbalMineThrower : Weapon
{
    public uint maxActiveMines = 2;
    private Queue<GameObject> mines = new Queue<GameObject>();

    public CymbalMineThrower(GameObject proj)
    {
        weaponType = WeaponType.SemiAuto;
        delay = 1f;
        weaponForce = 20f;
        spin = true;
        bulletSpread = 0f;
        projectile = proj;
    }

    public override void Fire(Vector2 direction, Transform transform)
    {
        GameObject b = GameObject.Instantiate(projectile) as GameObject;
        b.transform.position = GameObject.FindWithTag("Reticle").transform.position;
        b.GetComponent<Rigidbody2D>().AddForce(direction * weaponForce, ForceMode2D.Impulse);
        if (spin)
            b.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-100f, 100f));

        //make sure there is a limited number of mines
        if (mines.Count == maxActiveMines)
        {
            GameObject tmp = mines.Dequeue();
            GameObject.Destroy(tmp);
        }
        mines.Enqueue(b);

    }

}
