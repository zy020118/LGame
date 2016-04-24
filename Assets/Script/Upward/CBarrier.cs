using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class CBarrier : MonoBehaviour {

    public bool move;
    public RectTransform left;
    public RectTransform right;
    public float moveTime;

    public bool rotate;
    public RectTransform leftBody1;
    public RectTransform leftBody2;
    public RectTransform rightBody1;
    public RectTransform rightBody2;
    public float rotateTime;

	// Use this for initialization
	void Start () {
        if (move == true)
        {
            ShortcutExtensions46.DOAnchorPosX(left, 0, moveTime).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
            ShortcutExtensions46.DOAnchorPosX(right, 0, moveTime).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        }

        if (rotate == true)
        {
            Vector3 startRotation;

            if(leftBody1)
            {
                startRotation = leftBody1.rotation.eulerAngles;
                startRotation.z += 360f;
                ShortcutExtensions.DORotate(leftBody1, startRotation, rotateTime, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
            }
            if (leftBody2)
            {
                startRotation = leftBody2.rotation.eulerAngles;
                startRotation.z -= 360f;
                ShortcutExtensions.DORotate(leftBody2, startRotation, rotateTime, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
            }

            if (rightBody1)
            {
                startRotation = rightBody1.rotation.eulerAngles;
                startRotation.z += 360f;
                ShortcutExtensions.DORotate(rightBody1, startRotation, rotateTime, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
            }
            if (rightBody2)
            {
                startRotation = rightBody2.rotation.eulerAngles;
                startRotation.z -= 360f;
                ShortcutExtensions.DORotate(rightBody2, startRotation, rotateTime, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
