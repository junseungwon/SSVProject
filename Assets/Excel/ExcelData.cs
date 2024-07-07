using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class ExcelData : ScriptableObject
{
	public List<ExcelDataClass> Sheet1; // Replace 'EntityType' to an actual type that is serializable.
}
