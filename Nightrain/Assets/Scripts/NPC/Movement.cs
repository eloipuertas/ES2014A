using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	private float health = 100;
	private string state;
	private float damageAtack = 5;
	
	private float rotationSpeed = 1.0f;
	private int moveSpeed = 5;

	private Animator anim;
	private float attackTime;

	private float max_health = 100;

	private Music_Engine_Script music;
	private bool persiguiendo = false;
	
	// Los nombres de los tres puntos que estan distribuidos por el mapa
	string[] points = {"Point1", "Point2", "Point3"};
	int j = 0;
	
	GameObject player;
	Transform player_transform;

	private GameObject NPCbar;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		player_transform = player.transform;
		anim = GetComponent<Animator>();

		this.NPCbar = GameObject.FindGameObjectWithTag("NPCHealth");
		this.music = GameObject.FindGameObjectWithTag ("music_engine").GetComponent<Music_Engine_Script> ();
	}
	
	// Update is called once per frame
	void Update () {
		// Cosas a determinar por el programador de IA
		float distance_to_player = Vector3.Distance(player_transform.position,transform.position);

		if (distance_to_player < 10) {
			atack ();
		} else if (distance_to_player < 60) {
			perseguir();
		} else {
			seguirPuntos();
		}
	}

	//Return health
	public float getHealth(){
		return health;
	}
	
	// Metodo que hace que el personaje vaya uno a uno a los tres puntos del mapa
	void seguirPuntos(){
		anim.SetBool("w_attack", false);
		anim.SetBool("a_walk", true);
		anim.SetBool ("walk", true);
		GameObject punto = GameObject.Find(points[j]);
		// Calculamos la distancia entre nuestra posicion y el punto del mapa
		float distancia = Vector3.Distance(transform.position, punto.transform.position);
		// Si ya hemos llegado al punto, cambiamos la i para ir al siguiente
		if (distancia <= 15) {
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
	
	void perseguir(){
		if (persiguiendo == false) {
			music.play_Golem_Agresive ();
			persiguiendo = true;
		}
		anim.SetBool("w_attack", false);
		anim.SetBool("a_walk", true);
		anim.SetBool ("walk", true);
		Vector3 p= player_transform.position ;
		p.y = transform.position.y;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(p - transform.position), rotationSpeed * Time.deltaTime);
		transform.position += transform.forward * moveSpeed * 2 * Time.deltaTime;
	}
	
	
	// Metodo que rota el NPC unos determinados grados
	void rotar(int degrees){
		Quaternion newRotation = Quaternion.AngleAxis (degrees, Vector3.up);
		transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, rotationSpeed);
	}
	
	
	void atack(){
		Vector3 p= player_transform.position;
		p.y = transform.position.y;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(p - transform.position), rotationSpeed * Time.deltaTime);
		//Debug.Log("attack");
		anim.SetBool("a_walk", false);
		anim.SetBool("walk", false);
		p.y += 5f;
		anim.SetBool ("w_attack", true);
		p.y -= 5f;
		if (Time.time > attackTime) {
			// atacar(verificar colision con el player y enviarle via metodo que ha sido atacado)
			music.play_Golem_Agresive();
			music.play_Golem_Attack();
			player.GetComponent<CharacterScript>().setDamage(10);
			attackTime = Time.time + 1.0f;
		}
	}
	
	
	public void setDamage(float damage){
		health -= damage;
		this.NPCbar.renderer.material.SetFloat("_Cutoff", 1 - (this.health/this.max_health));
		if (health < 1) {
			Debug.Log ("NPC muerto");
			anim.SetBool("a_walk", false);
			anim.SetBool("walk", false);
			anim.SetBool ("w_attack", false);
			anim.SetBool("a_death", true);
		}
	}


	/*void OnTriggerEnter (Collider other){
		print("Tocado."); 
		if(other.gameObject == this.player){
			this.player.GetComponent<CharacterScript>().setDamage(10);
			print("Tocado."); 
			Debug.Log("Debug:Tocado.");
			System.Console.WriteLine("System:Tocado");	
		}	
	}*/
}