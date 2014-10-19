using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	
	public Transform target;					//Target position
	private Transform _myTransform; 			//Camera position 
	
	private float distance;
	private float height;
	private Vector3 scaleTarget;
	
	
	// Use this for initialization
	void Start () {
		
		if(target == null){ //Checking if we have a target
			Debug.LogWarning("We do not have a target for the camera.");	
		}
		else{
			//Set up the camera
			CameraSetUp();													
		}		
	}
	
	private void CameraSetUp(){
		scaleTarget = target.localScale;
		_myTransform = transform;

		//Set height and distance
		height = scaleTarget[1] * 30.0f;
		distance = scaleTarget [2] * 20.0f;
	}
	
	void LateUpdate(){
		//If a target is setted to the camera in order to follow it 
		if(target != null){
			//We can use the mouse wheel to zoom
			height += Input.GetAxis("Mouse ScrollWheel") * -27;
			distance += Input.GetAxis("Mouse ScrollWheel") * -20;
			
			//Limits zoom
			if(height < scaleTarget[1] * 15.0f) height = scaleTarget[1] * 15.0f;
			if(height > scaleTarget[1] * 60.0f) height = scaleTarget[1] * 60.0f;
			if(distance < scaleTarget [2] * 10.0f) distance = scaleTarget [2] * 10.0f;
			if(distance > scaleTarget [2] * 40.0f) distance = scaleTarget [2] * 40.0f;

			Debug.LogWarning(scaleTarget);

			//In each frame we get the position of the target and we update the position camera
			_myTransform.position = new Vector3(target.position.x + distance,
			                                    target.position.y + height,
			                                    target.position.z - distance);
			//We must update LookAt because the target can change position
			_myTransform.LookAt(target);
		}
	}
}