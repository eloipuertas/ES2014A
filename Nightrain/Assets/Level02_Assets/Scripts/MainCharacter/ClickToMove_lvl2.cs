using UnityEngine;
using System.Collections;

public class ClickToMove_lvl2 : MonoBehaviour {
	// Parameters publics
	public float speed = 30;
	public GameObject attack_range;
	public float one_atk_time = 0.30f;
	public Animator anim;

	private Player_Attack_System_lvl2 atk_script;
	private CharacterController controller;
	private Music_Engine_Script music;

	//Movements variables
	private RaycastHit getObjectScene;
	private Vector3 destinationPosition;
	private Vector3 targetPoint;
	private bool moving = false;
	private float disToDestination = 0.0f;
	private Plane playerPlane;
	private Ray ray;
	private Ray rayAttack;
	private float hitdist = 0.0f;
	
	//MODIFICACIO PER MANTENIRLO SOBRE EL TERRA
	private float gravity;
	private float atk_time = -1.0f;
	private bool attacking = false;
	private bool attack_target = false;
	private float atk_cd = 1.0f;
	private bool dead = false;
	private bool allowMovement = true;

	// Use this for initialization
	void Start () {
		atk_script = attack_range.GetComponent<Player_Attack_System_lvl2> ();
		music = GameObject.FindGameObjectWithTag("music_engine").GetComponent<Music_Engine_Script> ();
		controller = this.gameObject.GetComponent<CharacterController> ();
		//animation["metarig|Caminar"].speed = 2.75f;
		//animation ["metarig|Atacar"].speed = 1.25f;
		destinationPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.deltaTime != 0 && !dead) {
			if (anim.GetBool("attack")) anim.SetBool("attack",false);
			
			//if (attack_target) destinationPosition = getObjectScene.transform.position;
			disToDestination = Mathf.Abs (transform.position.x - destinationPosition.x) + Mathf.Abs(destinationPosition.z - transform.position.z);
			
			//Si llegamos a la position del click +-0.5f de distancia para evitar que se quede corriendo
			if(disToDestination  < .90f){
				speed = 0.0f;
				anim.SetFloat ("speed", speed);
			} else {
				if(allowMovement) moveToPosition (destinationPosition);
				if (!isAttacking()) speed = 30.0f;
				else speed = 20.0f;
				anim.SetFloat ("speed", speed);
			}

			// Si hacemos click o dejamos presionado el botón izquierdo del mouse, nos movemos al punto del mouse
			if(Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse0)) { 			
				attack_target = false;
				playerPlane = new Plane(Vector3.up, transform.position);
				ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				hitdist = 0.0f;
				
				if (playerPlane.Raycast(ray, out hitdist)) {
					targetPoint = ray.GetPoint(hitdist);
					if(allowMovement) {
						destinationPosition = ray.GetPoint(hitdist);
						rotateToMouse();
					}
					//destinationPosition = ray.GetPoint(hitdist);
					//rotateToMouse();
				}
			}

			//Bloque para atacar
			if (!isAttacking()) { // Si no esta haciendo un ataque
				if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.Mouse1)) { // Al puslar espacio
					// Si ha pasado mas de 1 segundo del inicio del ultimo ataque
					if (canAttack()) {
						rotateToMouse ();
						attackAnim ();
						atk_script.makeAttack();
						atk_time = Time.time;
						music.play_Player_Sword_Attack ();
					}
				}

				/*if (Input.GetKeyDown (KeyCode.Mouse1)) {
					if (canAttack ()) {
						rayAttack = Camera.main.ScreenPointToRay(Input.mousePosition);
						if(Physics.Raycast(rayAttack, out getObjectScene, 100.0f)){
							string npc_tag = getObjectScene.transform.gameObject.tag;
							if(atk_script.isEnemy(npc_tag)) {
								attack_target = true;
								rotateToPos (getObjectScene.transform.position);
							}
						}
					}
				}*/
			}
		}
	}
	

	public void rotateToMouse () {
		if (Time.time - atk_time > 0.5f) {
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			targetPoint = ray.GetPoint(hitdist);
			targetPoint.y = transform.position.y;
			Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
			transform.rotation = targetRotation;
		}
	}

	public void rotateToPos(Vector3 position) {
		Quaternion targetRotation = Quaternion.LookRotation(position - transform.position);
		transform.rotation = targetRotation;
	}

	void moveToPosition (Vector3 position) {
		Vector3 dir = position - transform.position;
		gravity -= 100f * Time.deltaTime;
		if (controller.isGrounded)	gravity = 0.0f;
		dir.y = gravity;
		Vector3 movement = dir.normalized * speed * Time.deltaTime;
		if (movement.magnitude > dir.magnitude) movement = dir;
		controller.Move(movement);
	}


	public void teleport(Vector3 position) {
		destinationPosition = position;
		speed = 0.0f;
		anim.SetFloat ("speed", speed);
	}

	public void dontWalk() {
		allowMovement = false;
	}
	
	public void Walk() {
		allowMovement = true;
	}

	private bool isAttacking() {
		if (Time.time - atk_time < one_atk_time) return true;
		else return false;
	}

	private bool canAttack() {
		if (Time.time - atk_time > atk_cd) return true;
		else return false;
	}

	public void attackAnim() {
		anim.SetBool ("attack", true);
		atk_time = Time.time;
	}

	public void dieAnim () {
		anim.SetBool ("dead", true);
		dead = true;
	}

	public void stopAttackAnim() {
		anim.SetBool ("attack", false);
	}
}
