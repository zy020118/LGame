using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Rabbit : MonoBehaviour {

    public CMap map;
    public CGrid curGrid = null;

    private Animation anim;


    void Start()
    {
        anim = GetComponent<Animation>();
    }

    public void StopMove()
    {
        curGrid = null;
    }

    public void InitPos(CGrid grid)
    {
        if (grid)
        {
            curGrid = grid;
            RectTransform rectTransform = transform as RectTransform;
            rectTransform.anchoredPosition = (grid.transform as RectTransform).anchoredPosition;
        }
    }

    public void Run()
    {
        if (!curGrid)
            return;

        CGrid grid = map.GetNextGrid(curGrid);
        if (grid)
        {
            curGrid = grid;

            Tweener jumpTween = ShortcutExtensions46.DOAnchorPos(transform as RectTransform, (grid.transform as RectTransform).anchoredPosition, 1);
            jumpTween.SetEase(Ease.InOutQuint);
            anim.Play("jump");

            if (curGrid.minStep == 1)
            {
                map.EndGame();
            }
        }
        else
        {
            map.EndGame();
        }
    }

    public void Stay()
    {
        anim.Play("leap");
    }
}
