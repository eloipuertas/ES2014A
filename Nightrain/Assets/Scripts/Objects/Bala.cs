using UnityEngine;
using System.Collections;

public class Bala : MonoBehaviour {
	// fly speed
	public float speed = 10.0f;
	private GameObject player;
	public int Damage = 10;

	//Inicializar destino, se le pasara como parametro desde tower_attack
	Transform destination;    
	
	void Start () {
			player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update () {
		//tiramos la bala hacia el destino (player)
		float stepSize = Time.deltaTime * speed;
		transform.position = Vector3.MoveTowards(transform.position, destination.position, stepSize);
		
		//Debug.Log ("BALA");
		// reached?
		if (transform.position.Equals (destination.position)) {
			//decrementamos la vida del player
			player.GetComponent<CharacterScript>().setDamage(Damage);
			// destroy bala
			Destroy (gameObject);
		}
	}


public void setDestination(Transform v) {
		destination = v;
		Debug.Log (destination);
}
}