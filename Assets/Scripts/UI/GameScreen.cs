using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public delegate void ChangeSubscriptionDelegate();

public class GameScreen : BaseScreen {

	public event ChangeSubscriptionDelegate		changeSubscriptionDelegate;

	public	GameObject				itemSlimButton;

	public	Transform						FoundCardPanel;
	public List<Game3TemplateButton> 		lettersList;
	public Text	mQuestionState;

	List<Game3TemplateButton>		finishLettersList;

	[HideInInspector]
	public QuestionItem	currentQuestion = null;

	public Button okButton;
	public Button reReadButton;

	public GameObject	finalPanel;
	public GameObject	questionPanel;

	public GameObject	rightAnswerPanel;
	public Text			rightAnswerText;
	public Image		finalImage;

	ChapterItem chapter = null;
	string[] rawKeys = new string[] {
		"А","Б","В","Г","Д","Е","Ё","Ж","З","И","Й","К","Л","М","Н","О","П","Р","С","Т","У","Ф","Х","Ц", 
		"Ч","Ш","Щ","Ъ","Ы","Ь","Э","Ю","Я"
	};

	public Image backGround;
	public Image keyBoard;
	public Image line;

	Vector3 panelTopStart, panelTopEnd;

	bool inprocess = false;
	bool QuestionPanelCollapsed = false;

	public void StartGame(){

		panelTopStart 	= new Vector3(questionPanel.transform.localPosition.x, questionPanel.transform.localPosition.y);
		panelTopEnd	= new Vector3 (panelTopStart.x , panelTopStart.y - 560); 

		chapter = Utility.getChapter (currentQuestion.chaper);

		Utility.setImage (backGround, chapter.QuestionBackGroundImagePath);
		Utility.setImage (keyBoard, chapter.QuestionKeyBoardImagePath);
		Utility.setImage (line, chapter.QuestionGreenLineElement);

		gameObject.SetActive (true);

		rightAnswerPanel.SetActive (false);
		finalPanel.SetActive (false);
		reReadButton.gameObject.SetActive (false);
		okButton.gameObject.SetActive (false);

		InitQuestionScreen ();

		InitGameScreen ();

		//ShowQuestionPanel ();

	}

	public void ShowQuestionPanel()
	{
		StartCoroutine (SmoothMovement ( panelTopEnd, questionPanel, 700, false));		
		QuestionPanelCollapsed = false;
	}

	public void HideQuestionPanel()
	{
		StartCoroutine (SmoothMovement ( panelTopStart, questionPanel, 700, true));		
		QuestionPanelCollapsed = true;
	}

	//Co-routine for moving units from one space to next, takes a parameter end to specify where to move to.
	protected IEnumerator SmoothMovement(Vector3 end, GameObject panelObj, float inverseMoveTime, bool hide) //(Transform panelTramsform, Vector3 end)
	{

		if (!hide && finalPanel.activeSelf) {
			finalImage.CrossFadeAlpha (.1f, 3f, false);
			yield return new WaitForSeconds(3);
			finalPanel.SetActive (false);
		}

		float sqrRemainingDistance = (panelObj.transform.localPosition - end).sqrMagnitude;

		while (sqrRemainingDistance > float.Epsilon) {
			Vector3 newPostion = Vector3.MoveTowards (panelObj.transform.localPosition, end, inverseMoveTime * Time.deltaTime);
			panelObj.transform.localPosition = newPostion;
			sqrRemainingDistance = (panelObj.transform.localPosition - end).sqrMagnitude;
			yield return null;
		}

		inprocess = false;
	}

	public void InitQuestionScreen(){

		if (Utility.IsQuestionAnswered (currentQuestion.number, currentQuestion.chaper)) {
			ShowFinalScreen ();
		} else {		
			ShowQuestionScreen ();
		}

	}

	public void ShowQuestionScreen(){

		rightAnswerPanel.SetActive (false);

		reReadButton.gameObject.SetActive (false);
		okButton.gameObject.SetActive (true);

		//finalPanel.SetActive (false);

		ShowQuestionPanel ();

		InitGameScreen ();

	}

	public void ShowFinalScreen(){
	
		//questionPanel.SetActive (false);
		rightAnswerPanel.SetActive (true);
		reReadButton.gameObject.SetActive (true);
		okButton.gameObject.SetActive (false);

		HideQuestionPanel ();

		finalPanel.SetActive (true);
		rightAnswerText.text = currentQuestion.rightAnsver;
		//text.CrossFadeAlpha(0, .5f, false);

		finalImage.GetComponent<CanvasRenderer>().SetAlpha(0.1f);
		Utility.setImage (finalImage, currentQuestion.imagePath);
		finalImage.CrossFadeAlpha(1f,3f,false);
	}

	public void ClosePanel(){

		currentQuestion = null;

		questionPanel.transform.localPosition = panelTopStart;
		SoundManager.ChoosePlayMusic (0);

		gameObject.SetActive (false);
	}

