using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Movements : MonoBehaviour
{
    public static float speed = 18f;
    public static float maxSpeed = 25f;
    public static float jumpingPower = 22f;
    public static float timeBetweenFlaps = .14f;
    public static float timeSinceLastFlap = 0f;
        
    public static void SimulateBalloons(GameObject balloonsRotator, Rigidbody2D playerRb, Vector3 balloonsBaseOffset)
    {
        // position
        balloonsRotator.transform.position = new Vector3(playerRb.position.x, playerRb.position.y, 0f) + balloonsBaseOffset;

        // orientation
        foreach (Transform child in balloonsRotator.transform)
        {
            if (!child.IsDestroyed())
            {
                Quaternion target = Quaternion.Euler(0, 0, playerRb.velocity.x * 4);
                child.transform.rotation = Quaternion.Lerp(child.transform.rotation, target, Time.deltaTime);
            }
        }

        // arc movement (simulated inertia)
        float minRot = -40f;
        float maxRot = 40f;
        float targetRotationValue = Mathf.Clamp(playerRb.velocity.x * 8f, minRot, maxRot);
        Quaternion balloonsRot = Quaternion.Euler(0, 0, targetRotationValue);
        balloonsRotator.transform.rotation = Quaternion.Lerp(balloonsRotator.transform.rotation, balloonsRot, Time.deltaTime);
    }


    public static bool Flip(bool isFacingRight, float horizontalInput, GameObject go)
    {
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = go.transform.localScale;
            localScale.x *= -1f;
            go.transform.localScale = localScale;
        }
        return isFacingRight;
    }

    public static IEnumerator flapAnim(Sprite defaultSprite, Sprite flapSprite, GameObject go)
    {
        go.GetComponent<SpriteRenderer>().sprite = flapSprite;

        if (go.GetComponent<MainController>() != null)
        {
            go.GetComponent<AudioSource>().PlayOneShot(go.GetComponent<MainController>().flapSFX, Settings.SFXVolumeLevel);
        }
        else
        {
            go.GetComponent<AudioSource>().PlayOneShot(go.GetComponent<BasicAI>().flapSFX, Settings.SFXVolumeLevel);
        }

        yield return new WaitForSeconds(.1f);
        go.GetComponent<SpriteRenderer>().sprite = defaultSprite;
    }
}
