using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class ItemBasic : MonoBehaviour
{
    public bool isRotate = true;
    private Vector3 v3;
    private int cnt = 0;
    // Start is called before the first frame update
    void Start()
    {
        v3 = transform.position;
        StartCoroutine(RotateObject());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator RotateObject()
    {
        
        while (true) {
            if (isRotate)
            {
                Rotate();
                UpDown();
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
    //돌면서 위에 떠있음
    private void Rotate()
    {
        transform.Rotate(Vector3.down * 0.5f);
    }
    private void UpDown()
    {
        if(cnt == 0) { v3.y += 0.01f; cnt = 1; } else { v3.y += -0.01f; cnt = 0; }
        transform.position = v3;
    }
}
