using UnityEngine;
using System.Collections;

public class Fireball_Sound_effects : MonoBehaviour {
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
		music.play_Fireball_Explosion ();
	}
}
