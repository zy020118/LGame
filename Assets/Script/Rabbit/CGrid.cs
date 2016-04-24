using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CGrid : MonoBehaviour {

    public CMap map;

    public int col;
    public int row;
    public int parity;

    public Image img;
    public Sprite yellow;
    public Text stepText;
    
    void Start()
    {   
    }

    public void setPos(int i, int j)
    {
        col = i;
        row = j;
        parity = j % 2;

        if (i < j )
        {
            initStep(i, j);
        }
        else
        {
            initStep(j, i);
        }


        RectTransform rectTransform = transform as RectTransform;
        Vector2 position = rectTransform.anchoredPosition;
        position.x = i * 74 + 60 + parity* 39;
        position.y = j*65 + 260;
        rectTransform.anchoredPosition = position;
    }

    public void setParent(RectTransform parent)
    {
        RectTransform rectTransform = transform as RectTransform;
        rectTransform.SetParent(parent, false);
    }

    private int _minStep;
    public int srcNum; 
    public int minStep
    {
        get
        {
            return _minStep;
        }
        set
        {
            _minStep = value;
            stepText.text = value.ToString();
        }
    }

    private void initStep(int i, int j)
    {
        int step = i + 1;
        if (9 - j < step)
            step = 9 - j;

        minStep = step;
    }

    private bool selected = false;
    public void select()
    {
        if (selected == true)
            return;

        img.sprite = yellow;

        map.SelectGrid(col, row);

        selected = true;
    }

}
