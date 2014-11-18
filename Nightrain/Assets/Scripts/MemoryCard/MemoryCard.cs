using UnityEngine;
using System.Collections;

public class MemoryCard : MonoBehaviour {

	public SaveData save;
	public LoadData load;

	public SaveData saveData(){
		return save;
	}

	public LoadData loadData(){
		return load;
	}
}
