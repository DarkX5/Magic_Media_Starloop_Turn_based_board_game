using System;
using TurnBased.Core;
using UnityEngine;

public class EnvironmentGenerator : MonoBehaviour
{
    public static event Action onEnvironmentGenerated = null;
    [Header("Settings")]
    [SerializeField] private GameObject[] floorTiles = null;
    [SerializeField] private GameObject[] separatorTiles = null;
    [SerializeField] private float tileSize = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 vPoint = GameObject.FindObjectOfType<VictoryPoint>().transform.position;
        int tileMaxPlacement = (int)(vPoint.x + (vPoint.x * 0.25f));
        int playerNo = GameData.Instance.PlayerNo;
        float newPosZ;
        GameObject newTile = null;
        for (int i = 0; i < tileMaxPlacement; i += (int)tileSize) {
            // generate player-walkable tiles
            for (int j = 0; j < playerNo; j += 1) {
                newTile = Instantiate(floorTiles[UnityEngine.Random.Range(0, floorTiles.Length)], transform);
                // calculate tile Z position -> the doubling is required to leave space for separator placements
                newPosZ = tileSize * j; // * 2;
                newTile.transform.position = new Vector3(i, 0f, newPosZ);
            }

            // // generate separator tiles
            // for (int j = 1; j < playerNo; j += 1)
            // {
            //     newTile = Instantiate(separatorTiles[UnityEngine.Random.Range(0, separatorTiles.Length)], transform);
            //     newPosZ = tileSize * j;
            //     newTile.transform.position = new Vector3(i, 0f, newPosZ);
            // }
        }
        onEnvironmentGenerated?.Invoke();
    }
}
