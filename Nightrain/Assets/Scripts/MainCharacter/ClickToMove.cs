using UnityEngine;
using System.Collections;
using Pathfinding;

public class ClickToMove : MonoBehaviour {
	// Parameters publics
	public float speed = 30;
	public GameObject attack_range;
	public float one_atk_time = 0.30f;
	
	private Animator anim;
	private Player_Attack_System_lvl1 atk_script;
	private CharacterController controller;
	private Music_Engine_Script music;
	
	//Movements variables
	private RaycastHit getObjectScene;
<<<<<<< HEAD
	private RaycastHit hitCheck;
	private Component music;
	private float rotationSpeed = 10.0f;
	private float attackTime = 0.90f;
<<<<<<< HEAD
	private float MIN_ENEMY_DIST =  4.6f;
=======
	private float MIN_ENEMY_DIST =  7f;
>>>>>>> 710a951727f91ce211db816c812bc01edeb77703
	/* =================================== */

	/* == PathFinding ==================== */
	private static Seeker seeker;
	public float repathRate = 10.0f;
	private float lastRepath = -9999;
	private bool done;
	//The calculated path
	public Path path = null;
	/* =================================== */

	//The animation speed per second
	public float speed = 5;

	//The max distance from the AI to a waypoint for it to continue to the next waypoint
	private float defaultNextWaypointDistance = 2;

	//The waypoint we are currently moving towards
	private int currentWaypoint = 0;
	private string state; // let state be {"Walk", "Chase", "Attack"}

	// ====================================================
	//FIX TO INVENTORY
	private bool walk = true;
	// ====================================================

<<<<<<< HEAD
	private float timer_w_attack = 1f;


=======
>>>>>>> 710a951727f91ce211db816c812bc01edeb77703
	public void Start () {
		player = GameObject.FindWithTag("Player");
		music = GameObject.Find("MusicEngine").GetComponent("Music_Engine_Script");
		character = GetComponent<CharacterScript> ();

		//Get a reference to the Seeker component we added earlier
		seeker = GetComponent<Seeker>();

		playerPlane = new Plane(Vector3.up, transform.position);
		controller = GetComponent<CharacterController>();
		anim = GetComponent<Animator> ();

		state = "None";
		done = true; //initially consider pathfinding done.
	}


