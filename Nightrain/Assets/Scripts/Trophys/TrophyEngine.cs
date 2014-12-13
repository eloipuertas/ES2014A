using UnityEngine;
using System.Collections.Generic;

public class TrophyEngine : MonoBehaviour {

	private const int reference_width = 1366; 
	private const int reference_height = 598;

	private int MiniGolem;
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

		print ("MiniGolem: " + trofeos [0]);

		TrophyTexture = Resources.Load<Texture2D>("Trofeos/trophy_platino");

		this.music = GameObject.FindGameObjectWithTag("music_engine").GetComponent<Music_Engine_Script>();

	}
	

	public void countMiniGolem(int count){
		PlayerPrefs.SetInt ("MiniGolem", PlayerPrefs.GetInt ("MiniGolem") + count);
		MiniGolem = PlayerPrefs.GetInt ("MiniGolem");
		print ("MiniGolem: " + MiniGolem);
		TrophyMiniGolems ();
	}
	
	private void TrophyMiniGolems(){

		if(MiniGolem == 3 && !trofeos[0]){
			print ("Trophy MiniGolems");
			trofeos[0] = true;
			unlock = true;
			PlayerPrefs.SetInt("Trofeo_MiniGolem", 1);
			TrophyTexture = Resources.Load<Texture2D>("Trofeos/trophy_minigolem");
			trophyQ.Enqueue(TrophyTexture);
		}

	}

	public void TrophyGolemLava(){

		print ("Trophy Golem Lava");
		if(!trofeos[1]){
			trofeos[1] = true;
			unlock = true;
			PlayerPrefs.SetInt("Trofeo_GolemLava", 1);
			TrophyTexture = Resources.Load<Texture2D>("Trofeos/trophy_golemlava");
			trophyQ.Enqueue(TrophyTexture);
		}

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
		
	}

	public void TrophyBusterSword(){
		
		print ("Trophy Buster Sword");
		if(!trofeos[6]){
			trofeos[6] = true;
			unlock = true;
			PlayerPrefs.SetInt("Trofeo_BusterSword", 1);
			TrophyTexture = Resources.Load<Texture2D>("Trofeos/trophy_bustersword");
			trophyQ.Enqueue(TrophyTexture);
		}
		
	}

	void loadTrophys(){
		trofeos [0] = PlayerPrefs.GetInt("Trofeo_MiniGolem")==1?true:false;
		trofeos [1] = PlayerPrefs.GetInt("Trofeo_GolemLava")==1?true:false;
		trofeos [2] = PlayerPrefs.GetInt("Trofeo_Level05")==1?true:false;
		trofeos [3] = PlayerPrefs.GetInt("Trofeo_Level10")==1?true:false;
		trofeos [4] = PlayerPrefs.GetInt("Trofeo_Level20")==1?true:false;
		trofeos [5] = PlayerPrefs.GetInt("Trofeo_Level50")==1?true:false;
		trofeos [6] = PlayerPrefs.GetInt("Trofeo_BusterSword")==1?true:false;
	}

	public void getTrophys(string[] trophy_unlock){

		trophy_unlock [0] = PlayerPrefs.GetInt("Trofeo_MiniGolem")==1?"unlock_":"lock_";
		trophy_unlock [1] = PlayerPrefs.GetInt("Trofeo_GolemLava")==1?"unlock_":"lock_";
		trophy_unlock [2] = PlayerPrefs.GetInt("Trofeo_Level05")==1?"unlock_":"lock_";
		trophy_unlock [3] = PlayerPrefs.GetInt("Trofeo_Level10")==1?"unlock_":"lock_";
		trophy_unlock [4] = PlayerPrefs.GetInt("Trofeo_Level20")==1?"unlock_":"lock_";
		trophy_unlock [5] = PlayerPrefs.GetInt("Trofeo_Level50")==1?"unlock_":"lock_";
		trophy_unlock [6] = PlayerPrefs.GetInt("Trofeo_BusterSword")==1?"unlock_":"lock_";
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
			trofeos[0] = false;
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
