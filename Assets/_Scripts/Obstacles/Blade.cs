using UnityEngine;
using DG.Tweening;

public class Blade : Obstacle
{
    [SerializeField] Ease easeType;
    private void Start()
    {
        transform.DORotate(new Vector3(0, 0, -90), animDuration).SetLoops(-1, LoopType.Yoyo).SetEase(easeType);
    }
}
