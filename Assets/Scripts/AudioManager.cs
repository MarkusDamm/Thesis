using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public void PlaySoundOnSource(AudioClip _sound, AudioSource _source)
    {
        _source.PlayOneShot(_sound);
    }
}
