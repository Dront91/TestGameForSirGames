using UnityEngine.UI;
using UnityEngine;

public class LoseWinScreen : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Text _text;
    private void Start()
    {
        LevelController.Instance.OnPlayerWin += OnPlayerWin;
        LevelController.Instance.OnPlayerLose += OnPlayerLose;
        _panel.SetActive(false);
    }
    private void OnDestroy()
    {
        LevelController.Instance.OnPlayerWin -= OnPlayerWin;
        LevelController.Instance.OnPlayerLose -= OnPlayerLose;
    }

    private void OnPlayerWin()
    {
        _panel.SetActive(true);
        _text.text = "Congratulations you passed the level, and earn: " + LevelController.Instance.CoinCount + " coins";
    }

    private void OnPlayerLose()
    {
        _panel.SetActive(true);
        _text.text = "You're dead, try again";
    }
    public void Restart()
    {
        LevelManager.Instance.StartScene("Level_01");
    }
    public void ReturnToMainMenu()
    {
        LevelManager.Instance.StartScene("main_menu");
    }
}
