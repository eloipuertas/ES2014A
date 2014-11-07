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
	
	public void saveDifficult(string difficult){
		PlayerPrefs.SetString("Difficult", difficult);
	}
}
