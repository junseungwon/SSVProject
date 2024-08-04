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
        Debug.Log("������ ����");
       Animation animation= GetComponentInChildren<Animation>();
        //���� ä�� Ÿ�̹��̸�
        if (GameManager.Instance.PlayStoryManager.chapterStep == 1 && GameManager.Instance.PlayStoryManager.storyStep==1)
        {
            Debug.Log("�����ڸ��� �Ϸ�");
            GameManager.Instance.PlayStoryManager.chapterManager[1].GetComponent<Chapter1>().CompleteCutTree();
        }
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
