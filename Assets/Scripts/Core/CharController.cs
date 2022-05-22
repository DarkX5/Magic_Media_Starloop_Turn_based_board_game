using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;

namespace TurnBased.Core {
    public class CharController : MonoBehaviour
    {
        public static event Action<int, int> onMoveDone = null;
        [SerializeField] protected int playerId = 0;
        [SerializeField] protected float moveStep = 0.1f;
        [SerializeField] private float moveUpdateInterval = 0.1f;
        [Header("Auto-Set - visible 4 debugging")]
        [SerializeField] protected CharacterAnimationControl animationControl = null;
        protected float targetX = 0;

        public int PlayerID { get { return playerId; } }

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
                Timing.RunCoroutine(MoveToTargetCO());
            } else {
                PlayIdleAnimation();
                onMoveDone?.Invoke(playerId, (int)transform.position.x);
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
            animationControl?.EnableRunAnimation();
        }
        private void PlayIdleAnimation()
        {
            animationControl?.EnableIdleAnimation();
        }
        private void PlayVictoryAnimation() {
            animationControl?.EnableVictoryAnimation();
        }
        private void PlayDefeatAnimation()
        {
            animationControl?.EnableDefeatAnimation();
        }
    }
}