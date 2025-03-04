using UnityEngine;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance { get; private set; }

    public StallItemData[] stallItems;
    public Transform stallRoot;
    public StallItem stallPrefab;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Start() {
        InitializeStall();
    }

    public void InitializeStall() {
        // clear existing items
        foreach (Transform child in stallRoot)
        {
            Destroy(child.gameObject);
        }

        // create new items
        foreach (var item in stallItems)
        {
            StallItem stallItem = Instantiate(stallPrefab, stallRoot);
            stallItem.itemData = item;
            stallItem.UpdateItemDisplay();
        }


    }

    public int GetUpgradeCost(string item)
    {
        int level = InventoryManager.instance.GetLevel(item);
        StallItemData itemData = GetStallItemData(item);
        return (level - 1 < itemData.displays.Length) ? itemData.displays[level - 1].price : -1;
    }

    public void PurchaseUpgrade(string item)
    {
        int cost = GetUpgradeCost(item);
        if (cost == -1)
        {
            Debug.Log($"{item} is already maxed out!");
            return;
        }

        if (CurrencyManager.instance.SpendCoins(cost))
        {
            InventoryManager.instance.UpgradeItem(item);
            Debug.Log($"{item} upgraded to level {InventoryManager.instance.GetLevel(item)}!");
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }

    private StallItemData GetStallItemData(string itemKey)
    {
        foreach (var item in stallItems)
        {
            if (item.itemKey == itemKey)
            {
                return item;
            }
        }
        return null;
    }
}