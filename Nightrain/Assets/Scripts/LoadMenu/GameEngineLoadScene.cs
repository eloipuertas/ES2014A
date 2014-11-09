using UnityEngine;
using System.Collections;
using System.Threading;

public class GameEngineLoadScene : MonoBehaviour {

	private string level = "Level01";

	// ========== TEXTURES ============
	private Texture2D backgroundTexture;

	// Use this for initialization
	void Start () {

		this.backgroundTexture = Resources.Load<Texture2D>("LoadScene/background_load" + level);
		Application.LoadLevel(3);
	}

	void OnGUI(){
		Rect background_box = new Rect (0, 0, Screen.width, Screen.height);
		GUI.DrawTexture (background_box, this.backgroundTexture);
	}



}
