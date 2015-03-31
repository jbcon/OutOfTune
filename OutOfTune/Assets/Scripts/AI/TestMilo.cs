using UnityEngine;
using System.Collections;

public class TestMilo : MonoBehaviour {
	public GameObject player;
	public GameObject projectile;
	public int health;
	public float range;
	public bool fire;
	// Use this for initialization
	void Start () {
		fire = false;
		range = 20f;
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	IEnumerator MiloFire(){
		while (fire == true){
			GameObject b = GameObject.Instantiate(projectile) as GameObject;
			//assuming the object is facing right 
			//moving it out of collision zone
			b.transform.position = new Vector3 (gameObject.transform.position.x- 8, gameObject.transform.position.y,gameObject.transform.position.z);
			Vector3 playerpos = player.transform.position;
			playerpos.z = 1;
			playerpos = Camera.main.ScreenToWorldPoint(playerpos);
			//Debug.DrawLine(gameObject.transform, playerpos);
			Vector2 direction = new Vector2(playerpos.x - gameObject.transform.position.x, playerpos.y - gameObject.transform.position.y);
			direction.Normalize();
			//b.transform.position = new Vector3(gameObject.transform.position.x - 10,gameObject.transform.position.y ,gameObject.transform.position.z);  
			
			
			//Vector3 direction = transform.TransformDirection(Vector3.forward);
			//Debug.Log(b.transform.position);
			//b.transform.position = GameObject.FindWithTag("Reticle").transform.position;
			
			//treat it as an angle
			//float spreadModifier = Random.Range(-2, 2);
			
			//Vector2 direction = new Vector2(2,3);
			//rotates direction by amount of spread
			Vector3 spreadVector = Quaternion.Euler(0.0f, 0.0f, 1f) * direction;
			b.GetComponent<Rigidbody2D>().AddForce(spreadVector *2f, ForceMode2D.Impulse);
			if (true)
				b.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-100, 100));       //put a spin on it so it looks nice
			yield return new WaitForSeconds(2);
		}
	}
	// Update is called once per frame
	void Update () {
		float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
		if ( distance < range && fire == false){
			fire = true;
			StartCoroutine("MiloFire");
		}else if (fire == true && distance > range){
			fire = false;
			//Debug.Log("safd");
			//StopCoroutine("MiloFire");
		}
	}
}
[System.Serializable]
public class TestMiloAI : GeneralAI{
	public TestMiloAI(){
		range = 20f;
		
	}
}
