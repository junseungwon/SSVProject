using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabInteractive_InstallObj : MonoBehaviour
{
    //설치한 아이템은 이동 수정을 못하도록 설정 수정이 필요함
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

    //임시로 설치 위치를 보여줌
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
    //1.컨트롤은 가능한데 y값만 플레이어 위치값으로 
    private IEnumerator TransrateInstanceObj()
    {
        Setting();
        Debug.Log("발동되엇습니다");
        RaycastHit hit;
        float yPos = GameManager.Instance.player.transform.position.y;
        Vector3 newPosition = Vector3.zero;
        while (isGrab)
        {
            yPos = GameManager.Instance.player.transform.position.y;
            // 타겟 오브젝트의 새로운 위치를 계산합니다.
            newPosition = transform.position + transform.forward * 0.5f;

            // 타겟 오브젝트의 위치를 업데이트합니다.
            newPosition.y = yPos + objHeight;
            virtualInstallObj.transform.position = newPosition;
            
            //버튼 클릭여부 검사
            InstallObject();

            yield return new WaitForSeconds(0.01f);
        }
        SetItem();
    }
    //설치버튼 a버튼 클릭 여부 검사
    private void InstallObject()
    {
        float selectNum = GameManager.Instance.PlayerController.inputActions.actionMaps[4].actions[11].ReadValue<float>();
        //버튼 눌렸으면 물체를 설치함
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
        Debug.Log("설치를 작동시켰습니다.");
    }
    //설치 아이템 설치
    private void SetItem()
    {
        //설치가 되면 해당 설치 오브젝트 위치로 바뀌고 예상 오브젝트는 비활성화
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
            Debug.Log("설치가 되었습니다.");
        }
        virtualInstallObj.SetActive(false);
    }
}
