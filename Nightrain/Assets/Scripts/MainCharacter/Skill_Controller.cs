using UnityEngine;
using System.Collections;

public class Skill_Controller : MonoBehaviour {

	Transform fireball;
	
	// Use this for initialization
	void Start () {
		this.fireball = Resources.Load<Transform>("Prefabs/Character_Skills/Fireball_Skill");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			animation.CrossFade("metarig|Atacar",0.2f);
			fireball.rotation = transform.rotation;
			Vector3 newPosition = transform.position;
			newPosition.y += 2;
			fireball.transform.position = newPosition;
			Instantiate (fireball);
		}
	}
	
	void OnParticleCollision(GameObject other) {
		print ("hit by fire");
	}
}
