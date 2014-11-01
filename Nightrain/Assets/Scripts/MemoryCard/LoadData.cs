using UnityEngine;
using System.Collections;

public class LoadData : MonoBehaviour {

	public int loadCutScene(){
		return PlayerPrefs.GetInt("Cutscene");
	}

	public int loadFrames(){
		return PlayerPrefs.GetInt("Frames");
	}

	public string loadPath(){
		return PlayerPrefs.GetString("Path");
	}

	public int loadLevel(){
		return PlayerPrefs.GetInt("Level");
	}

	public string loadPlayer(){
		return PlayerPrefs.GetString ("Player");
	}

	public string loadDifficulty(){
		return PlayerPrefs.GetString("Difficult");
	}

}
