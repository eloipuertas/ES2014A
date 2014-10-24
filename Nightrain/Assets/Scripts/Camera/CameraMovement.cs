using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	
	public Transform target;					//Target position
	private Transform _myTransform; 			//Camera position 
	
	private float distance = 20.0f;
	private float height = 20.0f;
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
	}
	
	void LateUpdate(){
		//If a target is setted to the camera in order to follow it 
		if(target != null){
			//We can use the mouse wheel to zoom
			height += Input.GetAxis("Mouse ScrollWheel") * -10;
			distance += Input.GetAxis("Mouse ScrollWheel") * -10;
			
			if(height < 1.0f) height = 1.0f;
			if(distance < 2.0f) distance = 2.0f;
			
			//In each frame we get the position of the target and we update the position camera
			_myTransform.position = new Vector3(target.position.x + distance,
			                                    target.position.y + height * scaleTarget[1],
			                                    target.position.z - distance * scaleTarget[2]);
			//We must update LookAt because the target can change position
			_myTransform.LookAt(target);
		}
	}
}