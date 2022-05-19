using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TurnBased.Core
{
    public class GameHandler : MonoBehaviour
    {
        // public static event Action<int, int> onMoveCurrentPlayer = null;
        public static event Action<bool> onMoveStateChanged = null;
        public static event Action<int> onGameEnd = null;

        [Header("Auto-Set -> only for debug")]
        [SerializeField] private CharController[] players = null;
        [SerializeField] private int currentPlayerID = 0;
        [SerializeField] private int currentTurn = 1;
        [SerializeField] private bool isVictory = false;

        public int CurrentPlayerID { get { return currentPlayerID; } }
        public int CurrentTurn { get { return currentTurn; } }
        public bool IsVictory { get { return isVictory; } }

        private int maxPlayerId = -1;
        private bool isPlayerMoving = false;

        private void Start()
        {
            maxPlayerId = GameData.Instance.PlayerNo - 1;

            // check why it doesn't find items
            if (players == null)
            {
                players = GameObject.FindObjectsOfType<CharController>();
            }
            if (players == null || players.Length < 1)
            {
                Debug.LogError("No players in scene");
            }

            // subscribe to victory checks
            VictoryPoint.onVictory += VictoryEndGame;

            // subscribe to char movements calls
            CharController.onMoveDone += NextTurn;

            // subscribe to dice rolls
            DiceController.onDiceRoll += MoveCurrentPlayer;
        }
        private void OnDestroy()
        {
            // unsubscribe to victory checks
            VictoryPoint.onVictory -= VictoryEndGame;

            // unsubscribe to char movements calls
            CharController.onMoveDone -= NextTurn;

            // unsubscribe to dice rolls
            DiceController.onDiceRoll -= MoveCurrentPlayer;
        }

        private void MoveCurrentPlayer(int diceValue)
        {
            // if game won || player moving - ignore move commands
            if (isVictory || isPlayerMoving) return;

            isPlayerMoving = true;
            onMoveStateChanged?.Invoke(isPlayerMoving);
            // better to call a single method that do "PlayerNumber" ifs
            players[currentPlayerID].Move(diceValue);

            // // move current player
            // onMoveCurrentPlayer?.Invoke(currentPlayerID, diceValue);
        }

        private void NextTurn(int finalMovePosition)
        {
            if (isVictory) { return; }

            isPlayerMoving = false;
            onMoveStateChanged?.Invoke(isPlayerMoving);
            currentPlayerID += 1;
            if (currentPlayerID > maxPlayerId)
            {
                currentPlayerID = 0;
            }
            currentTurn += 1;
        }
        private void VictoryEndGame()
        {
            Debug.Log("victory check - player " + currentPlayerID);
            isVictory = true;
            onGameEnd?.Invoke(currentPlayerID);
        }
    }
}