using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculateDistance : MonoBehaviour {

	private const float conversionFactor = 0.70331728f;

	bool isCalibrated;
	bool firstLoop;
	float realWorldDistance = 15.0f; //10 cm
	float factorSum = 0.0f;
	Vector3 startPos;
	Vector3 pastPos;
	Vector3 currentPos;
	List<string> Data;
	GameObject startPoint;
	GameObject stopPoint;

	public static string currentFileName = "Data";
	public InputField mInputField;
	public Text unitDist;
	public Text cmDist;
	public Text convFac;


	void Start () {
		isCalibrated = false;
		startPos = new Vector3 ();
		firstLoop = true;
		Data = new List<string> ();
	}

	void FixedUpdate () {
		if (isCalibrated) {
			if (firstLoop) {
				pastPos = startPos;
				firstLoop = false;
			}
			currentPos = gameObject.transform.position;
			Vector3 shiftedPos = currentPos - startPos;
			DrawLine (pastPos, currentPos, Color.blue);
			pastPos = currentPos;
			Vector3 posCorrected = gameObject.transform.position - startPos;
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
	}

	public void calibrateCube(){
		if (startPoint != null) {
			GameObject.Destroy (startPoint);
		}
		startPoint = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		startPos = gameObject.transform.position;
		startPoint.transform.position = startPos;
	}

	public void StopRecording(){
		if (stopPoint != null) {
			GameObject.Destroy (stopPoint);
		}
		stopPoint = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		stopPoint.transform.position = gameObject.transform.position;
		DrawLine (startPoint.transform.position, stopPoint.transform.position, Color.magenta);
		Vector3 totalDistance = stopPoint.transform.position - startPoint.transform.position;
		//float conversionFactor = totalDistance.x / realWorldDistance;
		float convDistance = totalDistance.x / conversionFactor;
		//cmDist.text = "Distance in cm: " + totalDistance.x / conversionFactor;
		print ("Absolute Distance Traveled in Units: " + Mathf.Abs(totalDistance.magnitude));
		print ("X Distance Traveled in Units: " + totalDistance.x);
		print ("X Distance Traveled in cm: " + convDistance + " cm");
		print ("Actual Distance in cm: ~" + realWorldDistance + " cm");
		print ("Error Percentage: " + (Mathf.Abs (convDistance - realWorldDistance) / realWorldDistance) * 100 + " %");
		//print ("ConversionFactor: " + conversionFactor);
	}

	public void setRealDistance(){
		realWorldDistance = float.Parse(mInputField.text);
	}

}
