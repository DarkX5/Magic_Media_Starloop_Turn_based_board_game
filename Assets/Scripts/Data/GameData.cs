using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBased.Core
{
    public class GameData : MonoBehaviour
    {
        public static GameData Instance { get; private set; }
        [Header("Game Settings")]
        [SerializeField] private int playerNo = 2;

        // [Header("Auto - Set -> only for debug")]
        // [SerializeField] private int currentPlayerID = 0;
        // [SerializeField] private int currentTurn = 1;
        // [SerializeField] private bool isVictory = false;

        // public int CurrentPlayerID { get { return currentPlayerID; } }
        public int PlayerNo { get { return playerNo; } }
        // public int CurrentTurn { get { return currentTurn; } }
        // public bool IsVictory { get { return isVictory; } }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }
        // private void Start()
        // {
        //     // subscribe to victory checks
        //     VictoryPoint.onVictory += VictoryEndGame;

        //     // subscribe to char movements calls
        //     CharController.onMove += NextTurn;
        // }
        // private void OnDestroy()
        // {
        //     // unsubscribe to victory checks
        //     VictoryPoint.onVictory -= VictoryEndGame;

        //     // unsubscribe to char movements calls
        //     CharController.onMove -= NextTurn;
        // }

        // private void NextTurn(int finalMovePosition) {
        //     if (isVictory) { return; }

        //     currentPlayerID += 1;
        //     if (currentPlayerID > playerNo - 1) {
        //         currentPlayerID = 0;
        //     }
        //     currentTurn += 1;
        // }
        // private void VictoryEndGame()
        // {
        //     isVictory = true;
        // }
    }
}