using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System.IO;
using System;
using System.Globalization;
using System.Xml;
using System.Linq;


public static class Utility {

	public static ChapterItem[] chaptersArrayArray = new ChapterItem[] {
		new ChapterItem(GamePurchItems.CHAPTER_1_PRODUCT_ID, true , "Covers/book1", "Chapters/Chapter0/question_chapter0", "Covers/part0"
			,"Chapters/Chapter0/background", "Chapters/Chapter0/element214", "Chapters/Chapter0/element215", "Chapters/Chapter0/element216"),
		new ChapterItem(GamePurchItems.CHAPTER_2_PRODUCT_ID, false , "Covers/book2", "Chapters/Chapter1/question_chapter1", "Covers/part1"
			,"Chapters/Chapter1/part1BackGround", "Chapters/Chapter1/part1KeyBoard", "Chapters/Chapter1/part1ButtonElement", "Chapters/Chapter1/part1Line"),
		new ChapterItem(GamePurchItems.CHAPTER_3_PRODUCT_ID, false , "Covers/book3", "Chapters/Chapter2/question_chapter2", "Covers/part2"
			,"Chapters/Chapter2/part2BackGround", "Chapters/Chapter2/part2KeyBoard", "Chapters/Chapter2/part2ButtonElement", "Chapters/Chapter2/part2Line"),
		new ChapterItem(GamePurchItems.CHAPTER_4_PRODUCT_ID, false , "Covers/book4", "Chapters/Chapter3/question_chapter3", "Covers/part3"
			,"Chapters/Chapter0/background", "Chapters/Chapter0/element214", "Chapters/Chapter0/element215", "Chapters/Chapter0/element216"),
		new ChapterItem(GamePurchItems.CHAPTER_5_PRODUCT_ID, false , "Covers/book5", "Chapters/Chapter4/question_chapter4", "Covers/part4"
			,"Chapters/Chapter1/part1BackGround", "Chapters/Chapter1/part1KeyBoard", "Chapters/Chapter1/part1ButtonElement", "Chapters/Chapter1/part1Line"),
		new ChapterItem(GamePurchItems.CHAPTER_6_PRODUCT_ID, false , "Covers/book6", "Chapters/Chapter5/question_chapter5", "Covers/part5"
			,"Chapters/Chapter2/part2BackGround", "Chapters/Chapter2/part2KeyBoard", "Chapters/Chapter2/part2ButtonElement", "Chapters/Chapter2/part2Line")
	};

	public static ChapterItem getChapter(string chapter){

		foreach(var c in chaptersArrayArray){
			if (c.StoreItemID == chapter) {
				return c;
			}
		}
		return null;
	}

	public static bool TEST_MODE = true;

	public static void setImage(Image imgAvatar, string pImageFile){

		Sprite spr = null;
		Texture2D tex = (Texture2D)Resources.Load(pImageFile);
		if (tex != null) {
			spr = Sprite.Create (tex, new Rect (0, 0, tex.width, tex.height), new Vector2 (0.5f, 0.5f));

			imgAvatar.sprite = spr;
		}
	}

	public static string getResourceFolder(){

		return Utility.getDcumentsPath () + "/Resources/";
	}

	public static string getDcumentsPath(){

		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{ 
			Debug.Log("IT IS IPHONE !!!");

			if( !Directory.Exists(Application.persistentDataPath + "/Resources/")){
				var t = new DirectoryInfo(Application.persistentDataPath);
				t.CreateSubdirectory("Resources");
			}

			return Application.persistentDataPath;

		}else if(Application.platform == RuntimePlatform.Android){

			Debug.Log("IT IS ANDROID !!!");

			if( !Directory.Exists(Application.persistentDataPath + "/Resources/")){
				var t = new DirectoryInfo(Application.persistentDataPath);
				t.CreateSubdirectory("Resources");
			}

			return Application.persistentDataPath;
		}else{

			Debug.Log("OTHER ENIMAL !!!");

			return Application.dataPath;
		}
	}

	public static List<QuestionItem> getQuestionsFromRes ( string fileName, string chapter) {

		TextAsset textAsset = (TextAsset) Resources.Load(fileName);
		var xml = new XmlDocument();
		xml.LoadXml(textAsset.text);

		var rootElement = xml.DocumentElement;
		var question = rootElement.GetEnumerator ();

		List<QuestionItem> questionItems = new List<QuestionItem> ();

		while (question.MoveNext()) {

			Debug.Log (question.Current.ToString());

			var xmlItem = (XmlElement)question.Current;

			string number		= xmlItem.GetAttribute ("number");
			string quest		= xmlItem.GetAttribute ("question");
			string rightAnswer = xmlItem.GetAttribute ("right_answer");
			string final_image = xmlItem.GetAttribute ("final_image_path");
			string font_size = xmlItem.GetAttribute ("font_size");


			questionItems.Add (new QuestionItem(number,quest, rightAnswer, final_image, chapter, int.Parse(font_size)));

		}

		return questionItems;
	}

