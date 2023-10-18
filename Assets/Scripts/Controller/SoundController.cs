using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    AudioSource audioSource;
    public static SoundController instance;

    Dictionary<string, AudioClip> dic_Name_AudioClip = new Dictionary<string, AudioClip>();
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        AudioClip[] audioClips = Resources.LoadAll<AudioClip>("Sounds");
        foreach (AudioClip audioClip in audioClips)
        {
            dic_Name_AudioClip.Add(audioClip.name, audioClip);
        }
        PlayMusic("music_background");
    }

    public void PlayMusic(string musicName)
    {
        if (!dic_Name_AudioClip.ContainsKey(musicName))
        {
            Debug.LogError("music Name :" + musicName + " is not exist");
            return;
        }
        audioSource.clip = dic_Name_AudioClip[musicName];
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlaySound(string soundName)
    {
        if (!dic_Name_AudioClip.ContainsKey(soundName))
        {
            Debug.LogError("music Name :" + soundName + " is not exist");
            return;
        }
        audioSource.PlayOneShot(dic_Name_AudioClip[soundName]);
    }
}
