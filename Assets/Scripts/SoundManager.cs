using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
  public static SoundManager instance { get; private set; }
  AudioSource audioSource;

  private void Awake()
  {
    DontDestroyOnLoad(gameObject);
    instance = this;
    audioSource = GetComponent<AudioSource>();
  }

  public void PlaySound(AudioClip audio)
  {
    audioSource.clip = audio;
    audioSource.Play();
  }


}
