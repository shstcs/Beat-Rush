![Beat Rush](https://github.com/shstcs/Beat-Rush/assets/73222781/0c086a22-529a-4d60-ac1f-fb1d919a69d3)

# 프로젝트 개요
- Beat-Rush는 LowPoly 3D RPG One-Button 리듬게임입니다.
- 내일배움캠프의 최종 프로젝트.
- 2024.01 - 2024.03 ( 3 months )
- Monster, Note, Pattern, Sync 구현 **[팀장]**  

# 기술 스택
## **프레임 워크 & 언어**
- .Net 2.0
- C#
## **게임 엔진**
- Unity - 2022.3.16f1
## **버전 관리**
- GitHub
## 개발 환경
### IDE
- VisualStudio
### OS
- Window 10
- Window 11
## **데이터 관리**
- Excel (.csv)
- Json

# 진행 과정
## 1차 애자일
> ### 24.01.10 ~ 01.16
> #### 핵심 메카닉 구현
> - Pattern
> - Note
> - Map
> - Player

## 2차 애자일 
> ### 24.01.17 ~ 01.24
> #### 게임 사이클 구현
> - 로비->스테이지 플로우
> - Monster
> - Sound, Effect
> - Feedback System

## 3차 애자일
> ### 24.01.25 ~ 02.02
> #### 스테이지 확장
> - 2 스테이지 구현
> - 저장 기능
> - 튜토리얼

## 4차 애자일
> ###  24.02.05 ~ 02.15
> #### 유저 테스트 준비
> - 3 스테이지 구현
> - 퀘스트
> - 맵 디테일 추가

## 유저 테스트
> ### 24.02.19 ~ 02.29
> #### UT 피드백 반영
> - 1차, 2차 테스트 진행
> - 버그 수정 및 개선
> - 구조 리팩토링

# 세부 구현 기능 소개
## 노트 생성
- Csv파일을 사용하여 노트의 패턴을 저장.
  - 단순한 구조이기에 Json보다 csv파일이 속도 면에서 효율적일 것이라고 생각.
- 노트의 거리, 가로 위치, 노트의 종류를 저장하고 있음.
- CsVReader라는 클래스를 만들어, List<Dictionary<string,Object>>형식으로 읽어오도록 구현.  
 ![image](https://github.com/shstcs/Beat-Rush/assets/73222781/8de95b5a-b20a-45fd-8f84-93e2b6a079cc)

## 노트 생성
- 스테이지마다 일정 개수의 노트를 생성하여 Object Pool에 저장한다.
- 해당 스테이지의 정보에 맞추어 코루틴을 통해 순차적으로 노트를 활성화 시켜준다.
```
public IEnumerator CreateNewNotes()
{
    (_patternLength, _attackDelay, _bgm) = _monster.GetPatternData();  //현재 스테이지 정보 읽어오기

    for (int i = 0; i < _patternLength; i++)
    {
        _monster.RandomAttack();      //하나의 패턴 실행
        yield return new WaitForSeconds(_attackDelay);
        if (i != _patternLength - 1) _cameraAnimator.SetTrigger("Move");
    }
    _cameraAnimator.SetTrigger("EndMove");
    yield return new WaitForSeconds((32.5f / _stageNoteSpeed));
    _monster.EndStage();
}
```
```
public IEnumerator Attack()
{
    float waitTime;
    _startDsp[_curPatternNum] = AudioSettings.dspTime;

    for (int i = 0; i < _pattern.Count;)
    {
        waitTime = (128 - (float)_pattern[i]["noteLocation"]) * Managers.Game.speedModifier;

        GameObject note = Managers.Pool.SpawnFromPool((float)_pattern[i]["isTrap"]);
        note.GetComponent<Note>().noteNumber = _curPatternNum;
        note.GetComponent<Note>().stage = _curStage;
        if (note.GetComponent<Note>().mode == 3)
        note.transform.position = new Vector3((float)_pattern[i]["xValue"], 0, Managers.Game.delay * Managers.Game.speedModifier) + _noteStartPos;
        i++;

        yield return new WaitForSeconds(waitTime / _stageNoteSpeed);
    }
}
```
## 노트 이동
- NoteManager라는 클래스에서 현재 활성화되어있는 노트들을 한번에 이동시킨다.
- Time.deltaTime 대신 depTime을 사용한다.
```
private void MoveNotes()
{
    float movement = ((float)(AudioSettings.dspTime - _curDsp) * _stageNoteSpeed);
    foreach (GameObject note in Managers.Pool.GetActiveNotes())
    {
        note.transform.position = new Vector3(note.transform.position.x, note.transform.position.y,
        note.transform.position.z - movement);
    }
}
```
## 노트 위치 조정
- 환경적인 상황으로 노트가 원하는대로 생성되지 않는 경우를 대비하여 조정하도록 구현.
- Feedback이라는 메서드를 만들어 12프레임마다 노트의 위치를 조정해줌.
- 활성화된 노트들을 가져오는 법
```
public List<GameObject> GetActiveAliveNotes(int noteNum)
{
    List<GameObject> activepool = new List<GameObject>();
    foreach (GameObject obj in poolQueue)
    {
        if (obj.activeSelf == true && obj.GetComponent<Note>().noteNumber == noteNum)
        {
            activepool.Add(obj);
        }
        activepool.OrderBy(x => x.transform.position.z).ToList();
    }
    return activepool;
}
```
활성화 되어있으면서, 아직 판정 전인 노트들만 가져오도록 했다.
원래는 관성적으로 Queue로 했었는데, 모든 노드에 편하게 접근하기 위해서 List로 바꾸었다.
앞에 있는 노트부터 사용하기 위해 정렬도 해 둔다.

- 위치에 맞게 깔아두는 법
```
public void Feedback()
{
    if (!_isFeedbackStart)
    {
        _startDsp[_curPatternNum] = AudioSettings.dspTime - _pauseDsp;
        _isFeedbackStart = true;
    }

    List<GameObject> _activeNotes = Managers.Pool.GetActiveAliveNotes(_curPatternNum);
    float _noteDistance = _stageNoteSpeed * (float)(AudioSettings.dspTime - _startDsp[_curPatternNum]);

    int cnt = 0;
    for (int i = Managers.Game.stageInfos[_curStage].curNoteInStage[_curPatternNum]; i < Managers.Game.stageInfos[_curStage].curNoteInStage[_curPatternNum] + _activeNotes.Count; i++)
    {
        float curLocation = ((float)_pattern[i]["noteLocation"] * Managers.Game.speedModifier) - _noteDistance;
        GameObject note = _activeNotes[cnt++];
        if (note.GetComponent<Note>().mode == 3)
        {
            note.transform.position = new Vector3(note.transform.position.x, note.transform.position.y, curLocation + 42.5f);
        }
        else
        {
            note.transform.position = new Vector3(note.transform.position.x, note.transform.position.y, curLocation + 42.5f + Managers.Game.delay * Managers.Game.speedModifier);
        }

    }
}
```
노트가 파괴될 때마다 개수를 체크해준 후, 현재 노트부터 활성화된 노트의 수만큼 csv파일에서 값을 가져온 후,
현재까지 지난 시간을 바탕으로 지금까지 온 거리를 계산하여 노트의 거리 - 현재 거리로 세팅해준다.
## 몬스터 공격
- Monster 클래스를 상속받는 몬스터들이 스테이지마다 존재한다.
- Strategy Pattern을 사용하여 현재 스테이지에 맞는 몬스터를 접근하도록 한다.
```
[SerializeField] private GameObject _monsterObject; 

_monster = _monsterObject.GetComponent<Monster>();

(_patternLength, _attackDelay, _bgm) = _monster.GetPatternData();
_monster.RandomAttack();
_monster.EndStage();
```
# 트러블 슈팅
## dspTime 사용
- 문제 상황 : 노트가 이동하는 속도가 일정하지 않는 현상이 발생.
- 원인 분석 : 로그를 찍어보니 일정 음악 시간동안 이동하는 거리가 실행마다 조금 다름. float형이라서 그런 게 아닐까 추측.
- 해결 방안
1. double 형변환.
2. dspTime 사용.
- 의견 조율:
  1번의 방식은 매 업데이트마다 해야 한다는 부담이 큼. 또 오디오 재생시간은 유니티 엔진내에서도 다른 시간을 가지고 쓰레드가 진행된다고 함.
  그렇기에 audiosetting.dspTime이라는 유니티에서 제공하는 api를 사용하는 것이 낫겠다고 판단.
- 결과 : dspTime을 사용하여 노트의 이동 구현.  


## Feedback System
- 문제 상홍 : 노트의 생성이 밀리는 경우가 생김.
- 원인 문석 : 실행 시, 컴퓨터의 상태에 따라 달라지는 것으로 보아 환경적인 문제일 수 있다고 판단.
- 해결 방안 : 노트의 위치를 임의적으로 조정해주는 메서드를 만들기로 함. 구현하기 위해서는
1. 노트들이 원래 있어야 할 위치를 알아야 하고
2. 현재 노트들에 접근하여 수정할 수 있어야 한다.  


1번을 해결하기 위해 먼저는 csv파일을 사용하였다. 현재의 거리를 계산 한 후에, 패턴의 노트 거리를 순회하며 현재 보여야 하는 노트들을 접근하는 식으로 하였다.
다만 그랬더니 노트를 파괴한 경우와 놓친 경우를 구분하기 힘들고, 매번 모든 패턴을 살펴야 하기 때문에 부담이 너무 심했다.
그렇기에 노트의 현재 개수를 저장하는 변수를 만들어 사용하면서, 패턴에서의 현재 노트부터 씬에 활성화되어있는 개수만큼만 가져오게 하는 식으로 오버헤드를 최소화했다.  

2번을 위해서 Object Pool에 새로운 메서드를 만들었다. pool에서 활성화되어있는 노트를 가져온 후에, 
가까운 순서대로 배치해야 하므로 순서도 정렬해준 후 리턴하였다.


## 일시정지 구현

## 
