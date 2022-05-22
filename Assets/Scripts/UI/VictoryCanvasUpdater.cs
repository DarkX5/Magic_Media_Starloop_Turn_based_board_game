using UnityEngine;
using TMPro;
using TurnBased.Core;

public class VictoryCanvasUpdater : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private TMP_Text playerNoText = null;

    [Header("Auto-Set - 4 debugging")]
    [SerializeField] private Canvas victoryCanvas = null;

    // Start is called before the first frame update
    void Start()
    {
        if (playerNoText == null) {
            var texts = GetComponentsInChildren<TMP_Text>();
            playerNoText = texts[texts.Length - 1];
            Debug.LogWarning("'Player No Text' SHOULD be assigned! it has been assigne automatically as the last TMP_Text in children - may lead to errors");
        }

        // get victory canvas
        victoryCanvas = GetComponent<Canvas>();
        // disable victory screen
        victoryCanvas.enabled = false;

        // subscribe to game end event
        GameHandler.onGameEnd += UpdateVictoryScreen;
    }
    private void OnDestroy()
    {
        // unsubscribe to game end event
        GameHandler.onGameEnd -= UpdateVictoryScreen;
    }

    private void UpdateVictoryScreen(int winnerID)
    {
        // set winner text
        playerNoText.text = (winnerID + 1).ToString();
        // enable victory screen
        victoryCanvas.enabled = true;
    }
}
