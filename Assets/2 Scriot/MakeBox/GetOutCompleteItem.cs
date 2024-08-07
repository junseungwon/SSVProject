using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public delegate void GetAction();
public class GetOutCompleteItem : MonoBehaviour
{
    public Action getAction = null;
    public GetAction dAction = null;
    private GameObject preitem = null;
    private XRSocketInteractor socket = null;
    //손으로 잡고 나갔으면 

    private void Start()
    {
        socket = GetComponent<XRSocketInteractor>();
        //socket.selectEntered.AddListener(GetOutItem);
    }


    public void GetOutItem()
    {
        if (GameManager.Instance.MakeItemBox.completeItem != null)
        {

            GameObject playerGrabObj = GameManager.Instance.MakeItemBox.completeItem;
            Debug.Log(playerGrabObj);
            int count = GameManager.Instance.MakeItemBox.completeItemCount;
            if (count > 1)
            {
                Debug.Log("1개 이상을 가져감");
                //가져간것을 그대로 복사해서 원래 자리로 배치함
                GameManager.Instance.MakeItemBox.GetOutCompleteItem();
            }
            else if (count == 1)
            {
                Debug.Log("0개가 됨");
                if (dAction != null)
                {
                    Debug.Log("델리게이트가 실행됨");
                    dAction();
                }
                GameManager.Instance.MakeItemBox.RemoveCompleteItem();
            }
            Debug.Log("남은 아이템 수는" + count + " 아이템 이름은 " + playerGrabObj.name);
            //부모 초기화
            if (playerGrabObj.name == null)
            {
                Debug.Log("아무것도 없습니다.");
            }
            Debug.Log(playerGrabObj.name);
            if (getAction != null)
            {
                Debug.Log("액션이 있습니다.");
            }
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

            GameManager.Instance.MakeItemBox.RemoveItems();
        }
    }
}

//꺼냈을 땜 만약 인벤토리라면 챕터 1에 인벤토리 제작완료 되었다고 추가
