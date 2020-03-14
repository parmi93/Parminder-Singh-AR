using UnityEngine;
using System.Collections;
using System.IO;

public class ObbExtractor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(ExtractObbDatasets());
	}
	
	private IEnumerator ExtractObbDatasets () {
		string[] filesInOBB = {"/QCAR/Tesi_Esame.dat", "/QCAR/Tesi_Esame.xml", "/Cattivissimo_Me_2.mp4", "/CattivissimoMe2.jpg"};
		foreach (var filename in filesInOBB) {
			string uri = Application.streamingAssetsPath + filename;
			
			string outputFilePath = Application.persistentDataPath + filename;
			if(!Directory.Exists(Path.GetDirectoryName(outputFilePath)))
				Directory.CreateDirectory(Path.GetDirectoryName(outputFilePath));
			
			var www = new WWW(uri);
			yield return www;
			
			Save(www, outputFilePath);
			yield return new WaitForEndOfFrame();
		}
		
		// When done extracting the datasets, Start Vuforia AR scene
		Application.LoadLevelAsync("Scena_AR");
	}
	
	private void Save(WWW www, string outputPath) {
		File.WriteAllBytes(outputPath, www.bytes);
		
		// Verify that the File has been actually stored
		if(File.Exists(outputPath))
			Debug.Log("File successfully saved at: " + outputPath);
		else
			Debug.Log("Failure!! - File does not exist at: " + outputPath);
	}
}