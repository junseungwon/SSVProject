using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabInteractive_InstallObj : GrabInteractive_JSW
{

    private GameObject virtualInstallObj = null;
    private bool isGrab = false;
    private IEnumerator corutine;
    public float objHeight = 0.03f;
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
        else
        {
            virtualInstallObj.SetActive(true);
        }
        StartCoroutine(corutine);
    }
    //1.컨트롤은 가능한데 y값만 플레이어 위치값으로 
    private IEnumerator TransrateInstanceObj()
    {
        RaycastHit hit;
        float yPos = 0;
        Debug.Log(GameManager.Instance.player.transform.position);
        if (Physics.Raycast(GameManager.Instance.player.transform.position, Vector3.down, out hit))
        {
            yPos= hit.point.y;
        }
        while (isGrab)
        { 
            // 타겟 오브젝트의 새로운 위치를 계산합니다.
            Vector3 newPosition = transform.position + transform.forward*0.5f;

            // 타겟 오브젝트의 위치를 업데이트합니다.
            newPosition.y = yPos+objHeight;
            virtualInstallObj.transform.position = newPosition;
            yield return new WaitForSeconds(0.01f);
        }
        SetItem();
    }
    private void SetItem()
    {
     
        transform.position = virtualInstallObj.transform.position;
        virtualInstallObj.SetActive(false);
    }
}
