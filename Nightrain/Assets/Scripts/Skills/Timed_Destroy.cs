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
		print ("Destroy time: " + destroyTime);
		/*actualTime = Time.time;
		if (actualTime - initTime > destroyTime) {*/
		if (Mathf.Round(destroyTime) <= 0) {
			print("Destruyo objeto:");
			Skill_Controller.setEffect(false);
			ActionBarScript.disabledSkill1 = false;
			ActionBarScript.disabledSkill2 = false;
			ActionBarScript.disabledSkill3 = false;
			Destroy (gameObject);
		}
	}
}
