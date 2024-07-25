using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DropItem", menuName = "Scriptable Object/DropItem", order = int.MaxValue)]
public class DropItemsDB :ScriptableObject
{
    [SerializeField]
    private int[] itemsCode;
    public int[] ItemsCode { get { return itemsCode; } }

    [SerializeField]
    private int[] itemsCount;

    public int[] ItemsCount { get { return itemsCount; } }
  
}
