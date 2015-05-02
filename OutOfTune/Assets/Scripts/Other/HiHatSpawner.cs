using UnityEngine;
using System.Collections;

public class HiHatSpawner : MonoBehaviour {

    public GameObject spawnable;
    private BoxCollider2D area;
    private GameObject player;
    private bool containsPlayer = false;

	// Use this for initialization
	void Start () {
        area = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
	}

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject == player)
        {
            containsPlayer = true;
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
        yield return new WaitForSeconds(10.0f);
        if (containsPlayer)
        {
            GameObject newHiHat = Instantiate(spawnable) as GameObject;
        }
    }
}
