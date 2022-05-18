using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;

namespace TurnBased.Core {
    public class CharController : MonoBehaviour
    {
        [SerializeField] protected uint playerId = 0;
        protected float targetX = 0;

        private float moveUpdateInterval = 0.1f;
        
        // public virtual bool Move() { return true; }
        public virtual bool Move(int diceResult) { 
            targetX = (int)transform.position.x + diceResult;
            return true;
        }

        private void Start() {
            // subscribe to dice roll calls
            DiceController.onDiceRoll += MoveToTarget;
            DiceController.onBigDiceRoll += MoveToTarget;
        }
        private void OnDestroy() {
            // subscribe to dice roll calls
            DiceController.onDiceRoll -= MoveToTarget;
            DiceController.onBigDiceRoll -= MoveToTarget;
        }

        private void MoveToTarget(int diceValue) {
            // not current player turn - ignore call
            if (GameData.Instance.CurrentPlayerID != playerId) { return; }

            // current player -> move char to new position
            targetX = transform.position.x + diceValue;
            Timing.RunCoroutine(MoveToTargetCO());
        }
        private IEnumerator<float> MoveToTargetCO() {
            if (targetX > transform.position.x) {
                transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                PlayMoveAnimation();
            } else {
                PlayIdleAnimation();
            }
            yield return Timing.WaitForSeconds(moveUpdateInterval);
            if (targetX > transform.position.x) {
                StartCoroutine(MoveToTargetCO());
            }
        }

        private void PlayMoveAnimation() {
            /* TODO - implement */
        }
        private void PlayIdleAnimation()
        {
            /* TODO - implement */
        }
    }
}