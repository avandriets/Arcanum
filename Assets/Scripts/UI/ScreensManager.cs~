using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;
using System.IO;

public class ScreensManager : MonoBehaviour {

	public GameObject mMainScreenCanvas;
	public GameObject mChapterScreenCanvas;

	public GameObject currentScreenCanvas = null;


	private static ScreensManager s_Instance = null;

	// This defines a static instance property that attempts to find the manager object in the scene and
	// returns it to the caller.
	public static ScreensManager instance {
		get {
			if (s_Instance == null) {
				// This is where the magic happens.
				//  FindObjectOfType(...) returns the first AManager object in the scene.
				s_Instance =  FindObjectOfType(typeof (ScreensManager)) as ScreensManager;
			}

			// If it is still null, create a new instance
			if (s_Instance == null) {
				GameObject obj = new GameObject("GameManager");
				s_Instance = obj.AddComponent(typeof (ScreensManager)) as ScreensManager;
				Debug.Log ("Could not locate an AManager object. \n ScreensManager was Generated Automaticly.");
			}

			return s_Instance;
		}
	}

	// Ensure that the instance is destroyed when the game is stopped in the editor.
	void OnApplicationQuit() {
		s_Instance = null;
	}

	public void ShowMainScreen()
	{
		if (currentScreenCanvas != null) {
			currentScreenCanvas.SetActive(false);
		}

		currentScreenCanvas = mMainScreenCanvas;

		if (!mMainScreenCanvas.activeSelf) {
			mMainScreenCanvas.SetActive (true);
		}
	}


	public void ShowChapterScreen()
	{
		if (currentScreenCanvas != null) {
			currentScreenCanvas.SetActive(false);
		}

		currentScreenCanvas = mChapterScreenCanvas;

		if (!mChapterScreenCanvas.activeSelf) {
			mChapterScreenCanvas.SetActive (true);
		}
	}

}

