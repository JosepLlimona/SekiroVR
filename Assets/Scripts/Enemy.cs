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

    public float dodgeSpeed = 10f;  
    public float dodgeDistance = 3f; 
    public float dodgeCooldown = 2f; 
    private bool canDodge = true; 

    private Animator swordAnimator;
    private int attackType;

    public float attackCooldown = 2f;
    private float lastAttackTime = 0f;

    void Start()
    {
        swordAnimator = transform.Find("EnemySword").GetComponent<Animator>();
    }
    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (Vector3.Distance(transform.position, player.position) < stoppingDistance * 1.5f  && canDodge)
            {
                Dodge();
                Debug.Log("doge");
            }
            if (distanceToPlayer > stoppingDistance)
            {
                Vector3 direction = (player.position - transform.position).normalized;

                transform.position += direction * speed * Time.deltaTime;

                Quaternion lookRotation = Quaternion.LookRotation(-direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

                swordAnimator.SetBool("IsAttacking", false);
            }
            else
            {
                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    swordAnimator.SetBool("IsAttacking", true);
                    attackType = Random.Range(0, 2); // Randomize attack type
                    swordAnimator.SetInteger("AttackType", attackType);

                    Debug.Log("Attack: " + attackType);

                    Invoke(nameof(StopAttack), 1);
                    // Update last attack time
                    lastAttackTime = Time.time;
                }
            }
        }
    }
    void StopAttack()
    {
        swordAnimator.SetBool("IsAttacking", false);
        swordAnimator.SetInteger("AttackType", 5);
        Debug.Log("STOP Attack");
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
