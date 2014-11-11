using UnityEngine;
using System.Collections.Generic;

public class ItemsInventory : MonoBehaviour {

	//private Dictionary<int, Item> armor_dict = new Dictionary<int, Item>();
	private static Dictionary<int, Item> armor_list = new Dictionary<int, Item>();
	//public List<Armor> armorInspector;
	//private static List<Armor> armor;

	// Use this for initialization
	void Start () {
		//armor_hash = new Hashtable();
		//armor_dict = armor_list;
	}

	// Devolvemos un Item duplicado de tipo Armor
	/*public static Armor getArmor(int id){
		Armor armor = new Armor ();
		armor.ItemTexture = ItemsInventory.armor [id].ItemTexture;
		armor.width = ItemsInventory.armor [id].width;
		armor.height = ItemsInventory.armor [id].height;
		return armor;
	}*/

	public static void addArmor(int id, Item item){
		//armor.Add (item);
		//armor_hash.Add()
		if (!ItemsInventory.armor_list.ContainsKey (id)) {
			ItemsInventory.armor_list.Add (id, item);
		}

		//Debug.Log (armor_list [id].ItemTexture.name);
	}

	public static Armor getArmor(int id){
		Armor armor = new Armor ();
		/*armor.ItemTexture = ItemsInventory.armor [id].ItemTexture;
		armor.width = ItemsInventory.armor [id].width;
		armor.height = ItemsInventory.armor [id].height;*/
		armor.ItemTexture = ItemsInventory.armor_list [id].ItemTexture;
		armor.width = ItemsInventory.armor_list [id].width;
		armor.height = ItemsInventory.armor_list [id].height;
		return armor;
	}
	/*public static Armor getArmor(){
		Armor armor = new Armor ();
		armor.ItemTexture = ItemsInventory.armor [id].ItemTexture;
		armor.width = ItemsInventory.armor [id].width;
		armor.height = ItemsInventory.armor [id].height;
		return armor;
	}*/

}
