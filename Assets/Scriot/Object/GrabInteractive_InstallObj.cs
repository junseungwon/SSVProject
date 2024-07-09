using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabInteractive_InstallObj : GrabInteractive_JSW
{

    private GameObject virtualInstallObj = null;
    private bool isGrab = false;
    private IEnumerator corutine;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        grabbable.selectEntered.AddListener(VirtualInstallation);
        grabbable.selectExited.AddListener(SelectExitInterActive);
        corutine = TransrateInstanceObj();
    }

    private void SelectExitInterActive(SelectExitEventArgs arg0)
    {
        isGrab = false;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    //임시로 설치 위치를 보여줌
    private void VirtualInstallation(SelectEnterEventArgs arg0)
    {
        isGrab = true;
        if (virtualInstallObj == null)
        {
            virtualInstallObj = Instantiate(this.gameObject);
        }
        StartCoroutine(corutine);
    }
    //1. 바닥에만 무조건 설치 가능
    //2. 물체 위에 
    //3. 공중에 설치가 가능함
    private IEnumerator TransrateInstanceObj()
    {
        while (isGrab)
        {
            // 타겟 오브젝트의 새로운 위치를 계산합니다.
            Vector3 newPosition = transform.position+ transform.forward * 2;

            // 타겟 오브젝트의 위치를 업데이트합니다.
            virtualInstallObj.transform.position = newPosition;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
