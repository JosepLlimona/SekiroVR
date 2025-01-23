using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerController : MonoBehaviour
{

    int potions = 3;
    int health = 100;

    [SerializeField] ContinuousMoveProviderBase cmp;
    [SerializeField] ContinuousTurnProviderBase ctp;
    [SerializeField] TeleportationProvider tpp;
    [SerializeField] SnapTurnProviderBase stp;


    public void GetHurt(int hurt)
    {
        health -= hurt;
        if(health <= 0)
        {
            Debug.Log("Death");
        }
    }

    public void Heal()
    {
        if (potions > 0)
        {
            potions--;
            health += 50;
            Debug.Log("Curat: Actual Life: " + health);
            if(health >= 100)
            {
                health = 100;
            }
        }
    }

    public void Campfire()
    {
        potions = 3;
        health = 100;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemySword")
        {
            GetHurt(5);
        }
    }

    public void changeToTP()
    {
        Debug.Log("Canviant");
        cmp.enabled = false;
        ctp.enabled = false;
        tpp.enabled = true;
        stp.enabled = true;
    }

    public void changeToCont()
    {
        Debug.Log("Canviant");
        cmp.enabled = true;
        ctp.enabled = true;
        tpp.enabled = false;
        stp.enabled = false;
    }
}
