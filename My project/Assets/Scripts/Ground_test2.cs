using UnityEngine;

public class GroundGeneration : MonoBehaviour
{
    public float ClosestDistacnceFromPlayer;

    public GameObject GroundTile;

    public float TileWidth;

    public Transform Player;

    void Start()
    {
        //Spawn a tile so that the player won't fall off at the start
        SpawnTile(1);

    }

    void SpawnTile(int n)
    {
        int i = 0;

        //Spawn n tiles
        while (i < n)
        {
            Instantiate(GroundTile, transform.position, Quaternion.identity);
            i++;

            //Teleport the "Ground generator" to the end of the tile spawned
            transform.position += TileWidth * Vector3.right; // Or use Vector3.forward if you want to generate ground on z axis.

        }
    }


    void FixedUpdate()
    {
        if (Vector3.Distance(Player.position, transform.position) <= ClosestDistacnceFromPlayer)
        {
            SpawnTile(1);
        }
    }


}