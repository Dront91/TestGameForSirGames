using UnityEngine;

[RequireComponent(typeof(Destructable))]
public class EnemyController : BaseController
{
    private enum BehaviourType
    {
        StopAndFire,
        FindNewPosition,
        MoveToNextPosition
    }
    private BehaviourType _currentBehaviour = BehaviourType.StopAndFire;
    private float _standTimer;
    private Vector3 _newPosition;
    private float _moveOffset = 0.1f;
    private LayerMask _mask = 3;
    private float _stopFromWallOffset = 0.05f;
    private CircleCollider2D _collider;
    private int _borderLayer = 6;
    protected override void Awake()
    {
        base.Awake();
        _collider = GetComponent<CircleCollider2D>();
        _destructable.OnDeath += OnEnemyDie;
        if((_settings as EnemySettings).EnemyType == EnemyType.Fly)
        {
            if(TryGetComponent(out Rigidbody2D rigid))
            {
                Destroy(rigid);
            }
        }
    }
    protected override void Update()
    {
        base.Update();
        CheckStandTimer();
        if(_currentBehaviour == BehaviourType.StopAndFire)
        {
            _standTimer += Time.deltaTime;
            FindTarget();
            RotateAndAttack();
        }
        if(_currentBehaviour == BehaviourType.FindNewPosition)
        {
            FindNewPosition();
        }
        if(_currentBehaviour == BehaviourType.MoveToNextPosition)
        {
            MoveToNewPosition();
        }
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        _destructable.OnDeath -= OnEnemyDie;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out PlayerController player))
        {
            player.GetComponent<Destructable>().TakeDamage((_settings as EnemySettings).BaseDamageOnCollision);
        }
    }
    protected override void FindTarget()
    {
        if (_target == null)
        {
            var targets = Physics2D.OverlapCircleAll(transform.position, _settings.SightDistance);
            foreach (var target in targets)
            {
                if (target.TryGetComponent(out PlayerController player))
                {
                    _target = player.gameObject;
                }
            }
        }
    }
    
    private void FindNewPosition()
    {
        if(_target != null)
        {
            bool isPointFound = false;
            var newpos = transform.position + (_target.transform.position - transform.position).normalized * (_settings as EnemySettings).MovementDistance;
            if (CheckNewPoint(newpos))
            {
                _newPosition = newpos;
                isPointFound = true;
            }
            int iterator = 0;
            while(isPointFound == false)
            {
                iterator++;
                var newpos1 = (Vector2)transform.position + Random.insideUnitCircle * (_settings as EnemySettings).MovementDistance;
                if(CheckNewPoint(newpos1))
                {
                    _newPosition = newpos1;
                    isPointFound = true;
                }
                if(iterator >= 500)
                {
                    _newPosition = transform.position;
                    isPointFound = true;
                }
            }
        }
        else
        {
            _newPosition = transform.position;
        }
        _currentBehaviour = BehaviourType.MoveToNextPosition;
    }
    private bool CheckNewPoint(Vector3 point)
    {
        var hit = Physics2D.Raycast(transform.position, point - transform.position, (point - transform.position).magnitude, _mask);
        if (hit)
        {
            return false;
        }
        else
        { 
            return true; 
        }
    }
    private void MoveToNewPosition()
    {
        var translation = (_newPosition - transform.position) * _settings.MoveSpeed * Time.deltaTime;
        if(Vector3.Distance(transform.position, _newPosition) < _moveOffset)
        {
            _currentBehaviour = BehaviourType.StopAndFire;
        }
        else
        {
            transform.Translate(translation);
        }
        var hits = Physics2D.OverlapCircleAll(transform.position, _collider.radius + _stopFromWallOffset);
        foreach(var e in hits)
        {
            if (e != _collider && (_settings as EnemySettings).EnemyType == EnemyType.Ground)
            {
                _currentBehaviour = BehaviourType.StopAndFire;
            }
            if((_settings as EnemySettings).EnemyType == EnemyType.Fly && e.gameObject.layer == _borderLayer)
            {
                _currentBehaviour = BehaviourType.StopAndFire;
            }
        }
    }
    private void CheckStandTimer()
    {
        if(_standTimer >= (_settings as EnemySettings).StandTime)
        {
            _standTimer = 0;
            _currentBehaviour = BehaviourType.FindNewPosition;
        }
    }
    private void OnEnemyDie(Destructable des)
    {
        LevelController.Instance.AddCoin((_settings as EnemySettings).CoinCount);
    }
}
