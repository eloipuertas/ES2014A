using UnityEngine;
using System.Collections;

public class LethalKnife_controller : MonoBehaviour {
	private Music_Engine_Script music;

	public GameObject collisionEffect;
	public float skill_damage = 15.0f;
	
	private Vector3 direction;
	private float speed = 50.0f;
	
	// Use this for initialization
	void Start () {
		music = GameObject.FindGameObjectWithTag("music_engine").GetComponent<Music_Engine_Script>();
		music.play_Lethalknife_Shot ();
	}
	
	// Update is called once per frame
	void Update () {
		direction = transform.forward;
		//rigidbody.AddForce (direction * speed);
		rigidbody.velocity = direction * speed;
	}
	
	void OnCollisionEnter(Collision other) {
		string name = other.gameObject.tag;
		
		if (name != "Player") {
			music.play_Lethalknife_Collision ();
			collisionEffect.SetActive(true);
			Destroy (gameObject, 0.1f);
		}
		
		if (name == "Enemy") {
			other.gameObject.GetComponent<Movement>().setDamage(skill_damage);
		}
	}
}