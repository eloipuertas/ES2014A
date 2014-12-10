var rotationAmount : float = 5.0;
var eje : int =1; //1 ->x, 2->y, 3->z

function Update () {
	switch (eje){
	case(1):
		transform.Rotate(Vector3(rotationAmount,0,0));
		break;
	case(2):
		transform.Rotate(Vector3(0,rotationAmount,0));
		break;
	case(3):
		transform.Rotate(Vector3(0,0,rotationAmount));
		break;
	}
}
