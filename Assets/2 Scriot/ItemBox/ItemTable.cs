using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTable : MonoBehaviour
{
    //����ȭ�Ҷ� private���� ����
    public ExcelData excelDB;
    [SerializeField]
    private Dictionary<int, ItemData> itemTable = new Dictionary<int, ItemData>();
    [SerializeField]
    private GameObject[] itemObj = new GameObject[10];


    private void Start()
    {
        GameManager.Instance.itemTable = this;
        CombineExcelDB();
    }
    //���� �����͸� �޾ƿͼ� itemTable�� ����
    private void CombineExcelDB()
    {
        for(int i=0; i<excelDB.ItemDB.Count; i++)
        {
            itemTable.Add(excelDB.ItemDB[i].CodeName, new ItemData(excelDB.ItemDB[i].CodeName, excelDB.ItemDB[i].Name, excelDB.ItemDB[i].Type, excelDB.ItemDB[i].Inform, itemObj[i]));
        }
    }
    //���̺� ������ ������ return 
    public ItemData GetDBCodeNum(int codeNum)
    {
        return itemTable[codeNum];
    }
    public string GetDBName(int codeNum)
    {
        return itemTable[codeNum].name;
    }
    public string GetDBInform(int codeNum)
    {
        return itemTable[codeNum].inform;
    }
    public GameObject GetDBGameObject(int codeNum)
    {
        return itemTable[codeNum].obj;
    }
    public string GetDBType(int codeNum)
    {
        return itemTable[codeNum].type;
    }

    //��ü �̸��� ������ �ڵ� ��ȣ ã��
    public int GetCodeNumFromName(string text)
    {
        foreach(int i in itemTable.Keys)
        {
            if(text == itemTable[i].name)
            {
                return i;
            }
        }
        Debug.LogError("�ش�Ǵ� �������� �ڵ��ȣ�� ã�� �� �����ϴ�.");
        return 0;
    }

    //CombineTable�κ��� ���� �����ϴ�
    //������ �ڵ� ��ȣ, �̸�, ���� �̰� 3�� �ݺ� �������� CompleteITEM
    //���° ������ �������� �����ð���
    //order�� ������ ���� num�� 012�� �ؼ� �� element����
    public CombineItemElement GetItemElement(int order,int num)
    {
        return new CombineItemElement(excelDB.ItemComebine[order].Item1, excelDB.ItemComebine[order].Name1, excelDB.ItemComebine[order].Count1);
    }



}
//������ ������
//������ �ڵ�, ������ �̸�, ������ ����, ������ GameObject
public enum ItemDBOrder
{
    CodeNum, Name, Inform, GameObject
}
public class ItemData
{
    public int itemCode = 0;
    public string name = "";
    public string type = "";
    public string inform = "";
    public GameObject obj;
    public ItemData(int itemCode, string name, string type,string inform, GameObject obj)
    {
        this.itemCode = itemCode;
        this.name = name;
        this.type = type;
        this.inform = inform;
        this.obj = obj;
    }
}
public class CombineItemElement
{
    public int itemCode = 0;
    public string name = "";
    public int count = 0;
    public CombineItemElement(int itemCode, string name, int count)
    {
        this.itemCode=itemCode;
        this.name = name;
        this.count = count;
    }
}
