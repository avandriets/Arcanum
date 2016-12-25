using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public delegate void clickButtonDelegate(QuestionButtonTemplate pButton);

public class QuestionsObject : MonoBehaviour {

	public event clickButtonDelegate		buttonClickEvent;

	public	GameObject				questionButton;
	public	Transform				QuestionsListPanel;

	private List<QuestionButtonTemplate> 	questionsList = null;

	ChapterItem		currentChapter = null;

	public  ScrollRect scroll;

	public void ReInitQuestions(){
	
		foreach (var c in  questionsList) {
			c.InitQuestion ();
		}

	}

	public void InitList(ChapterItem	pChapter){

		currentChapter = pChapter;

		questionsList = new List<QuestionButtonTemplate> ();

		//int i = 1;

		foreach (var c in Utility.getQuestionsFromRes(pChapter.questions, currentChapter.StoreItemID)) {

			GameObject	newButtonItem = null;
			newButtonItem = Instantiate(questionButton) as GameObject;
			QuestionButtonTemplate button1 = newButtonItem.GetComponent<QuestionButtonTemplate>();

			button1.questionItem = c;
			button1.InitQuestion ();

			button1.button.onClick.RemoveAllListeners();
			button1.button.onClick.AddListener( () => onQuestionClick(button1) );

			newButtonItem.transform.SetParent(QuestionsListPanel);
			newButtonItem.transform.localScale = new Vector3(1,1,1);

			RectTransform rctr = newButtonItem.GetComponent<RectTransform>();
			rctr.offsetMax = new Vector2(0,0);
			rctr.offsetMin = new Vector2(0,0);

			rctr.anchoredPosition3D = new Vector3(0,0,0);

			questionsList.Add (button1);

			//i++;

			Debug.Log ("ADD QUESTION");
		}

		Canvas.ForceUpdateCanvases();
		scroll.verticalNormalizedPosition = 1f;
	}

	private void onQuestionClick(QuestionButtonTemplate pButton){

		if (buttonClickEvent != null) {
			buttonClickEvent (pButton);
		}
	}

	protected void PrepareQuestionsList(){

		foreach (Transform child in QuestionsListPanel) {
			GameObject.Destroy(child.gameObject);
		}
	}
}
