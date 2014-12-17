using UnityEngine;
using System.Collections.Generic;

public class ItemsInventory_lvl2 : MonoBehaviour {

	private static Dictionary<int, Item> weapon_list = new Dictionary<int, Item>();
	private static Dictionary<int, Item> shield_list = new Dictionary<int, Item>();
	private static Dictionary<int, Item> armor_list = new Dictionary<int, Item>();
	private static Dictionary<int, Item> healing_list = new Dictionary<int, Item>();


	// === WEAPON ===
	
	public static void addWeapon(int id, Item item){
		
		if (!ItemsInventory_lvl2.weapon_list.ContainsKey (id)) {
			ItemsInventory_lvl2.weapon_list.Add (id, item);
		}
	}
	
	public static Weapon getWeapon(int id){
		
		Weapon weapon = new Weapon ();
		weapon.id = ItemsInventory_lvl2.weapon_list [id].id;
		weapon.name = ItemsInventory_lvl2.weapon_list [id].name;
		weapon.type = ItemsInventory_lvl2.weapon_list [id].type;
		weapon.VIT = ItemsInventory_lvl2.weapon_list [id].VIT;
		weapon.PM = ItemsInventory_lvl2.weapon_list [id].PM;
		weapon.FRZ = ItemsInventory_lvl2.weapon_list [id].FRZ;
		weapon.DEF = ItemsInventory_lvl2.weapon_list [id].DEF;
		weapon.SPD = ItemsInventory_lvl2.weapon_list [id].SPD;
		weapon.ItemTexture = ItemsInventory_lvl2.weapon_list [id].ItemTexture;
		weapon.width = ItemsInventory_lvl2.weapon_list [id].width;
		weapon.height = ItemsInventory_lvl2.weapon_list [id].height;
		
		return weapon;
	}


	// === SHIELD ===

	public static void addShield(int id, Item item){
		
		if (!ItemsInventory_lvl2.shield_list.ContainsKey (id)) {
			ItemsInventory_lvl2.shield_list.Add (id, item);
		}
	}

	public static Shield getShield(int id){
		
		Shield shield = new Shield ();
		shield.id = ItemsInventory_lvl2.shield_list [id].id;
		shield.name = ItemsInventory_lvl2.shield_list [id].name;
		shield.type = ItemsInventory_lvl2.shield_list [id].type;
		shield.VIT = ItemsInventory_lvl2.shield_list [id].VIT;
		shield.PM = ItemsInventory_lvl2.shield_list [id].PM;
		shield.FRZ = ItemsInventory_lvl2.shield_list [id].FRZ;
		shield.DEF = ItemsInventory_lvl2.shield_list [id].DEF;
		shield.SPD = ItemsInventory_lvl2.shield_list [id].SPD;
		shield.ItemTexture = ItemsInventory_lvl2.shield_list [id].ItemTexture;
		shield.width = ItemsInventory_lvl2.shield_list [id].width;
		shield.height = ItemsInventory_lvl2.shield_list [id].height;
		
		return shield;
	}


	// === ARMOR ===
	
	public static void addArmor(int id, Item item){
		
		if (!ItemsInventory_lvl2.armor_list.ContainsKey (id)) {
			ItemsInventory_lvl2.armor_list.Add (id, item);
		}
	}
	
	public static Armor getArmor(int id){
		
		Armor armor = new Armor ();
		armor.id = ItemsInventory_lvl2.armor_list [id].id;
		armor.name = ItemsInventory_lvl2.armor_list [id].name;
		armor.type = ItemsInventory_lvl2.armor_list [id].type;
		armor.VIT = ItemsInventory_lvl2.armor_list [id].VIT;
		armor.PM = ItemsInventory_lvl2.armor_list [id].PM;
		armor.FRZ = ItemsInventory_lvl2.armor_list [id].FRZ;
		armor.DEF = ItemsInventory_lvl2.armor_list [id].DEF;
		armor.SPD = ItemsInventory_lvl2.armor_list [id].SPD;
		armor.ItemTexture = ItemsInventory_lvl2.armor_list [id].ItemTexture;
		armor.width = ItemsInventory_lvl2.armor_list [id].width;
		armor.height = ItemsInventory_lvl2.armor_list [id].height;
		
		return armor;
	}


	// === HEALING ===
	
	public static void addHealing(int id, Item item){
		
		if (!ItemsInventory_lvl2.healing_list.ContainsKey (id)) {
			ItemsInventory_lvl2.healing_list.Add (id, item);
		}
	}
	
	public static Healing getHealing(int id){
		
		Healing healing = new Healing ();
		healing.id = ItemsInventory_lvl2.healing_list [id].id;
		healing.name = ItemsInventory_lvl2.healing_list [id].name;
		healing.type = ItemsInventory_lvl2.healing_list [id].type;
		healing.VIT = ItemsInventory_lvl2.healing_list [id].VIT;
		healing.PM = ItemsInventory_lvl2.healing_list [id].PM;
		healing.FRZ = ItemsInventory_lvl2.healing_list [id].FRZ;
		healing.DEF = ItemsInventory_lvl2.healing_list [id].DEF;
		healing.SPD = ItemsInventory_lvl2.healing_list [id].SPD;
		healing.heal = ItemsInventory_lvl2.healing_list [id].heal;
		healing.ItemTexture = ItemsInventory_lvl2.healing_list [id].ItemTexture;
		healing.width = ItemsInventory_lvl2.healing_list [id].width;
		healing.height = ItemsInventory_lvl2.healing_list [id].height;
		
		return healing;
	}


}
