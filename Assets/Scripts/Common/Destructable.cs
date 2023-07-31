using System;
using UnityEngine;
using UnityEngine.UI;

public class Destructable : MonoBehaviour
{
    [SerializeField] private Text _healthTextUI;
    [SerializeField] private Image _healthBarImage;
    private int _currentHealth;
    public Action<Destructable> OnDeath;
    public Action OnDamageTaken;
    private int _maxHealth;
    public void SetCurrentHealth(int health)
    {
        _currentHealth = health;
        _maxHealth = health;
        UpdateHealthUI();
    }
    public void TakeDamage(int damage)
    {
        OnDamageTaken?.Invoke();
        _currentHealth -= damage;
        UpdateHealthUI();
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
        OnDeath?.Invoke(this);
    }
    private void UpdateHealthUI()
    {
        _healthBarImage.fillAmount = (float)(((float)_currentHealth * 100 / (float)_maxHealth) / 100);
        _healthTextUI.text = _currentHealth + "/" + _maxHealth;
    }

}
