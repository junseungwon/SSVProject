using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class ExcelData : ScriptableObject
{
	public List<ExcelDataClass> ItemDB; // Replace 'EntityType' to an actual type that is serializable.
	public List<ItemCombineData> ItemComebine; // Replace 'EntityType' to an actual type that is serializable.
}
