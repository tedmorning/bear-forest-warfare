using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

    public AudioClip[] music;
    private static AudioClip [] musicStatic;

    public AudioClip[] soundFX;
    private static AudioClip[] soundFXStatic;

    public static float musicVolume;
    public static float soundFXVolume;

    public static bool isPlayMusic;
    public static bool isPlaySoundFX;


    public void Awake() 
    {
        musicVolume = 1;
        soundFXVolume = 1;
        musicStatic = music;
        soundFXStatic = soundFX;
    }

    public static void off() 
    {
        isPlayMusic = true;
        isPlaySoundFX = true;
    }


    public static void PlayMusic(string name) 
    {
        if (isPlayMusic == false) return;

        for (int i = 0; i < musicStatic.Length; i++)
        {
            if (musicStatic[i].name == name) 
            {
                AudioSource.PlayClipAtPoint(musicStatic[i],Vector3.zero,1);
                return;
            }
        }
    }

    public static void PlayMusicNGUITools(string name) 
    {
        if (isPlayMusic == false) return;

        for (int i = 0; i < musicStatic.Length; i++)
        {
            if (musicStatic[i].name == name)
            {
                NGUITools.PlaySound(musicStatic[i], musicVolume, 1f);
                
                return;
            }
        }
    }

    public static void PlayFX(string name) 
    {
        if (isPlaySoundFX == false) return;

        for (int i = 0; i < soundFXStatic.Length; i++)
        {
            if (soundFXStatic[i].name == name)
            {
                AudioSource.PlayClipAtPoint(musicStatic[i], Vector3.zero, 1);
                return;
            }
        }
    }

    public static void PlayFXNGUITools(string name)
    {
        if (isPlaySoundFX == false) return;

        for (int i = 0; i < soundFXStatic.Length; i++)
        {
            if (soundFXStatic[i].name == name)
            {
                NGUITools.PlaySound(soundFXStatic[i], soundFXVolume, 1f);
                return;
            }
        }
    }
    
}
