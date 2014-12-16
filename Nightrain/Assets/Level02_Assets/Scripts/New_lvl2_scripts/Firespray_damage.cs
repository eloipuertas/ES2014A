using UnityEngine;
using System.Collections;

public class Firespray_damage : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnParticleCollision(GameObject other) {
		string name = other.gameObject.tag;
			
		if (name == "Player") {
				//music.play_Fireball_Explosion ();
			other.gameObject.GetComponent<CharacterScript>().setDamage(1);
		}
			
		if (name == "Enemy") {
			other.gameObject.GetComponent<Movement>().setDamage(1);
		}

		if (name.Substring(0, name.Length-1) == "Skeleton" || name == "Skeleton") {
			other.gameObject.GetComponent<Skeleton_controller_2>().damage(2.0f);
		}
	}
}
