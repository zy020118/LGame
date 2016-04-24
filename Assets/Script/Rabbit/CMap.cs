using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CMap : CGameController
{

    public CGrid gridBtn;
    public int Col;
    public int Row;
    public Text step;
    public Rabbit rabbit;
    public int startCol;
    public int startRow;
    public GameObject MenuPanel;

    private CGrid[,] gridMap = null;

    private int _useStep = 0;
    private int useStep
    {
        get {
            return _useStep;
        }
        set{
            _useStep = value;
            step.text = _useStep.ToString();
        }
    }

    void Start()
    {
        StartGame();
    }

    override public void StartGame()
    {
        InitGame();
    }

    public void EndGame()
    {
        MenuPanel.SetActive(true);
    }

    public CGrid GetNextGrid(CGrid grid)
    {
        CGrid nextGrid = null;
        if (grid)
        {
            int minStep = -1;
            for (int index = 0; index < 6; ++index)
            {
                CGrid neighborGrid = GetNeighbor(grid, index);
                if (neighborGrid && neighborGrid.minStep > 0)
                {

                    if (minStep == -1 || neighborGrid.minStep < minStep)
                    {
                        minStep = neighborGrid.minStep;
                        nextGrid = neighborGrid;
                    }
                }
            }

        }
        return nextGrid;
    }

    void InitGame()
    {
        rabbit.StopMove();
        InitMap();
        rabbit.InitPos(gridMap[startCol, startRow]);
        useStep = 0;
    }

    void InitMap()
    {
        if (gridMap == null)
        {
            gridMap = new CGrid[Col, Row];
        }
        foreach (CGrid grid in gridMap)
        {
            if (grid) GameObject.Destroy(grid.gameObject);
        }

        RectTransform rectTransform = transform as RectTransform;
        for (int i = 0; i < Col; ++i)
        {
            for (int j = 0; j < Row; ++j)
            {
                CGrid grid = GameObject.Instantiate(gridBtn) as CGrid;
                grid.map = this;
                grid.setPos(i, j);
                grid.setParent(rectTransform);
                gridMap[i, j] = grid;
            }
        }

        for (int i = 0; i < Col; ++i)
        {
            for (int j = 0; j < Row; ++j)
            {
                if (Random.value < 0.1 && !(i == startCol && j == startRow))
                {
                    gridMap[i, j].select();
                }
            }
        }
    }

    Queue changeQueue = new Queue();

    public void SelectGrid(int i, int j)
    {
        CGrid grid = gridMap[i, j];
        if (grid == null)
        {
            return;
        }
        int lastStep = grid.minStep;

        grid.minStep = -1;

        if (lastStep >= 1)
        {
            changeQueue.Clear();
            for (int index = 0; index < 6; ++index)
            {
                CGrid neighborGrid = GetNeighbor(grid, index);
                if (neighborGrid && neighborGrid.minStep > lastStep)
                {
                    changeQueue.Enqueue(neighborGrid);
                }
            }
            UpdateStep();
        }
        useStep++;
        rabbit.Run();
    }

    private void UpdateStep()
    {

        while (changeQueue.Count > 0)
        {
            CGrid grid = changeQueue.Dequeue() as CGrid;
            if (grid)
            {
                int curStep = grid.minStep;
                int newStep = -1;
                for(int index = 0; index < 6; ++index)
                {
                    CGrid neighborGrid = GetNeighbor(grid, index);
                    if (neighborGrid && neighborGrid.minStep > 0)
                    {
                        if(neighborGrid.minStep <= curStep)
                        {
                            if (newStep == -1 || neighborGrid.minStep < newStep)
                            {
                                newStep = neighborGrid.minStep;
                            }
                        }
                    }
                }
                if (newStep == -1) newStep = 100;
                newStep++;
                if (newStep > curStep)
                {
                    grid.minStep = newStep;
                    for (int index = 0; index < 6; ++index)
                    {
                        CGrid neighborGrid = GetNeighbor(grid, index);
                        if (neighborGrid && neighborGrid.minStep > curStep)
                        {
                            changeQueue.Enqueue(neighborGrid);
                        }
                    }
                }
            }
        }
    }


    private CGrid GetNeighbor(CGrid grid, int index)
    {
        if (index < 0 || index > 5) return null;

        int col = grid.col;
        int row = grid.row;
        switch(index)
        {
            case 0:
                {
                    col--; 
                }
                break;
            case 1:
                {
                    col -= 1 - grid.parity;
                    row++;
                }
                break;
            case 2:
                {
                    col += grid.parity;
                    row++;
                }
                break;
            case 3:
                {
                    col++;
                }
                break;
            case 4:
                {
                    col += grid.parity;
                    row--;
                }
                break;
            case 5:
                {
                    col -= 1 - grid.parity;
                    row--;
                }
                break;
            default:
                return null;
        }
        if (col < 0 || col >= Col) return null;
        if (row < 0 || row >= Row) return null;
        return gridMap[col, row];
    }

}
