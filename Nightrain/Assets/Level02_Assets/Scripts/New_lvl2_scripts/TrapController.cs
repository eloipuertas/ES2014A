using UnityEngine;
using System.Collections;

public class TrapController : MonoBehaviour {
	private GameObject[] firetraps;
	private ParticleSystem particle;

	private float firetraps_time = 0.0f;
	private float activated_firetrap = 3.0f;

	// Use this for initialization
	void Start () {
		firetraps = GameObject.FindGameObjectsWithTag ("FireTrap");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		activated_firetrap = Time.time - firetraps_time;
		if (activated_firetrap >= 1.5f) {
			firetraps = GameObject.FindGameObjectsWithTag ("FireTrap");
			setFireTraps ();
		}
	}

	void setFireTraps() {
		firetraps_time = Time.time;
		foreach (GameObject firetrap in firetraps) {
			int rand = Random.Range (1,3);
			particle = firetrap.GetComponent<ParticleSystem>();
			if (rand == 2) {
				particle.emissionRate = 20.0f;
				particle.startSpeed = 20.0f;
			} else {
				particle.emissionRate = 1.0f;
				particle.startSpeed = 1.0f;
			}
		}
	}
}
