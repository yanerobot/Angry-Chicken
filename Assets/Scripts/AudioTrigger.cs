using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider2D))]
public class AudioTrigger : MonoBehaviour
{
    [SerializeField, Range(0,0.5f)] float pitchRandomizer;

    AudioSource src;

    void Awake()
    {
        src = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Rigidbody2D _))
        {
            src.pitch += Random.Range(-pitchRandomizer, pitchRandomizer);

            src.PlayOneShot(src.clip);
        }
    }
}
