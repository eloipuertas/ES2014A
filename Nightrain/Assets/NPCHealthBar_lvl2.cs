using UnityEngine;
using System.Collections;

public class NPCHealthBar_lvl2 : MonoBehaviour {
	private Skeleton_controller_2 skel;
	private FireDemon_Controller demon;
	private Ice_Golem_controller golem;
	private Skeleton_boss_controller boss;
	
	public GameObject npc;
	
	// Values to postion de HealthBar
	public float x;
	public float y;
	public float x2;
	public float y2;
	
	private float health;	// <-- This value is to calculate the health 'Cutoff' of the texture [0, 1]
	private float resize_health;	// <-- This value is to do more large or short the health bar
	private string name;
	// ===== HEALTH BAR =====
	
	// --- TEXTURES ---
	private Texture2D HealthTexture;
	private Texture2D HealthBarTexture;
	
	// --- MATERIALS ---
	private Material HealthMaterial;
	public Material material;
	
	
	// Use this for initialization
	void Start () {
		name = npc.tag.Substring (0, npc.tag.Length - 1);
		//this.npc = GameObject.FindGameObjectWithTag ("Enemy");

		if (name == "Skeleton") {
			this.skel = this.npc.GetComponent<Skeleton_controller_2> ();
			this.resize_health = Mathf.Pow(this.skel.actual_health / this.skel.health, -1);
		} 
		else if (name == "FireDemon" || name == "IceDemon" || name == "MiniIceDemon") {
			this.demon = this.npc.GetComponent<FireDemon_Controller> ();
			this.resize_health = Mathf.Pow(this.demon.actual_health / this.demon.health, -1);
		}
		else if(this.npc.tag == "IceGolem"){
			this.golem = this.npc.GetComponent<Ice_Golem_controller> ();
			this.resize_health = Mathf.Pow(this.golem.actual_health / this.golem.health, -1);
		}
		else if(this.npc.tag == "Boss"){
			this.boss = this.npc.GetComponent<Skeleton_boss_controller> ();
			this.resize_health = Mathf.Pow(this.boss.health / this.boss.total_health, -1);
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
		if(this.health != 1f) {
			if (name == "Skeleton") {
				this.health = 1 - (this.skel.actual_health / this.skel.health);
			} 
			else if (name == "FireDemon" || name == "IceDemon" || name == "MiniIceDemon") {
				this.health = 1 - (this.demon.actual_health / this.demon.health);
			}
			else if(this.npc.tag == "IceGolem"){
				this.health = 1 - (this.golem.actual_health / this.golem.health);
			}
			else if(this.npc.tag == "Boss"){
				this.health = 1 - (this.boss.health / this.boss.total_health);
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
			if (name == "Skeleton") {
				if(skel.actual_health > 0) DrawHealth(xy);
			} 
			else if (name == "FireDemon" || name == "IceDemon" || name == "MiniIceDemon") {
				if(demon.actual_health > 0) DrawHealth(xy);
			}
			else if(this.npc.tag == "IceGolem"){
				if(golem.actual_health > 0) DrawHealth(xy);
			}
			else if(this.npc.tag == "Boss"){
				if(boss.health > 0) DrawHealth(xy);
			}
		}
	}
}
