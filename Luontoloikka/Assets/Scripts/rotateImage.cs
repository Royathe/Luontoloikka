using UnityEngine;
using System.Collections;

public class rotateImage : MonoBehaviour {

	public GameObject central;
	public GameObject image;
	private int rotationDirection;
	private int rotationStep;
	private Vector3 currentRotation, targetRotation;
	
	private GameController gc;
	
	void Start(){

		rotationStep = -1;
		rotationDirection = 45;

		GameObject go = GameObject.FindGameObjectWithTag("GameController");
		gc = go.GetComponent<GameController> ();
		
	}

	void OnTouchDown(){
		rotateObject ();
		if (gc.entryDirection == "right") { //If last road's nextDirection was "right"
			if (central == gc.straightLeft) {
				gc.newTile (gc.curveLeftDown, true);	
				gc.nextDirection = "down";
			} else if (central == gc.curveLeftDown) {
				gc.newTile (gc.curveLeftUp, true);	
				gc.nextDirection = "up";
			} else if (central == gc.curveLeftUp) {
				gc.newTile (gc.straightLeft, true);	
				gc.nextDirection = "right";
			}
		} else if (gc.entryDirection == "up") { //If last road's nextDirection was "up"
			if (central == gc.straightUp) {
				gc.newTile (gc.curveRightDown, true);	
				gc.nextDirection = "right";
			} else if (central == gc.curveRightDown) {
				gc.newTile (gc.curveLeftDown, true);	
				gc.nextDirection = "left";
			} else if (central == gc.curveLeftDown) {
				gc.newTile (gc.straightUp, true);	
				gc.nextDirection = "up";
			} 
		} else if (gc.entryDirection == "left") { //If last road's nextDirection was "left"
			if (central == gc.straightLeft) {
				gc.newTile (gc.curveRightUp, true);	
				gc.nextDirection = "up";
			} else if (central == gc.curveRightUp) {
				gc.newTile (gc.curveRightDown, true);	
				gc.nextDirection = "down";
			} else if (central == gc.curveRightDown) {
				gc.newTile (gc.straightLeft, true);	
				gc.nextDirection = "left";
			}
		} else if (gc.entryDirection == "down") { //If last road's nextDirection was "up"
			if (central == gc.straightUp) {
				gc.newTile (gc.curveLeftUp, true);	
				gc.nextDirection = "left";
			} else if (central == gc.curveLeftUp) {
				gc.newTile (gc.curveRightUp, true);	
				gc.nextDirection = "right";
			} else if (central == gc.curveRightUp) {
				gc.newTile (gc.straightUp, true);	
				gc.nextDirection = "down";
			}
		} 
				

	}
	
	private void rotateObject()
	{
		currentRotation = image.transform.eulerAngles;
		targetRotation.z = (currentRotation.z + (90 * rotationDirection));
		StartCoroutine (objectRotationAnimation());
	}

	IEnumerator objectRotationAnimation()
	{
		for(int i = 0; i < 8; i++){
			currentRotation.z += (rotationStep * rotationDirection);
			image.transform.eulerAngles = currentRotation;
			yield return new WaitForSeconds (0.02f);
		}
	}
}
