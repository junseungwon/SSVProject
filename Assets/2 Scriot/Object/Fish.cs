using UnityEngine;

public class Fish : MonoBehaviour
{
    public GetAction GetAction = null;
    public int itemCode = 0;
    public int state = 0;

    public GameObject tranObj = null;
    //��ü�� �������� �� �������� �Ҹ��� �������̸� ��� �ƴϸ� �������
    private void OnCollisionEnter(Collision collision)
    {
        //�ش� �������� ����⳪ ������ ��� ���� �����ϰ�
        if (collision.gameObject.tag == "Item")
        {
            SwichFishState(int.Parse(collision.gameObject.name), collision.gameObject);

        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    //�ش� �������� ����⳪ ������ ��� ���� �����ϰ�
    //    if(other.tag == "Item")
    //    {
    //    SwichFishState(int.Parse(other.gameObject.name), other.gameObject);

    //    }
    //}
    //����⸦ ����â���� ���̰� �� �� ���� �� �ְ� ����
    //���������� ���� 
    //��ġ ����Ⱑ ��
    //����⸦ ��ں� ��ó�� �ȾƵ� ������ õõ�� ����Ⱑ �;��(���׸��� override)
    //����Ⱑ ���;����� ���;��ٰ� ǥ�ð� ������ 
    //����⸦ �Կ��ٰ� ������ �Ծ���

    private void SwichFishState(int codeName, GameObject obj)
    {
        switch (state)
        {  
            case (int)FishState.Normal:
                DeadFish(codeName, obj);
                break;
            case (int)FishState.Dead:
                SkewerFish(codeName, obj);
                break;
            case (int)FishState.Skewer:
                BarbecuedFish(codeName, obj);
                break;
        }
    }
    private void Dead()
    {
        Debug.Log(gameObject.name);
        tranObj.transform.localRotation = Quaternion.Euler(0,180,0);
    }
    private void DeadFish(int codeName, GameObject obj)
    {
        //�⺻ ������ ��� ���� ������ ����
        if (codeName == 150)
        {
            Debug.Log("����⸦ �����̽��ϴ�.");
            //�ش� �ڵ带 ����
            itemCode = codeName;
            //�񸶸��̶� ��θ� ��ġ ���� 
            //�̺�Ʈ�� ����
            if (GetAction != null)
            {
                GetAction();
            }
            GetAction = null;
            state = (int)FishState.Dead;
            Dead();
            transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
    //���� �񸮴� ��ì�� ����
    private void SkewerFish(int codeName, GameObject obj)
    {
        //���������� ����
        if (codeName == 100)
        {
            Debug.Log("���������� ����Ⱑ �Ⱦ���");
            obj.name = "250";
            transform.parent = obj.transform;
            //����Ⱑ ����� �ڽ����� �̵��ϰ� rotation�̶� position�����
            transform.localPosition = new Vector3(0, 1.5f, 0.139f);
            transform.localRotation = Quaternion.Euler(0, 0, -180);
            state = (int)FishState.Skewer;
        }
    }
    private void BarbecuedFish(int codeName, GameObject obj)
    {
        Debug.Log(codeName);
        //��ںҿ� ���� 
        if (codeName == 160)
        {
            Debug.Log("������ �� ����");
            //������� ���׸����� �����
            Material[] materials = transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().materials;
            //��ü�� ���İ��� �����ϰ� �ٽ� return
            Color color = materials[1].color;
            color.a = 1f;
            materials[1].color = color;
            transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().materials = materials;
            //�̺�Ʈ�� ����
            state = (int)FishState.Barbecued;
            PlayerMouth mouth = GameManager.Instance.player.transform.GetChild(5).gameObject.GetComponent<PlayerMouth>();
            mouth.gameObject.SetActive(true);
        }
    }
    private enum FishState
    {
        Normal, Dead, Skewer, Barbecued, Eat
    }
}
