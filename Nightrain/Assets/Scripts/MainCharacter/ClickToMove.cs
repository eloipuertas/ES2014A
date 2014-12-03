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

	/* == Enemy detection ================ */
	private GameObject enemy;
	private RaycastHit getObjectScene;
	private RaycastHit hitCheck;
	private Component music;
	private float rotationSpeed = 10.0f;
	private float attackTime = 3.0f;
	/* =================================== */

	/* == PathFinding ==================== */
	private Seeker seeker;
	public float repathRate = 10.0f;
	private float lastRepath = -9999;
	private bool done;
	//The calculated path
	public Path path;
	/* =================================== */

	//The animation speed per second
	public float speed = 5;

	//The max distance from the AI to a waypoint for it to continue to the next waypoint
	private float defaultNextWaypointDistance = 2;

	//The waypoint we are currently moving towards
	private int currentWaypoint = 0;

	string[] states = {"Walk", "Chase", "Attack"};
	private string state = "None";


	public void Start () {
		player = GameObject.FindWithTag("Player");
		music = GameObject.Find("MusicEngine").GetComponent("Music_Engine_Script");

		//Get a reference to the Seeker component we added earlier
		seeker = GetComponent<Seeker>();

		playerPlane = new Plane(Vector3.up, transform.position);
		controller = GetComponent<CharacterController>();
		anim = GetComponent<Animator> ();
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

		if (!state.Equals ("Dead")) {

			if(state.Equals ("Walk")){
				tracking();
			} else if(state.Equals ("Attack")){
				tracking ();
			} else if(state.Equals ("None")){
				if (anim.GetCurrentAnimatorStateInfo(0).IsName("attack")) Debug.Log ("@Update: attack is True... let's wait until is finished");
				else{
					Debug.Log ("@Update: attack is supposed to be finished");
					anim.SetBool ("attack",false);
					anim.SetBool ("attack_stop", true);
				}

			}
		}
	
	}



	public void FixedUpdate () {

		if (Input.GetMouseButton(0)) {
			done = false;

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			float hitdist;

			//deteccion movimiento
			if (playerPlane.Raycast(ray, out hitdist)){
				targetPoint = ray.GetPoint(hitdist);
				targetPosition = ray.GetPoint(hitdist);
				Debug.Log (">> New targetPosition @("+ targetPosition.x + "," + targetPosition.y +")");
			}

			//Start a new path to the targetPosition, return the result to the OnPathComplete function
			state = "Walk";
			computePath(targetPosition);


			//Enemy Detection
			if(Physics.Raycast(ray, out hitCheck, 100f)){
				if(hitCheck.collider.gameObject.tag.Equals("Enemy")){
					enemy = hitCheck.collider.gameObject;
					Debug.Log(">> Enemy targeted -> state = Attack");
					state = "Attack";
					//Debug.DrawRay(transform.position, transform.forward, Color.green);
					//attack();
				} 
			}
			
		}


	}


	void tracking(){
		//Direction to the next waypoint
		Vector3 dir = (path.vectorPath[currentWaypoint]-transform.position).normalized;
		dir *= speed * Time.fixedDeltaTime;

		//END OF PATH CONDITIONS:
		if(state.Equals("Walk") && (currentWaypoint == path.vectorPath.Count-1)) {
			done = true;

			Debug.Log ("@tracking --> targetPosition reached!!");
			anim.SetBool ("walk", false);
			anim.SetBool("w_stop", true);
			state = "None";

		} else if(state.Equals("Attack")){
		
			float distance_to_enemy = Vector3.Distance(player.transform.position, enemy.transform.position);
			Debug.Log("@tracking -> Distance To Enemy:" + distance_to_enemy);
			
			if (distance_to_enemy <= 10) {
				attack();
				state = "None";
			} 

		}

		
		
		if (!done) {
			//Debug.Log ("Entra -> DONE is False");
			controller.Move (dir);
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



	void attack(){
		Debug.Log ("@attack!!");
		Vector3 p = player.transform.position;

		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(p - transform.position), rotationSpeed * Time.deltaTime);
		anim.SetBool ("walk", false);
		anim.SetBool("w_stop", true);
		anim.SetBool ("attack", true);
		
		if (Time.time > attackTime) {
			//player.GetComponent<CharacterScript>().setDamage((int) attackPower);
			attackTime = Time.time + 1.0f;
			if(music != null) {
				music.SendMessage("play_Player_Sword_Attack");
			}
		}	

	}


	private void computePath(Vector3 targetPosition){

		if (Time.time - lastRepath > repathRate && seeker.IsDone ()) {
			if (!done) {
				// Calculamos la ruta
				seeker.StartPath (transform.position, targetPosition, OnPathComplete);

				// walking animation transitions depending on attack boolean value.
				if(anim.GetBool("walk")==false){
					if(anim.GetBool ("attack")==true){
						anim.SetBool ("attack", false);
						anim.SetBool ("a_walk", true);
					}else{
						anim.SetBool ("walk", true);
						anim.SetBool ("w_attack", false);
						anim.SetBool ("w_stop",false);
					}
				}
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


	IEnumerator delayCoroutine(float secondsDelay){
		yield return new WaitForSeconds(secondsDelay);
	}
	
}