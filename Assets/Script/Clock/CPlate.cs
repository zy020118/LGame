using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CPlate : MonoBehaviour {


    public Image[] region;
    public int rotation;

    public void SetRegion(float[] regs)
    {
        for (int i = 1; i < regs.Length; ++i)
        {
            region[i-1].fillAmount = regs[i];
        }
    }

    public Color GetRegionColor(int index)
    {
        return region[index - 1].color;
    }
	
}
