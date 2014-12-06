using UnityEngine;
using System.Collections;

public class Final_Fireball_Controller : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnParticleCollision(GameObject other) {
		string name = other.gameObject.tag;
		print (name);
		if (name == "Boss") {
			other.gameObject.GetComponent<Skeleton_boss_controller> ().dieAnim ();
		}
	}
}
