using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceResultUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text diceResultText = null;

    // Start is called before the first frame update
    private void Start()
    {
        if (diceResultText == null) {
            diceResultText = GetComponent<TMP_Text>();
        }
        DiceController.onDiceRoll += UpdateText;
        DiceController.onBigDiceRoll += UpdateText;
    }
    private void OnDestroy() {
        DiceController.onDiceRoll -= UpdateText;
        DiceController.onBigDiceRoll -= UpdateText;
    }
    
    private void UpdateText(int diceResult) {
        diceResultText.text = diceResult.ToString();
    }
}
