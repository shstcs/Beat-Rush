using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum InstrumentType
{
    Metronome = 0,
    Drum,
    piano,
    guitar,
}

public class Guitar : MonoBehaviour, IInteractable
{
    public InstrumentType type;
    public GameObject instrument;
    public ParticleSystem particle;
    float time = 0f;
    bool rightTurn = true;

    private void Start()
    {
        particle.Stop();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            particle.Play();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            time += Time.deltaTime;

            switch (type)
            {
                case InstrumentType.guitar:
                    if (rightTurn)
                    {
                        instrument.transform.Rotate(0, 0, -20f * Time.deltaTime);

                        if (time >= 3f)
                        {
                            rightTurn = !rightTurn;
                            time = 0f;
                        }
                    }
                    else if (!rightTurn)
                    {
                        instrument.transform.Rotate(0, 0, 20f * Time.deltaTime);

                        if (time >= 3f)
                        {
                            rightTurn = !rightTurn;
                            time = 0f;
                        }
                    }
                    break;

                case InstrumentType.piano:
                    break;

                case InstrumentType.Drum:
                    break;

                case InstrumentType.Metronome:
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            particle.Stop();

            switch (type)
            {
                case InstrumentType.guitar:
                    instrument.transform.rotation = Quaternion.Euler(0, 0, -45f);
                    break;

                case InstrumentType.piano:
                    break;

                case InstrumentType.Drum:
                    break;
            }
        }
    }

    public string GetInteractPrompt()
    {
        switch (type)
        {
            case InstrumentType.Metronome:
                return "튜토리얼";
            case InstrumentType.Drum:
                return "1 스테이지";
            case InstrumentType.piano:
                return "2 스테이지";
            case InstrumentType.guitar:
                return "3 스테이지";
        }
        return instrument.gameObject.name;
    }

    public void OnInteract()
    {
        Managers.Game.InitNotes(); //판정 초기화
        //상호작용 구현
        switch (type)
        {
            case InstrumentType.guitar:
                Managers.Game.currentStage = 3;
                break;
            case InstrumentType.piano:
                Managers.Game.currentStage = 2;
                break;
            case InstrumentType.Drum:
                Managers.Game.currentStage = 1;
                break;
            case InstrumentType.Metronome:
                Managers.Game.currentStage = 0;
                break;
        }
        if(type == InstrumentType.Metronome)
            GameObject.Find("HUD_Canvas").transform.GetChild(3).gameObject.SetActive(true);
        else
            GameObject.Find("HUD_Canvas").transform.GetChild(2).gameObject.SetActive(true);
    }
}
