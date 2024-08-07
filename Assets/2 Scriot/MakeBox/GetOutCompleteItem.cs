using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public delegate void GetAction();
public class GetOutCompleteItem : MonoBehaviour
{
    public Action getAction = null;
    public GetAction dAction = null;
    private GameObject preitem = null;
    private XRSocketInteractor socket = null;
    //������ ��� �������� 

    private void Start()
    {
        socket = GetComponent<XRSocketInteractor>();
        //socket.selectEntered.AddListener(GetOutItem);
    }


    public void GetOutItem()
    {
        if (GameManager.Instance.MakeItemBox.completeItem != null)
        {

            GameObject playerGrabObj = GameManager.Instance.MakeItemBox.completeItem;
            Debug.Log(playerGrabObj);
            int count = GameManager.Instance.MakeItemBox.completeItemCount;
            if (count > 1)
            {
                Debug.Log("1�� �̻��� ������");
                //���������� �״�� �����ؼ� ���� �ڸ��� ��ġ��
                GameManager.Instance.MakeItemBox.GetOutCompleteItem();
            }
            else if (count == 1)
            {
                Debug.Log("0���� ��");
                if (dAction != null)
                {
                    Debug.Log("��������Ʈ�� �����");
                    dAction();
                }
                GameManager.Instance.MakeItemBox.RemoveCompleteItem();
            }
            Debug.Log("���� ������ ����" + count + " ������ �̸��� " + playerGrabObj.name);
            //�θ� �ʱ�ȭ
            if (playerGrabObj.name == null)
            {
                Debug.Log("�ƹ��͵� �����ϴ�.");
            }
            Debug.Log(playerGrabObj.name);
            if (getAction != null)
            {
                Debug.Log("�׼��� �ֽ��ϴ�.");
            }
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

            GameManager.Instance.MakeItemBox.RemoveItems();
        }
    }
}

//������ �� ���� �κ��丮��� é�� 1�� �κ��丮 ���ۿϷ� �Ǿ��ٰ� �߰�
