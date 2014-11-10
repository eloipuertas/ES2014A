using UnityEngine;
using System.Collections;

public class getWeapon : MonoBehaviour {
	private GameObject character;
	public GameObject tapa;
	public GameObject arma1;
	public GameObject arma2;
	public int pos_y;
	private float AngleX = -90.0f;
	private float targetValue = 0.0f;
	private float currentValue = 0.0f;
	private float easing = 0.05f;
	private bool first = true;

	// ========================= COLISION CON Player ==================================
	void Start(){
		arma1.SetActive(false);
		arma2.SetActive(false);
	}
	void OnTriggerEnter (Collider other){
		if(other.gameObject == GameObject.FindGameObjectWithTag ("Player")){
			targetValue = AngleX;
			currentValue = 0;
			first=true;
		}
	}
	void OnTriggerExit (Collider other) {
		currentValue = AngleX;
		targetValue = 0.0f;
		arma1.SetActive(false);
		first=false;
	}
	void Update(){
		currentValue = currentValue + (targetValue - currentValue) * easing;
		tapa.transform.rotation = Quaternion.identity; // set rotation to zero
		tapa.transform.Rotate(currentValue, pos_y, 0); // apply full Rotation
		if (currentValue<AngleX+20 && first==true){
			first=false;
			arma1.SetActive(true);
		}
	}
	
}