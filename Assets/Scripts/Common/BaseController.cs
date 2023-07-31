using UnityEngine;
[RequireComponent(typeof(Destructable))]
public class BaseController : MonoBehaviour
{
    [SerializeField] protected BaseSettings _settings;
    [SerializeField] protected GameObject _visualModel;
    protected Destructable _destructable;
    protected Weapon _currentWeapon;
    protected GameObject _target;
    protected float _fireRateTimer;
    protected float _offset = 1;
    protected virtual void Awake()
    {
        _destructable = GetComponent<Destructable>();
        LevelController.Instance.OnGameStart += OnGameStart;
        LevelController.Instance.OnGameEnd += OnGameEnd;
        _destructable.OnDamageTaken += OnDamageTaken;
    }
    protected virtual void Start()
    {
        SetSettings();
    }
    protected virtual void Update()
    {
        _fireRateTimer += Time.deltaTime;
    }

    protected virtual void OnDestroy()
    {
        LevelController.Instance.OnGameStart -= OnGameStart;
        LevelController.Instance.OnGameEnd -= OnGameEnd;
        _destructable.OnDamageTaken -= OnDamageTaken;
    }
    protected virtual void SetSettings()
    {
        _destructable.SetCurrentHealth(_settings.MaxHealth);
        _currentWeapon = _settings.BaseWeapon;
    }
    protected virtual void RotateAndAttack()
    {
        if (_target == null)
        {
            FindTarget();
        }
        if (_target != null)
        {
            _visualModel.transform.up = Vector3.Lerp(_visualModel.transform.up, _target.transform.position - transform.position, _settings.RotateSpeed * Time.deltaTime);

            if (_fireRateTimer >= _currentWeapon.FireRate)
            {
                _fireRateTimer = 0;
                var direction = (_target.transform.position - transform.position).normalized;
                var e = Instantiate(_currentWeapon.Projectille, transform.position + _offset * direction, Quaternion.identity);
                e.SetStartSettings(_currentWeapon.Damage, direction, gameObject);
            }
        }
    }
    protected virtual void FindTarget()
    {

    }
    protected virtual void OnGameStart()
    {
        enabled = true;
    }
    protected virtual void OnGameEnd()
    {
        Destroy(this);
    }
    private void OnDamageTaken()
    {
        if(_settings.ImpactEffect != null)
        {
            Instantiate(_settings.ImpactEffect, transform.position, Quaternion.identity);
        }
    }
}
