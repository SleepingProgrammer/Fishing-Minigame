using UnityEngine;
using TMPro;
public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance { get; private set; }
    private int coins;
    [SerializeField] TextMeshProUGUI coinsText;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        coins = PlayerPrefs.GetInt("coins", 0);
    }

    public int GetCoins() => coins;

    public void AddCoins(int amount)
    {
        coins += amount;
        PlayerPrefs.SetInt("coins", coins);
    }

    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            PlayerPrefs.SetInt("coins", coins);
            return true;
        }
        return false;
    }
}
