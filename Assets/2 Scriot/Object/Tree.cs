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
        Debug.Log("나무가 죽음");
       Animation animation= GetComponentInChildren<Animation>();
        animation.Play();
        
    }
    //나무 제거
    public void RemoveTree()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        InstanceItem();
    }
    //나무 위쪽이 사라지고 아이템이 생성됨
    private void InstanceItem()
    {
        Debug.Log("아이템이 생성됨");
    }
}
