using UnityEngine;
using System.Collections;

public class touchInput : MonoBehaviour {
	
	public LayerMask touchInputMask;
	private RaycastHit hit;

	// Update is called once per frame
	void Update () {
		
		if (Input.GetMouseButtonUp(0) ) {
				
			Ray ray = camera.ScreenPointToRay (Input.mousePosition);
				
			if (Physics.Raycast (ray, out hit, touchInputMask)) {
				GameObject recipient = hit.transform.gameObject;

				recipient.SendMessage ("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver); 
			}
		}

		else if (Input.touchCount > 0) {
			
			foreach (Touch touch in Input.touches) {
				
				Ray ray = camera.ScreenPointToRay (touch.position);
				
				if (Physics.Raycast (ray, out hit, touchInputMask)) {
					GameObject recipient = hit.transform.gameObject;
					
					if (touch.phase == TouchPhase.Ended) {
						recipient.SendMessage ("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver); 
					}
				}
				
			}
		}

	}
}
