using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
public class PlayerController : MonoBehaviour
{
    [Header("ActionMap")]
    public InputActionAsset inputActions;

    [Header("Teleport��Ʈ�ѷ�")]
    public GameObject teleportRayController;

    [Header("Ray��Ʈ�ѷ�")]
    public GameObject rightRayController;
    public GameObject leftRayController;

    [Header("Dir��Ʈ�ѷ�")]
    public CustomDirController leftDirController;
    public CustomDirController rightDirController;


    private bool isTeleport = false;
    private void Awake()
    {
        GameManager.Instance.PlayerController = this;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(teleportRayController.GetComponent<XRInteractorLineVisual>().reticle.gameObject);
        DontDestroyOnLoad(teleportRayController.GetComponent<XRInteractorLineVisual>().blockedReticle.gameObject);
    }
    private void Start()
    {
        Setting();
    }
    private void Setting()
    {

    }
    // Update is called once per frame
    void Update()
    {
        ActivateTeleport();
        UIRayControllerActive();
    }
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

    //teleportȰ��ȭ
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



    ////uirayȰ��ȭ
    //private void raycheckui()
    //{
    //    if (physics.raycast(starttf.position, starttf.forward, out hitinfo, mathf.infinity))
    //    {
    //        if ((hitinfo.collider.tag == "ui") && isteleport == false)
    //        {
    //            rightraycontroller.setactive(true);
    //        }
    //    }
    //    else
    //    {
    //        rightraycontroller.setactive(false);
    //    }
    //}
    //2 5�߿� 0
    //UIRayControllerȰ��ȭ

//#region Action-based
//[SerializeField] private InputActionReference m_LeftSelectRef;

////����Ҷ� select�κп� 
//private void OnEnable()
//{
//    //inpiutaction
//    inputActions.Enable();
//    //��� 1
//    inputActions.FindActionMap("XRI LeftHand").FindAction("Select").performed += SelectLeft;
//    inputActions.FindActionMap("XRI LeftHand").FindAction("Select").canceled += CancelSelectLeft;

//    // ���2
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

