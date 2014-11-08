#pragma strict

var max=10;
var expand=0.25;
private var x =0;
private var y=max;

function Update () {
	if(x<max){
		transform.localScale += Vector3(expand,expand,expand);
		x++;
	}
	else if(x==max){
		y=0;
		x=max+1;
	}
	if(y<max){
		transform.localScale += Vector3(-expand,-expand,-expand);
		y++;
	}
	else if(y==max){
		x=0;
		y=max+1;
	}
}