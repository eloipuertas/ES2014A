using UnityEngine;
using System.Collections;

public class GameEngineScene01 : MonoBehaviour {

	public Texture2D[] cutscene;
	public GameObject screen_video;

	private int frame = 0;
	public int delay;

	// Use this for initialization
	void Start() { 

		cutscene = new Texture2D[800];

		for (int i = 0; i < cutscene.Length; i++) {
			if((i+1) < 10)
				cutscene[i] = Resources.Load<Texture2D>("Cutscenes/Intro/Intro 00"+(i+1));
			else if(((i+1) >= 10) && ((i+1) < 100))
				cutscene[i] = Resources.Load<Texture2D>("Cutscenes/Intro/Intro 0"+(i+1));
			else
				cutscene[i] = Resources.Load<Texture2D>("Cutscenes/Intro/Intro "+(i+1));

		}

		this.screen_video.renderer.material.mainTexture = this.cutscene [9];
	}

	void Update(){

		if (Input.GetKeyDown (KeyCode.Escape))
			Application.LoadLevel(1);
		else if ((frame/delay) == 800)
			Application.LoadLevel (1);
		else
			this.screen_video.renderer.material.mainTexture = this.cutscene [(frame/delay)];
		frame++;

	}
	

}
