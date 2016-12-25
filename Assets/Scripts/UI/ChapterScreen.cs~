using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class ChapterScreen : BaseScreen {
	

	[HideInInspector]
	public static ChapterItem				currentChapter = null;

	public GameScreen 						gamePanel;

	public GameObject	questionsTemplate;
	QuestionsObject			questionObject;

	public Transform	questContainer;
	public Image		chapterFromtImage;

	void Start(){
	
		gamePanel.changeSubscriptionDelegate += stateQuestionChange;
	}

	public void stateQuestionChange(){
		questionObject.ReInitQuestions ();
	}

	void OnEnable ()
	{

		screensManager = ScreensManager.instance;

		Utility.setImage (chapterFromtImage, currentChapter.ChapterImagePath);
		InitQuestionsListObject (currentChapter);

		Debug.Log ("chapter screen enable");
	}

	public void InitQuestionsListObject(ChapterItem	currentChapter){
	
		GameObject	newObject = null;
		newObject = Instantiate(questionsTemplate) as GameObject;
		questionObject = newObject.GetComponent<QuestionsObject>();

		questionObject.InitList (currentChapter);
		questionObject.buttonClickEvent += onQuestionClick;

		newObject.transform.SetParent(questContainer);
		newObject.transform.localScale = new Vector3(1,1,1);

		RectTransform rctr = newObject.GetComponent<RectTransform>();
		rctr.offsetMax = new Vector2(0,0);
		rctr.offsetMin = new Vector2(0,0);

		rctr.anchoredPosition3D = new Vector3(0,0,0);

	}

	private void onQuestionClick(QuestionButtonTemplate pButton){
	
		gamePanel.currentQuestion = pButton.questionItem;
		gamePanel.StartGame ();

	}

	public void onBackButtonClick(){
		
		GameObject.Destroy (questionObject.gameObject);
		questionObject = null;

		currentChapter = null;

		SoundManager.ChoosePlayMusic (0);

		screensManager.ShowMainScreen ();
	}

}
