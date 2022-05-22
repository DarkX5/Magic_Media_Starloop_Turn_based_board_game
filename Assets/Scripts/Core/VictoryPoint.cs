using System;
using UnityEngine;

namespace TurnBased.Core
{
    public class VictoryPoint : MonoBehaviour
    {
        public static event Action onVictory = null;
        [SerializeField] private Vector3 victoryPoint = Vector3.zero;

        // Start is called before the first frame update
        void Start()
        {
            if (victoryPoint == Vector3.zero)
            {
                victoryPoint = transform.position;
            }

            // subscribe to char movements calls
            CharController.onMoveDone += CheckVictory;
        }
        private void OnDestroy() {
            // unsubscribe to char movements calls
            CharController.onMoveDone -= CheckVictory;
        }

        private void CheckVictory(int playerID, int finalMovePosition) {
            if (finalMovePosition >= (int)victoryPoint.x) {
                Victory();
            }
        }

        public bool Victory()
        {
            if (onVictory == null)
            {
                Debug.LogError("Not subscribers assigned to onVictory event");
                return false;
            }

            onVictory.Invoke();
            return true;
        }
    }
}