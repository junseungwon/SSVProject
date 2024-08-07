using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomDirController : XRDirectInteractor
{
    public GameObject grabObject;
    // Start is called before the first frame update

    void Start()
    {

    }
    public void SelectEnterItem(UnityAction<SelectEnterEventArgs> selectEnteredAction)
    {

        selectEntered.AddListener(selectEnteredAction);
    }

    //B�ڵ�
    public void Hello(SelectEnterEventArgs arg0)
    {
        Debug.Log("hello");
        // selectingInteractor
        XRBaseInteractable interactable = null;
        XRDirectInteractor inter = null;
        GameObject obj = selectTarget.gameObject;
    }
    //selecttarget
    protected override void OnSelectEntered(XRBaseInteractable interactable)
    {
        //Debug.Log(interactable.name+"��ü�� ��ҽ��ϴ�.");
        if (interactable.gameObject.tag == "IsAbsorbItem")
        {
            interactable.gameObject.tag = "Item";
        }
        Debug.Log(interactable.gameObject.name);
        grabObject = interactable.gameObject;
        if (grabObject.GetComponent<ItemGrabInteractive>() != null)
        {

            grabObject.GetComponent<ItemGrabInteractive>().AnimPlay(false);
        }
    }
    protected override void OnSelectExited(XRBaseInteractable interactable)
    {
        //isInstall = true;
        //Debug.Log(interactable.name + "��ü�� ���ҽ��ϴ�.");
        if (interactable.gameObject.tag == "Item")
        {
            if (grabObject.GetComponent<ItemGrabInteractive>() != null)
            {

                if (grabObject.GetComponent<ItemGrabInteractive>().isAnim == true)
                {

                    Debug.Log(grabObject.name + " " + grabObject.GetComponent<ItemGrabInteractive>().isAnim + " ������ ���ҽ��ϴ�.");
                    grabObject.GetComponent<ItemGrabInteractive>().AnimPlay(true);

                    interactable.gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    if (interactable.gameObject.GetComponent<Rigidbody>() != null)
                    {
                        interactable.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                        interactable.gameObject.GetComponent<Rigidbody>().useGravity = true;
                    }
                }
                if (grabObject.gameObject.tag == "190")
                {
                    grabObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                }
            }
            else
            {
                Debug.Log("�ش繰ü�� �������� �ٲ��� �ʾҽ��ϴ�.");
            }
        }

        Invoke("GrabNull", 0.1f);
    }
    private void GrabNull()
    {
        grabObject = null;
    }
}