	public static string intToRoman(int num) {

		// create 2-dimensional array, each inner array containing 
		// roman numeral representations of 1-9 in each respective 
		// place (ones, tens, hundreds, etc...currently this handles
		// integers from 1-3999, but could be easily extended)
		string [,] romanNumerals = new string[,] {
			{"", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX"}, // ones
			{"", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC"}, // tens
			{"", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM"}, // hundreds
			{"", "M", "MM", "MMM", "", "" ,"", "", "", ""} // thousands
		};
			
		// split integer string into array and reverse array
		//string[] intArr = num.ToString().Split();// .reverse(),

		int[] intArr = num.ToString().Select(str => int.Parse(str.ToString())).ToArray();
		Array.Reverse(intArr);

//		string source = num.ToString();
//		int[] intArr = new int[source.Length];
//
//		for (int j = 0; j < source.Length; j++) {
//			intArr[j] = source[j] - '0';
//		}
//		Array.Reverse(intArr);

		var len = intArr.Length;
		string romanNumeral = "";

		int i = len-1;

		// starting with the highest place (for 3046, it would be the thousands 
		// place, or 3), get the roman numeral representation for that place 
		// and append it to the final roman numeral string
		while (i >= 0) {
			romanNumeral += romanNumerals[ i , intArr[i] ];
			i--;
		}

		return romanNumeral;

	}

	public static bool IsQuestionAnswered(string shieldNum, string key){
	
		string values = PlayerPrefs.GetString ("opened_questions");

		if (values != null && values.Length > 0) {

			var jsonStr = JSON.Parse (values);

			var listNode = jsonStr [key].AsArray;

			if (listNode != null) {
				foreach (JSONData i in listNode) {
					if (i.Value == shieldNum) {
						return true;
					}
				}

				JSONNode jnod = new JSONNode();
				jnod.Add (shieldNum);

				listNode.Add (shieldNum);

				jsonStr [key] = listNode;

			} else {


				JSONArray listNode1 = new JSONArray();

				JSONNode jnod1 = new JSONNode();
				jnod1.Add (shieldNum);

				listNode1.Add (shieldNum);

				jsonStr.Add (key,	listNode1);
			}
				
			//PlayerPrefs.SetString ("opened_questions", jsonStr.ToString());
		}

		return false;

	}

	public static void ReReadAnswer(string shieldNum, string key){
	
		string values = PlayerPrefs.GetString ("opened_questions");

		if (values != null && values.Length > 0) {

			var jsonStr = JSON.Parse (values);

			var listNode = jsonStr [key].AsArray;

			if (listNode != null) {

				for (int i = 0; i < listNode.Count; i++) {
					if (listNode[i].Value == shieldNum) {
						listNode.Remove (i);
						break;
					}
				}

//				foreach (JSONData i in listNode) {
//					if (i.Value == shieldNum) {
//
//						listNode.Remove()
//						break;
//					}
//				}



//				JSONNode jnod = new JSONNode();
//				jnod.Add (shieldNum);
//
//				listNode.Add (shieldNum);
//
				jsonStr [key] = listNode;

			}
				
			//UserController.currentUser.Motto = jsonStr.ToString();
			//profile.SaveUser ();
			PlayerPrefs.SetString ("opened_questions", jsonStr.ToString());

			return;
		}

	}

	public static void SetQuestionWasAnswered(string shieldNum, string key){

		string values = PlayerPrefs.GetString ("opened_questions");

		if (values != null && values.Length > 0) {

			var jsonStr = JSON.Parse (values);

			var listNode = jsonStr [key].AsArray;

			if (listNode != null) {
				foreach (JSONData i in listNode) {
					if (i.Value == shieldNum) {
						return;
					}
				}

				JSONNode jnod = new JSONNode();
				jnod.Add (shieldNum);

				listNode.Add (shieldNum);

				jsonStr [key] = listNode;

			} else {


				JSONArray listNode1 = new JSONArray();

				JSONNode jnod1 = new JSONNode();
				jnod1.Add (shieldNum);

				listNode1.Add (shieldNum);

				jsonStr.Add (key,	listNode1);
			}



			//UserController.currentUser.Motto = jsonStr.ToString();
			//profile.SaveUser ();
			PlayerPrefs.SetString ("opened_questions", jsonStr.ToString());

			return;
		} else {

			JSONArray listNode = new JSONArray();

			JSONNode jnod = new JSONNode();
			jnod.Add (shieldNum);

			listNode.Add (shieldNum);


			JSONClass rootNode = new JSONClass ();
			rootNode.Add (key,	listNode);

			//UserController.currentUser.Motto = rootNode.ToString();

			//profile.SaveUser ();
			PlayerPrefs.SetString ("opened_questions", rootNode.ToString());

			return;
		}

	}
}
