using UnityEngine;
using System.Collections;

public class Fireball_controller : MonoBehaviour {
	public float skill_damage = 20.0f; 
	private Music_Engine_Script music;

	// Use this for initialization
	void Start () {
		music = GameObject.FindGameObjectWithTag("music_engine").GetComponent<Music_Engine_Script>();
		music.play_Fireball_Shot ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnParticleCollision(GameObject other) {
		string name = other.gameObject.tag;
		
		if (name != "Player") {
			music.play_Fireball_Explosion ();
		}
		
		if (name == "Enemy") {
			other.gameObject.GetComponent<Movement>().setDamage(skill_damage);
		}
	}
}
