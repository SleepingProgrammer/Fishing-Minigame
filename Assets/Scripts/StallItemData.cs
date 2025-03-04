using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class ItemDisplay {
    public Sprite icon;
    public int price;
}

[CreateAssetMenu(fileName = "New Stall Item", menuName = "Stall Item")]
public class StallItemData : ScriptableObject
{
    public ItemDisplay[] displays;
    public string itemName;
    public string itemDescription;
    public string itemKey;
}