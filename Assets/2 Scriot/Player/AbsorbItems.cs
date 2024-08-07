using System.Collections.Generic;
using UnityEngine;

public class AbsorbItems : MonoBehaviour
{
    public GetAction GetAction = null;
    public int itemCode = 0;
    private int count = 0;
    //��ü�� �������� �� �������� �Ҹ��� �������̸� ��� �ƴϸ� �������
    private void OnTriggerEnter(Collider other)
    {
        //�ش� ������ �ױװ� �Ҹ��� �������� ��츸 ��� ����
        //�������� ��� ������ �ش� �±׸� GrabItems�� �ٲ�
        if (other.tag == "IsAbsorbItem")
        {
            count++;
            Debug.Log(count);
            Debug.Log(other.name+ " �������� ����߽��ϴ�.");
            CheckInvenEmptyPlace(int.Parse(other.name), other.gameObject);
        }
    }
    //�κ��丮 �ٱ��Ͽ� ���� �ڸ��� �ִ��� Ȯ����
    private void CheckInvenEmptyPlace(int codeName, GameObject obj)
    {
        List<int> listNum = new List<int>();
        for (int i = 0; i < GameManager.Instance.ItemBox.itemBoxs.Length; i++)
        {
            //�ش� �ڽ��� ���� �������� Ȯ����
            if (GameManager.Instance.ItemBox.itemBoxs[i].code == codeName)
            {
                //�ش�Ǵ� �ε����� �������� ���� �ְ� ������
                PutItem(i, codeName, obj);
                Debug.Log("�κ��丮�� ���� ���ǿ� �������� �߰��߽��ϴ�.");
                return;
            }
            //�ش�ĭ�� ����ִ��� Ȯ���ϰ� ����ִ� ĭ�̸� �ش� �ε����� ����Ʈ�� ������
            else if(GameManager.Instance.ItemBox.itemBoxs[i].itemCount == 0)
            {
                listNum.Add(i);
            }
        }
        //�ڽ��� ���� ������ ���°�� ������ �ε����߿� ���� �����Ϳ� �������� �ִ´�.
        if(listNum.Count > 0)
        {
            Debug.Log("�ش�Ǵ� �������� �ߺ��Ǵ� �������� ��� ���ο� ������ �������� �Ҵ��߽��ϴ�.");
            PutItem(listNum[0], codeName, obj);
        }
        else
        {
            Debug.Log("�������� ���� ������ �����ϴ�.");
        }
    }
    private void PutItem(int num, int code, GameObject obj)
    {
        Debug.Log(1);
        itemCode = code;
        if(GetAction != null)
        {
            Debug.Log("�������� �־������ϴ�.");
            GetAction();
        }
        //�⺻ tag�� �����
        GameManager.Instance.itemTable.ItemTag(obj);

        GameManager.Instance.ItemBox.PutItem(num, code, obj);
    }
    //�������� �������� �ش� �������� ����� �������� Ȯ����
    //����� �����ϸ� ���� �κ��丮�� �ڸ��� �ִ��� Ȯ����
    //�ش� �������� �κ��丮�� ������ ���� �κ��丮�� �־��ְ� ������ �κ��丮�� �ش� ĭ�� �־��ְ� +1���� put��������

}
