using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CClock : CGameController
{

    public CPlate plate;
    public Image pointer;
    public Text step;

    public int speed;

    public GameObject MenuPanel;


    private float[] normal = {0, 0.25f, 0.5f, 0.75f, 1f};
    private float[] difficult = {0, 0.25f, 0.5f, 0.75f, 0.85f, 1f };

    private bool running = false;

    private float[] curRegion;
    private int targetRegion;
    private int curPos;

    private int minPos;
    private int maxPos;
    private bool inRegion;

    private int _curStep;
    private int curStep
    {
        get { return _curStep; }
        set {
            _curStep = value;
            step.text = value.ToString();
        }
    }
 
	void Start () {
        curRegion = normal;
        StartGame();
	}

	void Update () {
        if (running == false) return;

        curPos += speed;
        if (curPos > 360) curPos -= 360;
        if (curPos < 0) curPos += 360;

        pointer.transform.localRotation = Quaternion.AngleAxis(180 - curPos, Vector3.forward);

        if (curPos >= minPos && curPos <= maxPos)
        {
            inRegion = true;
        }
        else
        {
            if (inRegion == true)
            {
                EndGame();
            }
            inRegion = false;
        }

	}

    override public void StartGame()
    {
        curStep = 0;
        plate.SetRegion(curRegion);
        curPos = 180;
        targetRegion = -1;
        RandomPointer(1);
        running = true;
    }

    void EndGame()
    {
        running = false;
        MenuPanel.SetActive(true);
    }

    void NextStep()
    {
        curStep ++;
        speed *= -1;
        inRegion = false;
        RandomPointer();
    }

    public void Click()
    {
        if (inRegion == false)
        {
            EndGame();
        }
        else
        {
            NextStep();
        }
    }

    private void RandomPointer(int region = 0)
    {
        while (region <= 0 || targetRegion == region)
        {
            region = Random.Range(1, curRegion.Length);
        }
        targetRegion = region;

        pointer.color = plate.GetRegionColor(targetRegion);
        minPos = (int)(curRegion[targetRegion - 1] * 360);
        maxPos = (int)(curRegion[targetRegion] * 360);
    }
}
