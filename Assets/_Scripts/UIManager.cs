using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] GameObject startUI;
    [SerializeField] GameObject finishUI;

    public GameObject StartUI { get => startUI; }
    public GameObject FinishUI { get => finishUI; }

    public void OpenFinishPanel()
    {
        finishUI.SetActive(true);
        StartCoroutine(GameManager.instance.RestartLevel());
    }
    
}
