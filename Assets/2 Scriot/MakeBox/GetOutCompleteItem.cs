using System;
using UnityEngine;

public delegate void GetAction();
public class GetOutCompleteItem : MonoBehaviour
{
    public Action getAction = null;
    public GetAction dAction = null;
    //������ ��� �������� 
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Item")
        {

        }
        if (other.GetComponent<CustomDirController>() != null)
        {
            //�ش�Ǵ� ��Ʈ�ѷ��� ��� ���°� �ƴѰ��
            //��ũ��Ʈ�� ����� ��ü�� �����ͼ� ���ÿ� ������ ����Ѵ�.
            GameObject playerGrabObj = other.GetComponent<CustomDirController>().grabObject;
            int num = (other.tag == "Left") ? 2 : 5;
            float selectNum = GameManager.Instance.PlayerController.inputActions.actionMaps[num].actions[0].ReadValue<float>();
            Debug.Log(selectNum+" ��ư�� �������� �ִ��� Ȯ�� 1�̻� ��ư ����");
            if (selectNum > 0.5 && playerGrabObj != null)
            {
                //������ ����� �� ���� ������ ������ �����Ұǵ� ������ ������ 0�� �ƴ��� Ȯ���ؾ���
                int count = GameManager.Instance.MakeItemBox.completeItemCount;
                if (count > 1)
                {
                    Debug.Log("1�� �̻��� ������");
                    //���������� �״�� �����ؼ� ���� �ڸ��� ��ġ��
                    GameManager.Instance.MakeItemBox.GetOutCompleteItem();
                }
                else
                {
                    Debug.Log("0���� ��");
                    if (dAction != null)
                    {
                        Debug.Log("��������Ʈ�� �����");
                        dAction();
                    }
                    GameManager.Instance.MakeItemBox.RemoveCompleteItem();
                }
                //�θ� �ʱ�ȭ
                Debug.Log(playerGrabObj.name);
                playerGrabObj.tag = "Item";
                playerGrabObj.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                Debug.Log(playerGrabObj.transform.localScale);
                GameManager.Instance.MakeItemBox.RemoveItems();
                playerGrabObj.GetComponent<Rigidbody>().isKinematic = false;
                playerGrabObj.GetComponent<Rigidbody>().useGravity = true;
                //action�� ���
                if (playerGrabObj.name == "180")
                {
                    Debug.Log("�ش�������� 180�� �½��ϴ�.");
                    //�κ��丮 �ڵ��ȣ 180��
                    if (getAction != null)
                    {
                        Debug.Log("�ش� �׼��������մϴ�.");
                        getAction.Invoke();
                    }
                }
            }
        }
    }
}
//������ �� ���� �κ��丮��� é�� 1�� �κ��丮 ���ۿϷ� �Ǿ��ٰ� �߰�
