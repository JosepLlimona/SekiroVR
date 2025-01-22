using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaController : MonoBehaviour
{

    [SerializeField] Haptic h;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Katana trigger detecta algo");
        if (other.tag == "EnemySword")
        {
            h.HapticImpulse(Haptic.Contol.right, 0.5f, 0.5f);
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
