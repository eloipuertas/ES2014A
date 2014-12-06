using UnityEngine;
using System.Collections;

public class tower_attack : MonoBehaviour {
	// Arrow
	public Bala ArrowPrefab;
	
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
		if (timeLeft <= 0.0f) {
			//buscar el player
			this.player = GameObject.FindGameObjectWithTag("Player");
			if (this.player != null) {        
				//Si esta dentro del rango de vision, atacaremos
				//Debug.Log ("range:"+Vector3.Distance(transform.position, this.player.transform.position));
				if (Vector3.Distance(transform.position, this.player.transform.position) <= range) {
					//creamos una bala
					GameObject g = (GameObject)Instantiate(ArrowPrefab.gameObject, transform.position, Quaternion.identity);
					Bala b = g.GetComponent<Bala>();

					//le assignamos como destino, la posicion del player     
					b.setDestination(this.player.transform);

					/*if(this.player.transform.position.z>0 && this.player.transform.position.z<70){
						print ("rotar z");
						b.transform.Rotate(90, 0, 0);
					}*/
					// Para lanzar la bola a donde apunta con el mouse
					Plane playerPlane = new Plane(Vector3.up, player.transform.position);
					Ray ray = Camera.main.ScreenPointToRay(player.transform.position);
					float hitdist = 0.0f;
					
					if (playerPlane.Raycast(ray, out hitdist)) {
						Vector3 targetPoint = ray.GetPoint(hitdist);
						Vector3 destinationPosition = ray.GetPoint(hitdist);
						Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
						b.transform.rotation = targetRotation;
					}
					//b.transform.Rotate(0, -90, 0);
					//b.transform.forward =Vector3.Slerp(b.transform.forward, b.rigidbody.velocity.normalized, Time.deltaTime);
					//resetemos el timeLeft
					timeLeft = interval;
				}
			}
		}
	}
}