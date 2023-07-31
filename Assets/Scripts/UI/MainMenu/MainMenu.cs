using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private const string _level_01 = "Level_01";
    public void StartNewGame()
    {
        LevelManager.Instance.StartScene(_level_01);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
