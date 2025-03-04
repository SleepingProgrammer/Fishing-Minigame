using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StallItem : MonoBehaviour
{
    public StallItemData itemData;

    public Image itemIcon;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemPriceText;
    public TextMeshProUGUI itemDescriptionText;
 
    public void UpdateItemDisplay()
    {
        if (itemData != null)
        {
            int level = InventoryManager.instance.GetLevel(itemData.itemKey);
            itemIcon.sprite = itemData.displays[level].icon;
            itemNameText.text = itemData.itemName;
            itemPriceText.text = itemData.displays[level].price.ToString();
            itemDescriptionText.text = itemData.itemDescription;
        }
    }

    public void BuyItem() {
        ShopManager.instance.PurchaseUpgrade(itemData.itemKey);
        UpdateItemDisplay();
    }
}