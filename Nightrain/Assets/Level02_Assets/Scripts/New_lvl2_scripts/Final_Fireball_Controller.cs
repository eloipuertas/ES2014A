using UnityEngine;
using System.Collections;

public class Final_Fireball_Controller : MonoBehaviour {
	private Music_Engine_Script music;

	// Use this for initialization
	void Start () {
		music = GameObject.FindGameObjectWithTag ("music_engine").GetComponent<Music_Engine_Script> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnParticleCollision(GameObject other) {
		music.play_fire_explosion ();

		string name = other.gameObject.tag;
		//print (name);
		if (name == "Boss") {
			other.gameObject.GetComponent<Skeleton_boss_controller> ().dieAnim ();
		}
	}
}
