using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    //����� �����۵�
    public DropItemsDB dropItemsDB;

    public void DropItems()
    {
        Vector3 v3 = transform.position;
        //Debug.Log(dropItemsDB.ItemsCode[0]);
        //�迭�� ����� �����۵��� for���� ���ؼ� �ݺ� ȣ�� �ش�Ǵ� �����۵��� itemTable���� ������ ����
        for(int i=0; i<dropItemsDB.ItemsCode.Length; i++)
        {
            for(int j=0; j < dropItemsDB.ItemsCount[i]; j++)
            {
                float numX=(Random.Range(0, 1)==1)? 0.1f: -0.1f;
                float numZ=(Random.Range(0, 1)==1)? 0.1f: -0.1f;
                v3.x -= numX;
                v3.z-= numZ;
                Instantiate(GameManager.Instance.itemTable.GetDBGameObject(dropItemsDB.ItemsCode[i]), v3, Quaternion.identity);
            }
        }
    }
}