	public void InitGameScreen(){

		//PrepareScreenBeforFinishCall ();

		foreach(var c in lettersList){
			c.InitButton ();
		}

		Debug.Log (currentQuestion.rightAnsver);

		mQuestionState.text = System.Text.RegularExpressions.Regex.Unescape(currentQuestion.question);
		mQuestionState.fontSize = currentQuestion.fontSize;

		string answer = getRightWord();

		string[] arrLett = new string[12];

		//Create letters array
		for (int k = 0; k < arrLett.Length; k++) {
			if (k < answer.Length) {
				arrLett[k] = answer [k].ToString();
			} else {
				arrLett[k] = rawKeys[Random.Range(0, rawKeys.Length-1)];
			}
		}

		//shuffle letters array
		for (int ij = 0; ij < arrLett.Length; ij++) {
			string temp = arrLett[ij];
			int randomIndex = Random.Range(ij, arrLett.Length);
			arrLett[ij] = arrLett[randomIndex];
			arrLett[randomIndex] = temp;
		}

		PrepareScreenBeforFinishCall ();

		CreateGameObjects (arrLett);

	}

	protected void PrepareScreenBeforFinishCall(){

		foreach (Transform child in FoundCardPanel) {
			GameObject.Destroy(child.gameObject);
		}

	}

	private void CreateGameObjects(string[] pLettersArray){

		//lettersList			= new List<Game3TemplateButton>();
		finishLettersList	= new List<Game3TemplateButton>();

		//Bottom array

		for (int j = 0; j < pLettersArray.Length; j++) {

			Game3Item gItem = new Game3Item ();
			gItem.ItemName = pLettersArray[j];
			lettersList[j].item = gItem;

			lettersList[j].Letter.text = lettersList[j].item.ItemName;

		}

		string answer = getRightWord();

		for (int i = 0; i < answer.Length; i++) {

			GameObject	newButtonItem = null;
			newButtonItem = Instantiate(itemSlimButton) as GameObject;
			Game3TemplateButton button1 = newButtonItem.GetComponent<Game3TemplateButton>();

			button1.item = null;

			button1.Letter.text = "";
			Utility.setImage (button1.CardBack, chapter.QuestionKeyBoardElement);

			button1.button.onClick.RemoveAllListeners();
			button1.button.onClick.AddListener( () => onCardFinishClick(button1) );

			newButtonItem.transform.SetParent(FoundCardPanel);
			newButtonItem.transform.localScale = new Vector3(1,1,1);

			finishLettersList.Add (button1);
		}
	}

	public void onCardFromSetClick(Game3TemplateButton item){

		if (item.Letter.gameObject.activeSelf) {
			foreach (var c in finishLettersList) {
				if (c.item == null) {
					c.item = item.item;
					c.Letter.text = item.item.ItemName;

					c.relateButton = item;
					c.Letter.gameObject.SetActive (true);

					item.Letter.gameObject.SetActive (false);
					break;
				}
			}
		}

		Debug.Log (item.item.ItemName);
	}

	public void onCardFinishClick(Game3TemplateButton item){

		if (item.item != null) {
			item.item = null;
			item.Letter.gameObject.SetActive (false);
			item.relateButton.Letter.gameObject.SetActive (true);
		}

	}

	public void OnBackSpaceClick(){

		for (int i = finishLettersList.Count - 1; i >= 0; i--) {

			if (finishLettersList [i].item != null) {

				if (finishLettersList [i].Letter.gameObject.activeSelf) {
					finishLettersList [i].item = null;
					finishLettersList [i].Letter.gameObject.SetActive (false);
					finishLettersList [i].relateButton.Letter.gameObject.SetActive (true);
					break;
				}
			}
		}
	}

	protected bool GetAnswer(){

		if (getRightWord () == GetAnswerWord ()) {
			return true;
		} else {
			return false;
		}

	}

	private string getRightWord(){
		return currentQuestion.rightAnsver.ToUpper().Trim();
	}

	private string GetAnswerWord(){
		string word = "";

		foreach(var cc in finishLettersList){
			if (cc.gameObject.activeSelf && cc.item != null) {
				word += cc.Letter.text;
			}
		}

		return word.ToUpper();
	}

	public void onAnswerClick(){
		
		Debug.Log ("Answer click");

		if (GetAnswer ()) {
		
			ShowFinalScreen ();

			Utility.SetQuestionWasAnswered (currentQuestion.number, currentQuestion.chaper);

			if (changeSubscriptionDelegate != null) {
				changeSubscriptionDelegate ();
			}
			SoundManager.ChoosePlayMusic (3);
		} else {
			SoundManager.ChoosePlayMusic (1);
			ShowErrorDialog ("Вы ответили не правильно.", ErrorDialogReaction);
		}

	}

	public void onReReadClick(){
	
		Utility.ReReadAnswer (currentQuestion.number, currentQuestion.chaper);

		if (changeSubscriptionDelegate != null) {
			changeSubscriptionDelegate ();
		}

		SoundManager.ChoosePlayMusic (2);

		ShowQuestionScreen ();

	}

	public void ErrorDialogReaction(){
		
	}
}
