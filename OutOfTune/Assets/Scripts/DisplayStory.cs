using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
public class DisplayStory : MonoBehaviour {
	public GameObject player;
	public GameObject storyobj;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		storyobj = GameObject.FindGameObjectWithTag("Story");
	}
	
	// Update is called once per frame
	void Update () {
		float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
		if(distance < 10){
			if(gameObject.name == "Level1intro" &&	storyobj.GetComponent<Story>().level1start == false ){
				//var sr = new StreamReader(Application.dataPath +"/scripts/level1intro.txt");
				List<string> temp = new List<string> {
				"Narrator: Meet Peter. A young man with a promising musical career, a loving fiancee, and the promise of many happy years to come. Or so he thought...",
				"Narrator: Tragedy struck, and Peter's life was is shambles! Torn asunder by the fickle tides of fate, he lost everything. Now his mind is in tatters from the cruelty that the world has wrought for him.",
					"Renee: Oh please.",
					"Narrator: Ah! But there is someone who can save him! Renee, the love of his life, can enter his twisted mind and put it at ease.",
					"Renee: Wait, what? No no no, I'm not here on a rescue mission. Let me just set some things straight.",
					"Narrator: But save him she must, lest he be entrapped in agony forever!",
					"Renee: Are you serious?",
					"Narrator: Of course! Here we go!",
					"Renee: Woah!",
					"Narrator: It was here in the icy wastes of the orchestra pits that Peter's coworkers betrayed him, and he lost his cherished role as first chair violinist.",
					"Renee: ...To be a SECOND chair violinist. I've heard this before.",
					"Narrator: Injustice! Brutal injustice that drove him to obsession which made a void in his heart that could never be filled!",
					"Renee: Judging by the lack of times you answered my calls, that's probably true.",
					"Narrator: Oh how he wished he could teach that conductor a lesson for his vindictive ways...",
					"Renee: Vindictive as in 'world's smallest demotion'? Whatever, let's get to it."
				};
				storyobj.GetComponent<Story>().reader(temp, "level1start");
			}else if(gameObject.name == "level1preboss" &&	storyobj.GetComponent<Story>().level1preboss == false ){

				List<string> temp = new List<string> {
					"Maestro: Ah, I see you've come to avenge your beloved!",
					"Renee: Not my beloved, not big on avenging. Can I leave or should I use the trombone?",
					"Maestro: Harrumph to your double-talk! Master Peter shall never reclaim his first chair! Verily, the whole of the orchestra bends to mine ear and falls into line!",
					"Renee: Peter, Maestro Edward doesn't talk like that. He did Shakespeare in the park ONCE, and it was nice. And is he a penguin because of his tux? Really??",
					"Maestro: Forsooth! Anon! And then some! Enough talk, villain! Have at you!"
				};
				//var sr = new StreamReader(Application.dataPath +"/scripts/level1preboss.txt");
				storyobj.GetComponent<Story>().reader(temp, "level1preboss");
			}else if(gameObject.name == "level1postboss" &&	storyobj.GetComponent<Story>().level1postboss == false ){
				//var sr = new StreamReader(Application.dataPath +"/scripts/level1postboss.txt");
				List<string> temp = new List<string>{
				"Maestro: Alas, I am slain... fare thee well...",
				"Renee: Whoo! What a rush! Okay, where's the exit?",
				"Narrator: And so, fair Renee fought back against the tide of tyranny--",
				"Renee: Tyranny? He was doing his job!",
				"Narrator: Fair, SWEET Renee, the only one for whom Peter truly cared...",
				"Renee: You got a real funny way of showing care, dude.",
				"Narrator: The one who left him to lament in the desert of lonliness, alone, all by himself.",
				"Renee: What?! Oh no, not again!"
				};

				storyobj.GetComponent<Story>().reader(temp, "level1postboss");
			}else if(gameObject.name == "level2intro" &&	storyobj.GetComponent<Story>().level2intro == false ){
				//var sr = new StreamReader(Application.dataPath +"/scripts/level2intro.txt");
				List<string> temp = new List<string>{
				"Narrator: What was once a field of love has dried up into nothingness. What was once Renee's home in his heart is now deprived and empty.",
				"Renee: That's... actually kind of sweet. I guess you really--",
				"Narrator: What was once his most prized possession--",
				"Renee: Whoa, hold up! Excuse me?",
				"Narrator: His, uh, most treasured companion! Deprived! Wasted! Desert! Get to it!"
				};
				storyobj.GetComponent<Story>().reader(temp, "level2intro");
			}else if(gameObject.name == "level2end" &&	storyobj.GetComponent<Story>().level2end == false ){
				List<string>temp = new List<string>{
				"Renee: What. On EARTH. Is this?!",
				"Narrator: It was a shrine, made to Renee so long ago. A golden idol of--",
				"Renee: You made a temple for me in your BRAIN?!",
				"Narrator: I-it was out of reverence! Out of the memories and the hope of what had--",
				"Renee: Nope! No way. That's it. This is easily the creepiest thing you've ever done.",
				"Narrator: But his undying love for you!",
				"Renee: Oh, undying! I guess the time spent endlessly trying to win back first chair, not returning my calls, ignoring me completely, all that was just a nap for his UNDYING love, huh?",
				"Renee: All of the game nights I offered, the dinners and movies ignored, all of that was still part of that UNDYING love! Sure!",
				"Renee: Forget it. Love isn't this statue locked in a temple in the desert, Peter. I'm not made of gold, and I'm nobody's idol.",
				"Renee: Now let me out of here.",
				"Narrator: ...",
				"Narrator: This is all his fault.",
				"Renee: Oh, HERE we go.",
				"Narrator: Armand. He stole her from poor, pitiful Peter.",
				"Renee: I'm not anyone's to steal!",
				"Narrator: He'll PAY for this!"
				};

				//var sr = new StreamReader(Application.dataPath +"/scripts/level2end.txt");
				storyobj.GetComponent<Story>().reader(temp, "level2end");
			}else if(gameObject.name == "level3intro" &&	storyobj.GetComponent<Story>().level3intro == false ){
				//var sr = new StreamReader(Application.dataPath +"/scripts/level3intro.txt");
				List<string>temp = new List<string>{
				"Narrator: And so, the wicked villain Armand pounded away on his little drum at the top of a mountain of ignorance and lies. There he laughed, and reveled in the destruction of Peter's life.",
				"Narrator: It was here that all of Peter's problems were made incarnate.",
				"Renee: You know what? You want me to change your mind? Fine. Let's go give 'Armand' a little chat."
				};
				storyobj.GetComponent<Story>().reader(temp, "level3intro");
			}else if(gameObject.name == "level3preboss" &&	storyobj.GetComponent<Story>().level3preboss == false ){
				List<string> temp = new List<string>{
				"Renee: Okay, Armand, I- oh dear lord.",
				"Armand: Yeeeeeeees! Renee, my extravagant boquet of dumplings! You have returned to me as my prize!",
				"Renee: Ugh, this is GROSS, Peter.",
				"Armand: Whaaaaaaat? You and I, we are made for each other, yes? We should be making-",
				"Renee: If the next words out of your mouth are 'beautiful music', I'm going to hurl.",
				"Armand: Ahaha, she is always for the chase, yes! I shall play my songs for you, and make you mine all over again!"
				};
				//var sr = new StreamReader(Application.dataPath +"/scripts/level3preboss.txt");
				storyobj.GetComponent<Story>().reader(temp, "level3preboss");
			}
		}
	}
}
