using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CymbalMineThrower : Weapon
{
    public uint maxActiveMines = 2;
    private LinkedList<GameObject> mines = new LinkedList<GameObject>();

    public CymbalMineThrower(GameObject proj)
    {
        weaponType = WeaponType.SemiAuto;
        cooldown = 2f;
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
            //removes mines that have been detonated
            LinkedListNode<GameObject> itr, next;

            /* iterate using linked list nodes
             * This allows for O(n) removal of all null nodes,
             * probably the best we can get, even though it's not
             * that many mines at once anyway
             * ...why did I overthink this? */
            for (itr = mines.First; itr != null;)
            {
                next = itr.Next;
                if (itr.Value == null)
                {
                    mines.Remove(itr);
                }
                itr = next;
            }
            //if it is still too big
            if (mines.Count == maxActiveMines)
            {
                //remove oldest mine
                GameObject.Destroy(mines.First.Value);
                mines.RemoveFirst();
            }
        }
        mines.AddLast(b);

    }

}
