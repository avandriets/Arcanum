using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class ChapterButtonTemplate : MonoBehaviour {

	public Button	button;

	public Image 	chapterCover;
	public Image 	wasBought;

	[HideInInspector]
	public ChapterItem chapterItem;

	public void InitChapter(){
	
		Utility.setImage (chapterCover, chapterItem.imagePath);

		if (chapterItem.WasBought()) {
			wasBought.gameObject.SetActive (false);
		}
	}
}
