using UnityEngine;
using System.Collections.Generic;

public class TrophyEngine : MonoBehaviour {

	private const int reference_width = 1366; 
	private const int reference_height = 598;

	private int MiniGolem;
	private int Skull;
	private float timer = 5f;
	private bool[] trofeos;
	private string[] trophy_unlock;

	private bool unlock = false;
	private bool sound = false;

	private Queue<Texture2D> trophyQ = new Queue<Texture2D>();
	private Texture2D TrophyTexture;

	private Music_Engine_Script music;

	
	// Use this for initialization
	void Start () {

		//trophy_q = new Queue<Texture2D>();
	
		trofeos = new bool[18];
		trophy_unlock = new string[18];

		this.loadTrophys ();

		TrophyTexture = Resources.Load<Texture2D>("Trofeos/trophy_platino");

		this.music = GameObject.FindGameObjectWithTag("music_engine").GetComponent<Music_Engine_Script>();

	}
	

	public void countMiniGolem(int count){
		PlayerPrefs.SetInt ("MiniGolem", PlayerPrefs.GetInt ("MiniGolem") + count);
		MiniGolem = PlayerPrefs.GetInt ("MiniGolem");
		TrophyMiniGolems ();
	}

	public void countSkulls(int count){
		PlayerPrefs.SetInt ("Skull", PlayerPrefs.GetInt ("Skull") + count);
		Skull = PlayerPrefs.GetInt ("Skull");
		TrophySkulls ();
	}
	
	private void TrophyMiniGolems(){

		if(MiniGolem == 3 && !trofeos[0]){
			trofeos[0] = true;
			unlock = true;
			PlayerPrefs.SetInt("Trofeo_MiniGolem", 1);
			TrophyTexture = Resources.Load<Texture2D>("Trofeos/trophy_minigolem");
			trophyQ.Enqueue(TrophyTexture);
		}

		TrophyPlatino ();
	}

	private void TrophySkulls(){
		
		if(Skull == 10 && !trofeos[7]){
			trofeos[7] = true;
			unlock = true;
			PlayerPrefs.SetInt("Trofeo_Esqueleto", 1);
			TrophyTexture = Resources.Load<Texture2D>("Trofeos/trophy_esqueleto");
			trophyQ.Enqueue(TrophyTexture);
		}

		TrophyPlatino ();
	}

	public void TrophyGolemLava(){

		if(!trofeos[1]){
			trofeos[1] = true;
			unlock = true;
			PlayerPrefs.SetInt("Trofeo_GolemLava", 1);
			TrophyTexture = Resources.Load<Texture2D>("Trofeos/trophy_golemlava");
			trophyQ.Enqueue(TrophyTexture);
		}

		TrophyPlatino ();
	}

	public void TrophyGolemIce(){

		if(!trofeos[8]){
			trofeos[8] = true;
			unlock = true;
			PlayerPrefs.SetInt("Trofeo_GolemHielo", 1);
			TrophyTexture = Resources.Load<Texture2D>("Trofeos/trophy_golemhielo");
			trophyQ.Enqueue(TrophyTexture);
		}

		TrophyPlatino ();
	}


	public void TrophyLevels(int level){

		if(!trofeos[2] && level == 5){
			trofeos[2] = true;
			unlock = true;
			PlayerPrefs.SetInt("Trofeo_Level05", 1);
			TrophyTexture = Resources.Load<Texture2D>("Trofeos/trophy_level5");
			trophyQ.Enqueue(TrophyTexture);
		}else if(!trofeos[3] && level == 10){
			trofeos[3] = true;
			unlock = true;
			PlayerPrefs.SetInt("Trofeo_Level10", 1);
			TrophyTexture = Resources.Load<Texture2D>("Trofeos/trophy_level10");
			trophyQ.Enqueue(TrophyTexture);
		}else if(!trofeos[4] && level == 20){
			trofeos[4] = true;
			unlock = true;
			PlayerPrefs.SetInt("Trofeo_Level20", 1);
			TrophyTexture = Resources.Load<Texture2D>("Trofeos/trophy_level20");
			trophyQ.Enqueue(TrophyTexture);
		}else if(!trofeos[5] && level == 50){
			trofeos[5] = true;
			unlock = true;
			PlayerPrefs.SetInt("Trofeo_Level50", 1);
			TrophyTexture = Resources.Load<Texture2D>("Trofeos/trophy_level50");
			trophyQ.Enqueue(TrophyTexture);
		}

		TrophyPlatino ();
	}

