using UnityEngine;
using System.Collections;

public class FinalBoss_VideoAnimation_2 : MonoBehaviour {
	public GameObject boss;

	private Skeleton_boss_controller boss_ctrl;
	private float time;
	
	// Use this for initialization
	void Start () {
		boss_ctrl = boss.GetComponent <Skeleton_boss_controller> ();
		time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - time > 2.0f) {
			boss_ctrl.setAgressive (true);
			this.gameObject.SetActive (false);
		}
	}
}
