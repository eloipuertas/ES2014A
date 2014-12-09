using UnityEngine;
using System.Collections;

public class Rotar : MonoBehaviour {

	public float velocidadRot = 10.0F;
	Transform childTransform;

	public void Start(){

		for (int i = 0; i < transform.childCount; i++) {

			childTransform = transform.GetChild(i);
			print(childTransform.gameObject.name);
		}
	}

	public void Update(){

		//childTransform.Rotate (0.0F, velocidadRot*Time.deltaTime, 0.0F);


	}

}
