using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CUpward : CGameController {

    public Text step;
    public CBarrierContainer Barriers;
    public CMonkey monkey;

    public GameObject TabtoStart;
    public GameObject MenuPanel;

    void Start()
    {
        StartGame();
    }

    override public void StartGame()
    {
        curStep = 0;
        Barriers.ResetBarriers();
        monkey.curSpeed = 0;
        TabtoStart.SetActive(true);
    }

    public void run()
    {
        monkey.running = true;
    }

    public void EndGame()
    {
        monkey.running = false;
        Barriers.Stop();
        MenuPanel.SetActive(true);
    }

    private int _curStep;
    private int curStep
    {
        get { return _curStep; }
        set
        {
            _curStep = value;
            step.text = value.ToString();
        }
    }

    public void setStep(int step)
    {
        if (step != curStep)
        {
            curStep = step;
        }
    }

}
