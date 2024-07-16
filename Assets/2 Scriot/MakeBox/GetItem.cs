using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<CustomDirController>() != null)
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
                bool isBool= GameManager.Instance.MakeItemBox.PutItem(playerGrabObj.gameObject, this.gameObject.transform.GetSiblingIndex()-1);
                if (isBool)
                {
                   // playerGrabObj.GetComponent<ItemGrabInteractive>().itemBoxParentNum = parentIndex;
                    playerGrabObj.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }
    }
}
