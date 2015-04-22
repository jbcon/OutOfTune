using UnityEngine;
using System.Collections;

public class TubaShockwave : MonoBehaviour {

    public float lifetime = 1f;
    public float maxWidth = 40f;
    public float force = 30f;

    private Transform shockwave;
    private Vector2 targetScale;
    private Vector2 smoothness;
    private float t;


    void Start()
    {
        shockwave = transform;
        //shockwave.localScale = new Vector2(0.0f, shockwave.localScale.y);
        //targetScale = new Vector2(maxWidth, shockwave.localScale.y);
        //smoothness = new Vector2(20f, 0);
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Transform tf = other.gameObject.transform;
        Rigidbody2D rb = other.attachedRigidbody;
        if (rb)
        {
            Vector2 dist = new Vector2(tf.position.x - this.transform.position.x,
                tf.position.y - this.transform.position.y);
            dist.Normalize();
            rb.AddForce(dist * force, ForceMode2D.Impulse);
        }
    }

    /*
    // Update is called once per frame
    void FixedUpdate()
    {
        t += Time.deltaTime;
        shockwave.localScale = new Vector2(Mathf.Lerp(0, maxWidth, Mathf.SmoothStep(0.0f, 1.0f, t)/lifetime), shockwave.localScale.y);
        //shockwave.localScale = Vector2.(shockwave.localScale, targetScale, ref smoothness, lifetime);
        if (shockwave.localScale.x == maxWidth)
        {
            Destroy(gameObject);
        }
    }*/
}
