using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private int hp = 100;
    private void Start()
    {
       // DeadTree();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Knife")
        {
            DeadTree();
           // hp -= 10;
            //if (hp<=0) DeadTree();
        }
    }
    private void DeadTree()
    {
        Debug.Log("������ ����");
       Animation animation= GetComponentInChildren<Animation>();
        animation.Play();
        
    }
    //���� ����
    public void RemoveTree()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        InstanceItem();
    }
    //���� ������ ������� �������� ������
    private void InstanceItem()
    {
        Debug.Log("�������� ������");
    }
}
