using UnityEngine;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
  /* PRIVATE */
  [SerializeField] private Transform player;
  [SerializeField] private GameObject[] tilePrefabs;
  [SerializeField] private float tileLength = 10f;
  [SerializeField] private int numberOfTilesOnScreen = 5;
  [SerializeField] private float distanceThreshold = 15f;
  [SerializeField] private float tileSpeedMultiplier = 1f;
  private readonly List<Transform> spawnedTiles = new(); // new List<Transform>();

  /* PUBLIC */
  public float TileSpeedMultiplier { get => tileSpeedMultiplier; set => tileSpeedMultiplier = value; }

  /* CONSTRUCTOR */
  private void Start()
  {
    Debug.Log($"tile: {tileSpeedMultiplier}");
    Debug.Log($"TileSpeedMultiplier: {TileSpeedMultiplier}");

    for (int i = 0; i < numberOfTilesOnScreen; i++)
    {
      SpawnTile();
    }
  }

  private void Update()
  {
    // Move the tiles towards the player's position
    for (int i = 0; i < spawnedTiles.Count; i++)
    {
      Transform tile = spawnedTiles[i];
      // Calculate the new position of the tile
      Vector3 newPosition = tile.position + Vector3.back * (tileLength * TileSpeedMultiplier * Time.deltaTime);

      // Move the tile
      tile.position = newPosition;

      // Check if the tile has passed the player's position (z = 0)
      if (tile.position.z < -distanceThreshold)
      {
        RecycleTile(tile);
      }
    }
  }

  private void SpawnTile()
  {
    int randomIndex = Random.Range(0, tilePrefabs.Length);
    GameObject tileObject = Instantiate(tilePrefabs[randomIndex], transform.forward * (spawnedTiles.Count * tileLength), Quaternion.identity);
    spawnedTiles.Add(tileObject.transform);
  }

  private void RecycleTile(Transform tile)
  {
    tile.gameObject.SetActive(false);
    // Reposition it at the end of the tiles array and update its position
    tile.position = spawnedTiles[spawnedTiles.Count - 1].position + Vector3.forward * tileLength;
    tile.gameObject.SetActive(true);
    spawnedTiles.Remove(tile);
    spawnedTiles.Add(tile);
  }

  private void OnValidate()
  {
    TileSpeedMultiplier = Mathf.Max(1f, TileSpeedMultiplier);
  }
}
