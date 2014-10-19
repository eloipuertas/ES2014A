using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour {
	
	// ATRIBUTES CHARACTER
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
	
	// LOW HEALTH
	private Color c;

	// Use this for initialization
	void Start () {

		this.resize_health = 2 * Mathf.Pow(this.bar_health / this.max_health, -1);
		this.resize_magic = 2 * Mathf.Pow(this.bar_magic / this.max_magic, -1);

		this.max_health = this.bar_health;
		this.max_magic = this.bar_magic;

		// ADD TEXTURES
		this.AvatarTexture = Resources.LoadAssetAtPath<Texture2D>("Assets/Textures/HealthBar/avatar.png");
		this.HealthTexture = Resources.LoadAssetAtPath<Texture2D>("Assets/Textures/HealthBar/health.png");
		this.HealthBarTexture = Resources.LoadAssetAtPath<Texture2D>("Assets/Textures/HealthBar/bar_health.png");
		this.MagicTexture = Resources.LoadAssetAtPath<Texture2D>("Assets/Textures/HealthBar/magic.png");
		this.MagicBarTexture = Resources.LoadAssetAtPath<Texture2D>("Assets/Textures/HealthBar/bar_magic.png");
		this.DecotrationTextureUp = Resources.LoadAssetAtPath<Texture2D>("Assets/Textures/HealthBar/decoracion_up.png");
		this.DecotrationTextureDown = Resources.LoadAssetAtPath<Texture2D>("Assets/Textures/HealthBar/decoracion_down.png");

		// ADD MATERIALS
		this.HealthBarMaterial = Resources.LoadAssetAtPath<Material>("Assets/Textures/HealthBar/Materials/bar_health.mat");
		this.MagicBarMaterial = Resources.LoadAssetAtPath<Material>("Assets/Textures/HealthBar/Materials/bar_magic.mat");
		
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
		}else{
			this.c.r = 1.0f;
			this.c.g = 1.0f;
			this.c.b = 1.0f;
			this.HealthBarMaterial.color = c;
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

		/*if(this.magic == 1.0f){
			this.bar_magic = 0;
		}else{
			this.magic = 1 - ((this.bar_magic / this.max_magic) * .5f);
			this.MagicBarMaterial.SetFloat("_Cutoff", magic);
			print ("Magic " + this.magic);
		}*/
		
	}

	
	void OnGUI(){
		
		if (Event.current.type.Equals (EventType.Repaint)) {
	
			// AVATAR ZONE
			Rect avatar_box = new Rect (0, 0, this.AvatarTexture.width / 2, this.AvatarTexture.height / 2);
			Graphics.DrawTexture (avatar_box, this.AvatarTexture);

			// HEALTH BAR ZONE	
			Rect healthbar_box = new Rect ((this.AvatarTexture.width / 2) - 5,
			                                44, 
			                                this.HealthBarTexture.width / this.resize_health, 
			                                this.HealthBarTexture.height / 2);

			Graphics.DrawTexture (healthbar_box, this.HealthBarTexture, this.HealthBarMaterial);

			Rect health_box = new Rect ((this.AvatarTexture.width / 2) - 5,
			                             44, 
			                             this.HealthTexture.width / this.resize_health, 
			                             this.HealthTexture.height / 2);

			Graphics.DrawTexture (health_box, this.HealthTexture);

			Rect decorationUp_box = new Rect (((this.AvatarTexture.width / 2) + (this.HealthTexture.width / this.resize_health)) - 5,
					                            44, 
					                            this.DecotrationTextureUp.width / 2, 
												this.DecotrationTextureUp.height / 2);
			
			Graphics.DrawTexture (decorationUp_box, this.DecotrationTextureUp);

			// MAGIC BAR ZONE

			Rect magicbar_box = new Rect ((this.AvatarTexture.width / 2) - 5,
	                             75, 
	                             (this.MagicBarTexture.width / this.resize_magic), 
	                             (this.MagicBarTexture.height / 2) - 8);


			Graphics.DrawTexture (magicbar_box, this.MagicBarTexture, this.MagicBarMaterial);

			Rect magic_box = new Rect ((this.AvatarTexture.width / 2) - 5,
		                                75, 
		                                this.MagicTexture.width / this.resize_magic, 
		                                this.MagicTexture.height / 2);

			Graphics.DrawTexture (magic_box, this.MagicTexture);

			Rect decorationDown_box = new Rect (((this.AvatarTexture.width / 2) + (this.MagicTexture.width / this.resize_magic)) - 5,
		                                  	  	75, 
			                                  	this.DecotrationTextureDown.width / 2, 
		                                    	this.DecotrationTextureDown.height / 2);
			
			Graphics.DrawTexture (decorationDown_box, this.DecotrationTextureDown);
		}
	}
}
