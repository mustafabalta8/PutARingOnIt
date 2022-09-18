using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    public static State GameState;

    public override void Awake()
    {
        base.Awake();
        GameState = State.Menu;
    }

    public void StartGame()
    {
        GameState = State.InGame;
        UIManager.instance.StartUI.SetActive(false);
    }

    public IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

public enum State
{
    Menu,
    InGame,
    Collision
}
