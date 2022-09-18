using UnityEngine;
using DG.Tweening;
public class StableRoller : Obstacle
{
    private void Start()
    {
        transform.DORotate(new Vector3(360, 0, 0), animDuration, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }
}
