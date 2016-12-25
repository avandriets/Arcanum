using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;


public class QuestionButtonTemplate : MonoBehaviour {

	public Button	button;

	public Image 	finalImage;
	public Text		Questionnumber;
	public Text		RightQuestion;

	public GameObject		openQuestionObject;
	public GameObject		NotopenQuestionObject;

	[HideInInspector]
	public QuestionItem questionItem;

	[HideInInspector]
	public bool opened;

	public void InitQuestion(){
	
		if (Utility.IsQuestionAnswered (questionItem.number, questionItem.chaper)) {

			openQuestionObject.SetActive (true);
			NotopenQuestionObject.SetActive (false);

			Utility.setImage (finalImage, questionItem.imagePath);

			RightQuestion.text = questionItem.rightAnsver;

		} else {

			openQuestionObject.SetActive (false);
			NotopenQuestionObject.SetActive (true);


			Questionnumber.text = Utility.intToRoman (Int32.Parse(questionItem.number));
		}

		//wether user answer on this question
	}
}
