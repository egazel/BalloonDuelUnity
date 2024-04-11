using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static MainController;

public class SideTeleport : MonoBehaviour
{
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        bool isVisible = IsVisibleFromCamera();
        if (!isVisible && _rb.position.y > -(Camera.main.orthographicSize / 2f))
        {
            _rb.position = (new Vector2(-_rb.position.x, _rb.position.y));
        }
    }

    bool IsVisibleFromCamera()
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        if (GeometryUtility.TestPlanesAABB(planes, gameObject.GetComponent<CapsuleCollider2D>().bounds))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
