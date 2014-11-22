using UnityEngine;
using System.Collections;

public class EquipWeapons : MonoBehaviour {

	public GameObject weapon;
	public GameObject shield;

	private static GameObject w;
	private static GameObject s;

	private static Transform weaponTransform;
	private static Transform shieldTransform;

	void Start(){

		weaponTransform = weapon.transform;
		shieldTransform = shield.transform;
	}
	

	public static void setWeapon(Weapon weapon){

		w = Instantiate(Resources.Load<GameObject>("Prefabs/Inventory/Weapons/" + weapon.name)) as GameObject;
		w.transform.position = weaponTransform.position;
		w.transform.parent = weaponTransform;
		//w.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));

	}
	

	public static void removeWeapon(){
		Destroy (w);
	}


	public static void setShield(Shield shield){

		s = Instantiate(Resources.Load<GameObject>("Prefabs/Inventory/Shields/" + shield.name)) as GameObject;
		s.transform.position = shieldTransform.position;
		s.transform.parent = shieldTransform;
		//s.transform.rotation = Quaternion.Euler(new Vector3(20, 160, 0));
	}


	public static void removeShield(){
		Destroy (s);
	}


}
