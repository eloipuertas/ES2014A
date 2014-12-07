using UnityEngine;
using System.Collections;

public class MemoryCard_lvl2 : MonoBehaviour {

	public SaveData_lvl2 save;
	public LoadData_lvl2 load;

	public SaveData_lvl2 saveData(){
		return save;
	}

	public LoadData_lvl2 loadData(){
		return load;
	}
}
