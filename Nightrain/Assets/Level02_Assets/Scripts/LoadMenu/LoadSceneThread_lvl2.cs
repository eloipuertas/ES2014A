using UnityEngine;
using System.Collections;

public class LoadSceneThread_lvl2 : CustomThread {

	protected override void ThreadFunction(){
		// Do your threaded task. DON'T use the Unity API here
		Debug.Log("Se esta cargando el level 1");

	}

	protected override void OnFinished(){
		// This is executed by the Unity main thread when the job is finished
		Debug.Log("Se ha cargado el Level 1");
		this.Abort ();
		
	}
}
