using UnityEngine;
using DG.Tweening;

public class HandObstacle : Obstacle
{
    [SerializeField] float xMove = 5f;
    [SerializeField] Ease easeType;
    private void Start()
    {
        transform.DOMoveX(transform.position.x + xMove, animDuration).SetLoops(-1, LoopType.Yoyo).SetEase(easeType);
    }
}
