using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AudioAlarm: MonoBehaviour
{
    [SerializeField]
    private AudioTrigger _audioTrigger;

    [SerializeField]
    private AudioClip _audioClipe;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private float _rateOfRise = 0.5f;

    private const float _minVolume = 0f;
    private const float _maxVolume = 1f;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        PlayAudio();
    }

    private void PlayAudio()
    {
        if (_audioTrigger.TriggerActive == true)
        {
            ChangeVolume(_maxVolume);

            if (_audioSource.isPlaying == false)
            {
                _audioSource.PlayOneShot(_audioClipe);
            }
        }
        else
        {
            ChangeVolume(_minVolume);

            if (_audioSource.volume == 0f)
            {
                _audioSource.Stop();
            }
        }
    }

    private void ChangeVolume(float targetVolume)
    {
        _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _rateOfRise * Time.fixedDeltaTime);
    }
}