using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GetItem : MonoBehaviour
{
    public XRSocketInteractor socket = null;
    public GameObject obj = null;
    private void OnTriggerStay(Collider other)
    {
        //�÷��̾� ������ �������� �־��� ���
        if (other.GetComponent<CustomDirController>() != null)
        {
            //�ش�Ǵ� ��Ʈ�ѷ��� ��� ���°� �ƴѰ��
            //��ũ��Ʈ�� ����� ��ü�� �����ͼ� ���ÿ� ������ ����Ѵ�.
            GameObject playerGrabObj = other.GetComponent<CustomDirController>().grabObject;
            float selectNum = GameManager.Instance.PlayerController.inputActions.actionMaps[5].actions[0].ReadValue<float>();
            if (selectNum < 0.5 && playerGrabObj != null)
            {
                int parentIndex = transform.parent.GetSiblingIndex();
                int codeNum = int.Parse(playerGrabObj.name);
                other.GetComponent<CustomDirController>().grabObject = null;
                bool isBool= GameManager.Instance.MakeItemBox.PutItem(playerGrabObj.gameObject, this.gameObject.transform.parent.GetSiblingIndex());
                if (isBool)
                {
                    Debug.Log(parentIndex);
                    playerGrabObj.GetComponent<ItemGrabInteractive>().makeBoxParentNum = parentIndex;
                    playerGrabObj.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }
    }


    
}

//if (other.GetComponent<CustomDirController>() != null)
//{
//    �ش�Ǵ� ��Ʈ�ѷ��� ��� ���°� �ƴѰ��
//    ��ũ��Ʈ�� ����� ��ü�� �����ͼ� ���ÿ� ������ ����Ѵ�.
//    GameObject playerGrabObj = other.GetComponent<CustomDirController>().grabObject;
//    float selectNum = GameManager.Instance.PlayerController.inputActions.actionMaps[5].actions[0].ReadValue<float>();
//    if (selectNum < 0.5 && playerGrabObj != null)
//    {

//        int parentIndex = transform.parent.GetSiblingIndex();

//        int codeNum = int.Parse(playerGrabObj.name);
//        other.GetComponent<CustomDirController>().grabObject = null;
//        bool isBool = GameManager.Instance.ItemBox.PutItem(parentIndex, codeNum, playerGrabObj);
//        if (isBool)
//        {
//            playerGrabObj.GetComponent<ItemGrabInteractive>().itemBoxParentNum = parentIndex;
//            playerGrabObj.GetComponent<ItemGrabInteractive>().textUI = m_TextMeshPro;
//            playerGrabObj.GetComponent<Rigidbody>().isKinematic = true;
//            m_TextMeshPro.text = GameManager.Instance.ItemBox.itemBoxs[parentIndex].itemCount.ToString();
//        }

//    }
//    �ش�Ǵ� ��Ʋ�η��� ��� ������ ���
//    grab��ü�� null�ΰ��
//    ��ü�� �����ؼ� �ǳ���
//}
