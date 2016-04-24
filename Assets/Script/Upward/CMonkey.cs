using UnityEngine;
using System.Collections;

public class CMonkey : MonoBehaviour {

    public CUpward upwardGame;

    public float gravity;
    public float curSpeed;
    public float forceSpeed;
    public float maxSpeed;

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = transform as RectTransform;
    }

    public bool running = false;

    void Update()
    {
        if (running)
        {
            Vector2 position = rectTransform.anchoredPosition;
            curSpeed -= gravity;
            position.y += curSpeed;

            rectTransform.anchoredPosition = position;
        }
    }

    public void Jump()
    {
        if (curSpeed < 0)
        {
            curSpeed = forceSpeed;
        }
        else
        {
            curSpeed += forceSpeed;
        }
        if (curSpeed > maxSpeed) curSpeed = maxSpeed;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        upwardGame.EndGame();
    }
}
