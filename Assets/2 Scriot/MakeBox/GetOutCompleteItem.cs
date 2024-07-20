using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GetOutCompleteItem : MonoBehaviour
{
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
                GameManager.Instance.MakeItemBox.RemoveItems();
                playerGrabObj.GetComponent<Rigidbody>().isKinematic = false;
                playerGrabObj.GetComponent<Rigidbody>().useGravity = true;  
   
            }
        }
    }
}
