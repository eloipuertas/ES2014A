
using UnityEngine;
using System.Collections;
using Pathfinding;

public class ClickToMove : MonoBehaviour {
 
	private Vector3 targetPosition;
	private Vector3 targetPoint;
	private Plane playerPlane;
	private Animator anim;

	private Seeker seeker;
	public float repathRate = 10.0f;
	private float lastRepath = -9999;
	private bool done;

	private CharacterController controller;

	//The calculated path
	public Path path;

	//The AI's speed per second
	public float speed = 5;


	//The max distance from the AI to a waypoint for it to continue to the next waypoint
	private float defaultNextWaypointDistance = 2;

	//The waypoint we are currently moving towards
	private int currentWaypoint = 0;

	string[] states = {"Walk", "FollowEnemy", "Attack"};
	private string state = "None";

	public void Start () { 
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


	public void FixedUpdate () {
	 
		if (Input.GetMouseButton(0)) {
			done = false;

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			float hitdist;

			//deteccion movimiento
			if (playerPlane.Raycast(ray, out hitdist)){
				targetPoint = ray.GetPoint(hitdist);
				targetPosition = ray.GetPoint(hitdist);
				//Debug.Log ("targetPosition refreshed");
			}

			//Start a new path to the targetPosition, return the result to the OnPathComplete function
			computePath(targetPosition);

			//seeker.StartPath (transform.position, targetPosition, OnPathComplete);
		}

		//Direction to the next waypoint

		if(path != null){
			Vector3 dir = (path.vectorPath[currentWaypoint]-transform.position).normalized;
			dir *= speed * Time.fixedDeltaTime;

			if (currentWaypoint == path.vectorPath.Count-1) {
				done = true;
				state = "None";
				anim.SetBool ("walk", false);
				anim.SetBool("w_stop", true);
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
	}




	private void computePath(Vector3 targetPosition){

		if (Time.time - lastRepath > repathRate && seeker.IsDone ()) {
			if (!done) {
				// Calculamos la ruta
				seeker.StartPath (transform.position, targetPosition, OnPathComplete);

				state = "Walk";
				if(anim.GetBool("walk")==false){
					anim.SetBool ("w_stop",false);
					anim.SetBool ("walk", true);
				}
			}
		}
	
		if (path == null) {
			//We have no path to move after yet
			return;
		}
	 
		if (currentWaypoint == path.vectorPath.Count-1) {
			//Debug.Log ("End Of Path Reached");

			done = true;
			state = "None";
			anim.SetBool ("walk", false);

			return;
		}
	}
 
}