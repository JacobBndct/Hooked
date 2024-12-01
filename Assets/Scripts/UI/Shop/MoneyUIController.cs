using UnityEngine;
using TMPro;

public class MoneyUIController : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    private int currentMoney;

    private void Start()
    {
        DontDestroyOnLoad(transform.parent.parent);
        UpdateMoneyDisplay();
    }

    private void Update()
    {
        int playerMoney = PlayerManager.Instance.playerData.money;
        if (playerMoney != currentMoney)
        {
            currentMoney = playerMoney;
            UpdateMoneyDisplay();
        }
    }

    private void UpdateMoneyDisplay()
    {
        if (moneyText != null)
        {
            moneyText.text = $"${PlayerManager.Instance.playerData.money}";
        }
    }
}