	public void OnPathComplete (Path p) {
		//Debug.Log ("Yey, we got a path back. Did it have an error? "+p.error);
		if (!p.error) {
			path = p;
			//Reset the waypoint counter
			currentWaypoint = 1;		
		}
=======
	private Vector3 destinationPosition;
	private Vector3 targetPoint;
	private bool moving = false;
	private float disToDestination = 0.0f;
	private Plane playerPlane;
	private Ray ray;
	private Ray rayAttack;
	private float hitdist = 0.0f;
    private bool chestHit = false;
    private SphereCollider atk_range;

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
		atk_script = attack_range.GetComponent<Player_Attack_System_lvl1> ();
		music = GameObject.FindGameObjectWithTag("music_engine").GetComponent<Music_Engine_Script> ();
		controller = this.gameObject.GetComponent<CharacterController> ();
        atk_range = attack_range.GetComponent<SphereCollider>();
	
		anim = this.gameObject.GetComponent<Animator> ();
		destinationPosition = transform.position;
>>>>>>> Devel
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.deltaTime != 0 && !dead) {
			if (anim.GetBool("attack")) anim.SetBool("attack",false);

<<<<<<< HEAD
		if (/*!state.Equals ("Dead") && */!state.Equals("None")) {
			if(path != null) {
				if(!anim.GetBool("walk")) {
					//Debug.Log ("@Update: STARTING WALK ANIMATION!");
					anim.SetBool ("w_stop",false);
					anim.SetBool ("walk", true);
				}
				tracking ();
			}
		} else {
			// We can get here from previous attack animation!

<<<<<<< HEAD
			if(anim.GetBool("w_attack") && timer_w_attack <= 0f){
=======
			if(anim.GetBool("w_attack")){
>>>>>>> 710a951727f91ce211db816c812bc01edeb77703
				//Debug.Log ("@Udate: w_attack true -> stop WATTACK/WALK");
				anim.SetBool("w_attack", false);
				anim.SetBool ("w_stop",true);
				anim.SetBool ("walk", false);
<<<<<<< HEAD
			}else if(anim.GetBool("attack")){
=======
			} 

			else if(anim.GetBool("attack")){
>>>>>>> 710a951727f91ce211db816c812bc01edeb77703
				//Debug.Log ("@Udate: attack true -> stop ATTACK");
				anim.SetBool("attack", false);
			}
		}	
	}


<<<<<<< HEAD
	public void FixedUpdate () {
=======

	public void FixedUpdate () {

		if (Input.GetMouseButton(0)) {
			done = false; //assume every click is a new target -> done flag restart
>>>>>>> 710a951727f91ce211db816c812bc01edeb77703

		Ray ray;
		float hitdist;
		float distance_to_enemy;

<<<<<<< HEAD
		if (Input.GetMouseButton(0)) {

			done = false; //assume every click is a new target -> done flag restart
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
=======

>>>>>>> 710a951727f91ce211db816c812bc01edeb77703
			//deteccion movimiento
			if (playerPlane.Raycast(ray, out hitdist)){
				targetPoint = ray.GetPoint(hitdist);
				targetPosition = ray.GetPoint(hitdist);
				//Debug.Log (">> NEW target @("+ targetPosition.x + "," + targetPosition.y +")");
				if(walk){
					state = "Walk";
					Walk ();
				}
=======
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
>>>>>>> Devel
			}
			
			// Si hacemos click o dejamos presionado el botón izquierdo del mouse, nos movemos al punto del mouse
			if(Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse0)) { 			
				attack_target = false;

<<<<<<< HEAD
<<<<<<< HEAD
			if (Input.GetMouseButton(1)){	

=======
                enableCollider();    // Si estabamos en un cofre dehabilitamos el flag y habilitamos de nuevo el SphereCollider
				playerPlane = new Plane(Vector3.up, transform.position);
>>>>>>> Devel
				ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				hitdist = 0.0f;
				
				if (playerPlane.Raycast(ray, out hitdist)) {
					targetPoint = ray.GetPoint(hitdist);
					if(allowMovement) {
						destinationPosition = ray.GetPoint(hitdist);
						rotateToMouse();
					}
				}
			}
<<<<<<< HEAD

			if(anim.GetBool("w_attack") && timer_w_attack <= 0f){
				//Debug.Log ("@Udate: w_attack true -> stop WATTACK/WALK");
				anim.SetBool("w_attack", false);
				anim.SetBool ("w_stop",true);
				anim.SetBool ("walk", false);
			} else if(timer_w_attack > 0){
				timer_w_attack -= Time.deltaTime;
			}

		}else if (Input.GetMouseButton(1)){
			
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
=======

>>>>>>> 710a951727f91ce211db816c812bc01edeb77703
			//Enemy Detection (was the pointed target an Enemy? -> Attack STATE)
			if(Physics.Raycast(ray, out hitCheck, 100f)){
				if(hitCheck.collider.gameObject.tag.Equals("Enemy")){
					enemy = hitCheck.collider.gameObject;
<<<<<<< HEAD
					
					distance_to_enemy = Vector3.Distance(player.transform.position,enemy.transform.position);
					
					if(distance_to_enemy < 4.75f)
						state = "Attack";
					
				}
			}
			
=======
					//Debug.Log(">> Enemy targeted -> state = Attack");
					state = "Attack";
					//Debug.DrawRay(transform.position, transform.forward, Color.green);
				}
			}

>>>>>>> 710a951727f91ce211db816c812bc01edeb77703
			//Boss Detection (was the pointed target an Boss? -> Attack STATE)
			if(Physics.Raycast(ray, out hitCheck, 100f)){
				if(hitCheck.collider.gameObject.tag.Equals("Boss")){
					enemy = hitCheck.collider.gameObject;
					//Debug.Log(">> Boss targeted -> state = Attack");
<<<<<<< HEAD
					distance_to_enemy = Vector3.Distance(player.transform.position,enemy.transform.position);
					
					if(distance_to_enemy < 4.75f)
						state = "Attack";
					//Debug.DrawRay(transform.position, transform.forward, Color.green);
=======
			
			//Bloque para atacar
			if (!isAttacking()) { // Si no esta haciendo un ataque
				if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.Mouse1)) { // Al puslar espacio
					// Si ha pasado mas de 1 segundo del inicio del ultimo ataque
					if (canAttack()) {
                        enableCollider();   // Si estamos en el cofre activamos de nuevo el SphereCollider
						attackAnim ();
						rotateToMouse ();
						atk_script.makeAttack();
                        atk_time = Time.time;
						music.play_Player_Sword_Attack ();
					}
>>>>>>> Devel
				}

<<<<<<< HEAD
=======
					state = "Attack";
					//Debug.DrawRay(transform.position, transform.forward, Color.green);
				}
			}

			// If is outside the inventory region the character recalculate path
			if(walk)
				//Once we've noticed if the target is an enemy or targetPosition, lets repath
				computePath(targetPosition); //Start a new path to the targetPosition, return the result to the OnPathComplete function
		}

>>>>>>> 710a951727f91ce211db816c812bc01edeb77703

	}


