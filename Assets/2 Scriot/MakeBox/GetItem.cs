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
        //플레이어 손으로 아이템을 넣었을 경우
        if (other.GetComponent<CustomDirController>() != null)
        {
            //해당되는 컨트롤러가 잡기 상태가 아닌경우
            //스크립트에 저장된 물체를 가져와서 예시용 물건을 사용한다.
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
//    해당되는 컨트롤러가 잡기 상태가 아닌경우
//    스크립트에 저장된 물체를 가져와서 예시용 물건을 사용한다.
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
//    해당되는 컨틀로러가 잡기 상태인 경우
//    grab물체가 null인경우
//    물체를 복사해서 건내줌
//}
