using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class TextFileHelper : MonoBehaviour{
	
	public void WriteToFile(string filename,List<string> recordedData){
		string fullPath = "Assets/Resources/DATA/" + filename + ".txt";
		using (StreamWriter sw = new StreamWriter (fullPath)) {
			foreach (string line in recordedData) {
				sw.WriteLine (line);
			}
		}
	}

	public List<string> ReadFromFile(string filename){
		string fullPath = "Assets/Resources/DATA/" + filename + ".txt";
		List<string> recordedData = new List<string> ();
		try 
        {
            using (StreamReader sr = new StreamReader(fullPath)) 
            {
                string line;
                // Read and display lines from the file until the end of 
                // the file is reached.
                while ((line = sr.ReadLine()) != null) 
                {
                    recordedData.Add(line);
                }
            }
        }
		catch (Exception e) {
			Debug.Log ("No File Found");
		}
		return recordedData;
	}

}
