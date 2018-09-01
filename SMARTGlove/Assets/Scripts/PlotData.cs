using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(TextFileHelper))]
public class PlotData : MonoBehaviour {
	private TextFileHelper dataText;
	private Vector3[] positions;
	bool isPlotting;
	public InputField mInputField;
	GameObject myLine;
	int index;
	IEnumerator currentCoroutine;
	// Use this for initialization
	void Start () {
		DrawGraph ();
	}

	IEnumerator DrawGraph()
	{
		dataText = new TextFileHelper ();
		isPlotting = false;
		LoadData ();
		if (positions == null || positions.Length < 1) 
		{
			yield break;
		}
		if (myLine == null) 
		{
			myLine = new GameObject();
			myLine.transform.position = gameObject.transform.position;
			myLine.AddComponent<LineRenderer>();
		}
		LineRenderer lr = myLine.GetComponent<LineRenderer>();
		lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));

		lr.SetColors(Color.blue, Color.red);
		lr.SetWidth(0.1f, 0.1f);

		for(int i = 1; i <= positions.Length; i++)
		{
			lr.positionCount = i;
			lr.SetPositions (positions);
			yield return null;
		}

	}

	public void LoadData(){
		List<string> data = dataText.ReadFromFile (CubeTracking.currentFileName);
		positions = new Vector3[data.Count];
		int count = 0;
		foreach (string pos in data) {
			string[] parsedData = pos.Split (',');
			Vector3 newPos = new Vector3 (float.Parse (parsedData [0]), -1 * float.Parse (parsedData [2]), float.Parse (parsedData [1]));
			positions [count++] = newPos;
		}
	}

	public void SetFileName()
	{
		if (mInputField != null && mInputField.text != null && mInputField.text != "") {
			CubeTracking.currentFileName = mInputField.text;
		} else {
			mInputField.placeholder.GetComponent<Text>().text = "Enter filename here";
		}
		if (currentCoroutine != null) 
		{
			StopCoroutine (currentCoroutine);
		}
		currentCoroutine = DrawGraph();
		StartCoroutine (currentCoroutine);
	}

}
