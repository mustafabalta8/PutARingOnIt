using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            UIManager.instance.OpenFinishPanel();
            DOTween.KillAll();
        }
    }
}
