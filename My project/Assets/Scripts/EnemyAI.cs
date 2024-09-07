using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;         // Reference to the player's position
    public float moveSpeed = 3f;     // Speed of enemy movement
    public float followRange = 10f;  // The range within which the enemy will follow the player
    public float stopRange = 2f;     // The range where the enemy stops near the player
    public float fleeRange = 5f;     // Range to flee when the player holds light
    public float fleeSpeed = 5f;     // Speed when fleeing

    private ToggleLight playerLightScript;  // Reference to the ToggleLight script
    private Rigidbody2D rb;                // Rigidbody for enemy movement
    private Vector2 movement;              // Movement direction

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerLightScript = player.GetComponent<ToggleLight>();  // Access ToggleLight script from the player
    }

    void Update()
    {
        // Calculate distance between the enemy and the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (playerLightScript.isPlayerlighton() && distanceToPlayer <= fleeRange)
        {
            // Flee from the player when holding the light
            Vector2 direction = (transform.position - player.position).normalized;
            movement = direction;
            moveSpeed = fleeSpeed;
        }
        else if (distanceToPlayer <= followRange && distanceToPlayer > stopRange)
        {
            // Stalk the player when not holding the light
            Vector2 direction = (player.position - transform.position).normalized;
            movement = direction;
        }
        else
        {
            // Stop moving
            movement = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        // Apply movement to the enemy
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    private void LateUpdate()
    {
        print("bludgoogo gaga");
    }
}
