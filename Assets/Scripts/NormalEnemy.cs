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

    private bool isAttacking = false;

    private Animator swordAnimator; // Reference to the sword's Animator

    private Rigidbody rb;
    private bool canDie = false;

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

            // If the enemy is farther than the stopping distance, move towards the player
            if (distanceToPlayer > stoppingDistance)
            {
                Vector3 direction = (player.position - transform.position).normalized;
                rb.MovePosition(transform.position + direction * speed * Time.deltaTime);

                // Rotate to face the player
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                rb.MoveRotation(Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed));
                //swordAnimator.SetBool("IsAttacking", false);
            }
            else
            {
                isAttacking = true;
                // Trigger the sword's attack animation when close to the player
                //swordAnimator.SetBool("IsAttacking", true);
            }
        }
    }
    void StopAttack()
    {
        isAttacking = false;
    }

    public void changeDie()
    {
        canDie = true;
        StartCoroutine("revive");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (canDie && collision.gameObject.tag == "Katana")
        {
            Destroy(this.gameObject);
        }
    }

    private IEnumerator revive()
    {
        yield return new WaitForSeconds(2);
        canDie = true;
        transform.GetChild(0).GetComponent<EnemyController>().revive();
    }
}
