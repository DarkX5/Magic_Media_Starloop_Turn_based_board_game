// using System.Collections;
// using System.Collections.Generic;
using System;
using TurnBased.Core;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    public static event Action<int> onDiceRoll = null;
    public static event Action<int> onBigDiceRoll = null;
    [SerializeField] private DiceRange normalDiceRange = new DiceRange(1, 6);
    [SerializeField] private DiceRange bigDiceRange = new DiceRange(5, 10);
    private int currentTurn = 0;

    // called from UI
    public void RollDiceUI() {
        int resultValue = UnityEngine.Random.Range(normalDiceRange.min, normalDiceRange.max + 1);
        onDiceRoll?.Invoke(resultValue);
    }
    // called from UI
    public void RollBigDiceUI() {
        int resultValue = UnityEngine.Random.Range(bigDiceRange.min, bigDiceRange.max + 1);
        onDiceRoll?.Invoke(resultValue);
        onBigDiceRoll?.Invoke(resultValue);
    }
}
