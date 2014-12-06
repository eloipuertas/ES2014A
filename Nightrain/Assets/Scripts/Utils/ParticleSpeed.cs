using UnityEngine;
using System.Collections;

public class ParticleSpeed : MonoBehaviour {

	public float speed = 1.0f;

	// Use this for initialization
	void Start () {
		particleSystem.playbackSpeed = speed;
	}
	

}
