using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    private bool _isPaused = false;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchPauseMenu();
        }
    }
    public void ReturnToMainMenu()
    {
        LevelManager.Instance.StartScene("main_menu");
    }
    public void ReturnButton()
    {
        SwitchPauseMenu();
    }
    private void SwitchPauseMenu()
    {
        if(_isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        _panel.SetActive(_isPaused);
        _isPaused = !_isPaused;
    }
}
