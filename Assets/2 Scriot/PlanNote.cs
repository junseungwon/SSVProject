using System.Collections;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class PlanNote : MonoBehaviour
{
    [Header("PlanP 퀘스트 TEXT")]
    public string[] planNoteP;
    [Header("PlanN 퀘스트 TEXT")]
    public string[] planNoteN;
    [Header("PlanA 퀘스트 TEXT")]
    public string[] planNoteA;
    [Header("PlanL 퀘스트 TEXT")]
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
    //PLAN TEXT부분을 GRIDLayer로 변경
    //PLAN에 새로 퀘스트 메세지를 추가하기
    public void AddQuset()
    {
        stepCnt++;
        GameObject childObj = planTextUi.transform.GetChild(stepCnt).gameObject;
        childObj.SetActive(true);
    }
    //퀘스트가 완료 표시로 변환
    public void CompleteQuset()
    {
        TextMeshProUGUI uiText = planTextUi.transform.GetChild(stepCnt).GetComponent<TextMeshProUGUI>();
        //cnt를 받아와서 해당되는 text를 변경한다.
        //변경한 내용을 UITEXT로 다시 바꾼다.
        uiText.text = ChangeFinshText(uiText.text);
        uiText.fontStyle = FontStyles.Italic;
        //다음 퀘스트를 부여한다.
        AddQuset();
    }
    //TEXT내용에서 0을 찾아서 1로 바꿔서 리턴
    private string ChangeFinshText(string input)
    {
        string pattern = @"(\d+)/(\d+)"; // 숫자 패턴

        // 정규 표현식을 사용하여 숫자 추출
        string result = Regex.Replace(input, pattern, match =>
        {
            // 첫 번째 숫자는 0, 두 번째 숫자를 추출
            string secondNumber = match.Groups[2].Value;
            return $"{secondNumber}/{secondNumber}"; // 두 번째 숫자로 교체
        });

        return result;
    }
    //PLAN노트의 단계에서 해당되는 STRING배열을 RETURN 
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
                Debug.Log("해당되는 단계는 Plan노트에서 찾을 수 없습니다.");
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
