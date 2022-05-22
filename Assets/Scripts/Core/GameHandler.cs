using System;
using System.Collections.Generic;
using MEC;
using UnityEngine;


namespace TurnBased.Core
{
    public class GameHandler : MonoBehaviour
    {
        public static event Action<bool> onMoveStateChanged = null;
        public static event Action<bool> onBigDiceButtonVisibilityCheck = null;
        public static event Action<int, bool> onNextTurn = null;
        public static event Action<int> onNextTurnCameraUpdate = null;
        public static event Action<int> onGameEnd = null;
        public static event Action onPlayerVictory = null;
        public static event Action onPlayerDefeat = null;
        [Header("Settings")]
        [SerializeField] private float secondDelayAtEndOfTurn = 1f;
        [SerializeField] private int bigDiceTurnCooldown = 3;

        [Header("Auto-Set -> only for debug")]
        [SerializeField] private CharController[] players = null;
        // set player types Human / AI (true / false)
        [SerializeField] private bool[] playerTypes = null;
        [SerializeField] private int currentPlayerID = 0;
        [SerializeField] private int currentTurn = 0;
        [SerializeField] private bool isGameEnded = false;
        [SerializeField] private int[] bigDiceTurnUse;

        public int CurrentPlayerID { get { return currentPlayerID; } }
        public int CurrentTurn { get { return currentTurn; } }
        public bool IsVictory { get { return isGameEnded; } }

        private int maxPlayerId = -1;
        private bool isPlayerMoving = false;

        private void Start()
        {
            var pNo = GameData.Instance.PlayerNo;
            maxPlayerId = pNo - 1;

            // // check why it doesn't find items
            // if (players == null)
            // {
                /* TODO - find way to get players */
            //     players = GameObject.FindObjectsOfType<CharController>();
            // }
            if (players == null || players.Length < 1)
            {
                Debug.LogError("No players in scene");
            } else {
                for (int i = 0; i < players.Length; i += 1) {
                    if (players[i] == null) {
                        Debug.LogError("Player list invalid!");
                    }
                }
            }
            
            // setup big dice & player types start data
            bigDiceTurnUse = new int[pNo];
            playerTypes = new bool[pNo];
            for (int i = 0; i < bigDiceTurnUse.Length; i += 1)
            {
                bigDiceTurnUse[i] = -bigDiceTurnCooldown;
                playerTypes[i] = players[i].GetType() == typeof(PlayerController) ? true : false;
            }

            // subscribe to victory checks
            VictoryPoint.onVictory += VictoryEndGame;

            // subscribe to char movements calls
            CharController.onMoveDone += NextTurn;

            // subscribe to dice rolls
            DiceController.onDiceRoll += MoveCurrentPlayer;
            // subscribe to big dice rolls
            DiceController.onBigDiceRoll += SetBigDiceRollTurn;
        }
        private void OnDestroy()
        {
            // unsubscribe to victory checks
            VictoryPoint.onVictory -= VictoryEndGame;

            // unsubscribe to char movements calls
            CharController.onMoveDone -= NextTurn;

            // unsubscribe to dice rolls
            DiceController.onDiceRoll -= MoveCurrentPlayer;
            // unsubscribe to big dice rolls
            DiceController.onBigDiceRoll -= SetBigDiceRollTurn;
        }

        private void MoveCurrentPlayer(int diceValue)
        {
            // stop movement if game ended
            if (isGameEnded) return;
            isPlayerMoving = true;
            onMoveStateChanged?.Invoke(isPlayerMoving);
            // better to call a single method that do "PlayerNumber" ifs
            players[currentPlayerID].Move(diceValue);
        }
        private void SetBigDiceRollTurn(int diceValue) {
            bigDiceTurnUse[currentPlayerID] = (int)(currentTurn / 2);
        }

        private void NextTurn(int movingPlayerID, int finalMovePosition)
        {
            if (isGameEnded) { return; }

            isPlayerMoving = false;
            onMoveStateChanged?.Invoke(isPlayerMoving);
            currentPlayerID += 1;
            if (currentPlayerID > maxPlayerId)
            {
                currentPlayerID = 0;
            }
            currentTurn += 1;

            Timing.RunCoroutine(CallSubscribersCO().CancelWith(gameObject));
        }
        private IEnumerator<float> CallSubscribersCO() {
            yield return Timing.WaitForSeconds(secondDelayAtEndOfTurn);

            int gameTurn = (int)(currentTurn / 2);

            // call big dice visibility change
            onBigDiceButtonVisibilityCheck?.Invoke((gameTurn - bigDiceTurnUse[currentPlayerID]) > bigDiceTurnCooldown);

            // call next turn subscribers
            onNextTurn?.Invoke(gameTurn, playerTypes[currentPlayerID]);

            // call camera subscribers
            onNextTurnCameraUpdate?.Invoke(currentPlayerID);
        }
        private void VictoryEndGame()
        {
            isGameEnded = true;
            onGameEnd?.Invoke(currentPlayerID);

            if (playerTypes[currentPlayerID] == true) {
                // if player type == true (player type == Player Controller) call victory subscribers
                onPlayerVictory?.Invoke();
            } else {
                // if player type == false (player type != Player Controller) call defeat subscribers
                onPlayerDefeat?.Invoke();
            }
        }
    }
}