using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    int numEnemies = 6;
    [SerializeField] GameObject boss;
    [SerializeField] GameObject win;

    public void UpdateEnemies()
    {
        numEnemies--;

        if (numEnemies <= 0)
        {
            boss.SetActive(true);
        }
    }

    public void KillBoss()
    {
        StartCoroutine(Win());
    }

    private IEnumerator Win()
    {
        win.SetActive(true);
        yield return new WaitForSeconds(3);
        Application.Quit();
    }
}
