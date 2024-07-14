using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReMoveObject : MonoBehaviour
{
    public int ObjectHp = 100;
    public int damage = 10;
    //public int code
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void GetDamaged()
    {
        ObjectHp -= damage;
    }
    private void RemoveObject()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        GetDamaged();
        if(ObjectHp <= 0)
        {

        }
    }
}
