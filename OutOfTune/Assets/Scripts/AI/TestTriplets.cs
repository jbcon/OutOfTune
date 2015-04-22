using UnityEngine;
using System.Collections;

public class TestTriplets : MonoBehaviour {
	public GameObject player;
	public GameObject projectile;
	private bool moving;
	Animator animator;
	TestTripletsAI testing = new TestTripletsAI();
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		testing.SetPos(gameObject.transform.localScale.x);
		testing.CreateBasicStats(gameObject,projectile);
		testing.SetPlayer(player);
		moving= false;
		animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		//different movement set compared to other ai getes its own
		//testing.Movement();
		float distance = Vector3.Distance(player.transform.position, testing.self.transform.position);
		//Debug.Log(distance+"sdf"+testing.range);
		//moving left off the platform basically
		if (distance < testing.range || moving == true){
			animator.SetBool("Sliding",true);
			if (player.GetComponent<PlayerController>().grounded ){
				testing.faceright = true;
				gameObject.transform.localScale = new Vector2(testing.pos_scale, testing.self.transform.localScale.y);
				//right side of the player move right
				gameObject.transform.Translate(Vector3.left * testing.speed * Time.deltaTime);
				//continues to move even after player gets out of ranger
				moving = true;
			}

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
public class TestTripletsAI : GeneralAI{
	public TestTripletsAI(){
		range = 200f;
		speed = 10f;
	}
}