using UnityEngine;
[CreateAssetMenu]
public class BaseSettings : ScriptableObject
{
    public int MaxHealth;
    public float MoveSpeed;
    public float RotateSpeed;
    public float SightDistance;
    public Weapon BaseWeapon;
    public GameObject ImpactEffect;
}
