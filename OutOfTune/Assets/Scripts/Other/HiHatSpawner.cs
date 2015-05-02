using UnityEngine;
using System.Collections;

public class HiHatSpawner : MonoBehaviour {

    public GameObject spawnable;
    public float interval = 10f;
    private BoxCollider2D area;
    private GameObject player;
    private bool containsPlayer = false;

	// Use this for initialization
	void Start () {
        area = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("Spawn");
	}

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject == player)
        {
            containsPlayer = true;
            Debug.Log("PLAYER PRESENT");
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject == player)
        {
            containsPlayer = false;
        }
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            if (containsPlayer)
            {
                GameObject newHiHat = Instantiate(spawnable) as GameObject;
                newHiHat.transform.position = transform.position;
            }
        }
        
    }
}
