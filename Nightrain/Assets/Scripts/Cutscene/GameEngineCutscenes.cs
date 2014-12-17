using UnityEngine;
using System.Collections;

public class GameEngineCutscenes : MonoBehaviour {

	private SceneManager sm;

	// LOAD DATA to GameEngine
	private int cutscene;
	private int frames;
	private string path;
	private int scene;

	public GameObject screen_video;
	public int delay;

	// Use this for initialization
	void Start() { 

		this.cutscene = PlayerPrefs.GetInt ("Cutscene");
		this.frames = PlayerPrefs.GetInt ("Frames");
		this.path = PlayerPrefs.GetString ("Path");
		this.scene = PlayerPrefs.GetInt ("Scene");

		this.sm = new SceneManager (this.scene, this.frames, this.delay);
		this.sm.LoadEscene (this.cutscene, this.path);

		this.audio.Play ();
	}

	void Update(){
		sm.UpdateCutScene (this.screen_video);
	}
	

}
