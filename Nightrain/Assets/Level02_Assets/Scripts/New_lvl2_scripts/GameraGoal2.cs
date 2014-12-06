using UnityEngine;
using System.Collections;

public class GameraGoal2 : MonoBehaviour {

	public Transform target;					//Target position
	private Transform _myTransform; 			//Camera position
	
	//Adaptar coordenadas de inicio segun punto deseado
	private float coordX = 78.0f;
	private float coordY = 15.0f;
	private float coordZ = -20.0f;
	private float timeLeft = 9.0f;
	
	
	// Use this for initialization
	void Start () {
		CameraSetUp ();
	}
	
	private void CameraSetUp(){
		_myTransform = transform;
		_myTransform.position = new Vector3 (coordX, coordY, coordZ);
		//Situar las coordenadas segun la meta
		_myTransform.LookAt (new Vector3 (coordX, coordY, 310));
	}
	
	void LateUpdate(){
		timeLeft -= Time.deltaTime;
		if (timeLeft > 3) {

			//Avanzamos en coordenadas de z
			if (coordZ < 300) {
				coordZ += Time.deltaTime * 50;
			}
		
			_myTransform.position = new Vector3 (coordX, coordY, coordZ);
			//Situar las coordenadas segun la meta
			_myTransform.LookAt (new Vector3 (78, 10, 310));
		}

		if(timeLeft < 7){
			Destroy(GameObject.FindGameObjectWithTag("FireWall"));
		} 
		
		if (timeLeft < 0) {
			GameObject.FindGameObjectWithTag ("CameraGoal").SetActive(false);
		}
	}
}
