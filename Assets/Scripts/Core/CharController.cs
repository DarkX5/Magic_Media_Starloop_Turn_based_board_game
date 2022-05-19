using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;

namespace TurnBased.Core {
    public class CharController : MonoBehaviour
    {
        public static event Action<int> onMoveDone = null;
        [SerializeField] protected uint playerId = 0;
        [SerializeField] protected float moveStep = 0.1f;
        [SerializeField] private float moveUpdateInterval = 0.1f;
        protected float targetX = 0;

        public virtual bool Move(int diceValue)
        {
            // move char to new position
            targetX = transform.position.x + diceValue;
            Timing.RunCoroutine(MoveToTargetCO());
            return true;
        }

        // move char over time
        private IEnumerator<float> MoveToTargetCO() {
            if (targetX > transform.position.x) {
                transform.position = new Vector3(transform.position.x + moveStep, transform.position.y, transform.position.z);
                PlayMoveAnimation();
            }
            
            yield return Timing.WaitForSeconds(moveUpdateInterval);

            if (targetX > transform.position.x) {
                StartCoroutine(MoveToTargetCO());
            } else {
                PlayIdleAnimation();
                onMoveDone?.Invoke((int)transform.position.x);
            }
        }

        private void PlayMoveAnimation() {
            /* TODO - implement */
            // Debug.Log($"Move {playerId}");
        }
        private void PlayIdleAnimation()
        {
            /* TODO - implement */
            Debug.Log($"Idle {playerId}");
        }
        private void CheckEndGameAnimation(int currentPlayerID = -1) {
            if (currentPlayerID == playerId)
                PlayVictoryAnimation();
            else
                PlayDefeatAnimation();
        }
        private void PlayVictoryAnimation() {
            /* TODO - implement */
            Debug.Log($"Victory {playerId}");
        }
        private void PlayDefeatAnimation()
        {
            /* TODO - implement */
            Debug.Log($"Defeat {playerId}");
        }
    }
}