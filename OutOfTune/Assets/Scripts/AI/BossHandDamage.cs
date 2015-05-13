using UnityEngine;
using System.Collections;

public class BossHandDamage : MonoBehaviour {

    GameObject bossParent;
    ArmandBoss a;

	// Use this for initialization
	void Start () {
        bossParent = GameObject.FindGameObjectWithTag("Boss");
        a = bossParent.GetComponent<ArmandBoss>();
	}

    public void InflictDamage(float dmg)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.material.color = new Vector4(0.5f, 0.0f, 0.0f, 1.0f);
        a.Damage(dmg);
        sr.material.color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
    }

	// Update is called once per frame
	void Update () {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        GetComponent<BoxCollider2D>().isTrigger = !a.attacking;
        if (a.attacking)
        {
            sr.material.color = new Vector4(0.8f, 0.5f, 0.0f, 1.0f);
        }
        else
        {
            sr.material.color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        }
	}
}
