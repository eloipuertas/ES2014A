using UnityEngine;
using System.Collections;

public class CameraGoal : MonoBehaviour {
	
	public Transform target;					//Target position
	private Transform _myTransform; 			//Camera position

	//Adaptar coordenadas de inicio segun punto deseado
	private float coordX = 110.0f;
	private float coordY = 25.0f;
	private float coordZ = 50.0f;
	private float timeLeft = 7.0f;


	// Use this for initialization
	void Start () {
		GameObject.FindGameObjectWithTag ("Player").GetComponent<ClickToMove>().enabled = false;
		GameObject.FindGameObjectWithTag ("Player").GetComponent<Skill_Controller>().enabled = false;
		CameraSetUp ();
	}
	
	private void CameraSetUp(){
		_myTransform = transform;
		_myTransform.position = new Vector3 (coordX, coordY, coordZ);
		//Situar las coordenadas segun la meta
		_myTransform.LookAt (new Vector3 (coordX, coordY, 308));
	}

	void LateUpdate(){
		timeLeft -= Time.deltaTime;
		//Avanzamos en coordenadas de z
		if (coordZ < 300) {
			coordZ += Time.deltaTime * 25;
		}

		_myTransform.position = new Vector3 (coordX, coordY, coordZ);
		//Situar las coordenadas segun la meta
		_myTransform.LookAt (new Vector3 (110, 5, 308));
		if(timeLeft < 2){
			Destroy(GameObject.FindGameObjectWithTag("FireWall"));
		} 

		if (timeLeft < 0) {
			GameObject.FindGameObjectWithTag ("Player").GetComponent<ClickToMove>().enabled = true;
			GameObject.FindGameObjectWithTag ("Player").GetComponent<Skill_Controller>().enabled = true;
			GameObject.FindGameObjectWithTag ("CameraGoal").SetActive(false);
		}
	}
}