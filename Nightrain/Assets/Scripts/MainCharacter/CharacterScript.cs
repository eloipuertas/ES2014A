using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour {

	public float scale;

	// ATRIBUTES HEALTH/MAGIC CHARACTER
	public int bar_health;
	public int bar_magic;

	public float max_health;
	public float max_magic;

	private float health;
	private float magic;

	private float resize_health;
	private float resize_magic;

	// HEALTH BAR
	// --- TEXTURES ---
	private Texture2D AvatarTexture;
	
	private Texture2D HealthTexture;
	private Texture2D HealthBarTexture;
	
	private Texture2D MagicTexture;
	private Texture2D MagicBarTexture;

	private Texture2D DecotrationTextureUp;
	private Texture2D DecotrationTextureDown;
	
	// --- MATERIALS ---
	private Material HealthBarMaterial;
	private Material MagicBarMaterial;
	
	// CRITICAL HEALTH
	private Color c;
	private bool critical = false;

	// Use this for initialization
	void Start () {

		this.resize_health = this.scale * Mathf.Pow(this.bar_health / this.max_health, -1);
		this.resize_magic = this.scale * Mathf.Pow(this.bar_magic / this.max_magic, -1);

		this.max_health = this.bar_health;
		this.max_magic = this.bar_magic;

		// ADD TEXTURES
		this.AvatarTexture = Resources.Load<Texture2D>("HealthBar/avatar_" + PlayerPrefs.GetString("Character"));
		this.HealthTexture = Resources.Load<Texture2D>("HealthBar/health");
		this.HealthBarTexture = Resources.Load<Texture2D>("HealthBar/bar_health");
		this.MagicTexture = Resources.Load<Texture2D>("HealthBar/magic");
		this.MagicBarTexture = Resources.Load<Texture2D>("HealthBar/bar_magic");
		this.DecotrationTextureUp = Resources.Load<Texture2D>("HealthBar/decoracion_up");
		this.DecotrationTextureDown = Resources.Load<Texture2D>("HealthBar/decoracion_down");

		// ADD MATERIALS
		this.HealthBarMaterial = Resources.Load<Material>("HealthBar/Materials/bar_health");
		this.MagicBarMaterial = Resources.Load<Material>("HealthBar/Materials/bar_magic");
		
		this.c = this.HealthBarMaterial.color;
		
		this.HealthBarMaterial.SetFloat("_Cutoff", .5f);
		this.MagicBarMaterial.SetFloat("_Cutoff", .5f);
	
	}
	
	// Update is called once per frame
	void Update () {
		this.UpdateHealth ();
		//this.UpdateMagic ();
	}


	void UpdateHealth () {
			
		// Esto es para cuando le quede 1/4 de vida se ponga en rojo
		if(this.health >= 0.9f){
			this.c.r = 1.0f;
			this.c.g = 0;
			this.c.b = 0;
			this.HealthBarMaterial.color = c;
			this.critical = true;
		}else{
			this.c.r = 1.0f;
			this.c.g = 1.0f;
			this.c.b = 1.0f;
			this.HealthBarMaterial.color = c;
			this.critical = false;
		}

		if(this.health == 1.0f){
			this.bar_health = 0;
		}else{
			this.health = 1 - ((this.bar_health / this.max_health) * .5f);
			this.HealthBarMaterial.SetFloat("_Cutoff", health);
		}
		
	}

	// Este metodo debera ser implementado mas adelante con el tema de magias etc
	void UpdateMagic () {
		
	}


	public bool isCritical(){
		return this.critical;
	}

	public void setHeal(int heal){
		if (this.bar_health < this.max_health) {
			this.bar_health += heal;
			if(this.bar_health > this.max_health)
				this.bar_health = Mathf.FloorToInt(this.max_health);
		}
	}

	public void setDamage(int damage){
		this.bar_health -= damage;
	}


	void OnGUI(){

		// Comentar esta linia era solo para debug para ver como recuperaba vida, si la vida llega a 0 no recupera esta muerto
		if (GUI.Button(new Rect(Screen.width - 125,50,100,50), "Heal")) {
			print ("Heal");
			this.setHeal(10);
		}

		if (Event.current.type.Equals (EventType.Repaint)) {
	
			// AVATAR ZONE
			Rect avatar_box = new Rect (0, 0, this.AvatarTexture.width / this.scale, this.AvatarTexture.height / this.scale);
			Graphics.DrawTexture (avatar_box, this.AvatarTexture);

			// HEALTH BAR ZONE	
			Rect healthbar_box = new Rect ((this.AvatarTexture.width / this.scale) - 3,
			                                40, 
			                                this.HealthBarTexture.width / this.resize_health, 
			                                this.HealthBarTexture.height / this.scale);

			Graphics.DrawTexture (healthbar_box, this.HealthBarTexture, this.HealthBarMaterial);

			Rect health_box = new Rect ((this.AvatarTexture.width / this.scale) - 3,
			                             40, 
			                             this.HealthTexture.width / this.resize_health, 
			                             this.HealthTexture.height / this.scale);

			Graphics.DrawTexture (health_box, this.HealthTexture);

			Rect decorationUp_box = new Rect (((this.AvatarTexture.width / this.scale) + (this.HealthTexture.width / this.resize_health)) - 3,
					                            40, 
			                                    this.DecotrationTextureUp.width / this.scale, 
			                                    this.DecotrationTextureUp.height / this.scale);
			
			Graphics.DrawTexture (decorationUp_box, this.DecotrationTextureUp);

			// MAGIC BAR ZONE

			Rect magicbar_box = new Rect ((this.AvatarTexture.width / this.scale) - 3,
	                             		   66, 
	                             		   this.MagicBarTexture.width / this.resize_magic, 
			                               (this.MagicBarTexture.height / this.scale) - 8);


			Graphics.DrawTexture (magicbar_box, this.MagicBarTexture, this.MagicBarMaterial);

			Rect magic_box = new Rect ((this.AvatarTexture.width / this.scale) - 3,
		                                66, 
		                                this.MagicTexture.width / this.resize_magic, 
			                            this.MagicTexture.height / this.scale);

			Graphics.DrawTexture (magic_box, this.MagicTexture);

			Rect decorationDown_box = new Rect (((this.AvatarTexture.width / this.scale) + (this.MagicTexture.width / this.resize_magic)) - 3,
		                                  	  	66, 
			                                    this.DecotrationTextureDown.width / this.scale, 
			                                    this.DecotrationTextureDown.height / this.scale);
			
			Graphics.DrawTexture (decorationDown_box, this.DecotrationTextureDown);

			
		}
	}
}
