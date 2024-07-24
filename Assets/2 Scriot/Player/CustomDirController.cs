using SerializableCallback;
using System;
using System.Collections;
using System.Collections.Generic;
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
       GameObject obj= selectTarget.gameObject;
    }
    //selecttarget
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
