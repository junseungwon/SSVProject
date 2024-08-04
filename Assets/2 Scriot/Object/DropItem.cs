using UnityEngine;

public class DropItem : MonoBehaviour
{
    //����� �����۵�
    public DropItemsDB dropItemsDB;
    private void Start()
    {
        DropItems();
    }
    public void DropItems()
    {
        Vector3 v3 = new Vector3(transform.position.x, 0.1f, transform.position.z);
        //Debug.Log(dropItemsDB.ItemsCode[0]);
        //�迭�� ����� �����۵��� for���� ���ؼ� �ݺ� ȣ�� �ش�Ǵ� �����۵��� itemTable���� ������ ����
        for (int i = 0; i < dropItemsDB.ItemsCode.Length; i++)
        {
            for (int j = 0; j < dropItemsDB.ItemsCount[i]; j++)
            {
                float numX = (Random.Range(0, 1) == 1) ? 0.05f : -0.05f;
                float numZ = (Random.Range(0, 1) == 1) ? 0.05f : -0.05f;
                v3.x += numX;
                v3.z += numZ;

                //������ ����
                GameObject obj = Instantiate(GameManager.Instance.itemTable.GetDBGameObject(dropItemsDB.ItemsCode[i]), v3, Quaternion.identity);
                obj.GetComponent<ItemGrabInteractive>().TagIsAbSorb();
                //�������� �ִϸ��̼��� ������
                obj.GetComponent<ItemGrabInteractive>().AnimPlay(true);
                //�������� �ִϸ��̼� ������
                //������ �̸� ����
                obj.name = dropItemsDB.ItemsCode[i].ToString();

                //�±׸� ��������� ���������� �ٲ�
                GameManager.Instance.itemTable.IsAbsorbItemsTagChange(obj);
                obj.GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }
}
