using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static Movements;

public class MainController : MonoBehaviour
{
    public int balloonsLeft = 2;
    private float _horizontal;
    private bool _isFacingRight = true;
    private Vector3 _balloonsRotatorOffset;
    private AudioSource _audioSource;
    private float _energy = 100f;
    private TextMeshProUGUI _energyTxt;

    [SerializeField] private GameObject _balloonsRotator;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite _flapSprite;
    [SerializeField] private AudioClip _deathSFX;
    [SerializeField] public AudioClip flapSFX;
 
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _energyTxt = GameObject.Find("EnergyText").GetComponent<TextMeshProUGUI>();
        _balloonsRotatorOffset = _balloonsRotator.transform.localPosition;
    }

    void Update()
    {
        if (_energy < 100f)
        {
            _energy += 1f * Time.deltaTime;
        }

        _energyTxt.text = "Energy: " + _energy.ToString();

        timeSinceLastFlap += Time.deltaTime;
        _horizontal = Input.GetAxisRaw("Horizontal");
        _isFacingRight = Flip(_isFacingRight, _horizontal, gameObject);
        if (Input.GetButtonDown("Jump") && timeSinceLastFlap > timeBetweenFlaps)
        {
            _energy -= 20f;
            StartCoroutine(flapAnim(_defaultSprite, _flapSprite, gameObject));
            if (balloonsLeft != 0)
            {
                _rb.AddForce(new Vector2(_horizontal * speed, jumpingPower), ForceMode2D.Impulse);
                if (_rb.velocity.magnitude > maxSpeed)
                {
                    _rb.velocity = _rb.velocity.normalized * maxSpeed;
                }
            }
            timeSinceLastFlap = 0f;
            StopCoroutine(flapAnim(_defaultSprite, _flapSprite, gameObject));
        }
        SimulateBalloons(_balloonsRotator, _rb, _balloonsRotatorOffset);
    }

    private void UseAccessory()
    {
        // pew pew flap flap
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

