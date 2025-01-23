using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Curacions : MonoBehaviour
{
    public Image curacions;

    public Sprite c0;
    public Sprite c1;
    public Sprite c2;
    public Sprite c3;
    public void updateCuracions(int nCuracions)
    {
        if(nCuracions == 0) curacions.GetComponent<Image>().sprite = c0;
        if(nCuracions == 1) curacions.GetComponent<Image>().sprite = c1;
        if(nCuracions == 2) curacions.GetComponent<Image>().sprite = c2;
        if(nCuracions == 3) curacions.GetComponent<Image>().sprite = c3;
    }
}
