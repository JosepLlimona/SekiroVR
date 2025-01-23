using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Image barravida;

    public float vidaActual;
    public void actualitzarVida(int vida){
        barravida.fillAmount = vida/100;
    }
}
