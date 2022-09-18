using UnityEngine;
using DG.Tweening;

public class Razor : Obstacle
{
    private void Start()
    {
        transform.DORotate(new Vector3(0, 0, 360), animDuration, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }
}
