using UnityEngine;
using System.Collections;

public class LethalKnife_controller : MonoBehaviour {
	private Music_Engine_Script music;
	
	public GameObject collisionEffect;
	public float skill_damage = 15.0f;
	public bool music_done = false;
	
	private Vector3 direction;
	private float speed = 50.0f;
	
	private bool skill_hit = false;
	
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
			if (!music_done) {
				music.play_Lethalknife_Collision ();
				music_done = true;
			}
			//Skill_Controller.setEffect(false);
			//ActionBarScript.disabledSkill1 = false;
			ActionBarScript.disabledSkill2 = false;
			//ActionBarScript.disabledSkill3 = false;
			collisionEffect.SetActive(true);
			Destroy (gameObject, 0.1f);
		}
		
		if (name == "Enemy") {
			other.gameObject.GetComponent<Movement_graveler>().setDamage(skill_damage);
		} 
		else if (name.Substring(0,name.Length-1) == "Skeleton") {
			if(!skill_hit) {
				other.gameObject.GetComponent<Skeleton_controller_2>().damage(skill_damage);
				skill_hit = true;
			}
		}
		else if (name.Substring(0,name.Length-1) == "FireDemon") {
			if(!skill_hit) {
				other.gameObject.GetComponent<FireDemon_Controller>().damage(skill_damage);
				skill_hit = true;
			}
		}
		else if (name.Substring(0,name.Length-1) == "IceDemon") {
			if(!skill_hit) {
				other.gameObject.GetComponent<FireDemon_Controller>().damage(skill_damage);
				skill_hit = true;
			}
		}
		else if (name.Substring(0,name.Length-1) == "MiniIceDemon") {
			if(!skill_hit) {
				other.gameObject.GetComponent<FireDemon_Controller>().damage(skill_damage);
				skill_hit = true;
			}
		}
		else if (name == "IceGolem") {
			if(!skill_hit) {
				other.gameObject.GetComponent<Ice_Golem_controller>().damage(skill_damage);
				skill_hit = true;
			}
		}
		else if (name == "Boss") {
			if(!skill_hit && other != null) {
				if(other.gameObject.name == "Golem_lava_surface"){
					print("Atacando a golem");
					other.gameObject.GetComponent<Movement>().setDamage(skill_damage);
					skill_hit = true;
				}
			}
			if(!skill_hit && other != null) {
				if(other.gameObject.GetComponent<Skeleton_boss_controller>() != null)
					other.gameObject.GetComponent<Skeleton_boss_controller>().damage(skill_damage);
				skill_hit = true;
			}
		}
	}
}