using UnityEngine;
using System.Collections;

public class Bala : MonoBehaviour {
	// fly speed
	public float speed = 10.0f;
	public int Damage = 10;

	//Inicializar destino, se le pasara como parametro desde tower_attack
	private Vector3 destination;
	private Vector3 playerPos;
	private GameObject player;
	private float distance;
	private float stepSize;
	private bool hit = false;

	
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerPos = player.transform.position;	
		destination = playerPos;
		stepSize = Time.deltaTime * speed;
	}

	void Update () {
		//tiramos la bala hacia el destino (player)
		playerPos = player.transform.position;
		destination = playerPos + new Vector3(0f,4f,0f);
		distance = Vector3.Distance (destination, this.gameObject.transform.position);

		if (destination != null){
			transform.position = Vector3.MoveTowards(transform.position, destination, stepSize);
			if(distance > 10.0f) transform.LookAt(player.transform.position);

			if (distance < 2.0f && !hit) {
				hit = true;
				player.GetComponent<CharacterScript>().setDamage(Damage);//decrementamos la vida del player
				this.gameObject.transform.parent = player.transform; //la dejamos pegada al player
				Destroy (gameObject,5.0f);
			}
		}
	}
}