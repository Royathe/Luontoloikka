using UnityEngine;
using System.Collections;

public class centerTile : MonoBehaviour {
	
	public string flavorText;
	public string taskText;
	
	private GameController gc;
	private centerTile ct;
	
	public void OnTouchDown(){

		gc.animateText ();
		StartCoroutine (objectAnimation());
		
	}
	
	private IEnumerator objectAnimation(){

			for(int i = 1; i <= 2; i++){
				ct.transform.localScale = new Vector2(1f + 0.1f*i,1f + 0.1f*i);
				yield return new WaitForSeconds (0.02f);
			}
			for(int i = 2; i >= 0; i--){
				ct.transform.localScale = new Vector2(1f + 0.1f*i,1f + 0.1f*i);
				yield return new WaitForSeconds (0.02f);
			}
	}
	// Use this for initialization
	void Start () {
		
		GameObject go = GameObject.FindGameObjectWithTag("GameController");
		ct = GameObject.FindGameObjectWithTag ("CenterTile").GetComponent<centerTile> ();
		gc = go.GetComponent<GameController> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
