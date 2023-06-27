using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IsDoorOpened : MonoBehaviour
{
    [SerializeField] private UnityEvent _alarmIsOn;

    private float _maxVolume = 1f;
    private float _fadeSpeed = 0.5f;
    private bool _isBurglarInside = false;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0f;
    }

    private void Update()
    {
        if (_isBurglarInside)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _maxVolume, _fadeSpeed * Time.deltaTime);
        }
        else
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, 0f, _fadeSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            _isBurglarInside = true;
            _alarmIsOn.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            _isBurglarInside = false;
        }
    }
}
