using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabInteractive_InstallObj : MonoBehaviour
{
    //��ġ�� �������� �̵� ������ ���ϵ��� ���� ������ �ʿ���
    private GameObject virtualInstallObj = null;
    private bool isGrab = false;
    private bool isInstall = false;
    private IEnumerator corutine;
    public float objHeight = 0.03f;
    public GetAction eventAction = null;
    private XRGrabInteractable grabbable;
    // Start is called before the first frame update
    void Start()
    {
        grabbable = transform.GetComponent<ItemGrabInteractive>().grabbable;
        grabbable.selectEntered.AddListener(VirtualInstallation);
        grabbable.selectExited.AddListener(SelectExitInterActive);
        corutine = TransrateInstanceObj();
        //transform.GetChild(0).GetComponent<Animator>().SetBool("Play", true);
    }

    private void SelectExitInterActive(SelectExitEventArgs arg0)
    {
        isGrab = false;
    }

    //�ӽ÷� ��ġ ��ġ�� ������
    private void VirtualInstallation(SelectEnterEventArgs arg0)
    {
        isGrab = true;
        if (virtualInstallObj == null)
        {
            virtualInstallObj = Instantiate(this.gameObject);
            virtualInstallObj.name = this.gameObject.name;
        }
        else
        {
            virtualInstallObj.SetActive(true);
        }
        StartCoroutine(TransrateInstanceObj());
    }
    //1.��Ʈ���� �����ѵ� y���� �÷��̾� ��ġ������ 
    private IEnumerator TransrateInstanceObj()
    {
        Debug.Log("�ߵ��Ǿ����ϴ�");
        RaycastHit hit;
        float yPos = 0;
        if (Physics.Raycast(GameManager.Instance.player.transform.position, Vector3.down, out hit))
        {
            yPos = hit.point.y;
        }
        Vector3 newPosition = Vector3.zero;
        while (isGrab)
        {
            // Ÿ�� ������Ʈ�� ���ο� ��ġ�� ����մϴ�.
            newPosition = transform.position + transform.forward * 0.5f;

            // Ÿ�� ������Ʈ�� ��ġ�� ������Ʈ�մϴ�.
            newPosition.y = yPos + objHeight;
            virtualInstallObj.transform.position = newPosition;
            
            //��ư Ŭ������ �˻�
            InstallObject();

            yield return new WaitForSeconds(0.01f);
        }
        SetItem();
    }
    //��ġ��ư a��ư Ŭ�� ���� �˻�
    private void InstallObject()
    {
        float selectNum = GameManager.Instance.PlayerController.inputActions.actionMaps[4].actions[11].ReadValue<float>();
        //��ư �������� ��ü�� ��ġ��
        if (selectNum > 0.8f)
        {
            if (eventAction != null)
            {
                eventAction();
            }
            isInstall = true;
            isGrab = false;
        }
    }

    public void InstallValue()
    {
        isInstall = true;
        isGrab = false;
        grabbable.movementType = XRBaseInteractable.MovementType.Kinematic;
        grabbable.trackPosition = false;
        grabbable.trackRotation = false;
        transform.position = virtualInstallObj.transform.position;
        transform.rotation = Quaternion.identity;
        GetComponent<ItemGrabInteractive>().AnimPlay(false);
        virtualInstallObj.SetActive(false);
        Debug.Log("��ġ�� �Ǿ����ϴ�.");
    }
    //��ġ ������ ��ġ
    private void SetItem()
    {
        //��ġ�� �Ǹ� �ش� ��ġ ������Ʈ ��ġ�� �ٲ�� ���� ������Ʈ�� ��Ȱ��ȭ
        if (isInstall)
        {
            Debug.Log(this.gameObject.name);
            
            grabbable.movementType = XRBaseInteractable.MovementType.Kinematic;
            grabbable.trackPosition = false;
            grabbable.trackRotation = false;
            transform.position = virtualInstallObj.transform.position;
            transform.rotation = Quaternion.identity;
            GetComponent<ItemGrabInteractive>().AnimPlay(false);
            Debug.Log("��ġ�� �Ǿ����ϴ�.");
        }
        virtualInstallObj.SetActive(false);
    }
}
