using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrenchedGridCreator : MonoBehaviour
{
    public static DrenchedGridCreator instance;

    public GameObject tilePrefab;
    public Transform tileParent;
    public int gridSize;
    public GridLayoutGroup gridLayoutGroup;
    public DrenchedTile[,] grid;

    private void Awake()
    {
        instance = this;    
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateGrid()
    {
        grid = new DrenchedTile[gridSize, gridSize];
        float cellSize = tileParent.GetComponent<RectTransform>().rect.width / gridSize;
        gridLayoutGroup.cellSize = new Vector2(cellSize, cellSize);

        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                GameObject go = Instantiate(tilePrefab, tileParent);
                DrenchedTile dt = go.GetComponent<DrenchedTile>();
                grid[j, i] = dt;

                dt.x = j;
                dt.y = i;
                
                dt.UpdateColor(Random.Range(0, 6));
            }
        }

        DrenchedGameManager.instance.owned.Add(grid[0, 0]);
        DrenchedGameManager.instance.SetNeighbours(grid[0, 0].color);
    }

    public List<DrenchedTile> GetNeighbours(DrenchedTile DrenchedTile)
    {
        List<DrenchedTile> neighbours = new List<DrenchedTile>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if ((x == 0 && y == 0) || (x != 0 && y != 0))
                {
                    continue;
                }

                int checkX = DrenchedTile.x + x;
                int checkY = DrenchedTile.y + y;

                if (checkX >= 0 && checkX < gridSize && checkY >= 0 && checkY < gridSize)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }
}