	public void TrophyDifficult(string dificult){
		
		if(!trofeos[9] && dificult == "Easy"){
			trofeos[9] = true;
			unlock = true;
			PlayerPrefs.SetInt("Trofeo_Easy", 1);
			TrophyTexture = Resources.Load<Texture2D>("Trofeos/trophy_easy");
			trophyQ.Enqueue(TrophyTexture);
		}else if(!trofeos[10] && dificult == "Normal"){
			trofeos[10] = true;
			unlock = true;
			PlayerPrefs.SetInt("Trofeo_Normal", 1);
			TrophyTexture = Resources.Load<Texture2D>("Trofeos/trophy_normal");
			trophyQ.Enqueue(TrophyTexture);
		}else if(!trofeos[11] && dificult == "Hard"){
			trofeos[11] = true;
			unlock = true;
			PlayerPrefs.SetInt("Trofeo_Hard", 1);
			TrophyTexture = Resources.Load<Texture2D>("Trofeos/trophy_hard");
			trophyQ.Enqueue(TrophyTexture);
		}else if(!trofeos[12] && dificult == "Extreme"){
			trofeos[12] = true;
			unlock = true;
			PlayerPrefs.SetInt("Trofeo_Extreme", 1);
			TrophyTexture = Resources.Load<Texture2D>("Trofeos/trophy_extreme");
			trophyQ.Enqueue(TrophyTexture);
		}	

		TrophyPlatino ();
	}

	public void TrophyCharacter(string character){
		
		if(!trofeos[13] && character == "hombre"){
			trofeos[13] = true;
			unlock = true;
			PlayerPrefs.SetInt("Trofeo_Guerrero", 1);
			TrophyTexture = Resources.Load<Texture2D>("Trofeos/trophy_guerrero");
			trophyQ.Enqueue(TrophyTexture);
		}else if(!trofeos[14] && character == "mujer"){
			trofeos[14] = true;
			unlock = true;
			PlayerPrefs.SetInt("Trofeo_Maga", 1);
			TrophyTexture = Resources.Load<Texture2D>("Trofeos/trophy_maga");
			trophyQ.Enqueue(TrophyTexture);
		}else if(!trofeos[15] && character == "joven"){
			trofeos[15] = true;
			unlock = true;
			PlayerPrefs.SetInt("Trofeo_Ladron", 1);
			TrophyTexture = Resources.Load<Texture2D>("Trofeos/trophy_ladron");
			trophyQ.Enqueue(TrophyTexture);
		}

		if (trofeos [13] && trofeos [14] && trofeos [15] && !trofeos [16]) {
			trofeos[16] = true;
			unlock = true;
			PlayerPrefs.SetInt("Trofeo_Todos", 1);
			TrophyTexture = Resources.Load<Texture2D>("Trofeos/trophy_todos");
			trophyQ.Enqueue(TrophyTexture);
		}

		TrophyPlatino ();
	}


	public void TrophyBusterSword(){

		if(!trofeos[6]){
			trofeos[6] = true;
			unlock = true;
			PlayerPrefs.SetInt("Trofeo_BusterSword", 1);
			TrophyTexture = Resources.Load<Texture2D>("Trofeos/trophy_bustersword");
			trophyQ.Enqueue(TrophyTexture);
		}

		TrophyPlatino ();
		
	}

	private bool TrophyPlatino(){

		for (int i = 0; i < trofeos.Length-1; i++) 
			if(!trofeos[i])
				return false;

		if(!trofeos[17]){
			trofeos[17] = true;
			unlock = true;
			PlayerPrefs.SetInt("Trofeo_Platino", 1);
			TrophyTexture = Resources.Load<Texture2D>("Trofeos/trophy_platino");
			trophyQ.Enqueue(TrophyTexture);
		}

		return true;
	}

