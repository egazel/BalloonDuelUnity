using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BalloonPop : MonoBehaviour
{
    private float _timeSinceLastBalloonPopped = 0f;
    private float _invincibilityTimer = .3f;
    [SerializeField] private AudioClip _popSfx; 

    private void Update()
    {
        _timeSinceLastBalloonPopped += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Balloon" && _timeSinceLastBalloonPopped > _invincibilityTimer)
        {
            Destroy(collision.gameObject);
            GameObject rootParent = collision.transform.root.gameObject;
            Rigidbody2D playerRb = gameObject.transform.root.GetComponentInChildren<Rigidbody2D>();
            if (rootParent.GetComponentInChildren<BasicAI>() != null)
            {
                rootParent.GetComponentInChildren<BasicAI>().balloonsLeft -= 1;
                playerRb = gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>();
            }
            else
            {
                rootParent.GetComponentInChildren<MainController>().balloonsLeft -= 1;
            }
            rootParent.GetComponentInChildren<AudioSource>().PlayOneShot(_popSfx, Settings.SFXVolumeLevel);
            playerRb.velocity = new Vector2(-playerRb.velocity.x, 15f);
            _timeSinceLastBalloonPopped = 0f;
        }
    }
}
