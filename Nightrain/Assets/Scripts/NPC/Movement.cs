using UnityEngine;
using System.Collections;
using Pathfinding;

public class Movement : MonoBehaviour {
	
	//##############################
	//Atributos personaje
	public float moveSpeed = 5; 
	public float health = 75;
	public float max_health = 75;
	public float defense = 5;
	public float attackPower = 3;
	public int experience = 100;
	//##############################
	
	
	//##############################
	//Pathfinding
	public Path path;
	private Seeker seeker;
	public float repathRate = 10.0f;
	private float lastRepath = -9999;
	public float nextWaypointDistance = 3;
	private int currentWaypoint = 1;
	private bool hecho = false;
	public float speed = 100;
	//#############################
	
	
	private string state = "None";
	private string difficulty;
	//string[] states = {"Walk", "Find", "Attack", "Dead"};
	private float rotationSpeed = 10.0f;
	private float attackTime = 3.0f;
	
	// Los nombres de los tres puntos que estan distribuidos por el mapa
	string[] points = {"Point1", "Point2", "Point3"};
	int j = 0;
	
	private NPCAttributes npcAttributes;
	private GameObject player;
	private Transform player_transform;
	private Animator anim;
	//private GameObject NPCbar;
	
	// Effect to die
	private static GameObject explosion;
	private float explosion_delay = 3.5f;
	private bool activateEffect = true; 
	
	
	private Music_Engine_Script music;
	
	private CharacterController controller;

	private TrophyEngine trofeos;

	private bool isDead = false;
	
	
	// Metodo que se llama cuando una ruta ha sido calculada
	public void OnPathComplete (Path p) {
		p.Claim (this);
		if (!p.error) {
			if (path != null) path.Release (this);
			path = p;
			currentWaypoint = 1;
		} else {
			p.Release (this);
			Debug.Log ("No se puede llegar a este punto de destino: "+p.errorLog);
		}
	}
	
	
	void Awake(){
		this.npcAttributes = new NPCAttributes (health, max_health, attackPower, defense, moveSpeed, experience);
		//print ("Experiencia: " + this.npcAttributes.getExperience ());
		
	}
	
