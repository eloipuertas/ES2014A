using UnityEngine;
using System.Collections;

public class FireDemon_Controller : MonoBehaviour {
	public AnimationClip RunAnimation;
	public AnimationClip IdleAnimation;
	public AnimationClip AttackAnimation;
	public AnimationClip DeathAnimation;
	public AnimationClip DanceAnimation;
	public AnimationClip GotHit;
	public AnimationClip WaitingFor;
	public Transform Anim;
	public float health = 10.0f;
	public bool preanimation = true;

	private CharacterController ctrl;
	private GameObject player;
	private CharacterScript_lvl2 player_script;
	private StageController stage;
	
	private Vector3 respawn;
	private bool returningRespawn = false;
	private bool player_seen = false;
	private bool notAnim = false;
	
	private float actual_health;
	private float actual_time;
	private float attack_time = -2.0f;
	public float destroy_time = 10.0f;
	private float seen_time = 0.0f;
	
	//0 idle, 1 running, 2 attacking, 3 hited, 4 death, 5 waiting, 6 dancing
	int state = 0;
	bool attackDone = false;
	bool attackAudio = false;
	float gravity = 0.0f;
	
	//private GameObject NPCbar;
	private Music_Engine_Script music;
	
	// Use this for initialization
	void Start () {
		this.player = GameObject.FindGameObjectWithTag("Player");
		this.player_script = player.GetComponent<CharacterScript_lvl2> ();
		this.ctrl = GetComponent<CharacterController> ();
		this.stage = GameObject.FindGameObjectWithTag ("GameController").GetComponent<StageController> ();
		this.music = GameObject.FindGameObjectWithTag ("music_engine").GetComponent<Music_Engine_Script> ();
		this.respawn = transform.position;

		notAnim = !preanimation;

		actual_health = health;
		state = 0;
		idleAnim ();
	}
	
	// Update is called once per frame
	void Update () {
		//Si no esta muerto
		if (state != 4 && notAnim) {
			actual_time = Time.time;
			
			Vector3 p= player.transform.position ;
			float distance = Vector3.Distance(p,transform.position);
			float distanceRespawn = Vector3.Distance(respawn, transform.position);
			
			float t = actual_time - attack_time;
			// Si ha acabado de atacar
			if (t >= 1.5f) {
				//Si esta cerca del player
				if (distance <= 8.0f) {
					attackAnim ();
					attackDone = false;
					attackAudio = false;
					attack_time = Time.time;
					state = 2;
				} else {
					if (distance <= 25.0f && !player_seen) {
						playerSeen ();
					}
					// Si esta en su area y no volviendo al respawn
					if(distanceRespawn <= 300.0f && distance <= 200.0f && !returningRespawn && player_seen) {
						if(Time.time - seen_time > 0.5f) followPlayer(p);
					} 
					// Si esta en zona de agre lejos del player
					else {
						if(distanceRespawn >= 1.0f) {
							returningRespawn = true;
							followPlayer(respawn);
						} else {
							returningRespawn = false;
							//idleAnim();
						}
					}
					state = 1;
				}
			} else {
				attackEffect(t,distance);
				rotateToPlayer (p);
			}
		} else {
			if(state==4) Destroy (this.gameObject, destroy_time);
			else idleAnim ();
		}
	}
	
	void rotateToPlayer(Vector3 playerPos) {
		Vector3 destination = playerPos;
		destination.y = transform.position.y;
		transform.rotation = Quaternion.LookRotation (destination - transform.position);
	}
	
	
	void followPlayer (Vector3 playerPos) {
		rotateToPlayer (playerPos);
		Vector3 dir = playerPos - transform.position;
		
		gravity -= 9.81f * Time.deltaTime;
		if (ctrl.isGrounded)	gravity = 0.0f;
		dir.y = gravity;
		
		Vector3 movement = dir.normalized * 20.0f * Time.deltaTime;
		if (movement.magnitude > dir.magnitude) movement = dir;
		
		runAnim ();
		ctrl.Move(movement);
	}
	
	
	
	void attackEffect(float t, float distance) {
		if (t>=0.3f && !attackAudio) {
			music.play_Lethalknife_Shot ();
			attackAudio = true;
		}
		if (t <= 0.5f && t >= 0.4f) {
			if (!attackDone) {
				attackDone = true;
				if (distance <= 9.0f) {
					music.play_Player_Hurt ();
					player_script.setDamage (20);
				}
			}
		}
	}

	/*
	public void setAgressive() {
		playerSeen ();
		GameObject [] skeletons = GameObject.FindGameObjectsWithTag (this.gameObject.tag);
		print ("Skeletons hay?" + this.gameObject.tag + " " + skeletons.Length);
		foreach (GameObject skel in skeletons) {
			if (skel != null) {
				float dist = Vector3.Distance (skel.transform.position, this.transform.position);
				if (dist < 21.0f)
					skel.GetComponent<FireDemon_Controller>().playerSeen ();
			}
		}
	}*/
	
	public void playerSeen() {
		player_seen = true;
		seen_time = Time.time;
		waitingAnim();
		rotateToPlayer (player.transform.position);
	}
	
	public void damage(float dmg) {
		actual_health -= dmg;
		if (actual_health <= 0.0f) {
			dieAnim ();
			stage.dead_npc (this.gameObject.tag);
			Vector3 newPosition = transform.position;
			newPosition.y += 1.5f;
			transform.position = newPosition;
			state = 4;
			Destroy (this.GetComponent<CharacterController>());
			Destroy (this.GetComponent<Rigidbody> ());
		} 
	}
	
	// ANIMATIONS
	public void runAnim() {
		Anim.animation.CrossFade (RunAnimation.name, 0.12f);
	}
	
	public void attackAnim() {
		Anim.animation.CrossFade (AttackAnimation.name, 0.0f);	
	}
	
	public void rageAnim() {
		Anim.animation.CrossFade (WaitingFor.name, 0.0f);	
	}
	
	public void dieAnim() {
		Anim.animation.CrossFade (DeathAnimation.name, 0.12f);	
	}
	
	public void idleAnim() {
		Anim.animation.CrossFade (IdleAnimation.name, 0.12f);
	}
	
	public void waitingAnim() {
		Anim.animation.CrossFade (WaitingFor.name, 0.12f);
	}

	public void set_notAnim () {
		notAnim = true;
	}
}
