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

    //�ӽ÷� ��ġ ��ġ�� ������
    private void VirtualInstallation(SelectEnterEventArgs arg0)
    {
        isGrab = true;
        if (virtualInstallObj == null)
        {
            virtualInstallObj = Instantiate(this.gameObject);
        }
        StartCoroutine(corutine);
    }
    //1. �ٴڿ��� ������ ��ġ ����
    //2. ��ü ���� 
    //3. ���߿� ��ġ�� ������
    private IEnumerator TransrateInstanceObj()
    {
        while (isGrab)
        {
            // Ÿ�� ������Ʈ�� ���ο� ��ġ�� ����մϴ�.
            Vector3 newPosition = transform.position+ transform.forward * 2;

            // Ÿ�� ������Ʈ�� ��ġ�� ������Ʈ�մϴ�.
            virtualInstallObj.transform.position = newPosition;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
