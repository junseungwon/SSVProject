using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : DropItem
{
    private int hp = 100;
    private void Start()
    {
      //DeadTree();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "170")
        {
            DeadTree();
           // hp -= 10;
            //if (hp<=0) DeadTree();
        }
    }
    private void DeadTree()
    {
        DropItems();
        Debug.Log("나무가 죽음");
       Animation animation= GetComponentInChildren<Animation>();
        //나무 채굴 타이밍이면
        if (GameManager.Instance.PlayStoryManager.chapterStep == 1 && GameManager.Instance.PlayStoryManager.storyStep==1)
        {
            Debug.Log("나무자르기 완료");
            GameManager.Instance.PlayStoryManager.chapterManager[1].GetComponent<Chapter1>().CompleteCutTree();
        }
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
