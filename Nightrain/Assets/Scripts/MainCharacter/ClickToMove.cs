using UnityEngine;
using System.Collections;
using Pathfinding;

public class ClickToMove : MonoBehaviour {
	private Animator anim;
	private GameObject player;
	private Vector3 targetPosition;
	private Vector3 targetPoint;
	private Plane playerPlane;
	private CharacterController controller;
	private CharacterScript character;

	/* == Enemy detection ================ */
	private GameObject enemy;
	private RaycastHit getObjectScene;
	private RaycastHit hitCheck;
	private Component music;
	private float rotationSpeed = 10.0f;
	private float attackTime = 0.90f;
	private float MIN_ENEMY_DIST =  4.6f;
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

	private float timer_w_attack = 1f;


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
	}


	// Update is called once per frame
	void Update () {

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

			if(anim.GetBool("w_attack") && timer_w_attack <= 0f){
				//Debug.Log ("@Udate: w_attack true -> stop WATTACK/WALK");
				anim.SetBool("w_attack", false);
				anim.SetBool ("w_stop",true);
				anim.SetBool ("walk", false);
			}else if(anim.GetBool("attack")){
				//Debug.Log ("@Udate: attack true -> stop ATTACK");
				anim.SetBool("attack", false);
			}
		}	
	}


	public void FixedUpdate () {

		Ray ray;
		float hitdist;
		float distance_to_enemy;

		if (Input.GetMouseButton(0)) {

			done = false; //assume every click is a new target -> done flag restart
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			//deteccion movimiento
			if (playerPlane.Raycast(ray, out hitdist)){
				targetPoint = ray.GetPoint(hitdist);
				targetPosition = ray.GetPoint(hitdist);
				//Debug.Log (">> NEW target @("+ targetPosition.x + "," + targetPosition.y +")");
				if(walk){
					state = "Walk";
					Walk ();
				}
			}

			if (Input.GetMouseButton(1)){	

				ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				
				//Enemy Detection (was the pointed target an Enemy? -> Attack STATE)
				if(Physics.Raycast(ray, out hitCheck, 100f)){
					if(hitCheck.collider.gameObject.tag.Equals("Enemy")){
						enemy = hitCheck.collider.gameObject;
						
						distance_to_enemy = Vector3.Distance(player.transform.position,enemy.transform.position);
						
						if(distance_to_enemy < 4.75f)
							state = "Attack";
						
					}
				}
				
				//Boss Detection (was the pointed target an Boss? -> Attack STATE)
				if(Physics.Raycast(ray, out hitCheck, 100f)){
					if(hitCheck.collider.gameObject.tag.Equals("Boss")){
						enemy = hitCheck.collider.gameObject;
						//Debug.Log(">> Boss targeted -> state = Attack");
						distance_to_enemy = Vector3.Distance(player.transform.position,enemy.transform.position);
						
						if(distance_to_enemy < 4.75f)
							state = "Attack";
						//Debug.DrawRay(transform.position, transform.forward, Color.green);
					}
				}
			}

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
			
			//Enemy Detection (was the pointed target an Enemy? -> Attack STATE)
			if(Physics.Raycast(ray, out hitCheck, 100f)){
				if(hitCheck.collider.gameObject.tag.Equals("Enemy")){
					enemy = hitCheck.collider.gameObject;
					
					distance_to_enemy = Vector3.Distance(player.transform.position,enemy.transform.position);
					
					if(distance_to_enemy < 4.75f)
						state = "Attack";
					
				}
			}
			
			//Boss Detection (was the pointed target an Boss? -> Attack STATE)
			if(Physics.Raycast(ray, out hitCheck, 100f)){
				if(hitCheck.collider.gameObject.tag.Equals("Boss")){
					enemy = hitCheck.collider.gameObject;
					//Debug.Log(">> Boss targeted -> state = Attack");
					distance_to_enemy = Vector3.Distance(player.transform.position,enemy.transform.position);
					
					if(distance_to_enemy < 4.75f)
						state = "Attack";
					//Debug.DrawRay(transform.position, transform.forward, Color.green);
				}
			}
		}else if(Input.GetMouseButtonUp(1))
			this.timer_w_attack = 1f;

		// If is outside the inventory region the character recalculate path
		if(walk)
			//Once we've noticed if the target is an enemy or targetPosition, lets repath
			computePath(targetPosition); //Start a new path to the targetPosition, return the result to the OnPathComplete function
	


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
			}
			transform.LookAt (new Vector3 (path.vectorPath [currentWaypoint].x, transform.position.y, path.vectorPath [currentWaypoint].z));
		}
		
		float nextWaypointDistance = defaultNextWaypointDistance;
		if(currentWaypoint == path.vectorPath.Count -1) nextWaypointDistance = 0f;

		//Check if we are close enough to the next waypoint
		//If we are, proceed to follow the next waypoint
		if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
			currentWaypoint++;
			return;
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

			float distance_to_enemy = Vector3.Distance(player.transform.position, enemy.transform.position);
			//ENEMY DAMAGE
			if(enemy.tag == "Boss" && distance_to_enemy <= 7f) enemy.GetComponent<Movement>().setDamage( character.computeDamage() );
			else if(enemy.tag == "Enemy" && distance_to_enemy <= 5f) enemy.GetComponent<Movement_graveler>().setDamage( character.computeDamage() );

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
	}

	public void Walk(){
		walk = true;
	}

	// ====================================================


	private void computePath(Vector3 targetPosition){

		if (Time.time - lastRepath > repathRate && seeker.IsDone ()) {
			if (!done) {
				// Calculamos la ruta
				seeker.StartPath (transform.position, targetPosition, OnPathComplete);
			}

		}
	
		if (path == null) {
			//We have no path to move after yet
			return;
		}
	 	
		if (currentWaypoint == path.vectorPath.Count-1) {
			done = true;
			return;
		}
	}


	void OnControllerColliderHit(ControllerColliderHit hit){
		if (hit.gameObject.tag == "Chest" && state == "Walk") {
			//Debug.Log ("@onControllerColliderHit " + hit.gameObject.tag + " -> STOP");
			dontWalk ();
			state = "None";
		}
	}

	
}