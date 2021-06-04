using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public GameObject empty;
    public List<Sound> sounds;

    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }

    void Awake()
    {
        //Check singleton
        if (_instance != null)
            Destroy(gameObject);
        else
            _instance = this;

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            var g = Instantiate(empty, Vector3.zero, Quaternion.identity, transform);
            g.name = s.name;
            s.source = g.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.vol;
            s.source.pitch = s.pitch;
        }
    }

    public void Play(string soundName)
    {
        Sound s = sounds.Find(s => s.name == soundName);
        if (s == null)
            Debug.LogWarning($"Sound: {soundName} not found");
        else
            s.source.Play();
    }
}

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0, 1f)]
    public float vol = 1;
    [Range(.1f, 3)]
    public float pitch = 1;

    [HideInInspector]
    public AudioSource source;
}