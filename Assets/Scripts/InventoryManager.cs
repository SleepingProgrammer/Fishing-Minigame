using UnityEngine;
using System.Collections.Generic;
using TMPro;
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance { get; private set; }

    private Dictionary<string, int> itemLevels = new Dictionary<string, int>();
    public TextMeshProUGUI rodLevelText;
    public TextMeshProUGUI reelLevelText;
    public TextMeshProUGUI lineLevelText;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        LoadItemLevel("rod");
        LoadItemLevel("reel");
        LoadItemLevel("line");

    }

    private void LoadItemLevel(string item)
    {
        itemLevels[item] = PlayerPrefs.GetInt($"{item}Level", 1);
        UpdateItemLevelText(item);
    }

    private void UpdateItemLevelText(string item)
    {
        string levelText = itemLevels[item].ToString();
        switch (item)
        {
            case "rod":
                rodLevelText.text = levelText;
                break;
            case "reel":
                reelLevelText.text = levelText;
                break;
            case "line":
                lineLevelText.text = levelText;
                break;
        }
    }

    public int GetLevel(string item)
    {
        return itemLevels.ContainsKey(item) ? itemLevels[item] : 1;
    }

    public void UpgradeItem(string item)
    {
        if (itemLevels.ContainsKey(item))
        {
            itemLevels[item]++;
            PlayerPrefs.SetInt($"{item}Level", itemLevels[item]);
        }
    }
}