	// Use this for initialization
	void Start () {
		
		seeker = GetComponent<Seeker>();
		player = GameObject.FindGameObjectWithTag("Player");
		player_transform = player.transform;
		anim = GetComponent<Animator>();
		difficulty = PlayerPrefs.GetString ("Difficult");
		npcAttributes.setDificulty (difficulty);
		//this.NPCbar = GameObject.FindGameObjectWithTag("NPCHealth");
		
		this.trofeos = GameObject.FindGameObjectWithTag("Trofeos").GetComponent<TrophyEngine>();
		this.music = GameObject.FindGameObjectWithTag ("music_engine").GetComponent<Music_Engine_Script> ();
		
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!state.Equals("Dead")) {
			float distance_to_player = Vector3.Distance(player_transform.position,transform.position);
			if (distance_to_player < 8) {
				atack ();
			} else if (distance_to_player < 30) {
				perseguir ();
			} else {
				seguirPuntos ();
			}
		}else{
			anim.SetBool("a_walk", false);
			anim.SetBool("walk", false);
			anim.SetBool ("w_attack", false);
			anim.SetBool("a_death", true);
			anim.SetBool("w_death", true);
			anim.SetBool("death", true);
			
			explosion_delay -= Time.deltaTime;
			
			if(explosion_delay < 2.25f && activateEffect){
				explosion = Instantiate(Resources.Load<GameObject>("Prefabs/Effects/explosion")) as GameObject;
				explosion.transform.position = transform.position;
				explosion.transform.parent = transform;
				activateEffect = false;
			}else if(explosion_delay < 0){
				Destroy(gameObject);
			}
		}
	}
	
	
	// Metodo que hace que el personaje vaya uno a uno a los tres puntos del mapa
	void seguirPuntos(){
		
		//state = "Walk";
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
		
		// Calculamos el camino hacia el punto
		calcularPath(punto.transform.position);
		
		if (path != null){
			// En caso de llegar al final del camino
			if (currentWaypoint > path.vectorPath.Count)
				return; 
			
			
			Vector3 dir = (path.vectorPath[currentWaypoint]-transform.position).normalized;
			dir *= 0.1F * Time.fixedDeltaTime;
			
			
			controller.Move (dir);
			transform.LookAt (new Vector3 (path.vectorPath [currentWaypoint].x, transform.position.y, path.vectorPath [currentWaypoint].z));
			
			// Rotamos y trasladamos hacia el siguente punto de la ruta
			//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(path.vectorPath[currentWaypoint] - transform.position), rotationSpeed * Time.deltaTime);
			//transform.position += transform.forward * moveSpeed * Time.deltaTime;
			// Incrementamos para poder ir al siguiente punto de la ruta calculada
			currentWaypoint++;
		}
	}
	
	void perseguir(){
		if (!state.Equals("Find")) {
			if(music != null) music.play_Golem_Agresive ();
		}
		state = "Find";
		anim.SetBool("w_attack", false);
		anim.SetBool("a_walk", true);
		anim.SetBool ("walk", true);
		Vector3 p= player_transform.position ;
		p.y = transform.position.y;
		
		// Calculamos la ruta hacia el personaje principal
		calcularPath(p);
		
		if (path != null){
			// En caso de llegar al final del camino
			if (currentWaypoint > path.vectorPath.Count)
				return; 
			
			
			Vector3 dir = (path.vectorPath[currentWaypoint]-transform.position).normalized;
			dir *= 0.1F * Time.fixedDeltaTime;
			
			
			controller.Move (dir);
			transform.LookAt (new Vector3 (path.vectorPath [currentWaypoint].x, transform.position.y, path.vectorPath [currentWaypoint].z));
			
			// Rotamos y trasladamos hacia el siguente punto de la ruta
			//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(path.vectorPath[currentWaypoint] - transform.position), rotationSpeed * Time.deltaTime);
			//transform.position += transform.forward * moveSpeed * Time.deltaTime;
			// Incrementamos para poder ir al siguiente punto de la ruta calculada
			currentWaypoint++;
		}
	}
	
	// Metodo que rota el NPC unos determinados grados
	void rotar(int degrees){
		Quaternion newRotation = Quaternion.AngleAxis (degrees, Vector3.up);
		transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, rotationSpeed);
	}
	
	void atack(){
		state = "Attack";
		Vector3 p= player_transform.position;
		p.y = transform.position.y;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(p - transform.position), rotationSpeed * Time.deltaTime);
		anim.SetBool("a_walk", false);
		anim.SetBool("walk", false);
		//p.y -= 5f;
		anim.SetBool ("w_attack", true);
		//p.y += 5f;
		if (Time.time > attackTime) {
			player.GetComponent<CharacterScript>().setDamage((int) npcAttributes.getAttackPower());
			attackTime = Time.time + 1.0f;
			if(music != null) {
				music.play_Golem_Agresive();
				music.play_Golem_Attack();
			}
		}
	}
	
	
	public void setDamage(float damage){
		npcAttributes.setDamage (damage);
		//this.NPCbar.renderer.material.SetFloat("_Cutoff", 1 - (this.health/this.max_health));
		if (npcAttributes.getHealth() < 1 && !isDead) {
			this.isDead = true;
			state = "Dead";
			trofeos.TrophyGolemLava();
			player.GetComponent<CharacterScript>().setEXP(npcAttributes.getExperience());
			this.collider.enabled = false;
			//Debug.Log ("NPC muerto");
			anim.SetBool("a_walk", false);
			anim.SetBool("walk", false);
			anim.SetBool ("w_attack", false);
			anim.SetBool("a_death", true);
			
		}
	}
	
	
	
	/*public void setHealth(float health){
		this.health = health;
		this.max_health = health;
	}*/
	
	
	public NPCAttributes getAttributes(){
		return npcAttributes;
	}
	
	
	private void calcularPath(Vector3 punto){
		if (Time.time - lastRepath > repathRate && seeker.IsDone ()) {
			if (!hecho) {
				// Calculamos la ruta
				seeker.StartPath (transform.position, punto, OnPathComplete);
				hecho = true;
			}
		}
		if (path == null) {
			// No se ha podido calcular ninguna ruta
			return;
		}
		if (currentWaypoint > path.vectorPath.Count)
			return; 
		if (currentWaypoint == path.vectorPath.Count) {
			//Debug.Log ("Se ha llegado al final de la ruta");
			currentWaypoint++;
			hecho = false;
			return;
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