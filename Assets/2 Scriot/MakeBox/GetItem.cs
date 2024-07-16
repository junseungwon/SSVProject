using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
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
                bool isBool= GameManager.Instance.MakeItemBox.PutItem(playerGrabObj.gameObject, this.gameObject.transform.GetSiblingIndex()-1);
                if (isBool)
                {
                   // playerGrabObj.GetComponent<ItemGrabInteractive>().itemBoxParentNum = parentIndex;
                    playerGrabObj.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }
    }
}
