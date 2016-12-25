using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;


public class ErrorPanelTwoButtons : MonoBehaviour {

	public GameObject 	errorPanelObject;
	public Text			actionDescription;
	public Button 		yesButton;
	public Button 		cancelButton;

	private ErrorPanel errorPanel;

	public void SetText(string text, UnityAction yesEvent, UnityAction cancelEvent){
		errorPanelObject.SetActive (true);
		actionDescription.text = text;

		yesButton.onClick.RemoveAllListeners();
		yesButton.onClick.AddListener (yesEvent);
		yesButton.onClick.AddListener (ClosePanel);

		yesButton.gameObject.SetActive (true);

		cancelButton.onClick.RemoveAllListeners();
		cancelButton.onClick.AddListener (cancelEvent);
		cancelButton.onClick.AddListener (ClosePanel);

		cancelButton.gameObject.SetActive (true);
	}

	public void ClosePanel () {
		errorPanelObject.SetActive (false);
	}
}
