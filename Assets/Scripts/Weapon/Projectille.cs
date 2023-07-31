using UnityEngine;

public class Projectille : MonoBehaviour
{
    [SerializeField] private int _projectilleBaseDamage;
    [SerializeField] private float _projectilleSpeed;
    [SerializeField] private GameObject _visualModel;
    private int _finalDamage;
    private GameObject _parent;
    private Vector3 _direction;
    public void SetStartSettings(int weaponDamage, Vector3 direction, GameObject parent)
    {
        _finalDamage = _projectilleBaseDamage + weaponDamage;
        _parent = parent;
        _visualModel.transform.up = direction;
        _direction = direction;
    }
    private void Update()
    {
        var translation = _direction * _projectilleSpeed * Time.deltaTime;
        transform.Translate(translation);
        var hit = Physics2D.Raycast(transform.position, transform.up, translation.magnitude);
        if(hit)
        {
            if(hit.collider.gameObject == _parent) { return; }
            if(hit.collider.transform.TryGetComponent(out Destructable des))
            {
                des.TakeDamage(_finalDamage);
            }
            DestroyProjectille();
        }
    }
    private void DestroyProjectille()
    {
        Destroy(gameObject);
    }
}
