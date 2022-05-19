using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBased.Core
{
    public class GameData : MonoBehaviour
    {
        public static GameData Instance { get; private set; }
        [Header("Game Settings")]
        [SerializeField] private int playerNo = 2;

        public int PlayerNo { get { return playerNo; } }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }
    }
}