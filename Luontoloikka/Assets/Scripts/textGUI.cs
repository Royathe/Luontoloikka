using UnityEngine;
using System.Collections;

public class textGUI : MonoBehaviour {

	private GUIStyle style;
	public float textY = 70;
	public float textSpacingDivider = 3;
	private centerTile ct;

	void OnGUI(){
		GUI.Label (new Rect (50, Screen.height/2+Screen.height/10+textY, Screen.width-100, 50), ct.flavorText, style);
		GUI.Label (new Rect (50, Screen.height/2+(Screen.height/textSpacingDivider)+textY, Screen.width-100, Screen.height/2-50), ct.taskText, style);

	}

	// Use this for initialization
	void Start () {
		float textAdjustment = 600 / textY;
		textY = Screen.height / textAdjustment; //Adjusts position of text incase the resolution height is different than 600

		style = new GUIStyle ();
		style.font = (Font)Resources.Load ("18century");
		style.fontSize = Screen.height / 22;
		style.alignment = TextAnchor.UpperCenter;
		style.wordWrap = true;
		style.normal.textColor = Color.white;
		ct = GameObject.FindGameObjectWithTag ("CenterTile").GetComponent<centerTile> ();

		GameController gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		gc.textAreaUpLocation = textY;

	}

	void OnTouchDown(){
		ct.OnTouchDown ();
	}
}
