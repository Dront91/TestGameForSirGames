using UnityEngine;

public class FinalArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.transform.TryGetComponent(out PlayerController player))
        {
            LevelController.Instance.PlayerWon();
        }
    }
}
