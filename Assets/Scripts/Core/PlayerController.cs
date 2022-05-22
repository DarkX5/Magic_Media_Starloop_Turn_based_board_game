// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

namespace TurnBased.Core
{
    public class PlayerController : CharController
    {
        private void Start()
        {
            base.Init();
        }
        protected override void CheckEndGameAnimation(int winningPlayerID = -1)
        {
            base.CheckEndGameAnimation(winningPlayerID);
        }
    }
}