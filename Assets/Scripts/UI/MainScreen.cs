using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
//using Soomla;
//using Soomla.Store;


public class MainScreen : BaseScreen {

	public	GameObject				chapterButton;
	public	Transform				ChaptersListPanel;

	[HideInInspector]
	public	List<ChapterButtonTemplate> chapterList = null;

	bool firstStart = true;

	public Button purchButton;

	Vector3 doorLeftEnd, doorRightEnd;
	public GameObject doorLeft, doorRight;

	void OnEnable ()
	{

		//SoundManager.ChoosePlayMusic (2);

		doorLeftEnd	= new Vector3 (doorLeft.transform.position.x - 900, doorLeft.transform.position.y);
		doorRightEnd	= new Vector3 (doorRight.transform.position.x + 900, doorRight.transform.position.y);

//		#if UNITY_ANDROID
//		purchButton.gameObject.SetActive(false);
//		#endif

		purchButton.gameObject.SetActive(false);
		//Debug.Log (Utility.intToRoman(24));

		screensManager = ScreensManager.instance;

		if (firstStart) {
			InflateList ();
			firstStart = false;

			StartCoroutine (SmoothMovement ( doorLeftEnd, doorLeft, 300));
			StartCoroutine (SmoothMovement ( doorRightEnd, doorRight, 300));
		}

//		if (!InitMuteState) {
//			soundMan = GameObject.Find ("MusicManager").GetComponent<SoundManager> ();
//			InitMuteState = true;
//		}
//
//		muteButton.onValueChanged.RemoveAllListeners ();
//
//		SoundManager.InitMuteState (muteButton);
//
//		muteButton.onValueChanged.AddListener ((value) => {   // you are missing this
//			handleCheckbox (value);       // this is just a basic method call within another method
//		}   // and this one
//		);

		Debug.Log ("ok");
	}



	protected void PrepareChaptersList(){

		foreach (Transform child in ChaptersListPanel) {
			GameObject.Destroy(child.gameObject);
		}
	}

	private void InflateList(){

		chapterList = new List<ChapterButtonTemplate> ();
		//int i = 1;
		foreach (var c in Utility.chaptersArrayArray) {

			GameObject	newButtonItem = null;
			newButtonItem = Instantiate(chapterButton) as GameObject;
			ChapterButtonTemplate button1 = newButtonItem.GetComponent<ChapterButtonTemplate>();

			button1.chapterItem = c;

			button1.InitChapter ();

			button1.button.onClick.RemoveAllListeners();
			button1.button.onClick.AddListener( () => onChapterClick(button1) );

			newButtonItem.transform.SetParent(ChaptersListPanel);
			newButtonItem.transform.localScale = new Vector3(1,1,1);

			RectTransform rctr = newButtonItem.GetComponent<RectTransform>();
			rctr.offsetMax = new Vector2(0,0);
			rctr.offsetMin = new Vector2(0,0);

			rctr.anchoredPosition3D = new Vector3(0,0,0);

			chapterList.Add (button1);

			//i++;
		}
	}

	private void onChapterClick(ChapterButtonTemplate pButton){
		Debug.Log (pButton.chapterItem.StoreItemID);

//		if (pButton.chapterItem.StoreItemID != GamePurchItems.CHAPTER_1_PRODUCT_ID && pButton.chapterItem.StoreItemID != GamePurchItems.CHAPTER_2_PRODUCT_ID) {
//			ShowErrorDialog ("Данный том еще создается и скоро будет доступен для покупки.", ErrorDialogReaction);
//			return;
//		}

		if (!pButton.chapterItem.WasBought ()) {
			BuyProduct (pButton.chapterItem.StoreItemID);
		} else {
			ChapterScreen.currentChapter = pButton.chapterItem;
			//PrepareChaptersList ();
			screensManager.ShowChapterScreen ();
		}
	}

