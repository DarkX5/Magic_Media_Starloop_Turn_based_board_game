using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TurnBased.Core;

public class PlayerPositionUpdater : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int playerId = 0;
    [Header("Auto-Set - visible 4 debug")]
    [SerializeField] private TMP_Text text = null;

    private void Start() {
        // get text component
        text = GetComponent<TMP_Text>();
        
        CharController.onMoveDone += UpdateText;
    }
    private void OnDestroy() {
        CharController.onMoveDone -= UpdateText;
    }

    private void UpdateText(int movingPlayerID, int finalMovePos) {
        if (playerId == movingPlayerID) {
            text.text = finalMovePos.ToString();
        }
    }
}
