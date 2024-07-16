using System;
using System.Collections.Generic;
using UnityEngine;

public class MakeItemBox : MonoBehaviour
{

    public GameObject completeItemPrefab = null;
    public ExcelData excelData;

    [SerializeField]
    private GameObject[] itemsParent = new GameObject[3];

    [SerializeField]
    private GameObject completeItemParent = null;
    private GameObject[] items = new GameObject[3];
    
    private int[] itemCount = new int[3];
    
    private GameObject completeItem = null;
    private int completeItemCode = 0;
    private void Start()
    {
        GameManager.Instance.MakeItemBox = this;
        //ProduceCompleteItem();
    }
    public bool PutItem(GameObject item, int num)
    {
        //���� �������� ������ �������� �ְ�
        //�������� ������ Ƚ���� �ö�
        //�������� �ٸ����� �ִ´ٸ� ���� �� ���� ����� 
        Debug.Log(num);
        if (itemCount[num] == 0)
        {
            items[num] = item;
            itemCount[num] += 1;
            //�������� ��������
            ItemSetting(item, itemsParent[num]);
            Debug.Log(items[num].name + "�� ������ ������迡 �־����ϴ�.");
        } else if (items[num].name == item.name)
        {
            itemCount[num] += 1;
            Destroy(item);
            Debug.Log(items[num].name + " " + itemCount[num] +"���� ������ ������迡 �־����ϴ�.");
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
        //GetCompleteItem();
        completeItem = Instantiate(completeItemPrefab);
        if (completeItem != null)
        {
            ItemSetting(completeItem, completeItemParent);
        }

    }
    
    private void ItemSetting(GameObject item,GameObject parentObj)
    {
        item.transform.parent = parentObj.transform;
        item.transform.position = parentObj.transform.GetChild(1).position;
        item.transform.rotation = Quaternion.identity;
        item.transform.localScale = new Vector3(50,50,50);
    }
    private void GetCompleteItem()
    {
        //������ �̸����� �ڵ��̰� �ڵ���� �޾ƿͼ� 
        //��ù��°�� ���� �̸��̶� ������ ������ Ȯ���Ѵ�.
        //������ �ٽ� �ݺ��ؼ� �ι�° �������� �̸��̶� ������ ������ Ȯ���Ѵ�.
        // ���������� ����° �����۱��� �̸��̶� ������ ���� ��� �ϼ��������� complete������ �ڵ尪�� �����Ѵ�.
        int cnt = 0;
        List<int> listItemNum = new List<int>();
        //itemCnt���ٰ� 0���� count������ŭ �־��ش�.
        for (int i = 0; i < excelData.ItemComebine.Count; i++)
        {
            listItemNum.Add(i);
        }
        CheckItems(listItemNum, cnt);
    }
    private void CheckItems(List<int> listItemNum, int cnt)
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
            return;
        }
        else
        {
            cnt++;
            CheckItems(storeInt, cnt);
        }

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
                itemCode = excelData.ItemComebine[listItemNum[num]].Item1;
                itemCnt = excelData.ItemComebine[listItemNum[num]].Count1;
                break;
            case 1:
                itemCode = excelData.ItemComebine[listItemNum[num]].Item2;
                itemCnt = excelData.ItemComebine[listItemNum[num]].Count2;
                break;
            case 2:
                itemCode = excelData.ItemComebine[listItemNum[num]].Item3;
                itemCnt = excelData.ItemComebine[listItemNum[num]].Count3;
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
}
