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
	private float hit_dist = 2.0f;
	private float y_diff = 4f;
	private GameObject player_body;

	
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		player_body = GameObject.FindGameObjectWithTag ("player_body");
		playerPos = player.transform.position;	
		destination = playerPos;
		stepSize = Time.deltaTime * speed;
		if (PlayerPrefs.GetString ("Player").Equals ("hombre")) hit_dist = 3.0f;
	}

	void Update () {
		//tiramos la bala hacia el destino (player)

		y_diff = (float)(Random.Range (1, 4));

		playerPos = player.transform.position;
		destination = playerPos + new Vector3(0f,y_diff,0f);
		distance = Vector3.Distance (destination, this.gameObject.transform.position);

		if (destination != null){
			if(!hit) transform.position = Vector3.MoveTowards(transform.position, destination, stepSize);
			else stepSize = 0.0f;
			if(distance > 10.0f) transform.LookAt(player.transform.position);

			if (distance < hit_dist && !hit) {
				hit = true;
				player.GetComponent<CharacterScript>().setDamage(Damage);//decrementamos la vida del player
				this.gameObject.transform.parent = player_body.transform; //la dejamos pegada al player
				Destroy (gameObject,5.0f);
			}
		}
	}
}