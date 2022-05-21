// // using System.Collections;
// // using System.Collections.Generic;
// using UnityEngine;
// using TurnBased.Core;

// public class CameraFollow : MonoBehaviour
// {
//     [SerializeField] private CharController[] players = null;
//     [SerializeField] private Transform targetX = null;

//     private void Start() {
//         players = GameObject.FindObjectsOfType<CharController>();
//         Debug.Log("PLen: " +players.Length);

//         // GameHandler.onNextTurn += FollowCurrentPlayer;
//     }
//     private void OnDestroy() {
//         // GameHandler.onNextTurn -= FollowCurrentPlayer;
//     }

//     private void FollowCurrentPlayer() {

//     }

//     // [SerializeField] private int followId = 1;
//     // [SerializeField] private float moveUpdateInterval = 0.1f;
//     // private float targetX = 0;

//     // private void Start()
//     // {
//     //     // subscribe to dice roll calls
//     //     DiceController.onDiceRoll += FollowTarget;
//     //     DiceController.onBigDiceRoll += FollowTarget;
//     // }
//     // private void OnDestroy()
//     // {
//     //     // subscribe to dice roll calls
//     //     DiceController.onDiceRoll -= FollowTarget;
//     //     DiceController.onBigDiceRoll -= FollowTarget;
//     // }

//     // private void FollowTarget(int diceValue) {
//     //     // not current player turn - ignore call
//     //     if (GameData.Instance.CurrentPlayerID != followId) { return; }

//     //     // current player -> move char to new position
//     //     targetX = transform.position.x + diceValue;
//     //     Timing.RunCoroutine(FollowTargetCO());
//     // }
//     // private IEnumerator<float> FollowTargetCO()
//     // {
//     //     if (targetX > transform.position.x)
//     //     {
//     //         transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
//     //     }
//     //     yield return Timing.WaitForSeconds(moveUpdateInterval);
//     //     if (targetX > transform.position.x)
//     //     {
//     //         Timing.RunCoroutine(FollowTargetCO());
//     //     }
//     // }
// }
