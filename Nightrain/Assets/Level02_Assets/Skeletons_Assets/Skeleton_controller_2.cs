using UnityEngine;
using System.Collections;

public class Skeleton_controller_2 : MonoBehaviour {
	public AnimationClip RunAnimation;
	public AnimationClip IdleAnimation;
	public AnimationClip AttackAnimation;
	public AnimationClip DeathAnimation;
	public AnimationClip DanceAnimation;
	public AnimationClip GotHit;
	public AnimationClip WaitingFor;
	public Transform Anim;

	private CharacterController ctrl;
	private GameObject player;
	private Transform player_transform;
	private CharacterScript player_script;

	private Vector3 respawn;
	private bool returningRespawn = false;

	private float actual_time;
	private float attack_time = -2.0f;
	public float destroy_time = 10.0f;

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
		this.player_transform = player.transform;
		this.player_script = player.GetComponent<CharacterScript> ();
		this.ctrl = GetComponent<CharacterController> ();
		this.music = GameObject.FindGameObjectWithTag ("music_engine").GetComponent<Music_Engine_Script> ();

		this.respawn = transform.position;
		print (respawn);

		state = 0;
		Anim.animation.CrossFade (WaitingFor.name, 0.12f);
	}
	
	// Update is called once per frame
	void Update () {
		actual_time = Time.time;

		Vector3 p= player_transform.position ;
		float distance = Vector3.Distance(p,transform.position);
		float distanceRespawn = Vector3.Distance(respawn, transform.position);

		//Si no esta muerto
		if (state != 4) {
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
					// Si esta en su area y no volviendo al respawn
					if(distanceRespawn <= 100.0f && distance <= 70.0f && !returningRespawn) {
						followPlayer(p);
					} 
					// Si esta en zona de agre lejos del player
					else {
						if(distanceRespawn >= 1.0f) {
							returningRespawn = true;
							followPlayer(respawn);
						} else {
							returningRespawn = false;
							idleAnim();
						}
					}
					state = 1;
				}
			} else {
				attackEffect(t,distance);
				rotateToPlayer (p);
			}
		} else {
			Destroy (this.gameObject, destroy_time);
		}
	}

	void rotateToPlayer(Vector3 playerPos) {
		transform.rotation = Quaternion.LookRotation (playerPos - transform.position);
	}


	void followPlayer (Vector3 playerPos) {
		rotateToPlayer (playerPos);

		Vector3 dir = playerPos - transform.position;
		gravity -= 9.81f * Time.deltaTime;
		print (ctrl.isGrounded);
		if (ctrl.isGrounded)	gravity = 0.0f;
		dir.y = gravity;
		
		// calculate movement at the desired speed:
		Vector3 movement = dir.normalized * 20.0f * Time.deltaTime;
		// limit movement to never pass the target position:
		if (movement.magnitude > dir.magnitude) movement = dir;
		runAnim ();
		
		
		
		//MODIFICACIO OPTIMITZACIO
		// move the character:
		ctrl.Move(movement);

		/*
		//Vector3 movement = transform.position + transform.forward * 7.0f * 2 * Time.deltaTime;
		transform.position += transform.forward * 7.0f * 2 * Time.deltaTime;
		//ctrl.SimpleMove ();
		runAnim ();*/
	}

	void runAnim() {
		Anim.animation.CrossFade (RunAnimation.name, 0.12f);
	}

	void attackAnim() {
		Anim.animation.CrossFade (AttackAnimation.name, 0.0f);	
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

	void dieAnim() {
		Anim.animation.CrossFade (DeathAnimation.name, 0.12f);	
	}

	void idleAnim() {
		Anim.animation.CrossFade (IdleAnimation.name, 0.12f);
	}

	public void damage() {
		dieAnim ();
		Vector3 newPosition = transform.position;
		newPosition.y += 1.5f;
		transform.position = newPosition;
		state = 4;
		Destroy (this.GetComponent<CapsuleCollider>());
		Destroy (this.GetComponent<Rigidbody> ());
	}

	public void rageAnim() {
		Anim.animation.CrossFade (WaitingFor.name, 0.0f);	
	}
}
