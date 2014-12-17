using UnityEngine;
using System.Collections;

public class Skeleton_boss_controller : MonoBehaviour {
	public AnimationClip RunAnimation;
	public AnimationClip IdleAnimation;
	public AnimationClip AttackAnimation;
	public AnimationClip DeathAnimation;
	public AnimationClip DanceAnimation;
	public AnimationClip GotHit;
	public AnimationClip WaitingFor;
	public Transform Anim;
	public float destroy_time = 10.0f;
	public float total_health = 500.0f;
	public float base_dmg = 15.0f;
	public int exp = 1000;
	public GameObject camera_death;

	private GameObject game_engine;
	private NPCHealthBar_lvl2 health_bar;
	private GameEngineLevel02_new game_script;
	private CharacterController ctrl;
	private GameObject player;
	private Transform player_transform;
	private CharacterScript player_script;
	private GameObject skill;
	
	private Vector3 respawn;
	private bool returningRespawn = false;

	private float atk_range = 9.0f;
	private float sparks_cd = 1.0f;
	private float sparks_dmg = 20.0f;
	private float skill_cd = 0.0f;
	private float actual_time;
	private float attack_time = -2.0f;
	public float health;

	//0 idle, 1 running, 2 attacking, 3 hited, 4 death, 5 waiting, 6 dancing
	private int state = 0;
	private bool attackDone = false;
	private bool attackAudio = false;
	private float gravity = 0.0f;
	private bool agressive = false;
	private int nshots = 0;
	private int maxshots = 5;
	private float last_skill_time = 0.0f;
	private float death_video_delay = 0.0f;
	private bool killed = false;

	//private GameObject NPCbar;
	private Music_Engine_Script music;
	
	// Use this for initialization
	void Start () {
		this.game_engine = GameObject.FindGameObjectWithTag ("GameController");
		this.health_bar = this.gameObject.GetComponent<NPCHealthBar_lvl2> ();
		this.game_script = game_engine.GetComponent <GameEngineLevel02_new> ();
		this.player = GameObject.FindGameObjectWithTag("Player");
		this.music = GameObject.FindGameObjectWithTag ("music_engine").GetComponent<Music_Engine_Script> ();
		this.player_transform = player.transform;
		this.player_script = player.GetComponent<CharacterScript> ();
		this.skill = Resources.Load<GameObject> ("Prefabs/Boss_Skills/Boss_skill_2");

		this.respawn = transform.position;
		this.health_bar.enabled = false;

		setAtrributesDifficulty (PlayerPrefs.GetString ("Difficult"));

		state = 0;
		Anim.animation.CrossFade (IdleAnimation.name, 0.12f);
	}
	
