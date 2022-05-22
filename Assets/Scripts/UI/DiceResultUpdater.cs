using UnityEngine;
using TMPro;
using TurnBased.Core;

public class DiceResultUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text diceResultText = null;

    private bool isMoving = false;

    // Start is called before the first frame update
    private void Start()
    {
        if (diceResultText == null) {
            diceResultText = GetComponent<TMP_Text>();
        }

        // subscribe to dice rolls
        DiceController.onDiceRoll += UpdateText;

        // subscribe to move state calls
        GameHandler.onMoveStateChanged += SetIsMoving;
    }
    private void OnDestroy() {
        // unsubscribe to dice rolls
        DiceController.onDiceRoll -= UpdateText;

        // unsubscribe to move state calls
        GameHandler.onMoveStateChanged -= SetIsMoving;
    }
    
    private void UpdateText(int diceResult) {
        if (isMoving) return;
        diceResultText.text = diceResult.ToString();
    }
    private void SetIsMoving(bool newValue) {
        isMoving = newValue;
    }
}
