using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class generateTiles : MonoBehaviour {

	//public TextAsset textFile;
	
	public GameObject straightLeft, straightUp, curveLeftDown, curveLeftUp, curveRightUp, curveRightDown;
	public GameObject snowTile, homeTile;

	public List<GameObject> greenTileList;
	public List<GameObject> redTileList;
	public List<GameObject> yellowTileList;

	public List<string> GTFlavorList;
	public List<string> GTTaskList;
	public List<string> GTGraphicList;
	
	public List<string> RTFlavorList;
	public List<string> RTTaskList;
	public List<string> RTGraphicList;

	public List<string> YTFlavorList;
	public List<string> YTTaskList;
	public List<string> YTGraphicList;

	private StreamReader sr;

	public List<FileInfo> fileList;

	public void Start(){
		snowTile.GetComponent<tile> ().type = "snow";
	}

	public void generate(){

		GTFlavorList = new List<string> ();
		GTTaskList = new List<string> ();
		GTGraphicList = new List<string> ();

		RTFlavorList = new List<string> ();
		RTTaskList = new List<string> ();
		RTGraphicList = new List<string> ();

		YTFlavorList = new List<string> ();
		YTTaskList = new List<string> ();
		YTGraphicList = new List<string> ();

		if(Resources.Load ("text") as TextAsset != null){
			androidReadFile(Resources.Load ("text") as TextAsset);
		}
		/*
#if UNITY_STANDALONE_WIN
		Debug.Log ("RAN UNITY STANDALONE WINDOWS SCRIPT");
		try{ 
		DirectoryInfo dir = new DirectoryInfo("Texts");
		FileInfo[] info = dir.GetFiles("*.txt");
		foreach (FileInfo f in info) {
			Debug.Log(f);
			setFile(f);
		}
		}catch(DirectoryNotFoundException){
			Application.LoadLevel("Menu");
		}
#endif
*/
	}
	private List<string> flavorList;
	private List<string> taskList;
	private List<string> graphicList;
	private List<GameObject> tileList;
	/*
	private void setFile(FileInfo f){
		try{ sr = new StreamReader ("Texts/" + f.ToString (), System.Text.Encoding.Unicode);
		}catch(DirectoryNotFoundException){Debug.Log("Directory Not Found");Application.LoadLevel("Menu");}
		generateFromFile ();
	}
*/
	private void androidReadFile(TextAsset textFile){

		string[] linesFromfile = textFile.text.Split("\n"[0]);
		int c = 0;

		foreach (string line in linesFromfile)
		{
			//Debug.Log(c + " :c Var --- "+"LINE: " + line);
			if(line[0] == 'V' && line[1] == '\t'){
				flavorList = GTFlavorList;
				taskList = GTTaskList;
				graphicList = GTGraphicList;
				tileList = greenTileList; 
				c = 0;
			}
			else if(line[0]  == 'P' && line[1] == '\t'){
				flavorList = RTFlavorList;
				taskList = RTTaskList;
				graphicList = RTGraphicList;
				tileList = redTileList; 
				c = 0;
			}
			else if(line[0]  == 'K' && line[1] == '\t'){
				flavorList = YTFlavorList;
				taskList = YTTaskList;
				graphicList = YTGraphicList;
				tileList = yellowTileList; 
				c = 0;
			}
			else if((line[0] == '\t' || line[0] == ' ' || line[0] == '\n') && c == 0){
				c = 0;
			}
			else if((byte)line[0] == 13 && c == 0){
				c = 0;
			}
			else if(c == 0){
				flavorList.Add(line);
				tileList.Add (getRoadTile());
				c = 1;
			}
			else if(c == 1){
				taskList.Add(line);
				c = 2;
			}
			else if(c == 2){
				graphicList.Add(line);
				c = 0;
			}
		}
	}

	//Randomizes added road piece, prioritizing straight roads over curved ones slightly.
	private GameObject getRoadTile(){
		int select = (int)Random.Range (0,4);
		switch (select) {
		case 0:
			return curveLeftDown;
		case 1:
			return curveLeftUp;
		default:
			return straightUp;
		}
	}

	/*
#if UNITY_EDITOR
	private void generateFromFile(){
		int c = 0;
		string line = sr.ReadLine();

		while(c < 5 && !sr.EndOfStream){
			if(line[0] == 'V' && line[1] == '\t'){
				flavorList = GTFlavorList;
				taskList = GTTaskList;
				tileList = greenTileList; 
				c = 0;
				
				while( c < 50 && !sr.EndOfStream){
					line = sr.ReadLine();
					if(line[0] == 'P' && line[1] == '\t'){break;}
					flavorList.Add(line);
					
					line = sr.ReadLine();
					if(line == "\t"){line = "";}
					taskList.Add(line);
					c++;
					
					line = sr.ReadLine();
					tileList.Add (straightUp);
				}
			}
			Debug.Log("LINE: " + line);
			if(line[0]  == 'P' && line[1] == '\t'){
				flavorList = RTFlavorList;
				taskList = RTTaskList;
				tileList = redTileList; 
				c = 0;
				
				while( c < 50 && !sr.EndOfStream){
					line = sr.ReadLine();
					if(line[0] == 'K' && line[1] == '\t'){break;}
					flavorList.Add(line);
					
					line = sr.ReadLine();
					if(line == "\t"){line = "";}
					taskList.Add(line);
					c++;
					
					line = sr.ReadLine();
					tileList.Add (straightUp);
				}
			}
			Debug.Log("LINE: " + line);
			if(line[0]  == 'K' && line[1] == '\t'){
				flavorList = YTFlavorList;
				taskList = YTTaskList;
				tileList = yellowTileList; 
				c = 0;
				
				while(!sr.EndOfStream){
					line = sr.ReadLine();
					flavorList.Add(line);
					
					line = sr.ReadLine();
					if(line == "\t"){line = "";}
					taskList.Add(line);
					c++;
					
					line = sr.ReadLine();
					tileList.Add (straightUp);
				}
			}
			c++;
		}
		sr.Dispose ();
	}
#endif
*/
}
