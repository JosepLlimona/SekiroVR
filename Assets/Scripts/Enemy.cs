using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool canDie = false;

    public Transform player;
    public float speed = 5f;
    public float rotationSpeed = 5f;
    public float stoppingDistance = 2f;
    public float agroDistance = 10f; // Distance at which the enemy starts reacting to the player

    public float dodgeSpeed = 10f;
    public float dodgeDistance = 3f;
    public float dodgeCooldown = 2f;
    private bool canDodge = true;

    private Animator swordAnimator;
    private int attackType;

    public float attackCooldown = 2f;
    private float lastAttackTime = 0f;

    private Rigidbody rb;

    void Start()
    {
        swordAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (player != null)
        {
            // Calculate the distance to the player
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Check if the player is within agro distance
            if (distanceToPlayer <= agroDistance)
            {
                swordAnimator.SetBool("Inrange", true);
                if (distanceToPlayer < stoppingDistance * 1.5f && canDodge)
                {
                    Dodge();
                }

                if (distanceToPlayer > stoppingDistance)
                {
                    Vector3 direction = (player.position - transform.position).normalized;

                    rb.MovePosition(transform.position + direction * speed * Time.deltaTime);

                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

                    swordAnimator.SetBool("IsAttacking", false); // Stop attack when moving
                }
                else
                {
                    if (Time.time >= lastAttackTime + attackCooldown)
                    {
                        swordAnimator.SetBool("IsAttacking", true);
                        attackType = Random.Range(0, 2); // Randomize attack type
                        swordAnimator.SetInteger("AttackType", attackType);

                        // Get the current attack animation length
                        float attackAnimationLength = GetAttackAnimationLength();

                        // Stop the attack after the animation finishes
                        Invoke(nameof(StopAttack), attackAnimationLength);

                        // Update last attack time
                        lastAttackTime = Time.time;
                    }
                }
            }
            else
            {
                // If the player is out of agro distance, idle
                swordAnimator.SetBool("IsAttacking", false);
                swordAnimator.SetBool("Inrange", false);
            }
        }
    }

    float GetAttackAnimationLength()
    {
        // Get the current animation state info
        AnimatorStateInfo stateInfo = swordAnimator.GetCurrentAnimatorStateInfo(0);

        // Return the length of the animation
        return stateInfo.length;
    }

    void StopAttack()
    {
        swordAnimator.SetInteger("AttackType", 5); // Reset attack type (optional)
    }

    void Dodge()
    {
        if (!canDodge) return;

        canDodge = false;
        Vector3 dodgeDirection;
        int randomDirection = Random.Range(0, 3);
        if (randomDirection == 0) dodgeDirection = -transform.right;
        else if (randomDirection == 1) dodgeDirection = transform.right;
        else dodgeDirection = transform.forward;

        Vector3 dodgeTarget = transform.position + dodgeDirection * dodgeDistance;

        StartCoroutine(DodgeMovement(dodgeTarget));
    }

    IEnumerator DodgeMovement(Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        float dodgeDuration = dodgeDistance / dodgeSpeed;
        Vector3 startPosition = transform.position;

        while (elapsedTime < dodgeDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / dodgeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;

        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = lookRotation;

        Invoke(nameof(ResetDodge), dodgeCooldown);
    }

    void ResetDodge()
    {
        canDodge = true;
    }

    public void changeDie()
    {
        canDie = true;
        StartCoroutine("revive");
        swordAnimator.SetBool("Parry", true);
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
        transform.GetChild(0).GetComponent<EnemyController>().revive();
        swordAnimator.SetBool("Parry", false);
    }
}
