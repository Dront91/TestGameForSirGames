using UnityEngine.UI;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private Text _coinText;
    public void UpdateCoinUI()
    {
        _coinText.text = LevelController.Instance.CoinCount.ToString();
    }
}
