using UnityEngine;
using System.Collections;

public class Skill_Controller : MonoBehaviour {
	public float fireball_cooldown = 2.0f;
<<<<<<< HEAD

	private float actual_time;

	private float fireball_time = 0.0f;

=======
	
	private float actual_time;
	
	private float fireball_time = 0.0f;
	
>>>>>>> devel_c_music_sprint4
	private Transform fireball;
	
	// Use this for initialization
	void Start () {
		this.fireball = Resources.Load<Transform>("Prefabs/Character_Skills/Fireball_Skill");
		actual_time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 1) {
			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				actual_time = Time.time;
				if (fireball_time == 0.0f || (actual_time - fireball_time) >= fireball_cooldown) {
					animation.CrossFade ("metarig|Atacar", 0.2f);
					fireball.rotation = transform.rotation;
					Vector3 newPosition = transform.position;
					newPosition.y += 2;
					fireball.transform.position = newPosition;
					Instantiate (fireball);
					fireball_time = Time.time;
				}
			}
		}
	}
	
	void OnParticleCollision(GameObject other) {
		print ("hit by fire");
	}
}
