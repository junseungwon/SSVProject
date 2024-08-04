using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MakeItemSocket : MonoBehaviour
{
    public XRSocketInteractor socket;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(socket);
        socket.selectEntered.AddListener(PutItem);
    }
    private void PutItem(SelectEnterEventArgs arg0)
    {
        Debug.Log("아이템을 넣었습니다.");
        GameObject obj= socket.selectTarget.gameObject;
        if (obj.tag == "Item" || obj.tag == "IsAbsorbItem")
        {
            int parentIndex = socket.transform.parent.GetSiblingIndex();
            int codeNum = int.Parse(obj.name);
            //other.GetComponent<CustomDirController>().grabObject = null;
            bool isBool = GameManager.Instance.MakeItemBox.PutItem(obj, socket.transform.parent.GetSiblingIndex());
            if (isBool)
            {
                Debug.Log(parentIndex);
                obj.GetComponent<ItemGrabInteractive>().makeBoxParentNum = parentIndex;
                obj.GetComponent<Rigidbody>().isKinematic = true;
            }
        }

    }
}
