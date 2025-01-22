using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class NormalEnemy : MonoBehaviour
{
    public Transform player; // Reference to the player
    public float speed = 5f; // Movement speed of the enemy
    public float rotationSpeed = 5f; // Speed of rotation to face the player
    public float stoppingDistance = 2f; // Distance at which the enemy stops moving
    public float agroDistance = 10f; // Distance at which the enemy starts chasing the player

    private bool isAttacking = false;

    private bool canDie = false;

    private Animator swordAnimator; // Reference to the sword's Animator
    private Rigidbody rb;

    private GameObject sword;

    void Start()
    {
        // Get the Animator component from the child object "sword"
        swordAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (player != null)
        {
            // Calculate the distance to the player
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Check if the player is within agro range
            if (distanceToPlayer <= agroDistance)
            {
                // If the enemy is farther than the stopping distance, move towards the player
                if (distanceToPlayer > stoppingDistance)
                {
                    Vector3 direction = (player.position - transform.position).normalized;
                    rb.MovePosition(transform.position + direction * speed * Time.deltaTime);

                    // Rotate to face the player
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    rb.MoveRotation(Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed));

                    // Stop attacking animation if moving
                    swordAnimator.SetBool("IsAttacking", false);
                }
                else
                {
                    // Start attacking animation when close to the player
                    isAttacking = true;
                    swordAnimator.SetBool("IsAttacking", true);
                }
            }
            else
            {
                // If the player is outside the agro distance, stop attacking and idle
                swordAnimator.SetBool("IsAttacking", false);
            }
        }
    }

    void StopAttack()
    {
        isAttacking = false;
    }

    public void changeDie(GameObject child)
    {
        canDie = true;
        sword = child;
        StartCoroutine("revive");
    }

    public void Kill()
    {
        if (canDie)
        {
            Destroy(this.gameObject);
        }
    }

    private IEnumerator revive()
    {
        yield return new WaitForSeconds(2);
        canDie = true;
        sword.GetComponent<EnemyController>().revive();
    }
}