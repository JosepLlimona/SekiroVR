using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Image barravida;

    public float vidaActual;
    void Update()
    {
        barravida.fillAmount = vidaActual/100;
    }
}
