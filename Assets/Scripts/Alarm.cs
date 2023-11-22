using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private float _volumeSpeedChanging;
    [SerializeField] private AlarmSystem _detector;

    private Coroutine _activeCoroutine;
    private AudioSource _sound;
    private float _requiredVolume;

    private void OnEnable()
    {
        _detector.CrookInEvent += Play;
        _detector.CrookOutEvent += Stop;
    }

    private void OnDisable()
    {
        _detector.CrookInEvent -= Play;
        _detector.CrookOutEvent -= Stop;
    }

    private void Awake()
        => _sound = GetComponent<AudioSource>();

    private void Start()
        => _sound.volume = 0;

    public void Play()
    {
        _requiredVolume = 1f;
        _sound.Play();
        _activeCoroutine = StartCoroutine(ChangeVolume());
    }

    public void Stop()
    {
        _requiredVolume = 0f;
        StopCoroutine(_activeCoroutine);
        _activeCoroutine = StartCoroutine(ChangeVolume());
    }

    private IEnumerator ChangeVolume()
    {
        while (_sound.volume != _requiredVolume) 
        {
            _sound.volume = Mathf.MoveTowards(_sound.volume, _requiredVolume, _volumeSpeedChanging * Time.deltaTime);
            yield return null;
        }
    }
}