using UnityEngine;
using System.Collections;

public class AICount : MonoBehaviour {
	public Transform Player;
	
	// Use this for initialization
	void Start () {
		if(Player){
	PlayerController p=(PlayerController)Player.GetComponent("PlayerController");
		if(p){
		p.TotalAICount=p.TotalAICount+1;
				
		}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
