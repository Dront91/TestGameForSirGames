using UnityEngine;

[RequireComponent(typeof(Destructable))]
public class PlayerController : BaseController
{
    protected override void Update()
    {
        base.Update();
        if (VirtualJoystick.Instance.Value == Vector3.zero)
        {
            RotateAndAttack();
        }
        else
        {
            Move();
        }
    }
    private void Move()
    {
        _target = null;
        transform.Translate(VirtualJoystick.Instance.Value * _settings.MoveSpeed * Time.deltaTime);
        _visualModel.transform.up = Vector3.Lerp(_visualModel.transform.up, VirtualJoystick.Instance.Value, _settings.RotateSpeed * Time.fixedDeltaTime);
    }
    protected override void FindTarget()
    {
        var targets = Physics2D.OverlapCircleAll(transform.position, _settings.SightDistance);
        float distance = float.MaxValue;
        foreach( var target in targets)
        {
            if (target.TryGetComponent(out EnemyController enemy))
            {
                var distanceToTarget = Vector2.Distance(enemy.transform.position, transform.position);
                if(distanceToTarget < distance)
                {
                    distance = distanceToTarget;
                    _target = enemy.gameObject;
                }
            }
        }
    }
}
