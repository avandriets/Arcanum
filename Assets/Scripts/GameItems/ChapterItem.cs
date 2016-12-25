using UnityEngine;
using System.Collections;
//using Soomla;
//using Soomla.Store;


public class ChapterItem {

	public ChapterItem(string pStoreItemID, bool pIsPresent, string pImagePath, string pQuestions, string pChapterImagePath,
		string pQuestionBackGroundImagePath, string pQuestionKeyBoardImagePath, string pQuestionKeyBoardElement, string pQuestionGreenLineElement)
	{
		StoreItemID = pStoreItemID;
		isPresent = pIsPresent;
		imagePath = pImagePath;
		questions = pQuestions;
		ChapterImagePath = pChapterImagePath;

		QuestionBackGroundImagePath = pQuestionBackGroundImagePath;
		QuestionKeyBoardImagePath	= pQuestionKeyBoardImagePath;
		QuestionKeyBoardElement		= pQuestionKeyBoardElement;
		QuestionGreenLineElement	= pQuestionGreenLineElement;
	}

	public bool WasBought(){
	
//		if (StoreItemID != GamePurchItems.CHAPTER_1_PRODUCT_ID && StoreItemID != GamePurchItems.CHAPTER_2_PRODUCT_ID)
//			return false;

		if (isPresent || Utility.TEST_MODE)
			return true;

		return false;

//		try{
//
//			int itemBalance = StoreInventory.GetItemBalance (StoreItemID);
//			if ( itemBalance == 0)
//				return false;
//			else
//				return true;
//
//		}catch(System.Exception ex){
//			Debug.LogError ("SOOMLA GET BALANCE ERROR " + ex.Message);
//			return false;
//		}


	}

	public string StoreItemID;
	public bool	isPresent;
	public string imagePath;
	public string questions;
	public string ChapterImagePath;

	public string QuestionBackGroundImagePath;
	public string QuestionKeyBoardImagePath;
	public string QuestionKeyBoardElement;
	public string QuestionGreenLineElement;

}
