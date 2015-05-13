using UnityEngine;
using System.Collections;

public class MeleeWeapon : MonoBehaviour {

    public AudioClip[] violinClips = new AudioClip[3];
    public bool canPlayClip = true;

    private AudioSource source;
    private int clipIndex = 0;

	// Use this for initialization
	void Start (){
        source = GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<AudioSource>();
	}

    void OnEnable()
    {
        canPlayClip = true;
        clipIndex = 0;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy")
            || collider.gameObject.layer == LayerMask.NameToLayer("Hand"))
        {
            source.PlayOneShot(violinClips[clipIndex]);
            clipIndex++;
            if (clipIndex > 2)
                clipIndex = 0;
            canPlayClip = false;
            if (collider.gameObject.layer == LayerMask.NameToLayer("Hand"))
            {
                collider.gameObject.GetComponent<BossHandDamage>().InflictDamage(1f);
            }
            else
            {
                Health health = collider.gameObject.GetComponent<Health>();
                if (health)
                {
                    health.Defend(1f);
                }
            }
            
        }
    }
	

}
