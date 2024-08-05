using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabInteractive_InstallObj : MonoBehaviour
{
    //설치한 아이템은 이동 수정을 못하도록 설정 수정이 필요함
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

    //임시로 설치 위치를 보여줌
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
    //1.컨트롤은 가능한데 y값만 플레이어 위치값으로 
    private IEnumerator TransrateInstanceObj()
    {
        Debug.Log("발동되엇습니다");
        RaycastHit hit;
        float yPos = 0;
        if (Physics.Raycast(GameManager.Instance.player.transform.position, Vector3.down, out hit))
        {
            yPos = hit.point.y;
        }
        Vector3 newPosition = Vector3.zero;
        while (isGrab)
        {
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
        grabbable.movementType = XRBaseInteractable.MovementType.Kinematic;
        grabbable.trackPosition = false;
        grabbable.trackRotation = false;
        transform.position = virtualInstallObj.transform.position;
        transform.rotation = Quaternion.identity;
        GetComponent<ItemGrabInteractive>().AnimPlay(false);
        virtualInstallObj.SetActive(false);
        Debug.Log("설치가 되었습니다.");
    }
    //설치 아이템 설치
    private void SetItem()
    {
        //설치가 되면 해당 설치 오브젝트 위치로 바뀌고 예상 오브젝트는 비활성화
        if (isInstall)
        {
            Debug.Log(this.gameObject.name);
            
            grabbable.movementType = XRBaseInteractable.MovementType.Kinematic;
            grabbable.trackPosition = false;
            grabbable.trackRotation = false;
            transform.position = virtualInstallObj.transform.position;
            transform.rotation = Quaternion.identity;
            GetComponent<ItemGrabInteractive>().AnimPlay(false);
            Debug.Log("설치가 되었습니다.");
        }
        virtualInstallObj.SetActive(false);
    }
}
