using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    [SerializeField]
    private GameObject questWindow;
    [SerializeField]
    private TextMeshProUGUI questText;
    private int _textRowCount;
    [SerializeField]
    private GameObject _content;
    [SerializeField]
    private GameObject _questPrefab;

    private TextMeshProUGUI _stateText;
    private TextMeshProUGUI _descText;
    private TextMeshProUGUI _rewardText;

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
        if (Managers.Game.questDatas.Count == 0)
            QuestInit();
    }

    private void QuestInit()
    {
        if (Managers.Game.questDatas.Count > 0) return;

        Managers.Game.questDatas.Add(QuestName.TutorialComplete,
            new QuestData("튜토리얼 클리어하기", "체력 +1", new Reward(QuestReward.HealthUp, 1f), true));
        Managers.Game.questDatas.Add(QuestName.StageFirstComplete,
            new QuestData("스테이지 1회 클리어하기", "스킬 게이지 증가량 +10%", new Reward(QuestReward.SkillGaugIncrementUp, 0.1f)));
        Managers.Game.questDatas.Add(QuestName.Stage100Combo,
            new QuestData("스테이지 콤보 100회 이상으로 클리어하기", "스킬 속도 -10%", new Reward(QuestReward.SkillSpeedDown, -0.1f)));
        Managers.Game.questDatas.Add(QuestName.MaxHealthClear,
            new QuestData("체력을 잃지 않고 스테이지 1회 클리어하기", "스킬 거리 +10%", new Reward(QuestReward.SkillExtendedDistance, 0.1f)));
        Managers.Game.questDatas.Add(QuestName.SpeedUpClear,
            new QuestData("스테이지 1.5배속 이상으로 클리어하기", "스킬 게이지 증가량 +15%", new Reward(QuestReward.SkillGaugIncrementUp, 0.15f)));
        Managers.Game.questDatas.Add(QuestName.SuddenModeClear,
            new QuestData("스테이지 돌발모드 클리어하기", "스킬 거리 +15%", new Reward(QuestReward.SkillExtendedDistance, 0.15f)));
    }

    public void OpenQuest()
    {
        if (questWindow.activeSelf)
        {
            questWindow.GetComponent<UI_Popup_Quest>().OffPopup();
            Managers.Sound.ContinueBGM();
            return;
        }

        if (Managers.Popup.IsPopupActive()) return;

        int index = 0;
        foreach (var datas in Managers.Game.questDatas)
        {
            QuestData data = datas.Value;
            GameObject questObj;
            if (_content.transform.childCount > index)
                questObj = _content.transform.GetChild(index).gameObject;
            else
                questObj = Instantiate(_questPrefab, _content.transform);

            questObj.GetComponent<Quest>().key = datas.Key;

            _stateText = questObj.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            _descText = questObj.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            _rewardText = questObj.transform.GetChild(3).GetComponent<TextMeshProUGUI>();

            _stateText.text = "도전중";
            if (data.IsClear && !data.IsReceive)
            {
                questObj.transform.GetChild(4).GetComponent<Button>().interactable = true;
                _stateText.text = "수령 가능";
            }
            if (data.IsReceive)
            {
                questObj.transform.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, 0.7f);
                _stateText.text = "수령 완료";
            }

            _descText.text = data.QuestDesc;
            _rewardText.text = data.RewardDesc + "\n레벨 +1";

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
        Managers.Data.SaveQuestData();
    }

    public void QuestNotice()
    {
        QuestTextClear();
        foreach (var datas in Managers.Game.questDatas)
        {
            QuestData data = datas.Value;
            if(data.IsClear && !data.IsReceive)
            {
                if(_textRowCount < 3)
                    questText.text += new string($"\"{data.QuestDesc}\" 퀘스트 보상 수령 가능!\n");

                _textRowCount++;
            }
        }
    }

    public void QuestTextClear()
    {
        questText.text = "";
        _textRowCount = 0;
    }

}
