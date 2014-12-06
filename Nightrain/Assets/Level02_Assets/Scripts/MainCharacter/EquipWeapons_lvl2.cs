using UnityEngine;
using System.Collections;

public class EquipWeapons_lvl2 : MonoBehaviour {

	public GameObject weapon;
	public GameObject shield;

	private static GameObject w;
	private static GameObject s;

	private static Transform weaponTransform;
	private static Transform shieldTransform;

	private static ItemDrop_lvl2 item;

	// ========== CHARACTER ============
	private GameObject character;
	private static CharacterScript_lvl2 cs;

	void Start(){

		
		this.character = GameObject.FindGameObjectWithTag ("Player");
		cs = this.character.GetComponent<CharacterScript_lvl2> ();

		weaponTransform = weapon.transform;
		shieldTransform = shield.transform;
 
	}
	

	public static void setWeapon(Weapon weapon){

		if(w == null){
			w = Instantiate(Resources.Load<GameObject>("Prefabs/Inventory/Weapons/" + weapon.name)) as GameObject;
			w.transform.position = weaponTransform.position;
			w.transform.parent = weaponTransform;
			cs.setFRZ(weapon.FRZ);
		}else{
			item = w.GetComponent<ItemDrop_lvl2> ();
			cs.setFRZ(-item.FRZ);
			Destroy(w);
			w = Instantiate(Resources.Load<GameObject>("Prefabs/Inventory/Weapons/" + weapon.name)) as GameObject;
			w.transform.position = weaponTransform.position;
			w.transform.parent = weaponTransform;
			cs.setFRZ(weapon.FRZ);
		}
		//w.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));

	}
	

	public static void removeWeapon(Item i){

		if(w != null){
			item = w.GetComponent<ItemDrop_lvl2> ();

			if(item.id == i.id){
				cs.setFRZ(-item.FRZ);
				Destroy (w);
			}
		}
	}


	public static void setShield(Shield shield){

		if(s == null){
			s = Instantiate(Resources.Load<GameObject>("Prefabs/Inventory/Shields/" + shield.name)) as GameObject;
			s.transform.position = shieldTransform.position;
			s.transform.parent = shieldTransform;
			cs.setDEF(shield.DEF);
		}else{
			item = s.GetComponent<ItemDrop_lvl2> ();
			cs.setDEF(-item.DEF);
			Destroy(s);
			s = Instantiate(Resources.Load<GameObject>("Prefabs/Inventory/Shields/" + shield.name)) as GameObject;
			s.transform.position = shieldTransform.position;
			s.transform.parent = shieldTransform;
			cs.setDEF(shield.DEF);
		}
		//s.transform.rotation = Quaternion.Euler(new Vector3(20, 160, 0));
	}


	public static void removeShield(Item_lvl2 i){

		if(s != null){
			item = s.GetComponent<ItemDrop_lvl2> ();

			if(i.id == item.id){
				cs.setDEF(-item.DEF);
				Destroy (s);
			}
		}
	}


}
