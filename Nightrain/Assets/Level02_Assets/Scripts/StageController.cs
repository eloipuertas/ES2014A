using UnityEngine;
using System.Collections;

public class StageController : MonoBehaviour {
	public GameObject[] walls;
	private int[] skeletons = new int[5];



	// Use this for initialization
	void Start () {
		for (int i=1; i<5; i++) {
			string str = "Skeleton"+i;
			print (str);
			skeletons[i] = GameObject.FindGameObjectsWithTag (str).Length;	
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		for (int i=1; i<5; i++) {
			if(skeletons[i] <= 0) {
				walls[i].GetComponent<firewall_Controller> ().setCompleted (true);
				skeletons[i] -= 1;
			}
		}
	}

	public void dead_npc(string tag) {
		print (tag);
		switch (tag) {
		case "Skeleton1":
			skeletons[1] -= 1;
			break;

		case "Skeleton2":
			skeletons[2] -= 1;
			break;

		case "Skeleton3":
			skeletons[3] -= 1;
			break;
		
		case "Skeleton4":
			skeletons[4] -= 1;
			break;
		}
	}
}
