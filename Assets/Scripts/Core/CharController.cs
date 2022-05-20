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
        [Header("Auto-Set - visible 4 debugging")]
        [SerializeField] protected CharacterAnimationControl animationControl = null;
        protected float targetX = 0;

        protected virtual void Init() {
            if (animationControl == null)
                animationControl = GetComponent<CharacterAnimationControl>();

            GameHandler.onGameEnd += CheckEndGameAnimation;
        }
        private void OnDestroy() {
            GameHandler.onGameEnd -= CheckEndGameAnimation;
        }

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

        private void CheckEndGameAnimation(int winningPlayerID = -1) {
            if (winningPlayerID == playerId)
                PlayVictoryAnimation();
            else
                PlayDefeatAnimation();
        }
        private void PlayMoveAnimation()
        {
            /* TODO - implement */
            animationControl?.EnableRunAnimation();
            Debug.Log($"Move - {playerId}");
        }
        private void PlayIdleAnimation()
        {
            /* TODO - implement */
            animationControl?.EnableIdleAnimation();
            Debug.Log($"Idle {playerId}");
        }
        private void PlayVictoryAnimation() {
            /* TODO - implement */
            animationControl?.EnableVictoryAnimation();
            Debug.Log($"Victory {playerId}");
        }
        private void PlayDefeatAnimation()
        {
            /* TODO - implement */
            animationControl?.EnableDefeatAnimation();
            Debug.Log($"Defeat {playerId}");
        }
    }
}