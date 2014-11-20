using UnityEngine;
using System.Collections;

public class NPCHeatlhBar : MonoBehaviour {

	private Movement enemy;
	private GameObject npc;

	// Values to postion de HealthBar
	public float x;
	public float y;
	public float x2;
	public float y2;

	private float health;	// <-- This value is to calculate the health 'Cutoff' of the texture [0, 1]
	private float resize_health;	// <-- This value is to do more large or short the health bar

	// ===== HEALTH BAR =====

	// --- TEXTURES ---
	private Texture2D HealthTexture;
	private Texture2D HealthBarTexture;

	// --- MATERIALS ---
	private Material HealthMaterial;


	// Use this for initialization
	void Start () {
		
		this.npc = GameObject.FindGameObjectWithTag ("Enemy");
		this.enemy = this.npc.GetComponent<Movement> ();
		
		Debug.Log ("HEALTH:" + this.enemy.getHealth ());
		// Resize the health and magic bar with the actual values
		this.resize_health = Mathf.Pow(this.enemy.getHealth() / this.enemy.getMaxHealth(), -1);
		
		// ADD TEXTURES
		this.HealthTexture = Resources.Load<Texture2D>("NPC/NPCbar_health");
		this.HealthBarTexture = Resources.Load<Texture2D>("NPC/NPCbar");
		
		// ADD MATERIALS
		this.HealthMaterial = Resources.Load<Material>("NPC/Materials/NPCbar_health");

		
		this.health = 0f;
		this.HealthMaterial.SetFloat("_Cutoff", this.health);
		
	}
	
	// Update is called once per frame
	void Update () {
		UpdateHealth ();
	}

	void UpdateHealth () {
		
		if(this.health == 1.0f){
			this.enemy.setHealth(0);
		}else{
			this.health = 1 - (this.enemy.getHealth() / this.enemy.getMaxHealth());
			this.HealthMaterial.SetFloat("_Cutoff", health);
		}
	}

	void OnGUI(){

		Vector2 xy = Camera.main.WorldToScreenPoint(new Vector3(this.npc.transform.position.x,
		                                                        this.npc.transform.position.y,
		                                                        this.npc.transform.position.z));

		//Debug.Log ("X: " + xy.x + " Y:" + xy.y);
		if (Event.current.type.Equals (EventType.Repaint)) {
			
			// HEALTH BAR ZONE
			Rect bar_box = new Rect (xy.x+Screen.width*x,
			                         Screen.height*y - xy.y, 
			                         150,
			                         25);

			GUI.DrawTexture (bar_box, this.HealthBarTexture);
			
			// HEALTH ZONE	
			Rect healthbar_box = new Rect (bar_box.x + x2,
			                               bar_box.y + y2,  
			                               116,
			                               10);
			Graphics.DrawTexture (healthbar_box, this.HealthTexture, this.HealthMaterial);
		}
	}
}