	void loadTrophys(){
		trofeos [0] = PlayerPrefs.GetInt("Trofeo_MiniGolem")==1?true:false;
		trofeos [1] = PlayerPrefs.GetInt("Trofeo_GolemLava")==1?true:false;
		trofeos [2] = PlayerPrefs.GetInt("Trofeo_Level05")==1?true:false;
		trofeos [3] = PlayerPrefs.GetInt("Trofeo_Level10")==1?true:false;
		trofeos [4] = PlayerPrefs.GetInt("Trofeo_Level20")==1?true:false;
		trofeos [5] = PlayerPrefs.GetInt("Trofeo_Level50")==1?true:false;
		trofeos [6] = PlayerPrefs.GetInt("Trofeo_BusterSword")==1?true:false;
		trofeos [7] = PlayerPrefs.GetInt("Trofeo_Esqueleto")==1?true:false;
		trofeos [8] = PlayerPrefs.GetInt("Trofeo_GolemHielo")==1?true:false;
		trofeos [9] = PlayerPrefs.GetInt("Trofeo_Easy")==1?true:false;
		trofeos [10] = PlayerPrefs.GetInt("Trofeo_Normal")==1?true:false;
		trofeos [11] = PlayerPrefs.GetInt("Trofeo_Hard")==1?true:false;
		trofeos [12] = PlayerPrefs.GetInt("Trofeo_Extreme")==1?true:false;
		trofeos [13] = PlayerPrefs.GetInt("Trofeo_Guerrero")==1?true:false;
		trofeos [14] = PlayerPrefs.GetInt("Trofeo_Maga")==1?true:false;
		trofeos [15] = PlayerPrefs.GetInt("Trofeo_Ladron")==1?true:false;
		trofeos [16] = PlayerPrefs.GetInt("Trofeo_Todos")==1?true:false;
		trofeos [17] = PlayerPrefs.GetInt("Trofeo_Platino")==1?true:false;
	}

	public void getTrophys(string[] trophy_unlock){

		trophy_unlock [0] = PlayerPrefs.GetInt("Trofeo_MiniGolem")==1?"unlock_":"lock_";
		trophy_unlock [1] = PlayerPrefs.GetInt("Trofeo_GolemLava")==1?"unlock_":"lock_";
		trophy_unlock [2] = PlayerPrefs.GetInt("Trofeo_Level05")==1?"unlock_":"lock_";
		trophy_unlock [3] = PlayerPrefs.GetInt("Trofeo_Level10")==1?"unlock_":"lock_";
		trophy_unlock [4] = PlayerPrefs.GetInt("Trofeo_Level20")==1?"unlock_":"lock_";
		trophy_unlock [5] = PlayerPrefs.GetInt("Trofeo_Level50")==1?"unlock_":"lock_";
		trophy_unlock [6] = PlayerPrefs.GetInt("Trofeo_BusterSword")==1?"unlock_":"lock_";
		trophy_unlock [7] = PlayerPrefs.GetInt("Trofeo_Esqueleto")==1?"unlock_":"lock_";
		trophy_unlock [8] = PlayerPrefs.GetInt("Trofeo_GolemHielo")==1?"unlock_":"lock_";
		trophy_unlock [9] = PlayerPrefs.GetInt("Trofeo_Easy")==1?"unlock_":"lock_";
		trophy_unlock [10] = PlayerPrefs.GetInt("Trofeo_Normal")==1?"unlock_":"lock_";
		trophy_unlock [11] = PlayerPrefs.GetInt("Trofeo_Hard")==1?"unlock_":"lock_";
		trophy_unlock [12] = PlayerPrefs.GetInt("Trofeo_Extreme")==1?"unlock_":"lock_";
		trophy_unlock [13] = PlayerPrefs.GetInt("Trofeo_Guerrero")==1?"unlock_":"lock_";
		trophy_unlock [14] = PlayerPrefs.GetInt("Trofeo_Maga")==1?"unlock_":"lock_";
		trophy_unlock [15] = PlayerPrefs.GetInt("Trofeo_Ladron")==1?"unlock_":"lock_";
		trophy_unlock [16] = PlayerPrefs.GetInt("Trofeo_Todos")==1?"unlock_":"lock_";
		trophy_unlock [17] = PlayerPrefs.GetInt("Trofeo_Platino")==1?"unlock_":"lock_";
	}

	void OnGUI(){


		Rect trophy_box = new Rect (Screen.width - resizeTextureWidth(TrophyTexture), 
		                            0,
		                            resizeTextureWidth(TrophyTexture),
		                            resizeTextureHeight(TrophyTexture));

		/*if(trophyQ.Count != 0)
			print ("Hlaa: " + trophyQ.Peek().ToString());*/

		if(trophyQ.Count >= 1 && (int)timer == 0){
			if(trophyQ.Count == 0)
				unlock = false;
			sound = false;
			timer = 5f;
			trophyQ.Dequeue();
		}else if(trophyQ.Count >= 1 && unlock){
			timer -= Time.deltaTime;
			GUI.DrawTexture(trophy_box, trophyQ.Peek());
		}

		if((int)timer == 4 && !sound){
			this.music.play_trophy();
			sound = true;
		}

	}


	private float resizeTextureWidth(Texture2D texture){
		return ((Screen.width * texture.width) / (reference_width * 1.0f));
	}
	
	private float resizeTextureHeight(Texture2D texture){
		return ((Screen.height * texture.height) / (reference_height * 1.0f));
	}
}
