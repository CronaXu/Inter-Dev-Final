using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fluteSystem : MonoBehaviour
{
    public AudioSource audioSource;

    public float maxVolume;
    public float attackTime;
    public float releaseTime;

    public KeyCode keyToPlay = KeyCode.A;
    public AudioClip[] myfluteNotes;


    public enum ASRState { inactive, attack, sustain, release }
    public ASRState asrState;

    // Start is called before the first frame update
    void Start()
    {
        keyToPlay = KeyCode.A;
        asrState = ASRState.inactive;
        audioSource.volume = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //audioSource.clip = myfluteNotes[0];
        fluteNotes();
        if (Input.GetKey(keyToPlay))
        {
            switch (asrState)
            {
                case ASRState.inactive:
                    asrState = ASRState.attack;
                    break;
                case ASRState.attack:
                    if (audioSource.volume < maxVolume)
                    {
                        audioSource.volume += (Time.deltaTime / attackTime) * maxVolume;
                    }

                    else if (audioSource.volume >= maxVolume)
                    {
                        audioSource.volume = maxVolume;
                        asrState = ASRState.sustain;
                    }
                    break;
                case ASRState.sustain:
                    break;
                case ASRState.release:
                    asrState = ASRState.attack;
                    break;
            }
        }

        else
        {
            switch (asrState)
            {
                case ASRState.inactive:
                    break;
                case ASRState.attack:
                    asrState = ASRState.release;
                    break;
                case ASRState.sustain:
                    asrState = ASRState.release;
                    break;
                case ASRState.release:

                    if (audioSource.volume > 0f)
                    {
                        audioSource.volume -= (Time.deltaTime / releaseTime) * maxVolume;
                    }
                    else
                    {
                        audioSource.volume = 0f;
                        asrState = ASRState.inactive;
                    }
                    break;
            }
        }

    }

    void fluteNotes()
    {
        if(keyToPlay == KeyCode.A)
        {
            audioSource.clip = myfluteNotes[0];
        }
    }
}
