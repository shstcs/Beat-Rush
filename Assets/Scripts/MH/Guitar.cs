using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum InstrumentType
{
    guitar,
    piano,
    Drum,
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
        //상호작용 구현
        switch (type)
        {
            case InstrumentType.guitar:
                SceneManager.LoadScene("Minho");
                break;
            case InstrumentType.piano:
                SceneManager.LoadScene("Minho");
                break;
            case InstrumentType.Drum:
                SceneManager.LoadScene("Minho");
                break;
        }

    }
}
