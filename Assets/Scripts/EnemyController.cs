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

    public void Parry()
    {
        if (posture > 0)
        {
            posture -= 10;
            Debug.Log(posture);
            //anim.SetTrigger("Reset");
            hit.Play();

            if (posture <= 0)
            {
                //anim.enabled = false;
                if (transform.root.gameObject.GetComponent<Enemy>() != null)
                {
                    transform.root.gameObject.GetComponent<Enemy>().changeDie();
                }
                else
                {
                    transform.root.gameObject.GetComponent<NormalEnemy>().changeDie(gameObject);
                }
            }
        }
    }

    public void revive()
    {
        //anim.enabled = true;
        posture = 100;
    }
}
