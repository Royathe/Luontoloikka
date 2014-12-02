using UnityEngine;
using System.Collections;

public class animators : MonoBehaviour {
	
	public GameObject gameObject;
	public string animationType;

	
	void OnTouchDown(){

		switch (animationType) {
		
		case "pop":
			StartCoroutine(animatorPop());
			break;
		}
	}

	private IEnumerator animatorPop()
	{
		float desiredScale = 1f;
		for(int i = 0; i < 4; i++){
			
			desiredScale = i < 2 ? desiredScale += 0.1f : desiredScale -= 0.1f;
			
			gameObject.transform.localScale = new Vector2(desiredScale,desiredScale);
			yield return new WaitForSeconds (0.02f);
		}
	}
}
