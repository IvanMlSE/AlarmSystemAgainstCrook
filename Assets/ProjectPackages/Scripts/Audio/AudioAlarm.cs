using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AudioAlarm : MonoBehaviour
{
    [SerializeField]
    private Detector detector;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private float _rateOfRise = 0.25f;

    private Coroutine _coroutineVolumeController;

    private const float _minVolume = 0f;
    private const float _maxVolume = 1f;

    private bool _loopCycleCoroutineChangeVolume;

    private void OnEnable()
    {
        detector.Triggered += PlayAudio;
    }

    private void OnDisable()
    {
        detector.Triggered -= PlayAudio;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        _audioSource.volume = _minVolume;
    }

    private void PlayAudio(bool triggered)
    {
        if (triggered == true)
        {
            SwitchAudio(Selector.On);
            RestartCoroutineChangeVolume(_maxVolume);
        }
        else
        {
            RestartCoroutineChangeVolume(_minVolume);
        }
    }

    private void RestartCoroutineChangeVolume(float targetVolume)
    {
        if (_coroutineVolumeController != null)
        {
            StopCoroutine(_coroutineVolumeController);
        }

        _coroutineVolumeController = StartCoroutine(CoroutineChangeVolume(targetVolume));
    }

    private IEnumerator CoroutineChangeVolume(float targetVolume)
    {
        while (_loopCycleCoroutineChangeVolume == true)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _rateOfRise * Time.fixedDeltaTime);

            if (_audioSource.volume == _minVolume)
            {
                SwitchAudio(Selector.Off);
            }

            yield return null;
        }
    }

    private void SwitchAudio(Selector selector)
    {
        if (selector == Selector.On)
        {
            _audioSource.Play();
            _audioSource.loop = true;

            _loopCycleCoroutineChangeVolume = true;
        }
        else if (selector == Selector.Off)
        {
            _audioSource.Stop();
            _audioSource.loop = false;

            _loopCycleCoroutineChangeVolume = false;
        }
    }

    private enum Selector
    {
        On,
        Off
    }
}