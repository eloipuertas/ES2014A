using UnityEngine;
using System.Collections;

public class LoadData_lvl2 : MonoBehaviour {
	
	public float loadTimePlayed(){
		return PlayerPrefs.GetFloat("Time");
	}
	
	public string loadTimeFormat(){
		return PlayerPrefs.GetString("TimeFormat");
	}
	
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
	
	public string loadLevelName(){
		return PlayerPrefs.GetString("LevelName");
	}
	
	public string loadLoading(){
		return PlayerPrefs.GetString("Loading");
	}
	
	public string loadPlayer(){
		return PlayerPrefs.GetString ("Player");
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
