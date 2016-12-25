using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
//using Soomla.Store;

public delegate void BuyItemDelegate(string itemId);
public delegate void CancelBuyItemDelegate();

public class BuyItemDialog : MonoBehaviour {

	public event BuyItemDelegate			buyItemDelegate;
	public event CancelBuyItemDelegate		cancelBuyItemDelegate;

	public GameObject 	panelObject;
	public Text			actionDescription;
	public Button 		yesButton;
	public Button 		cancelButton;

	private ErrorPanel errorPanel;

	public void ShowDialog(string itemId){
		panelObject.SetActive (true);

		string template = "Том содержит 24 новых главы.\n\n Цена {0}";

		string price = "";
//		foreach (var itemFromStore in StoreInfo.Goods) {
//
//			price = ((PurchaseWithMarket)itemFromStore.PurchaseType).MarketItem.MarketPriceAndCurrency;
//			if (string.IsNullOrEmpty(price)) {
//				price = ((PurchaseWithMarket)itemFromStore.PurchaseType).MarketItem.Price.ToString("0.00");
//			}
//
//			if (itemFromStore.ItemId == itemId) {
//				actionDescription.text = System.Text.RegularExpressions.Regex.Unescape(string.Format(template, price));
//				//textTalants_10.text = price;
//			}
//		}



		yesButton.onClick.RemoveAllListeners();
		//yesButton.onClick.AddListener (yesEvent);
		yesButton.onClick.AddListener (()=> {
			if (buyItemDelegate != null) {
				buyItemDelegate (itemId);
			}
		});
		yesButton.onClick.AddListener (ClosePanel);

		yesButton.gameObject.SetActive (true);

		cancelButton.onClick.RemoveAllListeners();
		cancelButton.onClick.AddListener (()=> {
			if (cancelBuyItemDelegate != null) {
				cancelBuyItemDelegate ();
			}
		});

		cancelButton.onClick.AddListener (ClosePanel);

		cancelButton.gameObject.SetActive (true);
	}

	public void ClosePanel () {
		panelObject.SetActive (false);
	}
}
