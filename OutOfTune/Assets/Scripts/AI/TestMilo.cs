using UnityEngine;
using System.Collections;

public class TestMilo : MonoBehaviour {
	public GameObject player;
	public GameObject projectile;
	public float range;
	public bool fire;
	TestMiloAI testing = new TestMiloAI();
	//Immboile doesn't move just shooots
	// Use this for initialization
	void Start () {
		fire = false;
		range = 50f;
		player = GameObject.FindGameObjectWithTag ("Player");
		testing.SetPos(gameObject.transform.localScale.x);
		testing.CreateBasicStats(gameObject,projectile);
		testing.SetPlayer(player);
	}
	IEnumerator MiloFire(){
		while (fire == true){
			Vector2 direction = new Vector2(player.transform.position.x - gameObject.transform.position.x,player.transform.position.y - gameObject.transform.position.y);
			GameObject b = GameObject.Instantiate(projectile) as GameObject;

			//if the player is on the left or right side spawn the bullet on the correct side
			Vector3 point = player.gameObject.transform.InverseTransformPoint (gameObject.transform.position);
			if (point.x > 0){
				b.transform.position = new Vector3(gameObject.transform.position.x - 6,gameObject.transform.position.y ,gameObject.transform.position.z);  
			}else if (point.x < 0){
				b.transform.position = new Vector3(gameObject.transform.position.x + 6,gameObject.transform.position.y ,gameObject.transform.position.z);  
			}

			//treat it as an angle
			float spreadModifier = Random.Range(-1, 1);
			
			//rotates direction by amount of spread
			Vector3 spreadVector = Quaternion.Euler(0.0f, 0.0f, spreadModifier) * direction;
			
			b.GetComponent<Rigidbody2D>().AddForce(spreadVector * 1f, ForceMode2D.Impulse);
			b.transform.rotation = Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0f, 0f, 90f) * spreadVector);
			yield return new WaitForSeconds(1);
		}
	}
	// Update is called once per frame
	void Update () {
		float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
		if (!testing.stunned){
			StopCoroutine("MiloFire");
		}
		if ( distance < range && fire == false){
			fire = true;
			StartCoroutine("MiloFire");
		}else if (fire == true && distance > range){
			fire = false;
			//Debug.Log("safd");
			//StopCoroutine("MiloFire");
		}
	}
	public void FireBullet(){
		testing.FireBullet();
	}
	void OnReceiveDamage(float dmg)
	{        
		if (!testing.stunned)
		{
			testing.currenthealth.health -= dmg;
			if (testing.currenthealth.health > 0)
			{
				StartCoroutine(Stun());
				Debug.Log("AI Brassi script received damage");
			}
		}
	}
	
	IEnumerator Stun()
	{
		
		//testing.animator.SetTrigger("Stun");
		testing.stunned = true;
		if (!testing.grounded)
		{
			yield return new WaitForEndOfFrame();
		}
		else
		{
			yield return new WaitForSeconds(0.5f);
		}
		testing.stunned = false;
	}
}
[System.Serializable]
public class TestMiloAI : GeneralAI{
	public TestMiloAI(){
		range = 20f;
		
	}
}
