using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BackgroundPicker : MonoBehaviour
{
    [SerializeField] private Sprite[] bgList;

    void Start()
    {
        UnityEngine.UI.Image imageCmpnt = gameObject.GetComponent<UnityEngine.UI.Image>();
        imageCmpnt.sprite = bgList[Random.Range(0, bgList.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
