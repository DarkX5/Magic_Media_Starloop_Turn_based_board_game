// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

using System;

namespace TurnBased.Core
{
    public class AIController : CharController
    {
        public static event Action onAIDiceRoll = null;
        private void Start() {
            base.Init();
            GameHandler.onNextTurn += AIMove;   
        }
        private void OnDestroy() {
            GameHandler.onNextTurn -= AIMove;
        }

        private void AIMove(int turnNo, bool isHuman) {
            if (!isHuman)
                onAIDiceRoll?.Invoke();
        }
        protected override void CheckEndGameAnimation(int winningPlayerID = -1)
        {
            base.CheckEndGameAnimation(winningPlayerID);
        }
    }
}