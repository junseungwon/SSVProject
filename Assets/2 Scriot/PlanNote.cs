using System.Collections;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class PlanNote : MonoBehaviour
{
    [Header("PlanP ����Ʈ TEXT")]
    public string[] planNoteP;
    [Header("PlanN ����Ʈ TEXT")]
    public string[] planNoteN;
    [Header("PlanA ����Ʈ TEXT")]
    public string[] planNoteA;
    [Header("PlanL ����Ʈ TEXT")]
    public string[] planNoteL;

    private int planCnt = 0;
    private int stepCnt = 0;

    [SerializeField]
    private GameObject planTextUi = null;
    private void Awake()
    {
        GameManager.Instance.PlanNote = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        ResetTextSetting();
        StartCoroutine(corutineTime());
    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator corutineTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            CompleteQuset();

        }
    }
    //PLAN TEXT�κ��� GRIDLayer�� ����
    //PLAN�� ���� ����Ʈ �޼����� �߰��ϱ�
    public void AddQuset()
    {
        stepCnt++;
        GameObject childObj = planTextUi.transform.GetChild(stepCnt).gameObject;
        childObj.SetActive(true);
    }
    //����Ʈ�� �Ϸ� ǥ�÷� ��ȯ
    public void CompleteQuset()
    {
        TextMeshProUGUI uiText = planTextUi.transform.GetChild(stepCnt).GetComponent<TextMeshProUGUI>();
        //cnt�� �޾ƿͼ� �ش�Ǵ� text�� �����Ѵ�.
        //������ ������ UITEXT�� �ٽ� �ٲ۴�.
        uiText.text = ChangeFinshText(uiText.text);
        uiText.fontStyle = FontStyles.Italic;
        //���� ����Ʈ�� �ο��Ѵ�.
        AddQuset();
    }
    //TEXT���뿡�� 0�� ã�Ƽ� 1�� �ٲ㼭 ����
    private string ChangeFinshText(string input)
    {
        string pattern = @"(\d+)/(\d+)"; // ���� ����

        // ���� ǥ������ ����Ͽ� ���� ����
        string result = Regex.Replace(input, pattern, match =>
        {
            // ù ��° ���ڴ� 0, �� ��° ���ڸ� ����
            string secondNumber = match.Groups[2].Value;
            return $"{secondNumber}/{secondNumber}"; // �� ��° ���ڷ� ��ü
        });

        return result;
    }
    //PLAN��Ʈ�� �ܰ迡�� �ش�Ǵ� STRING�迭�� RETURN 
    private string[] CheckPlanNoteTextArray()
    {
        switch (planCnt)
        {
            case (int)PlanNoteStep.P:
                return planNoteP;
            case (int)PlanNoteStep.N:
                return planNoteN;
            case (int)PlanNoteStep.A:
                return planNoteA;
            case (int)PlanNoteStep.L:
                return planNoteL;

            default:
                Debug.Log("�ش�Ǵ� �ܰ�� Plan��Ʈ���� ã�� �� �����ϴ�.");
                return null;
        }
    }
    private void ResetTextSetting()
    {
        string[] textArray = CheckPlanNoteTextArray();
        for (int i = 0; i < textArray.Length; i++)
        {
            GameObject childObj = planTextUi.transform.GetChild(i).gameObject;
            if (i == 0) { childObj.SetActive(true); } else { childObj.SetActive(false); }
            childObj.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
            childObj.GetComponent<TextMeshProUGUI>().text = textArray[i];
        }
    }
    private void NextPlanNoteStep()
    {
        ResetTextSetting();
        planCnt++;
        stepCnt = 0;
    }
}
public enum PlanNoteStep
{
    P, N, A, L
}
