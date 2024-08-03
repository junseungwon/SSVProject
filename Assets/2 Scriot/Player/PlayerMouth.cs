using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouth : MonoBehaviour
{

    public GetAction GetAction = null;
    public int itemCode = 0;
    public GameObject item = null;
    //��ü�� �������� �� �������� �Ҹ��� �������̸� ��� �ƴϸ� �������
    private void OnTriggerEnter(Collider other)
    {
        //�ش� �������� ����⳪ ������ ��� ���� �����ϰ�
        EatItem(other.gameObject);
    }

    private void EatItem(GameObject other)
    {
        if (other.tag == "Item")
        {
            Debug.Log(other.name);
            if(other.name == "230"|| other.name == "240"|| other.name == "250")
            {

            //������ ����
            itemCode = int.Parse(other.name);
            item = other.gameObject;
            //�̺�Ʈ ����
            if (GetAction != null)
            {
                GetAction();
            }
            Destroy(other);
            }
        }
    }
}
