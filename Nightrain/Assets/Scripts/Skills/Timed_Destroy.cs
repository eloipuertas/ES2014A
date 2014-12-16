using UnityEngine;
using System.Collections;

public class Timed_Destroy : MonoBehaviour {
	public float destroyTime = 1.0f;
	private float initTime;
	private float actualTime; 

	// Use this for initialization
	/*void Start () {
		initTime = Time.time;
	}*/
	
	// Update is called once per frame
	void Update () {
		destroyTime -= Time.deltaTime;

		/*actualTime = Time.time;
		if (actualTime - initTime > destroyTime) {*/
		if (Mathf.Round(destroyTime) <= 0) {
			//Skill_Controller.setEffect(false);

			if(this.name == "Fireball_Skill(Clone)")
				ActionBarScript.disabledSkill1 = false;
			else if(this.name == "Daga_skill(Clone)")
				ActionBarScript.disabledSkill2 = false;
			else if(this.name == "Warrior_Aura1(Clone)")
				ActionBarScript.disabledSkill3 = false;
			Destroy (gameObject);
		}
	}
}
