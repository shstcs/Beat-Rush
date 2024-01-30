using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    [SerializeField]
    private GameObject questWindow;
    [SerializeField]
    private GameObject _content;
    [SerializeField]
    private GameObject _questPrefab;

    private TextMeshProUGUI _stateText;
    private TextMeshProUGUI _descText;
    private TextMeshProUGUI _rewardText;

    private QuestCondition _questCondition = new();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        QuestInit();
    }

    private void QuestInit()
    {
        if (Managers.Game.questDatas.Count > 0) return;

        Managers.Game.questDatas.Add(QuestName.TutorialComplete,
            new QuestData("튜토리얼 완료", "체력 + 1", new Reward(QuestReward.HealthUp, 1f)));
        Managers.Game.questDatas.Add(QuestName.StageFirstComplete,
            new QuestData("스테이지 1회 완료", "스킬 게이지 증가량 + 20%", new Reward(QuestReward.SkillGaugIncrementUp, 0.2f)));
        Managers.Game.questDatas.Add(QuestName.Stage100Combo,
            new QuestData("스테이지 콤보 100회 이상", "스킬 속도 10% 감소", new Reward(QuestReward.SkillSpeedDown, -0.1f)));
    }

    public void OpenQuest()
    {
        if (questWindow.activeSelf)
        {
            questWindow.GetComponent<UI_Popup_Quest>().closeWindow();
            Managers.Sound.ContinueBGM();
            return;
        }

        int index = 0;
        foreach (var datas in Managers.Game.questDatas)
        {
            QuestData data = datas.Value;
            GameObject questObj;
            if (_content.transform.childCount > index)
                questObj = _content.transform.GetChild(index).gameObject;
            else
                questObj = Instantiate(_questPrefab, _content.transform);

            questObj.GetComponent<Quest>().Data = data;

            _stateText = questObj.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            _descText = questObj.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            _rewardText = questObj.transform.GetChild(3).GetComponent<TextMeshProUGUI>();

            _stateText.text = data.IsClear ? "완료" : "도전중";
            if (data.IsClear && !data.IsReceive)
            {
                questObj.transform.GetChild(4).GetComponent<Button>().interactable = true;
            }
            _descText.text = data.QuestDesc;
            _rewardText.text = data.RewardDesc;

            index++;
        }
        questWindow.SetActive(true);
    }

    public void SetQuestClear(QuestName questName)
    {
        if (!Managers.Game.questDatas.ContainsKey(questName))
        {
            Debug.Log("퀘스트 정보가 없습니다.");
            return;
        }

        Managers.Game.questDatas[questName].IsClear = true;
    }
}
