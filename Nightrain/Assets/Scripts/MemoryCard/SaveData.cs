using UnityEngine;
using System.Collections;

public class SaveData : MonoBehaviour {

	public void saveTimePlayed(float time){
	

		int hour, min, second;
		string str = "";

		hour = ((int)time / 3600);
		min = ((int)time % 3600) / 60;
		second = ((int)time % 3600) % 60;

		if(hour < 10)
			str += "0" + hour;
		else 
			str += hour;

		if(min < 10)
			str += ":0" + min;
		else 
			str += ":" + min;

		if(second < 10)
			str += ":0" + second;
		else
			str += ":" + second;
			
		PlayerPrefs.SetString("TimeFormat", str);
		PlayerPrefs.SetFloat("Time", time);
	}

	public void saveTimeFormat(string time){
		PlayerPrefs.SetString ("TimeFormat", time);
	}

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

		if (level == 3) 
			PlayerPrefs.SetString ("LevelName", "La aldea maldita");
		if (level == 4)
			PlayerPrefs.SetString ("LevelName", "El monte del destino");

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
		PlayerPrefs.SetInt("LVL", 1);
		PlayerPrefs.SetInt("VIT", 150);
		PlayerPrefs.SetInt("PM", 40);
		PlayerPrefs.SetInt("STR", 10);
		PlayerPrefs.SetInt("DEF", 8);
		PlayerPrefs.SetInt("SPD", 5);
		PlayerPrefs.SetInt("EXP", 0);
	}

	public void SageAttributes(){
		PlayerPrefs.SetInt("LVL", 1);
		PlayerPrefs.SetInt("VIT", 75);
		PlayerPrefs.SetInt("PM", 125);
		PlayerPrefs.SetInt("STR", 6);
		PlayerPrefs.SetInt("DEF", 5);
		PlayerPrefs.SetInt("SPD", 7);
		PlayerPrefs.SetInt("EXP", 0);
	}

	public void ThiefAttributes(){
		PlayerPrefs.SetInt("LVL", 1);
		PlayerPrefs.SetInt("VIT", 100);
		PlayerPrefs.SetInt("PM", 75);
		PlayerPrefs.SetInt("STR", 7);
		PlayerPrefs.SetInt("DEF", 6);
		PlayerPrefs.SetInt("SPD", 9);
		PlayerPrefs.SetInt("EXP", 0);
	}

	public void saveStatus(string attribute, int value){

		if(attribute == "LVL" && value < 100){
			PlayerPrefs.SetInt (attribute, value);
			return;
		}

		if(attribute == "EXP"){
			PlayerPrefs.SetInt (attribute, value);
			return;
		}

		if(value <= 510 && (attribute == "VIT" || attribute == "PM")){
			PlayerPrefs.SetInt (attribute, value);
			return;
		}else if(value > 500 && (attribute == "VIT" || attribute == "PM")){
			print ("Attribute: " + attribute);
			PlayerPrefs.SetInt (attribute, 510);
			return;
		}

		if(value <= 255  && (attribute != "VIT" || attribute != "PM"))
			PlayerPrefs.SetInt (attribute, value);
		else if(value > 255  && (attribute != "VIT" || attribute != "PM"))
			PlayerPrefs.SetInt (attribute, 255);
	}

	public void saveNumItemsInventory(int items){
		PlayerPrefs.SetInt ("NumItemsInventory", items);
	}

	public void saveInventoryItem(int i, Item item){

		string str_item = item.id + "," + item.name + "," + item.type + "," + item.VIT + "," + item.PM +
						"," + item.FRZ + "," + item.DEF + "," + item.SPD + "," + item.heal + "," + item.magic +
						"," + item.ItemTexture.name + "," + item.x + "," + item.y + "," + item.width + "," + item.height;
		print ("Heal: " + item.heal + " Magic: " + item.magic );
		PlayerPrefs.SetString ("Item" + i, str_item);
	}

	public void saveEquipedItem(Item[] equip){
		//"Weapon, Shield, Helmet, Armor, Boots"
		string weapon_item = "";
		string shield_item = "";
		string helmet_item = "";
		string armor_item = "";
		string boots_item = "";

		if(equip[0] != null)
			weapon_item = equip [0].id + "," + equip [0].name + "," + equip [0].type + "," + equip [0].VIT + "," + equip [0].PM +
						  "," + equip [0].FRZ + "," + equip [0].DEF + "," + equip [0].SPD + "," + equip [0].heal + "," + equip [0].magic +
						  "," + equip [0].ItemTexture.name + "," + equip [0].x + "," + equip [0].y + "," + equip [0].width + "," + equip [0].height;

		if(equip[1] != null)
			shield_item = equip [1].id + "," + equip [1].name + "," + equip [1].type + "," + equip [1].VIT + "," + equip [1].PM +
						  "," + equip [1].FRZ + "," + equip [1].DEF + "," + equip [1].SPD + "," + equip [1].heal + "," + equip [1].magic +
						  "," + equip [1].ItemTexture.name + "," + equip [1].x + "," + equip [1].y + "," + equip [1].width + "," + equip [1].height;

		if(equip[2] != null)
			helmet_item = equip [2].id + "," + equip [2].name + "," + equip [2].type + "," + equip [2].VIT + "," + equip [2].PM +
						  "," + equip [2].FRZ + "," + equip [2].DEF + "," + equip [2].SPD + "," + equip [2].heal + "," + equip [2].magic +
						  "," + equip [2].ItemTexture.name + "," + equip [2].x + "," + equip [2].y + "," + equip [2].width + "," + equip [2].height;

		if(equip[3] != null)
			armor_item = equip [3].id + "," + equip [3].name + "," + equip [3].type + "," + equip [3].VIT + "," + equip [3].PM +
						  "," + equip [3].FRZ + "," + equip [3].DEF + "," + equip [3].SPD + "," + equip [3].heal + "," + equip [3].magic +
						  "," + equip [3].ItemTexture.name + "," + equip [3].x + "," + equip [3].y + "," + equip [3].width + "," + equip [3].height;

		if(equip[4] != null)
			boots_item = equip [4].id + "," + equip [4].name + "," + equip [4].type + "," + equip [4].VIT + "," + equip [4].PM +
						  "," + equip [4].FRZ + "," + equip [4].DEF + "," + equip [4].SPD + "," + equip [4].heal + "," + equip [4].magic +
						  "," + equip [4].ItemTexture.name + "," + equip [4].x + "," + equip [4].y + "," + equip [4].width + "," + equip [4].height;
		

		PlayerPrefs.SetString ("Equip_weapon", weapon_item);
		PlayerPrefs.SetString ("Equip_shield", shield_item);
		PlayerPrefs.SetString ("Equip_helmet", helmet_item);
		PlayerPrefs.SetString ("Equip_armor", armor_item);
		PlayerPrefs.SetString ("Equip_boots", boots_item);
	}

}
