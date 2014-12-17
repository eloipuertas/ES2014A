using UnityEngine;
using System.Collections;

public class Ice_Golem_controller : MonoBehaviour {
	public AnimationClip RunAnimation;
	public AnimationClip IdleAnimation;
	public AnimationClip AttackAnimation;
	public AnimationClip DeathAnimation;
	public AnimationClip DanceAnimation;
	public AnimationClip GotHit;
	public AnimationClip WaitingFor;
	public Transform Anim;
	public float health = 10.0f;
	public int base_dmg = 10;
	public int exp = 500;

	private Animator anim_ctrl;
	private CharacterController ctrl;
	private NPCHealthBar_lvl2 health_bar;
	private GameObject player;
	private CharacterScript player_script;
	private StageController stage;
	private GameObject health_sphere;
	private GameObject mana_sphere;
	
	private Vector3 respawn;
	private bool returningRespawn = false;
	private bool player_seen = false;
	
	public float actual_health;
	private float actual_time;
	private float attack_time = -2.0f;
	public float destroy_time = 10.0f;
	private float seen_time = 0.0f;
	private float atk_range = 9.0f;
	
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
		this.player_script = player.GetComponent<CharacterScript> ();
		this.health_bar = this.gameObject.GetComponent<NPCHealthBar_lvl2> ();
		this.ctrl = GetComponent<CharacterController> ();
		this.stage = GameObject.FindGameObjectWithTag ("GameController").GetComponent<StageController> ();
		this.music = GameObject.FindGameObjectWithTag ("music_engine").GetComponent<Music_Engine_Script> ();

		anim_ctrl = this.gameObject.GetComponent<Animator> ();

		this.health_bar.enabled = false;

		this.health_sphere = Resources.Load<GameObject> ("Lvl2/prefabs/Life_Major_sphere_lvl2");
		this.mana_sphere = Resources.Load<GameObject> ("Lvl2/prefabs/Mana_Major_sphere_lvl2");
		
		this.respawn = transform.position;
		
		setAtrributesDifficulty (PlayerPrefs.GetString ("Difficult"));
		
		state = 0;
		idleAnim ();
	}
	
	// Update is called once per frame
	void Update () {
		//Si no esta muerto
		if (state != 4) {
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
					if (distance <= 35.0f && !player_seen) {
						setAgressive();
					}
					// Si esta en su area y no volviendo al respawn
					if(distanceRespawn <= 110.0f && distance <= 100.0f && !returningRespawn && player_seen) {
						if(Time.time - seen_time > 0.5f) followPlayer(p);
					} 
					// Si esta en zona de agre lejos del player
					else {
						if(distanceRespawn >= 2.0f) {
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
			Destroy (this.gameObject, destroy_time);
		}
	}
	
	void rotateToPlayer(Vector3 playerPos) {
		Vector3 newPlayerPos = playerPos;
		newPlayerPos.y += 0;
		transform.rotation = Quaternion.LookRotation (newPlayerPos - transform.position);
	}
	
	
	void followPlayer (Vector3 playerPos) {
		rotateToPlayer (playerPos);
		Vector3 dir = playerPos - transform.position;
		
		gravity -= 100f * Time.deltaTime;
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
			music.play_skel_attack ();
			attackAudio = true;
		}
		if (t <= 0.5f && t >= 0.4f) {
			//attackAnim ();
			if (!attackDone) {
				attackDone = true;
				//Para no buggear el trigger lo movemos ligeramemnte
				transform.position += new Vector3(0.5f,0,0);
				if (distance <= 9.0f) {
					//music.play_Player_Hurt ();
					player_script.setDamage (base_dmg);
				}
			}
		} else if(t > 0.9f) anim_ctrl.SetBool("attack", false);
	}
	
	public void setAgressive() {
		this.health_bar.enabled = true;
		playerSeen ();
		GameObject [] skeletons = GameObject.FindGameObjectsWithTag (this.gameObject.tag);
		foreach (GameObject skel in skeletons) {
			float dist = Vector3.Distance (skel.transform.position, this.transform.position);
			if (dist < 21.0f);
				//skel.GetComponent<Skeleton_controller_2>().playerSeen ();
		}
	}
	
	public void playerSeen() {
		if (!player_seen) music.play_skel_shout ();
		player_seen = true;
		seen_time = Time.time;
		if(state != 4) waitingAnim();
		rotateToPlayer (player.transform.position);
	}
	
	public void damage(float dmg) {
		actual_health -= dmg;
		if (!player_seen) playerSeen ();
		if (actual_health <= 0.0f) {
			if(state != 4) music.play_skel_die ();
			state = 4;
			dieAnim ();
			stage.dead_npc (this.gameObject.tag);
			
			Vector3 newPosition_sphere = transform.position;
			newPosition_sphere.y += 3.0f;
			newPosition_sphere.x -= 2.0f;
 			Instantiate (health_sphere, newPosition_sphere, health_sphere.transform.rotation);
			newPosition_sphere.x += 4.0f;
			Instantiate (mana_sphere, newPosition_sphere, health_sphere.transform.rotation);
			
			
			Vector3 newPosition = transform.position;
			newPosition.y += 1.5f;
			transform.position = newPosition;
			Destroy (this.GetComponent<CharacterController>());
			Destroy (this.GetComponent<Rigidbody> ());
			Destroy (this.GetComponent<CapsuleCollider> ());
			this.player_script.setEXP(exp);
		} else {
			music.play_skel_die ();
		}
	}
	
	// ANIMATIONS
	public void runAnim() {
		Anim.animation.CrossFade (RunAnimation.name, 0.12f);
		anim_ctrl.SetBool("attack", false);
		anim_ctrl.SetBool("walk", true);
	}
	
	public void attackAnim() {
		Anim.animation.CrossFade (AttackAnimation.name, 0.0f);
		anim_ctrl.SetBool("walk", false);
		anim_ctrl.SetBool("attack", true);
	}
	
	public void rageAnim() {
		Anim.animation.CrossFade (WaitingFor.name, 0.0f);	
	}
	
	public void dieAnim() {
		Anim.animation.CrossFade (DeathAnimation.name, 0.12f);
		anim_ctrl.SetBool ("death", true);
	}
	
	public void idleAnim() {
		Anim.animation.CrossFade (IdleAnimation.name, 0.12f);
	}
	
	public void waitingAnim() {
		Anim.animation.CrossFade (WaitingFor.name, 0.12f);
	}
	
	private void setAtrributesDifficulty (string difficulty) {
		if(difficulty.Equals("Easy")) {
			base_dmg = base_dmg / 2;
			health = health / 2;
			atk_range = atk_range - 1f;
		}
		else if(difficulty.Equals("Normal")) {
			base_dmg = base_dmg;
			health = health;
			atk_range = atk_range;
		}
		else if(difficulty.Equals("Hard")) {
			base_dmg = base_dmg*2;
			health = health*2;
			atk_range = atk_range+1.5f;
		}
		else if(difficulty.Equals("Extreme")) {
			base_dmg = base_dmg*3;
			health = health*2.5f;
			atk_range = atk_range+2.5f;
		}
		actual_health = health;
	}
}
