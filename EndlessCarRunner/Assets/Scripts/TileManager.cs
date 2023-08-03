using UnityEngine;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
  public Transform player; // Reference to the player car
  public GameObject[] tilePrefabs; // Array of different tile prefabs
  public float tileLength = 10f; // Length of each tile
  public int tilesOnScreen = 5; // Number of tiles to keep on the screen at any given time

  private List<Transform> spawnedTiles = new List<Transform>();

  // Tile movement speed multiplier (20 times faster)
  public float tileSpeedMultiplier = 20f;

  private void Start()
  {
    for (int i = 0; i < tilesOnScreen; i++)
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
      Vector3 newPosition = tile.position + Vector3.back * (tileLength * tileSpeedMultiplier * Time.deltaTime);

      // Move the tile
      tile.position = newPosition;

      // Check if the tile has passed the player's position (z = 0)
      if (tile.position.z < -15)
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
    // Deactivate the tile
    tile.gameObject.SetActive(false);
    // Reposition it at the end of the tiles array and update its position
    tile.position = spawnedTiles[(spawnedTiles.Count - 1)].position + Vector3.forward * tileLength;
    // Activate the tile
    tile.gameObject.SetActive(true);
    // Move the tile to the end of the list
    spawnedTiles.Remove(tile);
    spawnedTiles.Add(tile);
  }

  // Adjust the tile movement speed in the Editor using a slider
  private void OnValidate()
  {
    tileSpeedMultiplier = Mathf.Max(1f, tileSpeedMultiplier);
  }
}
