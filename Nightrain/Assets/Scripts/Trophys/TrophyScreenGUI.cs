using UnityEngine;
using System.Collections;

public class TrophyScreenGUI : MonoBehaviour {

	private const int reference_width = 1366; 
	private const int reference_height = 598;
	
	// ====== TEXTURES MAIN MENU ======
	private Texture2D backgroundTexture;
	private Texture2D PlatinoTexture;
	private Texture2D EasyTexture;
	private Texture2D NormalTexture;
	private Texture2D HardTexture;
	private Texture2D ExtremeTexture;
	private Texture2D WarriorTexture;
	private Texture2D SageTexture;
	private Texture2D ThiefTexture;
	private Texture2D AllTexture;
	private Texture2D BusterSwordTexture;
	private Texture2D Level5Texture;
	private Texture2D Level10Texture;
	private Texture2D Level20Texture;
	private Texture2D Level50Texture;
	private Texture2D miniGolemTexture;
	private Texture2D EsqueletoTexture;
	private Texture2D GolemLavaTexture;
	private Texture2D GolemHieloTexture;

	private Texture2D btnExitTexture;
	private Texture2D hoverBtnExitTexture;

	// Hover button audio Source
	private GameObject hoverSound;
	// Variable to check current mouse hover button
	private Rect hoveredButton = new Rect();

	private string[] list_trophies;
	private TrophyEngine trofeos;

	// MEMORY CARD 
	private MemoryCard mc;
	private SaveData save;
	private LoadData load;
	
	// Use this for initialization
	void Start () {

		// Memory Card Save/Load data
		this.mc = GameObject.FindGameObjectWithTag ("MemoryCard").GetComponent<MemoryCard> ();
		this.save = this.mc.saveData ();
		this.load = this.mc.loadData (); 

		this.trofeos = GameObject.FindGameObjectWithTag("Trofeos").GetComponent<TrophyEngine>();
		string[] list_trophies = new string[18];
		this.trofeos.getTrophys (list_trophies);

		string str = "lock_";

		// MAIN MENU
		this.backgroundTexture = Resources.Load<Texture2D>("Trofeos/background_trophyv2");
		this.PlatinoTexture = Resources.Load<Texture2D>("Trofeos/"+list_trophies[17]+"platino");
		this.EasyTexture = Resources.Load<Texture2D>("Trofeos/"+list_trophies[9]+"easy");
		this.NormalTexture = Resources.Load<Texture2D>("Trofeos/"+list_trophies[10]+"normal");
		this.HardTexture = Resources.Load<Texture2D>("Trofeos/"+list_trophies[11]+"hard");
		this.ExtremeTexture = Resources.Load<Texture2D>("Trofeos/"+list_trophies[12]+"extreme");
		this.WarriorTexture = Resources.Load<Texture2D>("Trofeos/"+list_trophies[13]+"guerrero");
		this.SageTexture = Resources.Load<Texture2D>("Trofeos/"+list_trophies[14]+"maga");
		this.ThiefTexture = Resources.Load<Texture2D>("Trofeos/"+list_trophies[15]+"ladron");
		this.AllTexture = Resources.Load<Texture2D>("Trofeos/"+list_trophies[16]+"todos");
		this.BusterSwordTexture = Resources.Load<Texture2D>("Trofeos/"+list_trophies[6]+"bustersword");
		this.Level5Texture = Resources.Load<Texture2D>("Trofeos/"+list_trophies[2]+"level5");
		this.Level10Texture = Resources.Load<Texture2D>("Trofeos/"+list_trophies[3]+"level10");
		this.Level20Texture = Resources.Load<Texture2D>("Trofeos/"+list_trophies[4]+"level20");
		this.Level50Texture = Resources.Load<Texture2D>("Trofeos/"+list_trophies[5]+"level50");
		this.miniGolemTexture = Resources.Load<Texture2D>("Trofeos/"+list_trophies[0]+"minigolem");
		this.EsqueletoTexture = Resources.Load<Texture2D>("Trofeos/"+list_trophies[7]+"esqueleto");
		this.GolemLavaTexture = Resources.Load<Texture2D>("Trofeos/"+list_trophies[1]+"golemlava");
		this.GolemHieloTexture = Resources.Load<Texture2D>("Trofeos/"+list_trophies[8]+"golemhielo");

		this.btnExitTexture = Resources.Load<Texture2D>("MainMenu/exit");
		this.hoverBtnExitTexture = Resources.Load<Texture2D>("MainMenu/hover_exit");

		this.hoverSound = GameObject.FindGameObjectWithTag("music_engine");

	}


