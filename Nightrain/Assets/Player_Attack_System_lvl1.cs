using UnityEngine;
using System.Collections;

public class Player_Attack_System_lvl1 : MonoBehaviour {
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
		if (name == "Enemy") {
			return true;
		}
		else if (name == "Boss") {
			return true;
		}
		return false;
	}
	
	void attack(GameObject npc) {
		if (npc.tag == "Enemy") {
			npc.GetComponent <Movement_graveler> ().setDamage(this.cs.computeDamage());
			enemyHit = true;
		}
		else if (npc.tag == "Boss") {
			npc.GetComponent <Movement> ().setDamage (this.cs.computeDamage());
			enemyHit = true;
		}
	}
}
