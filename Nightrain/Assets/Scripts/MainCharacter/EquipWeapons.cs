using UnityEngine;
using System.Collections;

public class EquipWeapons : MonoBehaviour {

	public GameObject weapon;
	public GameObject shield;

	private static GameObject w;
	private static GameObject s;

	private static Transform weaponTransform;
	private static Transform shieldTransform;

	private static ItemDrop item;

	// ========== CHARACTER ============
	private GameObject character;
	private static CharacterScript cs;

	void Start(){

		
		this.character = GameObject.FindGameObjectWithTag ("Player");
		cs = this.character.GetComponent<CharacterScript> ();

		weaponTransform = weapon.transform;
		shieldTransform = shield.transform;
 
	}
	

	public static void setWeapon(Weapon weapon){

		if(w == null){
			w = Instantiate(Resources.Load<GameObject>("Prefabs/Inventory/Weapons/" + weapon.name)) as GameObject;
			w.transform.position = weaponTransform.position;
			w.transform.parent = weaponTransform;
		}else{
			Destroy(w);
			w = Instantiate(Resources.Load<GameObject>("Prefabs/Inventory/Weapons/" + weapon.name)) as GameObject;
			w.transform.position = weaponTransform.position;
			w.transform.parent = weaponTransform;
		}
		//w.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));

	}
	

	public static void removeWeapon(Item i){

		if(w != null){
			item = w.GetComponent<ItemDrop> ();
			cs.setFRZ(-item.FRZ);
			if(item.id == i.id)
				Destroy (w);
		}
	}


	public static void setShield(Shield shield){

		if(s == null){
			s = Instantiate(Resources.Load<GameObject>("Prefabs/Inventory/Shields/" + shield.name)) as GameObject;
			s.transform.position = shieldTransform.position;
			s.transform.parent = shieldTransform;
		}else{
			Destroy(s);
			s = Instantiate(Resources.Load<GameObject>("Prefabs/Inventory/Shields/" + shield.name)) as GameObject;
			s.transform.position = shieldTransform.position;
			s.transform.parent = shieldTransform;
		}
		//s.transform.rotation = Quaternion.Euler(new Vector3(20, 160, 0));
	}


	public static void removeShield(Item i){

		if(s != null){
			item = s.GetComponent<ItemDrop> ();
			cs.setDEF(-item.FRZ);
			if(i.id == item.id)
				Destroy (s);
		}
	}


}
