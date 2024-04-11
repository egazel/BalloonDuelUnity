using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceCeiling : MonoBehaviour
{
    [SerializeField] private float _downForce;
    [SerializeField] private AudioClip _bounceAudio;
    private AudioSource _audioSource;
    private Rigidbody2D _rb;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bot")
        {
            _rb = collision.gameObject.GetComponent<Rigidbody2D>();
            _rb.velocity = new Vector2(_rb.velocity.x, _downForce);
            _audioSource.PlayOneShot(_bounceAudio, Settings.SFXVolumeLevel);
        }
    }
}
