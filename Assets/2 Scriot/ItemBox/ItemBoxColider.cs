using UnityEngine;

public class ItemBoxColider : MonoBehaviour
{
    /// <summary>
    /// 플레이어 손이 itembox에 닿고 플레이어의 손이 잡기 상태가 아닌경우(grab버튼을 누르지 않은 상태)
    ///-> 물건이 안에 넣어짐

    ///trigger방식으로 제작한 이유가 물건을 잡은 상태인경우 다른 물체의 hover가 발동을 안함.
    ///-> trigger방식으로 제작함
    /// </summary>


    //colider랑 충돌했을 때 잡고 있는 상태인지 물어보고 
    //잡고 있지 않은 상태인경우 Grap물체가 null이 상태가 아니면 그물체를 itemBox에 넣음

    public void OnTriggerStay(Collider other)
    {
        if (other.tag=="LeftHand"||other.tag =="RightHand")
        {
            //해당되는 컨트롤러가 잡기 상태가 아닌경우
            //스크립트에 저장된 물체를 가져와서 예시용 물건을 사용한다.
            GameObject playerGrabObj = other.GetComponent<CustomDirController>().grabObject;
            float selectNum = GameManager.Instance.PlayerController.inputActions.actionMaps[5].actions[0].ReadValue<float>();
            if (selectNum < 0.5 && playerGrabObj != null)
            {
                int parentIndex = transform.parent.GetSiblingIndex();
                int codeNum = int.Parse(playerGrabObj.name);
                other.GetComponent<CustomDirController>().grabObject = null;
                GameManager.Instance.ItemBox.PutItem(parentIndex, codeNum, playerGrabObj);
            }

            //해당되는 컨틀로러가 잡기 상태인 경우 
            //grab물체가 null인경우
            //물체를 복사해서 건내줌
        }
    }

}
