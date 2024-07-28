using System;
using UnityEngine;

public class GetOutCompleteItem : MonoBehaviour
{
    public Action getAction = null;
    //손으로 잡고 나갔으면 
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CustomDirController>() != null)
        {
            //해당되는 컨트롤러가 잡기 상태가 아닌경우
            //스크립트에 저장된 물체를 가져와서 예시용 물건을 사용한다.
            GameObject playerGrabObj = other.GetComponent<CustomDirController>().grabObject;
            float selectNum = GameManager.Instance.PlayerController.inputActions.actionMaps[5].actions[0].ReadValue<float>();
            if (selectNum > 0.5 && playerGrabObj != null)
            {
                //물건을 잡았을 때 잡은 물건의 개수가 감소할건데 물건의 개수가 0이 아닌지 확인해야함
                int count = GameManager.Instance.MakeItemBox.completeItemCount;
                if (count > 1)
                {
                    //가져간것을 그대로 복사해서 원래 자리로 배치함
                    GameManager.Instance.MakeItemBox.GetOutCompleteItem();
                }
                else
                {
                    GameManager.Instance.MakeItemBox.RemoveCompleteItem();
                    GameManager.Instance.MakeItemBox.TextCompleteItemNumUIChange();

                }
                GameManager.Instance.MakeItemBox.RemoveItems();
                playerGrabObj.GetComponent<Rigidbody>().isKinematic = false;
                playerGrabObj.GetComponent<Rigidbody>().useGravity = true;
                if (playerGrabObj.name == "180")
                {
                    //인벤토리 코드번호 180번
                    if (getAction != null)
                    {
                        getAction.Invoke();
                    }
                }
            }
        }
    }
}
//꺼냈을 땜 만약 인벤토리라면 챕터 1에 인벤토리 제작완료 되었다고 추가
