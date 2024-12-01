using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BaitUIController : MonoBehaviour
{
    public TextMeshProUGUI baitText;
    private int currentBait;

    private void Start()
    {
        transform.parent.parent.parent = null;
        DontDestroyOnLoad(transform.parent.parent);
        UpdateMoneyDisplay();
    }

    private void Update()
    {
        int playerBait = PlayerManager.Instance.playerData.worms;
        if (playerBait != currentBait)
        {
            currentBait = playerBait;
            UpdateMoneyDisplay();
        }
    }

    private void UpdateMoneyDisplay()
    {
        if (baitText != null)
        {
            baitText.text = $"{PlayerManager.Instance.playerData.worms}";
        }
    }
}
