using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    int potions = 3;
    int health = 100;

    [SerializeField] ContinuousMoveProviderBase cmp;
    [SerializeField] ContinuousTurnProviderBase ctp;
    [SerializeField] TeleportationProvider tpp;
    [SerializeField] SnapTurnProviderBase stp;
    [SerializeField] TeleportationArea tpa;

    private BarraVida barraVida;
    private Curacions curacions;

    public void GetHurt(int hurt)
    {
        health -= hurt;
        Debug.Log("Hurting: " + health);
        barraVida.actualitzarVida(health);
        if(health <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void Heal(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (potions > 0)
            {
                potions--;
                curacions.updateCuracions(potions);
                health += 50;
                barraVida.actualitzarVida(health);
                Debug.Log("Curat: Actual Life: " + health);
                if (health >= 100)
                {
                    health = 100;
                }
            }
        }
    }

    public void Campfire()
    {
        potions = 3;
        health = 100;
        curacions.updateCuracions(potions);
        barraVida.actualitzarVida(health);
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
        tpa.enabled = true;
    }

    public void changeToCont()
    {
        Debug.Log("Canviant");
        cmp.enabled = true;
        ctp.enabled = true;
        tpp.enabled = false;
        stp.enabled = false;
        tpa.enabled = false;
    }
    public void Start()
    {
        barraVida = GetComponent<BarraVida>();
        curacions = GetComponent<Curacions>();
    }
}
