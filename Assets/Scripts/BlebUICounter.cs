using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlebUICounter : MonoBehaviour
{
    public TextMeshProUGUI counterText;

    private void Update()
    {
        counterText.text = BlebCounter.Instance.currentBlebs + "/" + BlebCounter.Instance.maxBlebs;
    }
}
