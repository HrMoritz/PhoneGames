using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DrenchedGameManager : MonoBehaviour
{
    public static DrenchedGameManager instance;
    public List<Color> colors;

    public List<DrenchedTile> owned = new List<DrenchedTile>();
    public Image[] buttons;

    public TMP_Text triesText;
    public int tries;

    public GameObject loosePanel;
    public GameObject winPanel;

    private void Awake()
    {
        instance = this;
        triesText.text = tries.ToString();

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].color = colors[i];
        }
    }

    public void SetNeighbours(int color)
    {
        tries--;
        triesText.text = tries.ToString();

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

        if (CheckWin())
        {
            winPanel.SetActive(true);
        }
        else if(tries == 0)
        {
            loosePanel.SetActive(true);
        }
    }

    private bool CheckWin()
    {
        return owned.Count == DrenchedGridCreator.instance.gridSize * DrenchedGridCreator.instance.gridSize;
    }
}
