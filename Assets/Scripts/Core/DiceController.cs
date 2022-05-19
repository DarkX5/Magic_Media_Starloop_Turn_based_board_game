// using System.Collections;
// using System.Collections.Generic;
using System;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    public static event Action<int> onDiceRoll = null;
    // public static event Action<int> onBigDiceRoll = null;
    [SerializeField] private DiceRange normalDiceRange = new DiceRange(1, 6);
    [SerializeField] private DiceRange bigDiceRange = new DiceRange(5, 10);
    [SerializeField] private int bigDiceTurnCooldown = 3;
    private int bigDiceCooldownDuration;

    // Start is called before the first frame update
    void Start()
    {
        // start it with -3 so that it can be used from the start of the game
        bigDiceCooldownDuration = -bigDiceTurnCooldown;
    }

    // called from UI
    public void RollDiceUI() {
        int resultValue = UnityEngine.Random.Range(normalDiceRange.min, normalDiceRange.max + 1);
        onDiceRoll?.Invoke(resultValue);
        // RollNormalDice();
    }
    // called from UI
    public void RollBigDiceUI() {
        int resultValue = UnityEngine.Random.Range(bigDiceRange.min, bigDiceRange.max + 1);
        onDiceRoll?.Invoke(resultValue);
        // RollBigDice();
    }
    
    // public int RollNormalDice() {
    //     int resultValue = UnityEngine.Random.Range(normalDiceRange.min, normalDiceRange.max + 1);
    //     onDiceRoll?.Invoke(resultValue);
    //     return resultValue;
    // }
    // public int RollBigDice()
    // {
    //     int resultValue = UnityEngine.Random.Range(bigDiceRange.min, bigDiceRange.max + 1);
    //     onDiceRoll?.Invoke(resultValue);
    //     // onBigDiceRoll?.Invoke(resultValue);
    //     return resultValue;
    // }
}