	public void BuyProduct(string itemId){

		try{

//			StoreInventory.BuyItem (itemId);
//			StoreEvents.OnMarketPurchaseCancelled += onMarketPurchaseCancelled;
//			StoreEvents.OnItemPurchased += onMarketPurchase;
//			StoreEvents.OnUnexpectedStoreError += onUnexpectedStoreError;

		}catch(System.Exception ex){
			Debug.Log ("SOOMLA BUY ERROR " + ex.Message);
		}

	}

//	public void onMarketPurchaseCancelled(PurchasableVirtualItem pvi) {
//
//		StoreEvents.OnMarketPurchaseCancelled -= onMarketPurchaseCancelled;
//		StoreEvents.OnItemPurchased -= onMarketPurchase;
//		StoreEvents.OnUnexpectedStoreError -= onUnexpectedStoreError;
//
//		ShowErrorDialog ("Покупка отменена.", ErrorDialogReaction);
//	}
//
//	public void onMarketPurchase(PurchasableVirtualItem pvi, string payload) {
//
//		StoreEvents.OnMarketPurchaseCancelled -= onMarketPurchaseCancelled;
//		StoreEvents.OnItemPurchased -= onMarketPurchase;
//		StoreEvents.OnUnexpectedStoreError -= onUnexpectedStoreError;
//
//		ShowErrorDialog ("Успешная покупка.", ErrorDialogReaction);
//
//		foreach (var c in chapterList) {
//			c.InitChapter ();
//		}
//	}
//
//	public void onUnexpectedStoreError(int errorCode) {
//
//		StoreEvents.OnMarketPurchaseCancelled -= onMarketPurchaseCancelled;
//		StoreEvents.OnItemPurchased -= onMarketPurchase;
//		StoreEvents.OnUnexpectedStoreError -= onUnexpectedStoreError;
//		ShowErrorDialog ("Ошибка при покупке.", ErrorDialogReaction);
//		SoomlaUtils.LogError ("ExampleEventHandler", "error with code: " + errorCode);
//	}

	private void ErrorDialogReaction(){
		//internalButton.onClick.Invoke ();
	}

	public void QuitFromApp(){
		Application.Quit ();
		#if UNITY_STANDALONE
		//Quit the application
		Application.Quit();
		#endif

		//If we are running in the editor
		#if UNITY_EDITOR
		//Stop playing the scene
		UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}

	void Update ()
	{

		if (Input.GetKey (KeyCode.Escape)) {
			Debug.Log ("Click exit action");

			Application.Quit ();
			#if UNITY_STANDALONE
			//Quit the application
			Application.Quit();
			#endif

			//If we are running in the editor
			#if UNITY_EDITOR
			//Stop playing the scene
			UnityEditor.EditorApplication.isPlaying = false;
			#endif
		}
	}

	public void RestorePurch(){
		//ShowErrorDialogTwoButtons ("Восстановить покупки?",restorePurchEvent,ErrorDialogReaction);
	}

//	public void restorePurchEvent(){
//		StoreEvents.OnRestoreTransactionsFinished += onRestoreTransactionsFinished;
//		SoomlaStore.RestoreTransactions ();
//	}

	public void onRestoreTransactionsFinished(bool success) {

		//StoreEvents.OnRestoreTransactionsFinished += onRestoreTransactionsFinished;

		if (success) {
			ShowErrorDialog ("Покупки успешно восстановлены.", ErrorDialogReaction);
		} else {
			ShowErrorDialog ("Ошибка при восстановлении покупок.", ErrorDialogReaction);
		}
		Debug.Log ("onRestoreTransactionsFinished");
	}


	//Co-routine for moving units from one space to next, takes a parameter end to specify where to move to.
	protected IEnumerator SmoothMovement(Vector3 end, GameObject panelObj, float inverseMoveTime) //(Transform panelTramsform, Vector3 end)
	{

		yield return new WaitForSeconds(1);
//		if (!hide && finalPanel.activeSelf) {
//			finalImage.CrossFadeAlpha (.1f, 3f, false);
//			yield return new WaitForSeconds(3);
//			finalPanel.SetActive (false);
//		}

		float sqrRemainingDistance = (panelObj.transform.localPosition - end).sqrMagnitude;

		while (sqrRemainingDistance > float.Epsilon) {
			Vector3 newPostion = Vector3.MoveTowards (panelObj.transform.localPosition, end, inverseMoveTime * Time.deltaTime);
			panelObj.transform.localPosition = newPostion;
			sqrRemainingDistance = (panelObj.transform.localPosition - end).sqrMagnitude;
			yield return null;
		}

		//inprocess = false;
	}

}
