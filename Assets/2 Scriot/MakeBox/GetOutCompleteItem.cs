using System;
using UnityEngine;

public delegate void GetAction();
public class GetOutCompleteItem : MonoBehaviour
{
    public Action getAction = null;
    public GetAction dAction = null;
    //손으로 잡고 나갔으면 
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Item")
        {

        }
        if (other.GetComponent<CustomDirController>() != null)
        {
            //해당되는 컨트롤러가 잡기 상태가 아닌경우
            //스크립트에 저장된 물체를 가져와서 예시용 물건을 사용한다.
            GameObject playerGrabObj = other.GetComponent<CustomDirController>().grabObject;
            int num = (other.tag == "Left") ? 2 : 5;
            float selectNum = GameManager.Instance.PlayerController.inputActions.actionMaps[num].actions[0].ReadValue<float>();
            Debug.Log(selectNum+" 버튼이 눌러지고 있는지 확인 1이상 버튼 누름");
            if (selectNum > 0.5 && playerGrabObj != null)
            {
                //물건을 잡았을 때 잡은 물건의 개수가 감소할건데 물건의 개수가 0이 아닌지 확인해야함
                int count = GameManager.Instance.MakeItemBox.completeItemCount;
                if (count > 1)
                {
                    Debug.Log("1개 이상을 가져감");
                    //가져간것을 그대로 복사해서 원래 자리로 배치함
                    GameManager.Instance.MakeItemBox.GetOutCompleteItem();
                }
                else
                {
                    Debug.Log("0개가 됨");
                    if (dAction != null)
                    {
                        Debug.Log("델리게이트가 실행됨");
                        dAction();
                    }
                    GameManager.Instance.MakeItemBox.RemoveCompleteItem();
                }
                //부모 초기화
                Debug.Log(playerGrabObj.name);
                playerGrabObj.tag = "Item";
                playerGrabObj.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                Debug.Log(playerGrabObj.transform.localScale);
                GameManager.Instance.MakeItemBox.RemoveItems();
                playerGrabObj.GetComponent<Rigidbody>().isKinematic = false;
                playerGrabObj.GetComponent<Rigidbody>().useGravity = true;
                //action을 사용
                if (playerGrabObj.name == "180")
                {
                    Debug.Log("해당아이템은 180이 맞습니다.");
                    //인벤토리 코드번호 180번
                    if (getAction != null)
                    {
                        Debug.Log("해당 액션을실행합니다.");
                        getAction.Invoke();
                    }
                }
            }
        }
    }
}
//꺼냈을 땜 만약 인벤토리라면 챕터 1에 인벤토리 제작완료 되었다고 추가
