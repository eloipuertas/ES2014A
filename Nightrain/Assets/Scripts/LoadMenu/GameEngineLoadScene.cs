using UnityEngine;
using System.Collections;
using System.Threading;

public class GameEngineLoadScene : MonoBehaviour {

	private string level = "Level01_v2";

	// ========== TEXTURES ============
	private Texture2D backgroundTexture;

	public float delay = 5;
	private bool isLoading = false;

	// MEMORY CARD 
	private MemoryCard mc;
	private LoadData load;


	// Use this for initialization
	void Start () {

		// Memory Card Save/Load data
		this.mc = GameObject.FindGameObjectWithTag ("MemoryCard").GetComponent<MemoryCard> ();
		this.load = this.mc.loadData (); 

		this.backgroundTexture = Resources.Load<Texture2D>("LoadScene/background_load" + level);


	}

	void Update(){

		delay -= 1 * Time.deltaTime;

		if(delay <= 0 && !isLoading){
			this.isLoading = true;
			Application.LoadLevel(this.load.loadLevel());
		}
	}

	void OnGUI(){
		Rect background_box = new Rect (0, 0, Screen.width, Screen.height);
		GUI.DrawTexture (background_box, this.backgroundTexture);
	}



}
