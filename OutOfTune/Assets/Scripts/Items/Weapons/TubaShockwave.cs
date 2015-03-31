using UnityEngine;
using System.Collections;

public class TubaShockwave : MonoBehaviour {

    public float lifetime = 1f;
    public float maxWidth = 40f;
    public float damage = 0f;

    private Transform shockwave;
    private Vector2 targetScale;
    private Vector2 smoothness;
    private float t;


    void Start()
    {
        shockwave = transform;
        shockwave.localScale = new Vector2(0.0f, shockwave.localScale.y);
        targetScale = new Vector2(maxWidth, shockwave.localScale.y);
        smoothness = new Vector2(20f, 0);
    }

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
    }
}
