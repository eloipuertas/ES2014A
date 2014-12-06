#pragma strict

var apear : GameObject;
var destroy : GameObject;
private var music : Component;

function Start(){
	apear.active=false;
	music = GameObject.Find("MusicEngine").GetComponent("Music_Engine_Script");
}

function OnTriggerEnter (other : Collider) {
	apear.active=true;
	music.SendMessage("Play_Barrel_Open");
	Destroy(destroy.gameObject);
}