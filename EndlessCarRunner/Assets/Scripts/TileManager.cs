using UnityEngine;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
  /* PRIVATE */
  [SerializeField] private Transform player;
  [SerializeField] private GameObject[] tilePrefabs;
  [SerializeField] private float tileLength = 10f;
  [SerializeField] private float playerTileDistance = 15f; // distance from player to the tile
  [SerializeField] private int numberOfTilesOnScreen = 1;
  [SerializeField] private float tileSpeedMultiplier = 1f;
  private readonly List<Transform> spawnedTiles = new(); // new List<Transform>();

  public float TileSpeedMultiplier { get => tileSpeedMultiplier; set => tileSpeedMultiplier = value; }

  private void Start()
  {
    Vector3 spawnPosition = player.position - (Vector3.forward * (tileLength / 2));

    SpawnTile(spawnPosition);

    for (int i = 0; i < numberOfTilesOnScreen; i++)
    {
      SpawnTile();
    }
  }

  private void Update()
  {
    for (int i = 0; i < spawnedTiles.Count; i++)
    {
      Transform tile = spawnedTiles[i];
      Vector3 newPosition = tile.position + Vector3.back * (TileSpeedMultiplier * Time.deltaTime);

      tile.position = newPosition;

      if (tile.position.z < -playerTileDistance)
      {
        RecycleTile(tile);
      }
    }
  }

  private void SpawnTile()
  {
    SpawnTile(Vector3.forward * tileLength);
  }

  private void SpawnTile(Vector3 spawnPosition)
  {
    int randomIndex = Random.Range(0, tilePrefabs.Length);
    if (spawnedTiles.Count > 0)
    {
      spawnPosition = spawnedTiles[spawnedTiles.Count - 1].position + Vector3.forward * tileLength;
    }

    spawnPosition.y = 0; // This ensures that the Y position is 0.

    GameObject tileObject = Instantiate(tilePrefabs[randomIndex], spawnPosition, Quaternion.identity);
    spawnedTiles.Add(tileObject.transform);
  }


  private void RecycleTile(Transform tile)
  {
    tile.gameObject.SetActive(false);
    //tile.position = spawnedTiles[spawnedTiles.Count - 1].position + Vector3.forward * tileLength;
    spawnedTiles.Remove(tile);
    Destroy(tile.gameObject);

    SpawnTile();
  }

  private void OnValidate()
  {
    TileSpeedMultiplier = Mathf.Max(1f, TileSpeedMultiplier);
  }
}