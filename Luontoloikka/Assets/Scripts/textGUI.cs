using UnityEngine;
using System.Collections;

public class textGUI : MonoBehaviour {

	//private string flavorText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras congue odio vel sem placerat, nec varius risus elementum. Lorem ipsum dolor sit amet, consectetur adipiscing elit.";
	//private string taskText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras congue odio vel sem placerat, nec varius risus elementum. Lorem ipsum dolor sit amet, consectetur adipiscing elit.";
	private GUIStyle style;
	private string text;
	public float textY = 0;
	private centerTile ct;

	void OnGUI(){
		text = ct.flavorText + "\n\n" + ct.taskText;
		//flavorText = GUI.TextArea (new Rect (700,25,100,100), flavorText, 250);
		GUI.Label (new Rect (50, Screen.height/2+Screen.height/10+textY, Screen.width-100, 50), ct.flavorText, style);
		GUI.Label (new Rect (50, Screen.height/2+Screen.height/3+textY, Screen.width-100, Screen.height/2-50), ct.taskText, style);

	}

	// Use this for initialization
	void Start () {
	
		style = new GUIStyle ();
		style.font = (Font)Resources.Load ("18century");
		if (Screen.height < 480) {
			style.fontSize = 20;
		}else if (Screen.height < 640){
			style.fontSize = 30;
		}else{
			style.fontSize = 40;
		}

		style.alignment = TextAnchor.UpperCenter;
		style.wordWrap = true;
		style.normal.textColor = Color.white;
		ct = GameObject.FindGameObjectWithTag ("CenterTile").GetComponent<centerTile> ();

	}

	void OnTouchDown(){
		ct.OnTouchDown ();
	}
}
