using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomDirController : XRDirectInteractor
{
    public GameObject grabObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    protected override void OnSelectEntered(XRBaseInteractable interactable)
    {
        Debug.Log(interactable.name+"��ü�� ��ҽ��ϴ�.");
        grabObject = interactable.gameObject;
    }
    protected override void OnSelectExited(XRBaseInteractable interactable)
    {
        Debug.Log(interactable.name + "��ü�� ���ҽ��ϴ�.");
        Invoke("GrabNull", 0.1f);
    }
    private void GrabNull()
    {
        grabObject = null;
    }
}
