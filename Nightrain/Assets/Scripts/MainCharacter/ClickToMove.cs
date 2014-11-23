using UnityEngine;
using System.Collections;
using Pathfinding;

public class ClickToMove : MonoBehaviour {
 
 public Transform target;
 public Vector3 targetPosition;
 
//    private Transform myTransform;
 
 private Seeker seeker;
 private CharacterController controller;

 //The calculated path
 public Path path;
 
 //The AI's speed per second
 private float speed = 5;
 
 //The max distance from the AI to a waypoint for it to continue to the next waypoint
 private float defaultNextWaypointDistance = 2;

 //The waypoint we are currently moving towards
 private int currentWaypoint = 0;
 
 
 public void Start () {
	 
	 //Get a reference to the Seeker component we added earlier
	 seeker = GetComponent<Seeker>();
	 controller = GetComponent<CharacterController>();
	 
	 
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
	 
	 targetPosition = target.transform.position;    
	 //Start a new path to the targetPosition, return the result to the OnPathComplete function
	 seeker.StartPath (transform.position,targetPosition, OnPathComplete);
	 
	 }
	 
	 if (path == null) {
		 //We have no path to move after yet
		 return;
	 }
	 
	 if (currentWaypoint == path.vectorPath.Count) {
		 Debug.Log ("End Of Path Reached");
		 return;
	 }
	 
	 //Direction to the next waypoint
	 Vector3 dir = (path.vectorPath[currentWaypoint]-transform.position).normalized;
	 dir *= speed * Time.fixedDeltaTime;
	 controller.Move (dir);
	 
	 transform.LookAt(new Vector3(path.vectorPath[currentWaypoint].x, transform.position.y, path.vectorPath[currentWaypoint].z));
	 
	 float nextWaypointDistance = defaultNextWaypointDistance;
	 if(currentWaypoint == path.vectorPath.Count -1)
		 nextWaypointDistance = 0f;
	 
	 
	 //Check if we are close enough to the next waypoint
	 //If we are, proceed to follow the next waypoint
	 if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
		 currentWaypoint++;
		 
		 return;
	 }
 }
 
}