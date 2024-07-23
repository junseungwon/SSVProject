using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
public class PlayerController : MonoBehaviour
{
    public InputActionAsset inputActions;

    public GameObject teleportRayController;
    public GameObject rightRayController;
    public GameObject leftRayController;
    public GameObject rightDirController;


    public Transform startTf;
    private RaycastHit hitInfo;


    private bool isTeleport = false;
    private void Awake()
    {
        GameManager.Instance.PlayerController = this;
       
    }
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        ActivateTeleport();
        UIRayControllerActive();
    }
    //uiray활성화
    private void RayCheckUi()
    {
        if (Physics.Raycast(startTf.position, startTf.forward, out hitInfo, Mathf.Infinity))
        {
            if ((hitInfo.collider.tag == "UI") && isTeleport == false)
            {
                rightRayController.SetActive(true);
            }
        }
        else
        {
            rightRayController.SetActive(false);
        }
    }
    //2 5중에 0
    //UIRayController활성화
    private void UIRayControllerActive()
    {
        UIRayControllerActive(2, leftRayController);
        UIRayControllerActive(5, rightRayController);
    }
    private void UIRayControllerActive(int num, GameObject obj)
    {
        float selectBt = inputActions.actionMaps[num].actions[2].ReadValue<float>();
        if (selectBt == 1)
        {
            obj.SetActive(true);
           // inputActions.actionMaps[5].actions[0].AddBinding
        }
        else
        {
            obj.SetActive(false);
        }
    }

    //teleport활성화
    private void ActivateTeleport()
    {
        if (inputActions.actionMaps[5].actions[8].ReadValue<Vector2>().y >= 0.8)
        {
            teleportRayController.SetActive(true);
            isTeleport = true;
        }
        else
        {
            if(isTeleport == true)
            {
                transform.position = teleportRayController.GetComponent<XRInteractorLineVisual>().reticle.transform.position;
                isTeleport = false;
            }
            teleportRayController.SetActive(false);
            isTeleport = false;
        }
    }
}




//#region Action-based
//[SerializeField] private InputActionReference m_LeftSelectRef;

////사용할때 select부분에 
//private void OnEnable()
//{
//    //inpiutaction
//    inputActions.Enable();
//    //방법 1
//    inputActions.FindActionMap("XRI LeftHand").FindAction("Select").performed += SelectLeft;
//    inputActions.FindActionMap("XRI LeftHand").FindAction("Select").canceled += CancelSelectLeft;

//    // 방법2
//    m_LeftSelectRef.action.performed += SelectLeft;
//    m_LeftSelectRef.action.canceled += CancelSelectLeft;
//}
//private void OnDisable()
//{

//    inputActions.Disable();
//    m_LeftSelectRef.action.performed -= SelectLeft;
//    m_LeftSelectRef.action.canceled -= CancelSelectLeft;
//}


//private void SelectLeft(InputAction.CallbackContext obj)
//{
//    Debug.Log("Select Left");

//}
//private void CancelSelectLeft(InputAction.CallbackContext obj)
//{
//    Debug.Log("Cancel Select Left");

//}
//#endregion

