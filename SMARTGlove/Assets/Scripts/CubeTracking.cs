using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeTracking : MonoBehaviour {
	bool isCalibrated;
	bool firstLoop;
	Vector3 startPos;
	Vector3 pastPos;
	Vector3 currentPos;
	Vector3 shiftVec;
	List<string> Data;
	public static string currentFileName = "Data";
	public InputField mInputField;
	// Use this for initialization
	void Start () {
		isCalibrated = false;
		startPos = new Vector3 ();
		firstLoop = true;
		Data = new List<string> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isCalibrated) {
			if (firstLoop) {
				pastPos = startPos;
				firstLoop = false;
			}
			currentPos = gameObject.transform.position;
			Vector3 shiftedPos = currentPos - startPos;
			string outString = shiftedPos.x.ToString() + ',' + shiftedPos.y.ToString() + ',' + shiftedPos.z.ToString();
			Data.Add (outString);
			DrawLine (pastPos, currentPos, Color.blue);
			pastPos = currentPos;
			Vector3 posCorrected = gameObject.transform.position - startPos;
			Debug.Log (posCorrected);
		}

	}

	void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
	{
		GameObject myLine = new GameObject();
		myLine.transform.position = start;
		myLine.AddComponent<LineRenderer>();
		LineRenderer lr = myLine.GetComponent<LineRenderer>();
		lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
		lr.SetColors(color, color);
		lr.SetWidth(0.1f, 0.1f);
		lr.SetPosition(0, start);
		lr.SetPosition(1, end);
		//GameObject.Destroy(myLine, duration);
	}

	public void calibrateCube(){
		if (mInputField != null && mInputField.text != null && mInputField.text != "") {
			currentFileName = mInputField.text;
			startPos = gameObject.transform.position;
			isCalibrated = true;
		} else {
			mInputField.placeholder.GetComponent<Text>().text = "Enter filename here";
		}
	}

	public void StopRecording(){
		TextFileHelper help = new TextFileHelper ();
		help.WriteToFile (currentFileName, Data);
		print ("Done");
		mInputField.text = "";
		isCalibrated = false;
	}
}
