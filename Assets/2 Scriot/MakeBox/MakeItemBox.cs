using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MakeItemBox : MonoBehaviour
{
    [Header("������ Element")]
    [SerializeField]
    private GameObject[] itemsParent = new GameObject[3];
    private GameObject[] items = new GameObject[3];
    private int[] itemCount = new int[3];
    
    
    [Header("������ Complete")]
    [SerializeField]
    private GameObject completeItemParent = null;
    private GameObject completeItem = null;
    private int completeItemCode = 0;
   
    private void Start()
    {
        GameManager.Instance.MakeItemBox = this;
    }

    //+�������� �������� ���� �ڵ� �ۼ� �ʿ���
    public bool PutItem(GameObject item, int num)
    {
        //���� �������� ������ �������� �ְ�
        //�������� ������ Ƚ���� �ö�
        //�������� �ٸ����� �ִ´ٸ� ���� �� ���� ����� 
        if (itemCount[num] == 0)
        {
            items[num] = item;
            itemCount[num] += 1;

            TextNumUIChange(num);

            //�������� ��������
            ItemSetting(item, itemsParent[num]);
            Debug.Log(items[num].name + "�� ������ ������迡 �־����ϴ�.");
        }
        else if (items[num].name == item.name)
        {
            itemCount[num] += 1;
            TextNumUIChange(num);
            Destroy(item);
            Debug.Log(items[num].name + " " + itemCount[num] + "���� ������ ������迡 �־����ϴ�.");
        }
        else
        {
            Debug.Log("�ƹ��͵� ���� �ʾҽ��ϴ�.");
            return false;
        }
        ProduceCompleteItem();
        return true;
    }
    public GameObject GetOutItem()
    {
        return completeItem;
    }
    private void ProduceCompleteItem()
    {
        //�ش�Ǵ� ������ �ڵ带 �޾Ƽ� �������� �����Ұǵ� ������ prefab�� �����ϴ� ������ �ش�Ǵ� �ڵ忡 �������� ����
        //�׸��� �ش�Ǵ� ��ҿ� ��ġ��
        if (completeItem != null)
        {
            Destroy(completeItem);
        }
        completeItem = GetCompleteItem();
        if (completeItem != null)
        {
            ItemSetting(completeItem, completeItemParent);
        }
    }

    private void ItemSetting(GameObject item, GameObject parentObj)
    {
        item.transform.parent = parentObj.transform;
        item.transform.position = parentObj.transform.GetChild(1).position;
        item.transform.rotation = Quaternion.identity;
        item.transform.localScale = new Vector3(50, 50, 50);
    }

    //�ϼ��� �������� �ڵ带 ��ȯ��
    private GameObject GetCompleteItem()
    {
        //������ �̸����� �ڵ��̰� �ڵ���� �޾ƿͼ� 
        //��ù��°�� ���� �̸��̶� ������ ������ Ȯ���Ѵ�.
        //������ �ٽ� �ݺ��ؼ� �ι�° �������� �̸��̶� ������ ������ Ȯ���Ѵ�.
        // ���������� ����° �����۱��� �̸��̶� ������ ���� ��� �ϼ��������� complete������ �ڵ尪�� �޾ƿͼ� �ش�Ǵ� �������� �����ϰ� ��ȯ��
        int cnt = 0;
        List<int> listItemNum = new List<int>();
        //itemCnt���ٰ� 0���� count������ŭ �־��ش�.
        for (int i = 0; i < GameManager.Instance.itemTable.excelDB.ItemComebine.Count; i++)
        {
            listItemNum.Add(i);
        }
        GameObject item = Instantiate(GameManager.Instance.itemTable.GetDBGameObject(CheckItems(listItemNum, cnt)));
        return item;
    }

    private int CheckItems(List<int> listItemNum, int cnt)
    {
        //�� ������ ���ռ��� 6���� �ִٰ� ���� ��
        //itemCnt���� 0���� 5������ ���� �������
        //ó���� ������ 0~5���� �ش�Ǵ� �������� �ִ��� Ȯ����
        //0�϶� ��� �ϴ� �ڵ� �ʿ���
        List<int> storeInt = new List<int>();
        for (int i = 0; i < listItemNum.Count; i++)
        {
            if (IsThisSameThing(cnt, i, listItemNum))
            {
                storeInt.Add(listItemNum[i]);
            }
        }
        if (cnt == 2)
        {
            completeItemCode = storeInt[0];
            return completeItemCode;
        }
        else
        {
            cnt++;
            CheckItems(storeInt, cnt);
        }
        Debug.LogError("�������� Ȯ���� �� �����ϴ�.");
        return 0;

    }
    //�������� ���� ������ Ȯ��
    private bool IsThisSameThing(int cnt, int num, List<int> listItemNum)
    {
        int itemCode = 0;
        int itemCnt = 0;
        //cnt�� �ٶ� item1 item2 item3���� ������ �ȴ�.
        switch (cnt)
        {
            case 0:
                itemCode = GameManager.Instance.itemTable.excelDB.ItemComebine[listItemNum[num]].Item1;
                itemCnt = GameManager.Instance.itemTable.excelDB.ItemComebine[listItemNum[num]].Count1;
                break;
            case 1:
                itemCode = GameManager.Instance.itemTable.excelDB.ItemComebine[listItemNum[num]].Item2;
                itemCnt = GameManager.Instance.itemTable.excelDB.ItemComebine[listItemNum[num]].Count2;
                break;
            case 2:
                itemCode = GameManager.Instance.itemTable.excelDB.ItemComebine[listItemNum[num]].Item3;
                itemCnt = GameManager.Instance.itemTable.excelDB.ItemComebine[listItemNum[num]].Count3;
                break;
        }

        //cnt�� �ش�Ǵ� �������� ���� ��� codeName�� 0���� ����
        int itemCodeName = 0;
        if (items[cnt] == null)
        {
            itemCodeName = 0;
        }
        else
        {
            itemCodeName = int.Parse(items[cnt].name);
        }

        if ((itemCode == itemCodeName) && (itemCnt == itemCount[cnt]))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //���� ������ ����
    private void RemoveItems()
    {
        for (int i = 0; i < items.Length; i++)
        {
            Destroy(items[i]);
        }
        Array.Clear(items, 0, items.Length);
        Array.Clear(itemCount, 0, itemCount.Length);
    }
    private void TextNumUIChange(int num)
    {
        itemsParent[num].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = itemCount[num].ToString();
    }
}
