using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum InstrumentType
{
    guitar,
    piano,
    Drum,
    Metronome
}

public class Guitar : MonoBehaviour, IInteractable
{
    public InstrumentType type;
    public GameObject instrument;
    public ParticleSystem particle;
    float time = 0f;
    bool rightTurn = true;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            particle.gameObject.SetActive(true);
            particle.Play();
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
            particle.gameObject.SetActive(false);
            particle.Pause();

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
        return instrument.gameObject.name;
    }

    public void OnInteract()
    {
        Managers.Game.InitJudgeNotes(); //판정 초기화
        //상호작용 구현
        switch (type)
        {
            case InstrumentType.guitar:
                Managers.Game.currentStage = 3;
                GameObject.Find("HUD_Canvas").transform.GetChild(2).gameObject.SetActive(true);
                break;
            case InstrumentType.piano:
                Managers.Game.currentStage = 2;
                GameObject.Find("HUD_Canvas").transform.GetChild(2).gameObject.SetActive(true);
                break;
            case InstrumentType.Drum:
                Managers.Game.currentStage = 1;
                GameObject.Find("HUD_Canvas").transform.GetChild(2).gameObject.SetActive(true);
                break;
            case InstrumentType.Metronome:
                Managers.Game.currentStage = 0;
                GameObject.Find("HUD_Canvas").transform.GetChild(2).gameObject.SetActive(true);
                break;
        }

    }
}
