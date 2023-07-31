using UnityEngine;
public enum EnemyType
{
    Ground,
    Fly
}
[CreateAssetMenu]
public class EnemySettings : BaseSettings
{
    public float StandTime;
    public float MovementDistance;
    public int BaseDamageOnCollision;
    public int CoinCount;
    public EnemyType EnemyType;
}
