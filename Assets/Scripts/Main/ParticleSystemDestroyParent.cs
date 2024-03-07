using UnityEngine;

public class ParticleSystemDestroyParent : MonoBehaviour
{
    void OnParticleSystemStopped()
    {
        Destroy(transform.parent.gameObject);
    }
}
