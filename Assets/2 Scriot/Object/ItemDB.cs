using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDB", menuName = "Scriptable Object/ItemDB", order = int.MaxValue)]
public class ItemDB : ScriptableObject
{
    //������ �ڵ� // ������ �̸� // ������ ����
    [SerializeField]
    private int itemCode;
    public int ItemsCode { get { return itemCode; } }

    [SerializeField]
    private string itemName;
    public string ItemName { get { return itemName; } }

    [SerializeField]
    private string itemState;
    public string ItemsState { get { return itemState; } }
    [SerializeField]
    private ItemTypes itemType;
    public ItemTypes ItemType { get { return itemType; } }

}
public enum ItemTypes
{
    Consume, Food, Tool, Install
}