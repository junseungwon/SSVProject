using UnityEngine;

public class ItemBoxColider : MonoBehaviour
{
    /// <summary>
    /// �÷��̾� ���� itembox�� ��� �÷��̾��� ���� ��� ���°� �ƴѰ��(grab��ư�� ������ ���� ����)
    ///-> ������ �ȿ� �־���

    ///trigger������� ������ ������ ������ ���� �����ΰ�� �ٸ� ��ü�� hover�� �ߵ��� ����.
    ///-> trigger������� ������
    /// </summary>


    //colider�� �浹���� �� ��� �ִ� �������� ����� 
    //��� ���� ���� �����ΰ�� Grap��ü�� null�� ���°� �ƴϸ� �׹�ü�� itemBox�� ����

    public void OnTriggerStay(Collider other)
    {
        if (other.tag=="LeftHand"||other.tag =="RightHand")
        {
            //�ش�Ǵ� ��Ʈ�ѷ��� ��� ���°� �ƴѰ��
            //��ũ��Ʈ�� ����� ��ü�� �����ͼ� ���ÿ� ������ ����Ѵ�.
            GameObject playerGrabObj = other.GetComponent<CustomDirController>().grabObject;
            float selectNum = GameManager.Instance.PlayerController.inputActions.actionMaps[5].actions[0].ReadValue<float>();
            if (selectNum < 0.5 && playerGrabObj != null)
            {
                int parentIndex = transform.parent.GetSiblingIndex();
                int codeNum = int.Parse(playerGrabObj.name);
                other.GetComponent<CustomDirController>().grabObject = null;
                GameManager.Instance.ItemBox.PutItem(parentIndex, codeNum, playerGrabObj);
            }

            //�ش�Ǵ� ��Ʋ�η��� ��� ������ ��� 
            //grab��ü�� null�ΰ��
            //��ü�� �����ؼ� �ǳ���
        }
    }

}