	void tracking(){
		//Direction to the next waypoint
		Vector3 dir = (path.vectorPath[currentWaypoint]-transform.position).normalized;
		dir *= speed * Time.fixedDeltaTime;
		bool enemy_closer = false;

		//END OF PATH CONDITIONS:
		if(state.Equals("Walk") && (currentWaypoint == path.vectorPath.Count-1)) {
			done = true;

			//Debug.Log ("@tracking -> targetPosition reached!!");
			anim.SetBool ("walk", false);
			anim.SetBool("w_stop", true);
			state = "None";

		} else if(state.Equals("Attack") && enemy!=null){
			
			float distance_to_enemy = Vector3.Distance(player.transform.position, enemy.transform.position);
<<<<<<< HEAD
=======
			//Debug.Log("@tracking -> Distance To Enemy:" + distance_to_enemy);
>>>>>>> 710a951727f91ce211db816c812bc01edeb77703
			
			if (distance_to_enemy <= MIN_ENEMY_DIST ) {
				enemy_closer = true;
			}
		}
		
		if (!done) {
			//Debug.Log ("Entra -> DONE is False");
			if(state.Equals ("Attack") && enemy_closer){
				//Debug.Log ("ENEMY IN RANGE!!");

				done = true;
				attack();
				state = "None";

			} else {
				controller.Move (dir);
=======
>>>>>>> Devel
			}
		}
<<<<<<< HEAD
		
		float nextWaypointDistance = defaultNextWaypointDistance;
		if(currentWaypoint == path.vectorPath.Count -1) nextWaypointDistance = 0f;

		//Check if we are close enough to the next waypoint
		//If we are, proceed to follow the next waypoint
		if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
			currentWaypoint++;
			return;
<<<<<<< HEAD
=======
	}
	
	private void enableCollider(){
        if (chestHit){
            chestHit = false;
            atk_range.enabled = true;
        }
    }

	public void rotateToMouse () {
		if (Time.time - atk_time > 0.5f) {
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			targetPoint = ray.GetPoint(hitdist);
			targetPoint.y = transform.position.y;
			Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
			transform.rotation = targetRotation;
>>>>>>> Devel
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
<<<<<<< HEAD
		anim.SetBool ("w_attack", true);
		//anim.SetBool ("run_attack", true);


		if (Time.time > attackTime && enemy != null) {

			float distance_to_enemy = Vector3.Distance(player.transform.position, enemy.transform.position);
			//ENEMY DAMAGE
			if(enemy.tag == "Boss" && distance_to_enemy <= 7f) enemy.GetComponent<Movement>().setDamage( character.computeDamage() );
			else if(enemy.tag == "Enemy" && distance_to_enemy <= 5f) enemy.GetComponent<Movement_graveler>().setDamage( character.computeDamage() );

			attackTime = Time.time + 1.0f;
			if(music != null) {
				music.SendMessage("play_Player_Sword_Attack");
			}
		}

=======
		}

	}


	public void attack(){
		//Debug.Log ("@attack!!");
		Vector3 p = player.transform.position;

		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(p - transform.position), rotationSpeed * Time.deltaTime);

		anim.SetBool ("w_stop",true);
		anim.SetBool ("walk", false);
		anim.SetBool ("attack", true);
		anim.SetBool ("w_attack", true);
		//anim.SetBool ("run_attack", true);


		if (Time.time > attackTime && enemy != null) {

			//ENEMY DAMAGE
			if(enemy.tag == "Boss") enemy.GetComponent<Movement>().setDamage( character.computeDamage() );
			else if(enemy.tag == "Enemy") enemy.GetComponent<Movement_graveler>().setDamage( character.computeDamage() );

			attackTime = Time.time + 1.0f;
			if(music != null) {
				music.SendMessage("play_Player_Sword_Attack");
			}
		}
	}


	public void death(){
	
		anim.SetBool ("w_stop",true);
		anim.SetBool ("walk", false);
		anim.SetBool ("attack", false);
		anim.SetBool ("w_attack", false);
		anim.SetBool ("death", true);
		walk = false;
	}
	// ====================================================
	// UPDATE TO INVENTORY FIX

	public void dontWalk(){

		//Debug.Log ("@don't walk!!");

		anim.SetBool ("w_stop",true);
		anim.SetBool ("walk", false);
		anim.SetBool ("attack", false);
		anim.SetBool ("w_attack", false);

		walk = false;
>>>>>>> 710a951727f91ce211db816c812bc01edeb77703
	}

	public void Walk(){
		walk = true;
	}

<<<<<<< HEAD
	public void death(){
=======
		atk_time = Time.time;
	}
>>>>>>> Devel
	
	public void attack() {
		anim.SetBool ("attack", true);
		atk_time = Time.time;
	}
	
	public void death() {
		dieAnim ();
	}
	
	public void dieAnim () {
		anim.SetBool ("dead", true);
		dead = true;
	}
<<<<<<< HEAD

=======
>>>>>>> 710a951727f91ce211db816c812bc01edeb77703
	// ====================================================


	private void computePath(Vector3 targetPosition){

		if (Time.time - lastRepath > repathRate && seeker.IsDone ()) {
			if (!done) {
				// Calculamos la ruta
				seeker.StartPath (transform.position, targetPosition, OnPathComplete);
			}

		}
=======
>>>>>>> Devel
	
	public void stopAttackAnim() {
		anim.SetBool ("attack", false);
	}

	void OnControllerColliderHit(ControllerColliderHit hit){
		if (!chestHit && hit.gameObject.tag == "Chest") {
            //Debug.Log("chest collision detected");
            chestHit = true;
            atk_range.enabled = false;

            destinationPosition = transform.position;
		}
	}
}