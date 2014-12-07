using UnityEngine;
using System.Collections;

public class Fireball_controller : MonoBehaviour {
	public float skill_damage = 20.0f; 
	private bool music_done = false;
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
			if (!music_done) {
				music.play_Fireball_Explosion ();
				music_done = true;
			}

			Destroy (gameObject, 0.1f);
		}
		
		if (name == "Enemy") {
			other.gameObject.GetComponent<Movement>().setDamage(skill_damage);
		} 
		else if (name.Substring(0,name.Length-1) == "Skeleton") {
			other.gameObject.GetComponent<Skeleton_controller_2>().damage(skill_damage);
		}
		else if (name.Substring(0,name.Length-1) == "FireDemon") {
				other.gameObject.GetComponent<FireDemon_Controller>().damage(skill_damage);
		}
		else if (name.Substring(0,name.Length-1) == "IceDemon") {
				other.gameObject.GetComponent<FireDemon_Controller>().damage(skill_damage);
		}
		else if (name.Substring(0,name.Length-1) == "MiniIceDemon") {
				other.gameObject.GetComponent<FireDemon_Controller>().damage(skill_damage);
		}
		else if (name == "Boss") {
			other.gameObject.GetComponent<Skeleton_boss_controller>().damage(skill_damage);
		}
	}
}
