using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;         // Reference to the player's position
    public float stalkSpeed = 2f;    // Speed for stalking from far away
    public float approachSpeed = 3f; // Speed for approaching the player
    public float followRange = 10f;  // The range within which the enemy will follow the player
    public float stopRange = 2f;     // The range where the enemy stops near the player
    public float fleeRange = 5f;     // Range to flee when the player holds light
    public float fleeSpeed = 5f;     // Speed when fleeing
    public float maxFleeDistance = 15f; // Maximum distance the enemy can flee from the player

    private ToggleLight playerLightScript;  // Reference to the ToggleLight script
    private Rigidbody2D rb;                 // Rigidbody for enemy movement
    private Vector2 movement;               // Movement direction
    private float currentSpeed;             // Variable to store the current speed
    private bool isFleeing = false;         // Flag to check if the enemy is fleeing
    private bool isStalking = true;         // Whether the enemy is stalking or approaching
    private bool decisionMade = false;      // Flag to check if the 50% decision has been made
    private float initialFleeDistance;      // Store initial distance when fleeing starts

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerLightScript = player.GetComponent<ToggleLight>();  // Access ToggleLight script from the player
        currentSpeed = stalkSpeed;  // Start by stalking from far away
    }

    void Update()
    {
        // Calculate distance between the enemy and the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (playerLightScript.isPlayerlighton() && distanceToPlayer <= fleeRange)
        {
            // Flee from the player when holding the light, but only within maxFleeDistance
            if (!isFleeing)
            {
                initialFleeDistance = distanceToPlayer;  // Store initial distance to compare later
            }

            if (distanceToPlayer - initialFleeDistance <= maxFleeDistance)
            {
                Vector2 direction = (transform.position - player.position).normalized;
                movement = direction;
                currentSpeed = fleeSpeed;
                isFleeing = true;
                decisionMade = false;  // Reset the 50% decision when fleeing
            }
        }
        else if (distanceToPlayer <= followRange && distanceToPlayer > stopRange)
        {
            // Make a 50% chance decision if not already made
            if (!decisionMade)
            {
                isStalking = Random.value <= 0.5f;  // 50% chance to stalk or approach
                decisionMade = true;
            }

            if (isStalking)
            {
                // Stalk the player from far away
                Vector2 direction = (player.position - transform.position).normalized;
                movement = direction;
                currentSpeed = stalkSpeed;
            }
            else
            {
                // Approach the player more closely
                Vector2 direction = (player.position - transform.position).normalized;
                movement = direction;
                currentSpeed = approachSpeed;
            }
            isFleeing = false;
        }
        else if (distanceToPlayer <= stopRange)
        {
            // Approach the player closely
            Vector2 direction = (player.position - transform.position).normalized;
            movement = direction;
            currentSpeed = approachSpeed;  // Speed for approaching the player closely
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
        rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);

        // Reset flee speed once the enemy stops fleeing
        if (!playerLightScript.isPlayerlighton() && isFleeing)
        {
            currentSpeed = stalkSpeed;  // Reset to normal stalking speed
            isFleeing = false;          // Reset the fleeing state
        }
    }
}
