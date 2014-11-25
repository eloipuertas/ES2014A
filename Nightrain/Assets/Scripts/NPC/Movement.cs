using UnityEngine;
using System.Collections;
using Pathfinding;


public class Movement : MonoBehaviour {
<<<<<<< HEAD

	//##############################
	//Atributos personaje
	public float moveSpeed = 5; 
	public float health = 75;
	public float max_health = 75;
	public float defense = 5;
	public float attackPower = 3;
	public int experience = 100;
	//##############################


	private NPCAttributes npcAttributes;
	private string state = "None";
	private string difficulty;

	string[] states = {"Walk", "Find", "Attack", "Dead"};
	private float rotationSpeed = 1.0f;
	private float attackTime = 3.0f;

=======
<<<<<<< HEAD

	//##############################
	//Atributos personaje
	private float moveSpeed = 2; 
	private float health = 100;
	private float max_health = 100;
	private float defense = 10;
	private float attackPower = 5;
	//##############################
	
	private string state = "None";
	string[] states = {"Walk", "Find", "Attack", "Dead"};
	private float rotationSpeed = 1.0f;
=======
	
	//##############################
	//Atributos personaje
	private float moveSpeed = 10; 
	private float health = 100;
	private float max_health = 100;
	private float defense = 10;
	private float attackPower = 5;
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
	string[] states = {"Walk", "Find", "Attack", "Dead"};
	private float rotationSpeed = 10.0f;
>>>>>>> devel_a
	private float attackTime = 3.0f;
	
>>>>>>> 3b3acbb0b4b2c81c9e17a4141d6481dfc8415649
	// Los nombres de los tres puntos que estan distribuidos por el mapa
	string[] points = {"Point1", "Point2", "Point3"};
	int j = 0;
	
	private GameObject player;
	private Transform player_transform;
	private Animator anim;
<<<<<<< HEAD
=======
<<<<<<< HEAD
	
	//private GameObject NPCbar;
	private Music_Engine_Script music;
=======
	private GameObject NPCbar;
	private Music_Engine_Script music;
	
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
	
>>>>>>> devel_a
>>>>>>> 3b3acbb0b4b2c81c9e17a4141d6481dfc8415649
	
	//private GameObject NPCbar;
	private Music_Engine_Script music;

	void Awake(){

		this.npcAttributes = new NPCAttributes (health, max_health, attackPower, defense, moveSpeed, experience);
		//print ("Experiencia: " + this.npcAttributes.getExperience ());

	}

	// Use this for initialization
	void Start () {
<<<<<<< HEAD

=======
		seeker = GetComponent<Seeker>();
>>>>>>> 3b3acbb0b4b2c81c9e17a4141d6481dfc8415649
		player = GameObject.FindGameObjectWithTag("Player");
		player_transform = player.transform;
<<<<<<< HEAD
		anim = GetComponent<Animator>();

<<<<<<< HEAD
		difficulty = PlayerPrefs.GetString ("Difficulty");
		npcAttributes.setDificulty (difficulty);

		//this.NPCbar = GameObject.FindGameObjectWithTag("NPCHealth");
=======
		//this.NPCbar = GameObject.FindGameObjectWithTag("NPCHealth");
=======
		anim = GetComponent<Animator> ();
		this.NPCbar = GameObject.FindGameObjectWithTag("NPCHealth");
>>>>>>> devel_a
>>>>>>> 3b3acbb0b4b2c81c9e17a4141d6481dfc8415649
		this.music = GameObject.FindGameObjectWithTag ("music_engine").GetComponent<Music_Engine_Script> ();
	}
	
	// Update is called once per frame
	void Update () {
<<<<<<< HEAD
		// Cosas a determinar por el programador de IA
<<<<<<< HEAD
=======
=======
>>>>>>> devel_a
>>>>>>> 3b3acbb0b4b2c81c9e17a4141d6481dfc8415649
		if (!state.Equals("Dead")) {
			float distance_to_player = Vector3.Distance(player_transform.position,transform.position);
			if (distance_to_player < 10) {
				atack ();
			} else if (distance_to_player < 60) {
				perseguir ();
			} else {
				seguirPuntos ();
			}
		}
	}
	
	// Metodo que hace que el personaje vaya uno a uno a los tres puntos del mapa
	void seguirPuntos(){
<<<<<<< HEAD
		
=======
<<<<<<< HEAD

=======
		state = "Walk";
>>>>>>> devel_a
>>>>>>> 3b3acbb0b4b2c81c9e17a4141d6481dfc8415649
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
<<<<<<< HEAD
		// Rotamos hacia la direccion
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(punto.transform.position - transform.position), rotationSpeed * Time.deltaTime);
		// Transladamos el NPC hacia el punto
		transform.position += transform.forward * npcAttributes.getMoveSpeed() * Time.deltaTime;
=======
		
		// Calculamos el camino hacia el punto
		calcularPath(punto.transform.position);
		
		// En caso de llegar al final del camino
		if (currentWaypoint > path.vectorPath.Count)
			return; 
		
