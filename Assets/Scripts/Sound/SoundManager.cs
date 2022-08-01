using CodeMonkey;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{

    static GameObject onShotGameObject;
    static AudioSource oneShotAudioSource;

    public enum Sound
    {
        FootStep
    }

    public static void PlaySound(Sound sound,float volume = 1)
    {
        if (onShotGameObject == null)
        {
            onShotGameObject = new GameObject("Sound");

            oneShotAudioSource = onShotGameObject.AddComponent<AudioSource>();

        }
        oneShotAudioSource.PlayOneShot(GetAudioClip(sound), volume);

    }


    public static void PlaySound(Sound sound,Vector3 position, float volume = 1)
    {
        GameObject SoundGameObject = new GameObject("Sound");
        SoundGameObject.transform.position = position;
        AudioSource audio = SoundGameObject.AddComponent<AudioSource>();
        audio.clip = GetAudioClip(sound);
        audio.volume = volume;
        audio.maxDistance = 100f;
        audio.spatialBlend = 1f;
        audio.rolloffMode = AudioRolloffMode.Linear;
        audio.dopplerLevel = 0f;
        audio.Play();
        Object.Destroy(SoundGameObject, audio.clip.length);
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (Assets.SoundAudioClip soundAudioClip in Assets.i.SoundClips)
        {
            if (soundAudioClip.sound == sound) return soundAudioClip.audioClips[Random.Range(0, soundAudioClip.audioClips.Length-1)];
        }
        return null;
    }

}
