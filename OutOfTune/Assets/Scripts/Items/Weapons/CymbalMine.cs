using UnityEngine;
using System.Collections;

public class CymbalMine : MonoBehaviour
{

    public float damage = 5;

    void OnCollisionEnter2D(Collision2D collision)
    {
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int groundLayer = LayerMask.NameToLayer("Ground");

        if (collision.gameObject.layer == enemyLayer)
        {
            //Debug.Log("HIT!");
            collision.gameObject.GetComponent<Health>().Defend(damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.layer == groundLayer)
        {
            //Debug.Log("Mine placed on ground");
        }
    }

}
