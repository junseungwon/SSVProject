using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallArea : MonoBehaviour
{
    //��ġ ����
    public int itemCode = 0;
    public GameObject item = null;
    private void OnTriggerStay(Collider other)
    {
        
        if(other.tag == "Item")
        {
            PutItem();
        }
    }
    private void PutItem()
    {
        //�ش� �������� ��ġ�� �Ϸ� �Ȱ��� Ȯ���ؾ���
        //��ġ object���� �Ϸ��� ǥ�ð� �߸� ������
    }
}
