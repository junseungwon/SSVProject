using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    //드랍할 아이템들
    public DropItemsDB dropItemsDB;
    private void Start()
    {
        DropItems();
    }
    public void DropItems()
    {
        Vector3 v3 = transform.position;
        //Debug.Log(dropItemsDB.ItemsCode[0]);
        //배열에 저장된 아이템들을 for문을 통해서 반복 호출 해당되는 아이템들을 itemTable에서 가져와 생성
        for(int i=0; i<dropItemsDB.ItemsCode.Length; i++)
        {
            for(int j=0; j < dropItemsDB.ItemsCount[i]; j++)
            {
                float numX=(Random.Range(0, 1)==1)? 0.02f: -0.02f;
                float numZ=(Random.Range(0, 1)==1)? 0.02f: -0.02f;
                v3.x += numX;
                v3.z += numZ;
                Instantiate(GameManager.Instance.itemTable.GetDBGameObject(dropItemsDB.ItemsCode[i]), v3, Quaternion.identity).name = dropItemsDB.ItemsCode[i].ToString();
            }
        }
    }
}
