using System;
using UnityEngine;

public class GetOutCompleteItem : MonoBehaviour
{
    public Action getAction = null;
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
                //������ ����� �� ���� ������ ������ �����Ұǵ� ������ ������ 0�� �ƴ��� Ȯ���ؾ���
                int count = GameManager.Instance.MakeItemBox.completeItemCount;
                if (count > 1)
                {
                    //���������� �״�� �����ؼ� ���� �ڸ��� ��ġ��
                    GameManager.Instance.MakeItemBox.GetOutCompleteItem();
                }
                else
                {
                    GameManager.Instance.MakeItemBox.RemoveCompleteItem();
                    GameManager.Instance.MakeItemBox.TextCompleteItemNumUIChange();

                }
                GameManager.Instance.MakeItemBox.RemoveItems();
                playerGrabObj.GetComponent<Rigidbody>().isKinematic = false;
                playerGrabObj.GetComponent<Rigidbody>().useGravity = true;
                if (playerGrabObj.name == "180")
                {
                    //�κ��丮 �ڵ��ȣ 180��
                    if (getAction != null)
                    {
                        getAction.Invoke();
                    }
                }
            }
        }
    }
}
//������ �� ���� �κ��丮��� é�� 1�� �κ��丮 ���ۿϷ� �Ǿ��ٰ� �߰�
