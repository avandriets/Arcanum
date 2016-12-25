using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class BaseScreen : MonoBehaviour {

	public ErrorPanel errorPanel;
	public ErrorPanelTwoButtons errorPanelTwoButtons;

	[HideInInspector]
	protected ScreensManager screensManager;

	public ErrorPanel ShowErrorDialog(string message, UnityAction clickButtonEvent){

		ErrorPanel NewErrorPanel = Instantiate(errorPanel) as ErrorPanel;

		NewErrorPanel.SetText(message, clickButtonEvent);

		NewErrorPanel.transform.SetParent(gameObject.transform);
		NewErrorPanel.transform.localScale = new Vector3(1,1,1);

		RectTransform rctr = NewErrorPanel.GetComponent<RectTransform>();
		rctr.offsetMax = new Vector2(0,0);
		rctr.offsetMin = new Vector2(0,0);
		rctr.anchoredPosition3D = new Vector3(0,0,0);

		return NewErrorPanel;
	}

	public ErrorPanelTwoButtons ShowErrorDialogTwoButtons(string message, UnityAction clickOkButtonEvent, UnityAction clickCancelButtonEvent){

		ErrorPanelTwoButtons NewErrorPanel = Instantiate(errorPanelTwoButtons) as ErrorPanelTwoButtons;

		NewErrorPanel.SetText(message, clickOkButtonEvent, clickCancelButtonEvent);

		NewErrorPanel.transform.SetParent(gameObject.transform);
		NewErrorPanel.transform.localScale = new Vector3(1,1,1);

		RectTransform rctr = NewErrorPanel.GetComponent<RectTransform>();
		rctr.offsetMax = new Vector2(0,0);
		rctr.offsetMin = new Vector2(0,0);
		rctr.anchoredPosition3D = new Vector3(0,0,0);

		return NewErrorPanel;
	}
}
