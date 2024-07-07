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
    
    //�ӽ÷� ��ġ ��ġ�� ������
    private void VirtualInstallation(SelectEnterEventArgs arg0)
    {
        if (virtualInstallObj == null)
        {
            virtualInstallObj  = Instantiate(this.gameObject);

            // Ÿ�� ������Ʈ�� ���ο� ��ġ�� ����մϴ�.
            Vector3 newPosition = GameManager.Instance.player.transform.position + GameManager.Instance.player.transform.forward * 2;

            // Ÿ�� ������Ʈ�� ��ġ�� ������Ʈ�մϴ�.
            virtualInstallObj.transform.position = newPosition;
            virtualInstallObj.transform.parent = Camera.main.transform;

        }
    }
}
