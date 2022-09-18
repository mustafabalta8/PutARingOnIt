using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

public class Gem : MonoBehaviour
{
    [SerializeField] float gemHeight = 0.3f;
    [SerializeField] Gems gemType;

    [SerializeField] float animDuration = 5f;
    [SerializeField] float yMove = 1f;
    [SerializeField] Ease jumpEase;
    [SerializeField] float maxJump = 8;
    [SerializeField] float minJump = 2;
    public float GemHeight { get => gemHeight; }
    public Gems GemType { get => gemType; }

    Sequence sequence;
    private void Start()
    {
        sequence = DOTween.Sequence();
        sequence.Append(transform.DORotate(new Vector3(0, 360, 0), animDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental))
            .Join(transform.DOLocalMoveY(transform.localPosition.y + yMove, animDuration/2).SetLoops(2, LoopType.Yoyo));
        sequence.Play().SetLoops(-1);

    }

    public void StopGemAnimation()
    {
        sequence.Kill();
    }
    [Button]
    public void PlayCollisionAnimation()
    {
        Vector3 randVector = new Vector3(Random.Range(-maxJump, maxJump),
            Random.Range(minJump, maxJump), Random.Range(minJump, maxJump));
        transform.DOMove(transform.position + randVector, 1).SetEase(jumpEase)
            .OnComplete(PlayCollisionAnimationOnComplete);
    }

    private void PlayCollisionAnimationOnComplete()
    {
        gameObject.AddComponent<Rigidbody>();
        Destroy(gameObject, 0.5f);
    }
}