	// Update is called once per frame
	void Update () {
		actual_time = Time.time;
		skill_cd -= Time.deltaTime;

		Vector3 p = new Vector3();
		if (player != null) {
			p = player_transform.position;
		} 
		float distance = Vector3.Distance(p,transform.position);
		float distanceRespawn = Vector3.Distance(respawn, transform.position);
		
		//Si no esta muerto
		if (state != 4 && agressive) {
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
					float percent = health/total_health;
					if (percent <= 0.75 && skill_cd <= 0.0f) {
						castSkills(percent);
					} else {
						followPlayer(p);
					}
					state = 1;
				}
			} else {
				attackEffect(t,distance);
				rotateToPlayer (p);
			}
		} else {
			if (state == 4) {
				if (!killed) prepareAnim ();
				if(Time.time - death_video_delay > 1.0f && !killed) {
					camera_death.SetActive (true);
				}
			}//Destroy (this.gameObject, destroy_time);
			else idleAnim ();
		}
	}
	
	public void rotateToPlayer(Vector3 playerPos) {
		Vector3 plaPos = playerPos;
		// Boss skeleton simple
		plaPos.y = transform.position.y;
		// Boss lanza
		//plaPos.y -= 2.0f;
		transform.rotation = Quaternion.LookRotation (plaPos - transform.position);
	}

	void rotateToPlayer(Vector3 playerPos, float lookAt) {
		Vector3 plaPos = playerPos;
		// Boss skeleton simple
		plaPos.y = transform.position.y;
		// Boss lanza
		//plaPos.y -= 2.0f;
		transform.rotation = Quaternion.LookRotation (plaPos - transform.position);
	}
	
	
	void followPlayer (Vector3 playerPos) {
		rotateToPlayer (playerPos);
		transform.position += transform.forward * 12.0f * 2 * Time.deltaTime;
		runAnim ();
	}
	
	void runAnim() {
		Anim.animation.CrossFade (RunAnimation.name, 0.12f);
	}
	
	void attackAnim() {
		rotateToPlayer (player.transform.position, 0.0f);
		Anim.animation.CrossFade (AttackAnimation.name, 0.0f);	
	}
	
	void attackEffect(float t, float distance) {
		if (t>=0.3f && !attackAudio) {
			music.play_Lethalknife_Shot ();
			music.play_boss_attack ();
			attackAudio = true;
		}
		if (t <= 0.5f && t >= 0.4f) {
			if (!attackDone) {
				attackDone = true;
				if (distance <= atk_range) {
					//music.play_Player_Hurt ();
					player_script.setDamage ((int) base_dmg);
				}
			}
		}
	}
	
	public void dieAnim() {
		if (!killed) {
			killed = true;
			this.player_script.setEXP(exp);
			TrophyEngine trofeos = GameObject.FindGameObjectWithTag("Trofeos").GetComponent<TrophyEngine>();
			trofeos.TrophyGolemIce();
			trofeos.TrophyCharacter(PlayerPrefs.GetString ("Player"));
			trofeos.TrophyDifficult(PlayerPrefs.GetString("Difficult"));
			music.play_boss_die ();
		}
		Anim.animation.CrossFade (DeathAnimation.name, 0.12f);	
	}
	
	void idleAnim() {
		Anim.animation.CrossFade (IdleAnimation.name, 0.12f);
	}
	
	public void damage(float dmg) {
		health -= dmg;
		if (health <= 0.0f) {
			if (state != 4) music.play_boss_fall ();
			state = 4;
			agressive = false;
			if(death_video_delay == 0.0f) death_video_delay = Time.time;
		} else {
			music.play_boss_got_hit ();
		}
		/*if (health <= 0) {
			dieAnim ();
			Vector3 newPosition = transform.position;
			newPosition.y += 1.5f;
			transform.position = newPosition;
			state = 4;
			Destroy (this.GetComponent<CapsuleCollider>());
			Destroy (this.GetComponent<Rigidbody> ());
		}*/
	}

	void castSkills (float percent) {
		last_skill_time -= Time.deltaTime;
		if (nshots < maxshots && last_skill_time <= 0.0f) {
			Vector3 skill_pos = player.transform.position;
			//skill_pos.y -= 0.0f;
			Instantiate (skill, skill_pos, skill.transform.rotation);
			nshots += 1;
			last_skill_time = 1.0f;
			attackAnim ();
			if (percent < 0.56f) {
				skill_pos.z += 20;
				Instantiate (skill, skill_pos, skill.transform.rotation);
			}
			if (percent < 0.40f) {
				skill_pos.z -= 40;
				Instantiate (skill, skill_pos, skill.transform.rotation);
				skill_pos.z += 20;
			}
			if (percent < 0.25f) {
				skill_pos.x += 20;
				Instantiate (skill, skill_pos, skill.transform.rotation);
				skill_pos.x -= 40;
				Instantiate (skill, skill_pos, skill.transform.rotation);
			}
		} else if (nshots == maxshots) {
			skill_cd = 5.0f;
			nshots = 0;
		} else {
			idleAnim ();
		}
	}

	public void setAgressive (bool b) {
		agressive = b;
		health_bar.enabled = true;
	}

	public void prepareAnim() {
		Anim.animation.Play (WaitingFor.name);	
	}

	private void setAtrributesDifficulty (string difficulty) {
		if(difficulty.Equals("Easy")) {
			base_dmg = base_dmg * 1;
			atk_range = atk_range - 1f;
			total_health = total_health / 4f;
			sparks_cd = sparks_cd;
			sparks_dmg = sparks_dmg;
		}
		else if(difficulty.Equals("Normal")) {
			base_dmg = base_dmg * 1.5f;
			atk_range = atk_range;
			total_health = total_health / 1.5f;
			sparks_cd = sparks_cd;
			sparks_dmg = sparks_dmg*1.5f;
		}
		else if(difficulty.Equals("Hard")) {
			base_dmg = base_dmg * 2f;
			atk_range = atk_range+2f;
			sparks_cd = sparks_cd - 0.30f;
			sparks_dmg = sparks_dmg*2f;
		}
		else if(difficulty.Equals("Extreme")) {
			base_dmg = base_dmg * 4f;
			total_health = total_health * 2;
			atk_range = atk_range+3.5f;
			sparks_cd = sparks_cd - 0.55f;
			sparks_dmg = sparks_dmg*3.5f;
		}
		health = total_health;
		skill.GetComponent<bossSkill_controller> ().skill_damage = sparks_dmg;
		skill.GetComponent<bossSkill_controller> ().time_sparks = sparks_cd;
	}

	public void teleportToRespawn() {
		transform.position = respawn;
	}
}
