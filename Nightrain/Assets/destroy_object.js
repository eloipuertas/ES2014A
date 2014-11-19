#pragma strict
var Destroy_o : GameObject;
var explosion : GameObject;
var explosion2 : GameObject;
var apear : GameObject;
var object_health : int = 2;

function Start(){
	apear.SetActive(false);
}
function OnCollisionEnter(collision : Collision){
	if(collision.gameObject == GameObject.FindGameObjectWithTag ("Player")){
		Debug.Log("Collision");
		if (object_health==0){
			apear.SetActive(true);
			var expl = Instantiate(explosion, transform.position, Quaternion.identity);
			var exp2 = Instantiate(explosion2, transform.position, Quaternion.identity);
			Destroy(Destroy_o);
			Destroy(expl, 2);
			Destroy(exp2, 2);
		}
		else{
			object_health --;
			Debug.Log(object_health);
		}
	}
}