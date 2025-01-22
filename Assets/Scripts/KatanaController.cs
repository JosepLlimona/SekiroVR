using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Katana trigger detecta algo");
        if (other.tag == "EnemySword")
        {
            Debug.Log("Katana parry");
            other.GetComponent<EnemyController>().Parry();
        }
        if(other.tag == "Enemy")
        {
            if(other.GetComponent<Enemy>() == null)
            {
                other.GetComponent<NormalEnemy>().Kill();
            }
            else
            {
                other.GetComponent<Enemy>().Kill();
            }
        }
    }
}
