using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    int numEnemies = 6;
    [SerializeField] GameObject boss;
    public void UpdateEnemies()
    {
        numEnemies--;

        if (numEnemies <= 0)
        {
            boss.SetActive(true);
        }
    }
}
