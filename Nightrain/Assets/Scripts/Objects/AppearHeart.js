var apear : GameObject;
var destroy : GameObject;
var music: Component;

function Start(){
	apear.active=false;
	music = GameObject.Find("MusicEngine").GetComponent("Music_Engine_Script");
}
function OnTriggerEnter (other : Collider) {
	apear.active=true;
	music.SendMessage("play_Barrel_Open");
	Destroy(destroy.gameObject);
}