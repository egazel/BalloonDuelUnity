using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static Movements;

public class BasicAI : MonoBehaviour
{
    public int balloonsLeft = 2;
    private int _dir = -1;
    private bool _isFacingRight = true;



    private Vector2 _playerPos;
    private float _timeSinceLastFlapAI;
    private float _timeBetweenFlapAI = 0.225f;
    private float _flapSpeedHigher = 0.45f;
    private float _flapSpeedLower = 0.225f;
    private float _verticalDiff;
    private float _horizontalDiff;
    private Vector3 _balloonsRotatorOffset;
    private AudioSource _audioSource;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite _flapSprite;
    [SerializeField] private GameObject _balloonsRotator;
    [SerializeField] private AudioClip _deathSFX;
    [SerializeField] public AudioClip flapSFX;

    private void Start()
    {
        _balloonsRotatorOffset = _balloonsRotator.transform.localPosition;
        _audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        _timeSinceLastFlapAI += Time.deltaTime;
        UpdateFlapSpeedAndDir();
        MoveAI();
    }

    private void UpdateFlapSpeedAndDir()
    {
        if (GameObject.FindWithTag("Player") != null )
        {
            _playerPos = GameObject.FindWithTag("Player").transform.position;
            _verticalDiff = transform.position.y - _playerPos.y;
            _horizontalDiff = transform.position.x - _playerPos.x;
            if (_verticalDiff > 0)
            {
                _timeBetweenFlapAI = _flapSpeedHigher;
                if (_horizontalDiff > 0)
                {
                    _dir = 1;
                }
                else
                {
                    _dir = -1;
                }    
            }
            else if (_verticalDiff <= 0)
            {
                _timeBetweenFlapAI = _flapSpeedLower;
                if (_horizontalDiff > 0)
                {
                    _dir = -1;
                }
                else
                {
                    _dir = 1;
                }
            }
        }
    }

    private void MoveAI()
    {
        if (balloonsLeft > 0)
        {
            if (_timeSinceLastFlapAI > _timeBetweenFlapAI)
            {
                StartCoroutine(flapAnim(_defaultSprite, _flapSprite, gameObject));
                _rb.AddForce(new Vector2(_dir * speed, jumpingPower), ForceMode2D.Impulse);
                _timeSinceLastFlapAI = 0f;
                StopCoroutine(flapAnim(_defaultSprite, _flapSprite, gameObject));

                _isFacingRight = Flip(_isFacingRight, _dir, gameObject);
            }

            if (_rb.velocity.magnitude > maxSpeed)
            {
                _rb.velocity = _rb.velocity.normalized * maxSpeed;
            }
        SimulateBalloons(_balloonsRotator, _rb, _balloonsRotatorOffset);
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.tag == "DeathPlane")
        {
            AudioSource collAudioSource = collision.gameObject.GetComponent<AudioSource>();
            collAudioSource.PlayOneShot(_deathSFX, Settings.SFXVolumeLevel);
            Destroy(_rb.gameObject.transform.parent.gameObject);
        }
    }
}