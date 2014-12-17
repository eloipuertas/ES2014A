using UnityEngine;
using System.Collections;

public class NPCHeatlhBar : MonoBehaviour {
	
	private Movement boss;
	private Movement_graveler enemy;
	private Skeleton_controller_2 skel;
	private FireDemon_Controller demon;
	private Ice_Golem_controller golem;
	private Skeleton_boss_controller boss2;

	public GameObject npc;
	
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
	public Material material;
	
	
	// Use this for initialization
	void Start () {

		//this.npc = GameObject.FindGameObjectWithTag ("Enemy");
		if(this.npc.tag == "Boss"){
			this.boss = (Movement)this.npc.GetComponent<Movement> ();
			this.resize_health = Mathf.Pow(this.boss.getAttributes().getHealth() / this.boss.getAttributes().getMaxHealth(), -1);
		}else if(this.npc.tag == "Enemy"){
			this.enemy = (Movement_graveler)this.npc.GetComponent<Movement_graveler> ();
			this.resize_health = Mathf.Pow(this.enemy.getAttributes().getHealth() / this.enemy.getAttributes().getMaxHealth(), -1);
		}

		
		// ADD TEXTURES
		this.HealthTexture = Resources.Load<Texture2D>("NPC/NPCbar_health");
		this.HealthBarTexture = Resources.Load<Texture2D>("NPC/NPCbar");

		this.health = 0f;
		
	}
	
	// Update is called once per frame
	void Update () {
		UpdateHealth ();
	}
	
	void UpdateHealth () {
		
		if(this.health == 1.0f){
			if(this.npc.tag == "Boss"){
				this.boss.getAttributes().setHealth(0);
			}else if(this.npc.tag == "Enemy"){
				this.enemy.getAttributes().setHealth(0);
			}

		}else{
			if(this.npc.tag == "Boss"){
				this.health = 1 - (this.boss.getAttributes().getHealth() / this.boss.getAttributes().getMaxHealth());
			}else if(this.npc.tag == "Enemy"){
				this.health = 1 - (this.enemy.getAttributes().getHealth() / this.enemy.getAttributes().getMaxHealth());
			}
		}
	}

	void DrawHealth(Vector2 xy){
	
		// HEALTH BAR ZONE
		Rect bar_box = new Rect (xy.x+Screen.width*x,
		                         Screen.height*y - xy.y, 
		                         150,
		                         25);
		
		GUI.DrawTexture (bar_box, this.HealthBarTexture);
		
		// HEALTH ZONE	
		Rect healthbar_box = new Rect (bar_box.x + x2,
		                               bar_box.y + y2,  
		                               116 - this.health * 110,
		                               10);

		//Graphics.DrawTexture (healthbar_box, this.HealthTexture, this.HealthMaterial);
		GUI.DrawTexture (healthbar_box, this.HealthTexture);

	}

	void OnGUI(){
		
		Vector2 xy = Camera.main.WorldToScreenPoint(new Vector3(this.npc.transform.position.x,
		                                                        this.npc.transform.position.y,
		                                                        this.npc.transform.position.z));

		if (Event.current.type.Equals (EventType.Repaint)) {

			if(this.npc.tag == "Boss"){
				if(boss.getAttributes().getHealth() > 0){
					DrawHealth(xy);
				}
			}else if(this.npc.tag == "Enemy"){
				if(enemy.getAttributes().getHealth() > 0){
					DrawHealth(xy);
				}
			}

		}
	}
}