	void OnGUI(){

		// BACKGROUND MAINMENU
		Rect background_box = new Rect (0, 0, Screen.width, Screen.height);
		GUI.DrawTexture (background_box, this.backgroundTexture);

		//TROPHIES
		Rect platino_box = new Rect (resizeTextureWidth(PlatinoTexture)*.3f,
		                             resizeTextureHeight(backgroundTexture)*.215f,
		                             resizeTextureWidth(PlatinoTexture)*.75f,
		                             resizeTextureHeight(PlatinoTexture)*.65f);
		GUI.DrawTexture (platino_box, this.PlatinoTexture);

		Rect easy_box = new Rect (platino_box.x, platino_box.y*1.425f, platino_box.width, platino_box.height);
		GUI.DrawTexture (easy_box, this.EasyTexture);

		Rect normal_box = new Rect (easy_box.x, easy_box.y*1.285f, easy_box.width, easy_box.height);
		GUI.DrawTexture (normal_box, this.NormalTexture);

		Rect hard_box = new Rect (normal_box.x, normal_box.y*1.2275f, normal_box.width, normal_box.height);
		GUI.DrawTexture (hard_box, this.HardTexture);

		Rect extreme_box = new Rect (hard_box.x, hard_box.y*1.185f, hard_box.width, hard_box.height);
		GUI.DrawTexture (extreme_box, this.ExtremeTexture);

		Rect bustersword_box = new Rect (extreme_box.x, extreme_box.y*1.1575f, extreme_box.width, extreme_box.height);
		GUI.DrawTexture (bustersword_box, this.BusterSwordTexture);

		Rect warrior_box = new Rect (platino_box.x*4f, platino_box.y, platino_box.width, platino_box.height);
		GUI.DrawTexture (warrior_box, this.WarriorTexture);

		Rect sage_box = new Rect (platino_box.x*4f, platino_box.y*1.42f, platino_box.width, platino_box.height);
		GUI.DrawTexture (sage_box, this.SageTexture);
		
		Rect thief_box = new Rect (easy_box.x*4f, easy_box.y*1.285f, easy_box.width, easy_box.height);
		GUI.DrawTexture (thief_box, this.ThiefTexture);
		
		Rect all_box = new Rect (normal_box.x*4f, normal_box.y*1.23f, normal_box.width, normal_box.height);
		GUI.DrawTexture (all_box, this.AllTexture);
		
		Rect minigolem_box = new Rect (hard_box.x*4f, hard_box.y*1.185f, hard_box.width, hard_box.height);
		GUI.DrawTexture (minigolem_box, this.miniGolemTexture);
		
		Rect golemlava_box = new Rect (extreme_box.x*4f, extreme_box.y*1.1575f, extreme_box.width, extreme_box.height);
		GUI.DrawTexture (golemlava_box, this.GolemLavaTexture);

		Rect level5_box = new Rect (platino_box.x*7f, platino_box.y, platino_box.width, platino_box.height);
		GUI.DrawTexture (level5_box, this.Level5Texture);
		
		Rect level10_box = new Rect (platino_box.x*7f, platino_box.y*1.42f, platino_box.width, platino_box.height);
		GUI.DrawTexture (level10_box, this.Level10Texture);
		
		Rect level20_box = new Rect (easy_box.x*7f, easy_box.y*1.285f, easy_box.width, easy_box.height);
		GUI.DrawTexture (level20_box, this.Level20Texture);
		
		Rect level50_box = new Rect (normal_box.x*7f, normal_box.y*1.23f, normal_box.width, normal_box.height);
		GUI.DrawTexture (level50_box, this.Level50Texture);
		
		Rect esqueleto_box = new Rect (hard_box.x*7f, hard_box.y*1.185f, hard_box.width, hard_box.height);
		GUI.DrawTexture (esqueleto_box, this.EsqueletoTexture);
		
		Rect golemhielo_box = new Rect (extreme_box.x*7f, extreme_box.y*1.1575f, extreme_box.width, extreme_box.height);
		GUI.DrawTexture (golemhielo_box, this.GolemHieloTexture);

		// BACK BUTTON
		Rect back_box = new Rect (Screen.width/1.35f,
		                          Screen.height/1.175f,
		                          this.resizeTextureWidth(this.btnExitTexture)/1.25f,
		                          this.resizeTextureHeight(this.btnExitTexture)/1.25f);
		Graphics.DrawTexture (back_box, this.btnExitTexture);

		// ACTION BACK BUTTON
		if (back_box.Contains (Event.current.mousePosition)) {
			Graphics.DrawTexture (back_box, this.hoverBtnExitTexture);
			if (hoveredButton != back_box) {
				hoverSound.audio.Play ();
				hoveredButton = back_box;
			}
			if (Input.GetMouseButtonDown (0)) {
				Application.LoadLevel(1);
			}
		} else {
			Graphics.DrawTexture (back_box, this.btnExitTexture);
			if(hoveredButton == back_box) hoveredButton = new Rect();
		}

	}

	private float resizeTextureWidth(Texture2D texture){
		return ((Screen.width * texture.width) / (reference_width * 1.0f));
	}
	
	private float resizeTextureHeight(Texture2D texture){
		return ((Screen.height * texture.height) / (reference_height * 1.0f));
	}
}
