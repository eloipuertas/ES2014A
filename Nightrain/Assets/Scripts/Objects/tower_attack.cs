using UnityEngine;
using System.Collections;

public class tower_attack : MonoBehaviour {
	// Arrow
	public GameObject ArrowPrefab;
	
	private GameObject player;
	// intervalo tiempo entre ataques
	public float interval = 2.0f;
	float timeLeft = 0.0f;
	
	// rango de vision de la torre
	public float range = 10.0f;
	
	
	void Update() {
		
		//decrementar el tiempo en cada update
		timeLeft -= Time.deltaTime;
		//atacar otra vez si a pasado el tiempo del intervalo
		if (timeLeft <= 0.0f){
			//buscar el player
			this.player = GameObject.FindGameObjectWithTag("Player");
			
			if(this.player != null && player.transform.position.z > 25f) {        
				
				//Si esta dentro del rango de vision, atacaremos y no hay nada entre medio
				if (Vector3.Distance(transform.position, this.player.transform.position) <= range) {
					//creamos una bala
					Instantiate(ArrowPrefab.gameObject, transform.position, Quaternion.identity);
					//if(g != null) Bala b = g.GetComponent<Bala>();

					//le assignamos como destino, la posicion del player 
					this.audio.Play();
					//b.setDestination(this.player.transform);
					
					timeLeft = interval;
				}
			}
		}
	}
}