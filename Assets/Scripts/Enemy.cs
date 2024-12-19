using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool canDie = false;

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
