using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabInteractive_InstallObj : MonoBehaviour
{
    //��ġ�� �������� �̵� ������ ���ϵ��� ���� ������ �ʿ���
    [SerializeField]
    private GameObject prefab = null;
    private GameObject virtualInstallObj = null;
    private bool isGrab = false;
    private bool isInstall = false;
    private IEnumerator corutine;
    public float objHeight = 0.03f;
    public GetAction eventAction = null;
    private XRGrabInteractable grabbable;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grabbable = transform.GetComponent<ItemGrabInteractive>().grabbable;
       grabbable.selectEntered.AddListener(VirtualInstallation);
        grabbable.selectExited.AddListener(SelectExitInterActive);
        corutine = TransrateInstanceObj();
        //transform.GetChild(0).GetComponent<Animator>().SetBool("Play", true);
    }

    private void SelectExitInterActive(SelectExitEventArgs arg0)
    {
        GetComponent<Rigidbody>().isKinematic = false;
        isGrab = false;
    }

    //�ӽ÷� ��ġ ��ġ�� ������
    private void VirtualInstallation(SelectEnterEventArgs arg0)
    {
        Debug.Log(arg0.interactor.gameObject.name+" 11111111111111");
        if(arg0.interactor.gameObject.tag == "LHand"|| arg0.interactor.gameObject.tag == "RHand")
        {
            StartCoroutine(TransrateInstanceObj());
        }
    }
    private void Setting()
    {
        Debug.Log("setting");
        isGrab = true;
        if (virtualInstallObj == null)
        {
            virtualInstallObj = Instantiate(prefab);
            //virtualInstallObj.name = this.gameObject.name;
           // virtualInstallObj.GetComponent<BoxCollider>().isTrigger = true;
            //virtualInstallObj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }
        else
        {
            virtualInstallObj.SetActive(true);
        }
    }
    //1.��Ʈ���� �����ѵ� y���� �÷��̾� ��ġ������ 
    private IEnumerator TransrateInstanceObj()
    {
        Setting();
        Debug.Log("�ߵ��Ǿ����ϴ�");
        RaycastHit hit;
        float yPos = GameManager.Instance.player.transform.position.y;
        Vector3 newPosition = Vector3.zero;
        while (isGrab)
        {
            yPos = GameManager.Instance.player.transform.position.y;
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
        Debug.Log("��ġ�� �۵����׽��ϴ�.");
    }
    //��ġ ������ ��ġ
    private void SetItem()
    {
        //��ġ�� �Ǹ� �ش� ��ġ ������Ʈ ��ġ�� �ٲ�� ���� ������Ʈ�� ��Ȱ��ȭ
        if (isInstall)
        {;
            ItemGrabInteractive item =GetComponent<ItemGrabInteractive>();
            item.isAnim = false;
            item.AnimPlay(false);
            GetComponent<Rigidbody>().isKinematic = false;
            grabbable.movementType = XRBaseInteractable.MovementType.Kinematic;
            grabbable.trackPosition = false;
            grabbable.trackRotation = false;
            rb.isKinematic = false;
            Vector3 v3 =  new Vector3(0.2f, 0.2f, 0.2f);
            this.transform.localScale = v3;
            Debug.Log(transform.localScale.x);
            transform.position = virtualInstallObj.transform.position;
            transform.rotation = Quaternion.identity;
            Debug.Log("��ġ�� �Ǿ����ϴ�.");
        }
        virtualInstallObj.SetActive(false);
    }
}
