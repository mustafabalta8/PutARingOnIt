using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class StakeManager : MonoBehaviour
{
    [SerializeField] List<Gem> gems;

    [Header("Gem Settings")]
    //[SerializeField] private float gemHeight = 0.3f;

    [SerializeField] private float punchScaleMultiplier = 1.1f;
    [SerializeField] private float punchScaleDuration = 0.65f;
    [SerializeField] private int vibrato = 8;
    [SerializeField] [Range(0, 1)] private float elasticity = 1;

    [SerializeField] private float scaleDuration = 0.65f;

    private Transform gemHolder;
    [SerializeField] private Transform previousObj;

    private Transform firstPlaceOnStake;
    private void Start()
    {
        gemHolder = transform.GetChild(1);
        firstPlaceOnStake= gemHolder.GetChild(0);
        previousObj = firstPlaceOnStake;

        scale = Vector3.one;
    }

    Vector3 scale;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Gem>())
        {
            AddGemToTheStack(other);
        }
        else if(other.GetComponent<Door>())
        {
            CombineGems(other);
        }
        else if(other.GetComponent<Obstacle>())
        {
            CollideWithObstacle();
        }

    }

    private void CollideWithObstacle()
    {
        Player.instance.CollideWithAnObstacle();

        if (gems.Count > 0)
        {
            int randomNum = Random.Range(1, 4);
            if (randomNum > gems.Count) { randomNum = 1; }

            for (int i = randomNum - 1; i >= 0; i--)
            {
                int gemToThrow = gems.Count - 1;

                gems[gemToThrow].PlayCollisionAnimation();
                gems[gemToThrow].transform.parent = null;
                gems.RemoveAt(gemToThrow);

            }
            if (gems.Count > 0)
                previousObj = gems[gems.Count - 1].transform;
            else
                previousObj = firstPlaceOnStake;
        }
    }

    private void CombineGems(Collider other)
    {
        if (gems.Count > 0) 
        {
            for (int i = gems.Count - 1; i > 0; i--)
            {
                //print("i " + i);
                if (i - 1 >= 0)
                    if (gems[i].GemType == gems[i - 1].GemType)
                    {
                        //DOTween.KillAll();//bunu eklemezsem alt satýrdaki destroy'dan dolayý hata alýrým. 
                        scale = gems[i].transform.localScale + new Vector3(0.15f, 0.15f, 0.15f);
                        Destroy(gems[i].gameObject);

                        gems[i - 1].transform.localScale = scale;

                        //gems[i-1].transform.DOScale(scale + new Vector3(0.15f, 0.15f, 0.15f), scaleDuration);

                        //print(scale + " scale is ");
                        gems.RemoveAt(i);
                        //previousObj = gems[i-1].transform;

                    }


            }
            other.GetComponent<Door>().enabled = false;
            SortGemsByHeight();
        }

        
    }
    Tween tween;
    private void AddGemToTheStack(Collider other)
    {
        var gem = other.GetComponent<Gem>();

        gem.StopGemAnimation();

        other.transform.parent = gemHolder;
        gems.Add(gem);
        other.transform.GetComponent<Collider>().enabled = false;

        Vector3 position = previousObj.localPosition;// gems[gems.Count - 2].transform.localPosition

        position.y += gems[gems.Count-1].GemHeight;

        //other.transform.DOLocalMove(position, 0.2f);
        other.transform.localPosition = position;

        previousObj = other.transform;

        if(!tween.IsActive())
        foreach (var item in gems)
        {
            tween= item.transform.DOPunchScale(Vector3.one * punchScaleMultiplier, punchScaleDuration, vibrato, elasticity);
        }
    }

    private void SortGemsByHeight()
    {    
        for (int i = 1; i < gems.Count; i++)
        {
            gems[i].transform.localPosition = gems[i - 1].transform.localPosition + new Vector3(0, gems[i - 1].GemHeight, 0);

            /* gems[i].transform.localPosition = gems[i - 1].transform.localPosition + 
                new Vector3(0, gems[i - 1].transform.localPosition.y * gems[i - 1].GemHeight, 0);*/
        }

        previousObj = gems[gems.Count-1].transform;
    }
}
