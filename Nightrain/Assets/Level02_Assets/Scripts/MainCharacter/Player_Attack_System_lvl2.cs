using UnityEngine;
using System.Collections;

public class Player_Attack_System_lvl2 : MonoBehaviour {

	private CharacterScript cs;

	public float one_atk_time = 0.6f;
	
	private bool attacking = false;
	private float atk_time = 0.0f;
	private bool enemyHit = false;
	
	// Use this for initialization
	void Start () {
		this.cs = GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - atk_time > one_atk_time) {
			attacking = false;
			enemyHit = false;
		}
	}
	
	public void makeAttack() {
		attacking = true;
		enemyHit = false;
		atk_time = Time.time;
	}
	
	/*
	void OnTriggerEnter(Collider other) {
		string tag = other.gameObject.tag;
		if (attacking && isEnemy(tag) && !enemyHit) {
			attack (other.gameObject);
			atk_time = Time.time;
		}
	}*/
	
	void OnTriggerStay(Collider other) {
		string tag = other.gameObject.tag;
		if (attacking && isEnemy(tag) && !enemyHit) {
			attack (other.gameObject);
			atk_time = Time.time;
		}
	}

	public bool isEnemy(string name) {
		if (name.Substring(0,name.Length-1) == "Skeleton") {
			return true;
		}
		else if (name.Substring(0,name.Length-1) == "FireDemon") {
			return true;
		}
		else if (name.Substring(0,name.Length-1) == "IceDemon") {
			return true;
		}
		else if (name.Substring(0,name.Length-1) == "MiniIceDemon") {
			return true;
		}
		else if (name == "IceGolem") {
			return true;
		}
		else if (name == "Boss") {
			return true;
		}
		return false;
	}
	
	void attack(GameObject npc) {
		string tag1 = npc.tag.Substring (0, npc.tag.Length - 1);

		if (tag1 == "Skeleton") {
			npc.GetComponent <Skeleton_controller_2> ().damage(this.cs.computeDamage());
			enemyHit = true;
		}
		else if (tag1 == "FireDemon" || tag1 == "IceDemon" || tag1 == "MiniIceDemon") {
			npc.GetComponent <FireDemon_Controller> ().damage (this.cs.computeDamage());
			enemyHit = true;
		}
		else if (npc.tag == "IceGolem") {
			npc.GetComponent <Ice_Golem_controller> ().damage (this.cs.computeDamage());
			enemyHit = true;
		}
		else if (npc.tag == "Boss") {
			npc.GetComponent <Skeleton_boss_controller> ().damage (this.cs.computeDamage());
			enemyHit = true;
		}
	}
}
