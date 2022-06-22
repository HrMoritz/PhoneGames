using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DrenchedGameManager : MonoBehaviour
{
    public static DrenchedGameManager instance;
    public List<Color> colors;

    public List<DrenchedTile> owned = new List<DrenchedTile>();
    public Image[] buttons;
    private void Awake()
    {
        instance = this;

        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].color = colors[i];
        }
    }

    public void SetNeighbours(int color)
    {
        List<DrenchedTile> newTiles = new List<DrenchedTile>();

        foreach (DrenchedTile dt in owned)
        {
            dt.UpdateColor(color);
            List<DrenchedTile> neighbours = DrenchedGridCreator.instance.GetNeighbours(dt);
            foreach(DrenchedTile n in neighbours)
            {
                if(!owned.Contains(n) && n.color == color && !newTiles.Contains(n))
                {
                    newTiles.Add(n);
                    print("test");
                }
            }
        }

        while (newTiles.Count > 0)
        {
            List<DrenchedTile> copy = new List<DrenchedTile>();
            foreach (DrenchedTile dt in newTiles)
            {
                copy.Add(dt);
                owned.Add(dt);
                dt.UpdateColor(color);
            }
            newTiles.Clear();

            foreach (DrenchedTile dt in copy)
            {
                List<DrenchedTile> neighbours = DrenchedGridCreator.instance.GetNeighbours(dt);
                foreach (DrenchedTile n in neighbours)
                {
                    if (!owned.Contains(n) && n.color == color && !newTiles.Contains(n))
                    {
                        newTiles.Add(n);
                    }
                }
            }
        }
    }
}
