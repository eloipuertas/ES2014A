using UnityEngine;
using System.Collections;

public class bossSkill_controller : MonoBehaviour {
	private Music_Engine_Script music;
	private CharacterScript_lvl2 char_script;
	
	public GameObject sparks;
	public BoxCollider sparks_area;
	public float skill_damage = 20;
	
	private bool skill_hit = false;
	public float time_sparks = 1.0f;

	// Use this for initialization
	void Start () {
		music = GameObject.FindGameObjectWithTag("music_engine").GetComponent<Music_Engine_Script>();
		char_script = GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterScript_lvl2> ();
		music.Play_Barrel_Open ();
	}
	
	// Update is called once per frame
	void Update () {
		time_sparks -= Time.deltaTime;
		if (time_sparks < 0) {
			sparks.SetActive(true);
			sparks_area.enabled = true;
		}
	}

	void OnTriggerEnter(Collider other) {
		string name = other.gameObject.tag;

		//print ("Tocado");
		if (name == "Player" && !skill_hit) {
			music.play_Player_Hurt ();
			char_script.setDamage ((int) skill_damage);
			skill_hit = true;
		}
	}

	void OnTriggerStay(Collider other) {
		string name = other.gameObject.tag;
		
		//print ("Tocado");
		if (name == "Player" && !skill_hit) {
			music.play_Player_Hurt ();
			char_script.setDamage ((int) skill_damage);
			skill_hit = true;
		}
	}

	void OnTriggerExit(Collider other) {
		string name = other.gameObject.tag;
		
		//print ("Tocado");
		if (name == "Player" && !skill_hit) {
			music.play_Player_Hurt ();
			char_script.setDamage ((int) skill_damage);
			skill_hit = true;
		}
	}
}
