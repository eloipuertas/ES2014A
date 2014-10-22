using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	public Texture2D[] cutscene;
	public GameObject screen_video;
	public int frame = 0;

	// Use this for initialization
	void Start() { 

		cutscene = new Texture2D[500];

		for (int i = 0; i < cutscene.Length; i++) {
			if((i+1) < 100){
				cutscene[i] = Resources.Load<Texture2D>("Cutscenes/Intro/Intro 00"+(i+1));
			}else{
				cutscene[i] = Resources.Load<Texture2D>("Cutscenes/Intro/Intro "+(i+1));
			}
		}

		this.screen_video.renderer.material.mainTexture = this.cutscene [9];
	}

	void Update(){

		if (frame > 500)
			frame = 0;

		this.screen_video.renderer.material.mainTexture = this.cutscene [frame];
		frame++;

	}
	

}
