using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }
    [SerializeField] private int currentPlayerID = 0;
    [SerializeField] private int currentTurn = 1;

    public int CurrentPlayerID { get { return currentPlayerID; } }
    public int CurrentTurn { get { return currentTurn; } }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }
}
