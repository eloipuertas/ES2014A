#pragma strict
var Destroy_o : GameObject;
var explosion : GameObject;
var explosion2 : GameObject;
var apear : GameObject;
var object_health : int = 2;

var audio1 : AudioSource; 
var audio2 : AudioSource; 

function Start(){
	apear.SetActive(false);
}
function OnCollisionEnter(collision : Collision){
	if(collision.gameObject == GameObject.FindGameObjectWithTag ("Player") && apear != null){
		if (object_health==0){
			this.audio1.Play();
			apear.SetActive(true);
			var expl = Instantiate(explosion, transform.position, Quaternion.identity);
			var exp2 = Instantiate(explosion2, transform.position, Quaternion.identity);
			Destroy(Destroy_o);
			Destroy(expl, 2);
			Destroy(exp2, 2);
		}
		else{
			this.audio2.Play();
			object_health --;
		}
	}
}