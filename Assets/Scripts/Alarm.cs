using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    private const float MaxVolume = 1.0f;
    private const float MinVolume = 0f;

    [SerializeField] private float _volumeSpeedChanging;
    [SerializeField] private HomeAlarm _detector;

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
        => _sound.volume = MinVolume;

    public void Play()
    {
        _requiredVolume = MaxVolume;
        _sound.Play();
        _activeCoroutine = StartCoroutine(ChangeVolume());
    }

    public void Stop()
    {
        _requiredVolume = MinVolume;
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