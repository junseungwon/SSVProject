using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabInteractive_InstallObj : GrabInteractive_JSW
{

    private GameObject virtualInstallObj = null;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        grabbable.selectEntered.AddListener(VirtualInstallation);
    }

    // Update is called once per frame
    void Update()
    {
        base .Update();
    }
    
    //임시로 설치 위치를 보여줌
    private void VirtualInstallation(SelectEnterEventArgs arg0)
    {
        if (virtualInstallObj == null)
        {
            virtualInstallObj  = Instantiate(this.gameObject);

            // 타겟 오브젝트의 새로운 위치를 계산합니다.
            Vector3 newPosition = GameManager.Instance.player.transform.position + GameManager.Instance.player.transform.forward * 2;

            // 타겟 오브젝트의 위치를 업데이트합니다.
            virtualInstallObj.transform.position = newPosition;
            virtualInstallObj.transform.parent = Camera.main.transform;

        }
    }
}