		// Rotamos y trasladamos hacia el siguente punto de la ruta
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(path.vectorPath[currentWaypoint] - transform.position), rotationSpeed * Time.deltaTime);
		transform.position += transform.forward * moveSpeed * Time.deltaTime;
		// Incrementamos para poder ir al siguiente punto de la ruta calculada
		currentWaypoint++;
>>>>>>> 3b3acbb0b4b2c81c9e17a4141d6481dfc8415649
	}
	
	// Metodo que se llama para perseguir al prsonaje principal
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
<<<<<<< HEAD
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(p - transform.position), rotationSpeed * Time.deltaTime);
		transform.position += transform.forward * npcAttributes.getMoveSpeed() * 2 * Time.deltaTime;
=======
		
		// Calculamos la ruta hacia el personaje principal
		calcularPath(p);
		
		// En caso de llegar al final del camino
		if (currentWaypoint > path.vectorPath.Count)
			return; 
		
		// Rotamos y trasladamos hacia el siguente punto de la ruta
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(path.vectorPath[currentWaypoint] - transform.position), rotationSpeed * Time.deltaTime);
		transform.position += transform.forward * moveSpeed * Time.deltaTime;
		// Incrementamos para poder ir al siguiente punto de la ruta calculada
		currentWaypoint++;
>>>>>>> 3b3acbb0b4b2c81c9e17a4141d6481dfc8415649
	}
	
	// Metodo que rota el NPC unos determinados grados
	void rotar(int degrees){
		Quaternion newRotation = Quaternion.AngleAxis (degrees, Vector3.up);
		transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, rotationSpeed);
	}
	
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
	// Metodo que se llama para atacar al personaje principal
>>>>>>> devel_a
>>>>>>> 3b3acbb0b4b2c81c9e17a4141d6481dfc8415649
	void atack(){
		state = "Attack";
		Vector3 p= player_transform.position;
		p.y = transform.position.y;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(p - transform.position), rotationSpeed * Time.deltaTime);
		anim.SetBool("a_walk", false);
		anim.SetBool("walk", false);
		p.y += 5f;
		anim.SetBool ("w_attack", true);
		p.y -= 5f;
		if (Time.time > attackTime) {
<<<<<<< HEAD
			player.GetComponent<CharacterScript>().setDamage((int) npcAttributes.getAttackPower());
=======
			player.GetComponent<CharacterScript>().setDamage((int) attackPower);
>>>>>>> 3b3acbb0b4b2c81c9e17a4141d6481dfc8415649
			attackTime = Time.time + 1.0f;
			if(music != null) {
				music.play_Golem_Agresive();
				music.play_Golem_Attack();
			}
		}
	}	
	
	// Metodo que llama el personaje principal para producir daño al NPC
	public void setDamage(float damage){
<<<<<<< HEAD
		npcAttributes.setDamage (damage);
		//this.NPCbar.renderer.material.SetFloat("_Cutoff", 1 - (this.health/this.max_health));
		if (npcAttributes.getHealth() < 1) {
			state = "Dead";
			player.GetComponent<CharacterScript>().setEXP(npcAttributes.getExperience());
			this.collider.enabled = false;
			//Debug.Log ("NPC muerto");
=======
		health -= damage;
<<<<<<< HEAD
		//this.NPCbar.renderer.material.SetFloat("_Cutoff", 1 - (this.health/this.max_health));
=======
		// Reproducir sonido golpeado
		// Reproducir animacion golpeado
		this.NPCbar.renderer.material.SetFloat("_Cutoff", 1 - (this.health/this.max_health));
>>>>>>> devel_a
		if (health < 1) {
			state = "Dead";
			Debug.Log ("NPC muerto");
>>>>>>> 3b3acbb0b4b2c81c9e17a4141d6481dfc8415649
			anim.SetBool("a_walk", false);
			anim.SetBool("walk", false);
			anim.SetBool ("w_attack", false);
			anim.SetBool("a_death", true);
		}
	}
<<<<<<< HEAD

	
	
	/*public void setHealth(float health){
		this.health = health;
		this.max_health = health;
	}*/

<<<<<<< HEAD
	
	public NPCAttributes getAttributes(){
		return npcAttributes;
=======
=======
	
	
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
			Debug.Log ("Se ha llegado al final de la ruta");
			currentWaypoint++;
			hecho = false;
			return;
		}
	}
	
	
>>>>>>> devel_a
	public void setHealth(float health){
		this.health = health;
		this.max_health = health;
	}
	
	public void setDefense(float defense){
		this.defense = defense;
	}
	
	public void setAttackPower(float attackPower){
		this.attackPower = attackPower;
	}
	
	public void setMoveSpeed(float moveSpeed){
		this.moveSpeed = moveSpeed;
	}
	
	public float getHealth(){
		return this.health;
	}
<<<<<<< HEAD

	public float getMaxHealth(){
		return this.max_health;
	}
=======
>>>>>>> devel_a
	
	public float getDefense(){
		return this.defense;
	}
	
	public float getMoveSpeed(){
		return this.moveSpeed;
	}
	
	public float getAttackPower(){
		return this.attackPower;
>>>>>>> 3b3acbb0b4b2c81c9e17a4141d6481dfc8415649
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

	void OnParticleCollision(GameObject other) {
		setDamage (20);
	}
}