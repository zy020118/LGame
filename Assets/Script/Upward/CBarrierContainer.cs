using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CBarrierContainer : MonoBehaviour {

    public CUpward upward;
    public CMonkey monkey;
    public CBarrier[] BarrierPrefab;
    public float barrierSpace;
    public float moveSpeed;

    private bool running = false;
    private RectTransform monkeyRectTransform;
    private RectTransform rectTransform;

	void Start () {
        rectTransform = transform as RectTransform;
        monkeyRectTransform = monkey.transform as RectTransform;
	}

    private Queue<CBarrier> Barriers = new Queue<CBarrier>();
    public float firstPos = 667f;
    public float nextPos = 667f;

    public void ResetBarriers()
    {
        rectTransform.anchoredPosition = Vector2.zero;
        monkeyRectTransform.anchoredPosition = new Vector2(375f, 200f);
        firstPos = 667f;
        nextPos = 667f;

        while (Barriers.Count > 0)
        {
            CBarrier barrier = Barriers.Dequeue();
            Destroy(barrier.gameObject);
        }

        UpdateBarriers(0);
        running = true;
    }

    public void Stop()
    {
        running = false;
    }

    void Update () {
        if(running)
        {
            float monkeyPos = monkeyRectTransform.anchoredPosition.y;
            float curPosY = rectTransform.anchoredPosition.y;

            if (monkeyPos + curPosY > 667)
            {
                curPosY = 667 - monkeyPos;
                rectTransform.anchoredPosition = new Vector2(0, curPosY);

                upward.setStep((int)(monkeyPos / barrierSpace));
            }
            UpdateBarriers(curPosY);
        }
    }


    void UpdateBarriers(float curPosY)
    {

        while (firstPos + curPosY <  -400)
        {
            CBarrier barrier = Barriers.Dequeue();
            Destroy(barrier.gameObject);
            firstPos += barrierSpace;
        }

        while (nextPos + curPosY < 1750)
        {
            int rand = Random.Range(0, BarrierPrefab.Length);

            CBarrier barrier = CBarrier.Instantiate(BarrierPrefab[rand]);
            RectTransform childBarrier = barrier.transform as RectTransform;
            childBarrier.SetParent(rectTransform, false);
            childBarrier.anchoredPosition = new Vector2(375f, nextPos);

            Barriers.Enqueue(barrier);

            nextPos += barrierSpace;
        }
    }

    public void moveDown(float distance)
    {
        Vector2 position = rectTransform.anchoredPosition;
        position.y -= distance;
        rectTransform.anchoredPosition = position;
    }
}
