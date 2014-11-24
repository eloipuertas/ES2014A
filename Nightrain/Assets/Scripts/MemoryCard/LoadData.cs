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

	public string loadLoading(){
		return PlayerPrefs.GetString("Loading");
	}

	public string loadPlayer(){
		return PlayerPrefs.GetString ("Character");
	}

	public string loadDifficulty(){
		return PlayerPrefs.GetString("Difficult");
	}

	public int loadLVL(){
		return PlayerPrefs.GetInt("LVL");
	}

	public int loadVIT(){
		return PlayerPrefs.GetInt("VIT");
	}

	public int loadPM(){
		return PlayerPrefs.GetInt("PM");
	}

	public int loadSTR(){
		return PlayerPrefs.GetInt("STR");
	}

	public int loadDEF(){
		return PlayerPrefs.GetInt("DEF");
	}

	public int loadSPD(){
		return PlayerPrefs.GetInt("SPD");
	}

	public int loadEXP(){
		return PlayerPrefs.GetInt("EXP");
	}

}
