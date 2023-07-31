using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image _progressBar;
    [SerializeField] private Text _progressText;
    private void Start()
    {
        _progressBar.fillAmount = 0;
        _progressText.text = "0%";
    }
    void Update()
    {
       _progressBar.fillAmount = Mathf.MoveTowards(_progressBar.fillAmount, LevelManager.Instance.Target, 2 * Time.deltaTime);
        _progressText.text = ((int)(LevelManager.Instance.Target * 100)).ToString() + "%";
    }
}
