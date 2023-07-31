using UnityEngine;

public class ImpactEffect : MonoBehaviour
{
    [SerializeField] private float _lifetime;
    private float m_Timer;


    void Update()
    {
        if (m_Timer < _lifetime)
            m_Timer += Time.deltaTime;
        else
            Destroy(gameObject);
    }
}
