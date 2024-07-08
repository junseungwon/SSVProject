using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
public class PlayerController : MonoBehaviour
{
    public InputActionAsset inputActions;

    public GameObject teleportRayController;
    public GameObject rightRayController;
    public GameObject leftRayController;
    public GameObject rightDirController;


    public Transform startTf;
    private RaycastHit hitInfo;
    private Color color;
    Vector2 pos;


    public Text st;
    public Text st1;
    public Text st2;
    public Text st3;

    private bool isTeleport = false;
    private void Awake()
    {
    }
    void Start()
    {
        GameManager.Instance.PlayerController = this;

    }


    // Update is called once per frame
    void Update()
    {
     
       st3.text = "select " + inputActions.actionMaps[5].actions[0].ReadValue <float>().ToString();
        ActivateTeleport();
        RayCheckUi();
    }
    //uiray활성화
    private void RayCheckUi()
    {
        if (Physics.Raycast(startTf.position, startTf.forward, out hitInfo, Mathf.Infinity))
        {
            if ((hitInfo.collider.tag == "UI") &&isTeleport == false)
            {
               rightRayController.SetActive(true);
            }
        }
        else
        {
            rightRayController.SetActive(false);
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
            teleportRayController.SetActive(false);
            isTeleport = false;
        }
    }
    private void CheckItemBox()
    {

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

