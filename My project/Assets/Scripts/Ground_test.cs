using UnityEngine;
using System.Collections.Generic;

public class InfiniteGround : MonoBehaviour
{
    public GameObject groundPrefab; // The ground tile prefab
    public Transform player; // Reference to the player
    public float groundWidth = 10f; // Width of a ground tile
    public int viewDistance = 5; // Number of ground tiles ahead of player
    public int maxGroundTiles = 5; // Maximum number of ground tiles

    private Vector2 lastGroundPosition;
    private List<GameObject> groundTiles = new List<GameObject>(); // List to keep track of ground tiles

    void Start()
    {
        lastGroundPosition = player.position;
        GenerateGround();
    }

    void Update()
    {
        // Check if player moved enough to generate new ground
        if (Vector2.Distance(player.position, lastGroundPosition) > groundWidth)
        {
            GenerateGround();
            lastGroundPosition = player.position;
        }

        // Remove ground tiles that are far away
        RemoveFarGroundTiles();
    }

    void GenerateGround()
    {
        float playerX = player.position.x;

        // Generate ground tiles ahead of the player
        for (int i = -viewDistance; i < viewDistance; i++)
        {
            Vector3 groundPosition = new Vector3(playerX + i * groundWidth, -1, 0);

            // Only create ground if it doesn't already exist at the position
            if (!GroundExists(groundPosition))
            {
                GameObject newGround = Instantiate(groundPrefab, groundPosition, Quaternion.identity);
                groundTiles.Add(newGround); // Add the newly created ground tile to the list
            }
        }
    }

    bool GroundExists(Vector3 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.1f);
        return colliders.Length > 0;
    }

    void RemoveFarGroundTiles()
    {
        // Remove any ground tiles that are far from the player and if the list exceeds maxGroundTiles
        if (groundTiles.Count > maxGroundTiles)
        {
            GameObject farthestTile = groundTiles[0]; // The oldest tile will be farthest from the player
            Destroy(farthestTile);
            groundTiles.RemoveAt(0); // Remove the reference from the list
        }
    }
}

