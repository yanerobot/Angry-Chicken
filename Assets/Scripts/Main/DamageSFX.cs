using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Health))]
public class DamageSFX : MonoBehaviour
{
    [SerializeField] AudioClipOneShot[] damageSoundsRandom;
    [SerializeField] AudioClipOneShot damageSound;
    [SerializeField] AudioClipOneShot dieSound;

    Result<AudioSource> res;
    Health health;
    AudioSource audioSrc;


    void Awake()
    {
        health = GetComponent<Health>();
        audioSrc = GetComponent<AudioSource>();
        health.OnDamage += PlayRandomDMGSound;
        health.OnDamage += PlayNormalDMGSound;
        health.OnDie += PlayerDieSound;
    }

    void PlayRandomDMGSound(int health)
    {
        if (damageSoundsRandom.Length > 0)
        {
            var randomClip = damageSoundsRandom[UnityEngine.Random.Range(0, damageSoundsRandom.Length)];
            audioSrc.PlayOneShot(randomClip.clip, randomClip.volume);
        }
    }

    void PlayerDieSound()
    {
        var src = gameObject.AddComponent<AudioSource>();
        src.clip = damageSound.clip;
        src.pitch = src.pitch - damageSound.randomizer;
        src.volume = damageSound.volume;
        src.loop = false;
        src.Play();
        res = new Result<AudioSource>();
        res.val = src;
        StartCoroutine(DestroyWhenDone(res));
    }
    AudioSource src;
    void PlayNormalDMGSound(int _)
    {
        src = gameObject.AddComponent<AudioSource>();
        src.clip = damageSound.clip;
        src.pitch = src.pitch + UnityEngine.Random.Range(-damageSound.randomizer/2, damageSound.randomizer/2);
        src.volume = damageSound.volume;
        src.loop = false;
        src.Play();
        res = new Result<AudioSource>();
        res.val = src;
        StartCoroutine(DestroyWhenDone(res));
    }

    IEnumerator DestroyWhenDone(Result<AudioSource> src)
    {
        yield return new WaitWhile(() => src.val.isPlaying);

        Destroy(src.val);
    }
}
public class Result<T>
{
    public T val;
}
