using UnityEngine;
using System.Collections;

public class StageController : MonoBehaviour {
	public GameObject[] walls;
	private int[] stage_npcs = new int[9];
	private Music_Engine_Script music;
	private bool firedemon_stage = false;
	private bool icedemon_stage = false;
	private bool finalboss_stage = false;
	private int current_stage = 1;
	
	
	// Use this for initialization
	void Start () {
		string str;
		for (int i=1; i<9; i++) {
			if(i<4 || i==5) str = "Skeleton"+i;
			else str = "MiniIceDemon"+i;
			
			if(i==4 || i==6) stage_npcs[i] = 1;
			else stage_npcs[i] = GameObject.FindGameObjectsWithTag (str).Length;
			stage_npcs[8] += 1;
		}
		
		music = GameObject.FindGameObjectWithTag ("music_engine").GetComponent<Music_Engine_Script> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		for (int i=1; i<9; i++) {
			if(stage_npcs[i] == 0) {
				walls[i].GetComponent<firewall_Controller> ().setCompleted (true);
			}
		}
	}
	
	public void dead_npc(string tag) {
		switch (tag) {
		case "Skeleton1":
			stage_npcs[1] -= 1;
			break;
			
		case "Skeleton2":
			stage_npcs[2] -= 1;
			break;
			
		case "Skeleton3":
			stage_npcs[3] -= 1;
			break;
			
		case "FireDemon4":
			stage_npcs[4] -= 1;
			break;
			
		case "Skeleton5":
			stage_npcs[5] -= 1;
			break;
			
		case "IceDemon6":
			stage_npcs[6] -= 1;
			break;
			
		case "MiniIceDemon7":
			stage_npcs[7] -= 1;
			break;
			
		case "MiniIceDemon8":
			stage_npcs[8] -= 1;
			break;
		
		case "IceGolem":
			stage_npcs[8] -= 1;
			break;
		}
	}
	
	public void deactive_Stage (int n) {
		if (n >= 1) {
			walls[n].SetActive (true);
			walls [n].GetComponent<firewall_Controller> ().setBackBlockingWall ();
			current_stage += 1;
		}
		
		if (n == 3 && !firedemon_stage) {
			music.setBattleAudio();
			firedemon_stage = true;
		} else if (n == 5 && !icedemon_stage) {
			music.setBattleAudio();
			icedemon_stage = true;
		} else if (n == 8 && !finalboss_stage) {
			music.setBattleAudio();
			finalboss_stage = true;
		} else {
			music.setNormalAudio();
		}
	}

	public int getCurrentStage() {
		return current_stage;
	}
}
