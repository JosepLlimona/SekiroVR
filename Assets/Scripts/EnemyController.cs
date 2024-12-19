using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int posture = 100;
    private Animator anim;
    [SerializeField] private AudioSource hit;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Katana" && posture > 0)
        {
            posture -= 10;
            Debug.Log(posture);
            anim.SetTrigger("Reset");
            hit.Play();

            if(posture <= 0)
            {
                anim.enabled = false;
                transform.parent.gameObject.GetComponent<Enemy>().changeDie();
            }
        }
    }

    public void revive()
    {
        anim.enabled = true;
        posture = 100;
    }
}
