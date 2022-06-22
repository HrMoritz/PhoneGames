using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DrenchedTile : MonoBehaviour
{
    public int color;
    public int x, y;

    public void UpdateColor(int c)
    {
        print(c);
        GetComponent<Image>().color = DrenchedGameManager.instance.colors[c];
        color = c;
    }
}
