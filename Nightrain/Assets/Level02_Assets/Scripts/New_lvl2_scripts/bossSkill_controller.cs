using UnityEngine;
using System.Collections;

public class bossSkill_controller : MonoBehaviour {
	private Music_Engine_Script music;
	private CharacterScript char_script;
	
	public GameObject sparks;
	public BoxCollider sparks_area;
	public float skill_damage = 20;

	private Skeleton_boss_controller boss;

	private bool boss_killed = false;
	private bool skill_hit = false;
	public float time_sparks = 1.0f;

	// Use this for initialization
	void Start () {
		music = GameObject.FindGameObjectWithTag("music_engine").GetComponent<Music_Engine_Script>();
		char_script = GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterScript> ();
		music.Play_Barrel_Open ();
		boss = GameObject.FindGameObjectWithTag ("Boss").GetComponent<Skeleton_boss_controller> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (boss.health <= 0.0f) boss_killed = true;
		time_sparks -= Time.deltaTime;
		if (time_sparks < 0) {
			sparks.SetActive(true);
			sparks_area.enabled = true;
		}
		if (time_sparks < -5.0f) {
			sparks_area.enabled = false;
		}
	}

	void OnTriggerEnter(Collider other) {
		string name = other.gameObject.tag;

		//print ("Tocado");
		if (name == "Player" && !skill_hit && !boss_killed) {
			music.play_Player_Hurt ();
			char_script.setDamage ((int) skill_damage);
			skill_hit = true;
		}
	}

	void OnTriggerStay(Collider other) {
		string name = other.gameObject.tag;
		
		//print ("Tocado");
		if (name == "Player" && !skill_hit && !boss_killed) {
			music.play_Player_Hurt ();
			char_script.setDamage ((int) skill_damage);
			skill_hit = true;
		}
	}

	void OnTriggerExit(Collider other) {
		string name = other.gameObject.tag;
		
		//print ("Tocado");
		if (name == "Player" && !skill_hit && !boss_killed) {
			music.play_Player_Hurt ();
			char_script.setDamage ((int) skill_damage);
			skill_hit = true;
		}
	}
}
