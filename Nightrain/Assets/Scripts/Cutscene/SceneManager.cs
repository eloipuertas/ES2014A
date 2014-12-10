using UnityEngine;
using System.Collections;

public class SceneManager{

	public Texture2D[] cutscene;
	private int count;
	
	private int level;
	private int frames;
	private int delay;

	public SceneManager(int level, int frames, int delay){
		this.level = level;
		this.frames = frames;
		this.delay = delay;
	}

	public void LoadEscene(int scene, string path){

		switch (scene) {
			case 0:
				this.LoadResourcesIntroScene(path);
				break;
			default:
				Debug.Log ("Can't load the scene.");
				break;
		}
	}


	private void LoadResourcesIntroScene(string path){

		this.cutscene = new Texture2D[this.frames];
		
		for (int i = 0; i < cutscene.Length; i++) {
			if((i+1) < 10)
				this.cutscene[i] = Resources.Load<Texture2D>(path + "00"+(i+1));
			else if(((i+1) >= 10) && ((i+1) < 100))
				this.cutscene[i] = Resources.Load<Texture2D>(path + "0"+(i+1));
			else
				this.cutscene[i] = Resources.Load<Texture2D>(path + ""+(i+1));
			
		}

	}

	public void UpdateCutScene(GameObject screen_video){

		// Si presionamos 'Esc' o se acaba el video carga la escena que le toca
		if (Input.GetKeyDown (KeyCode.Escape))
			Application.LoadLevel (this.level);
		else if ((this.count / this.delay) == this.frames)
			Application.LoadLevel (this.level);
		else
			screen_video.renderer.material.mainTexture = this.cutscene [(this.count/this.delay)];

		this.count++;

	}
	
}
