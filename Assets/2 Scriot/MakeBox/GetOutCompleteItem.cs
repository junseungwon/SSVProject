using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GetOutCompleteItem : MonoBehaviour
{
    //������ ��� �������� 
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CustomDirController>() != null)
        {
            //�ش�Ǵ� ��Ʈ�ѷ��� ��� ���°� �ƴѰ��
            //��ũ��Ʈ�� ����� ��ü�� �����ͼ� ���ÿ� ������ ����Ѵ�.
            GameObject playerGrabObj = other.GetComponent<CustomDirController>().grabObject;
            float selectNum = GameManager.Instance.PlayerController.inputActions.actionMaps[5].actions[0].ReadValue<float>();
            if (selectNum > 0.5 && playerGrabObj != null)
            {
                GameManager.Instance.MakeItemBox.RemoveItems();
                playerGrabObj.GetComponent<Rigidbody>().isKinematic = false;
                playerGrabObj.GetComponent<Rigidbody>().useGravity = true;  
   
            }
        }
    }
}
