using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	private float health = 100;
	private string state;
	private float damageAtack = 5;
	
	private float rotationSpeed = 1.0f;
	private int moveSpeed = 20;
	
	// Los nombres de los tres puntos que estan distribuidos por el mapa
	string[] points = {"Point1", "Point2", "Point3"};
	int j = 0;

	GameObject player;
	Transform player_transform;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		player_transform = player.transform;
	}
	
	// Update is called once per frame
	void Update () {
		// Cosas a determinar por el programador de IA
		float distance_to_player = Vector3.Distance(player_transform.position,transform.position);
		Debug.Log (distance_to_player);
		if(distance_to_player < 15){
			atack();
		}else{
			seguirPuntos();
		}
	}

	// Metodo que hace que el personaje vaya uno a uno a los tres puntos del mapa
	void seguirPuntos(){
		//Debug.Log ("caminar");
		GameObject punto = GameObject.Find(points[j]);
		// Calculamos la distancia entre nuestra posicion y el punto del mapa
		float distancia = Vector3.Distance(transform.position, punto.transform.position);
		// Si ya hemos llegado al punto, cambiamos la i para ir al siguiente
		if (distancia <= 1) {
			if (j < 2) {
					j++;
			} else {
					j = 0;
			}
		}
		// Rotamos hacia la direccion
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(punto.transform.position - transform.position), rotationSpeed * Time.deltaTime);
		// Transladamos el NPC hacia el punto
		transform.position += transform.forward * moveSpeed * Time.deltaTime;
	}


	// Metodo que rota el NPC unos determinados grados
	void rotar(int degrees){
		Quaternion newRotation = Quaternion.AngleAxis (degrees, Vector3.up);
		transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, rotationSpeed);
	}


	void atack(){
		//Debug.Log ("atack");
	}


	public void getDamage(float damage){
		health -= damage;
		if (health < 1) {
			Debug.Log ("NPC muerto");
			Destroy (gameObject);
		}
	}
}