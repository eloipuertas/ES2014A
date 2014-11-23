using UnityEngine;
using System.Collections;

public class SaveData : MonoBehaviour {

	public void saveCutScene(int cutscene){
		PlayerPrefs.SetInt("Cutscene", cutscene);
	}
	
	public void saveFrames(int frames){
		PlayerPrefs.SetInt("Frames", frames);
	}
	
	public void savePath(string path){
		PlayerPrefs.SetString("Path", path);
	}
	
	public void saveLevel(int level){
		PlayerPrefs.SetInt("Level", level);
	}
	
	public void savePlayer(string player){
		PlayerPrefs.SetString ("Player", player);
	}

	public void saveLoading(string loading){
		PlayerPrefs.SetString ("Loading", loading);
	}
	
	public void saveDifficult(string difficult){
		PlayerPrefs.SetString("Difficult", difficult);
	}

	public void WarriorAttributes(){
		PlayerPrefs.SetInt("Nv", 1);
		PlayerPrefs.SetInt("VIT", 150);
		PlayerPrefs.SetInt("PM", 50);
		PlayerPrefs.SetInt("STR", 10);
		PlayerPrefs.SetInt("DEF", 8);
		PlayerPrefs.SetInt("SPD", 5);
	}

	public void SageAttributes(){
		PlayerPrefs.SetInt("Nv", 1);
		PlayerPrefs.SetInt("VIT", 75);
		PlayerPrefs.SetInt("PM", 125);
		PlayerPrefs.SetInt("STR", 6);
		PlayerPrefs.SetInt("DEF", 5);
		PlayerPrefs.SetInt("SPD", 7);
	}

	public void ThiefAttributes(){
		PlayerPrefs.SetInt("Nv", 1);
		PlayerPrefs.SetInt("VIT", 100);
		PlayerPrefs.SetInt("PM", 75);
		PlayerPrefs.SetInt("STR", 7);
		PlayerPrefs.SetInt("DEF", 6);
		PlayerPrefs.SetInt("SPD", 9);
	}

	public void saveStatus(string attribute, int value){
		PlayerPrefs.SetInt (attribute, value);
	}

}
