using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guitar : MonoBehaviour
{
    public GameObject guitar;
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

            if (rightTurn)
            {
                guitar.transform.Rotate(0, 0, -10f * Time.deltaTime);

                if (time >= 3f)
                {
                    rightTurn = !rightTurn;
                    time = 0f;
                }
            }
            else if (!rightTurn)
            {
                guitar.transform.Rotate(0, 0, 10f * Time.deltaTime);

                if (time >= 3f)
                {
                    rightTurn = !rightTurn;
                    time = 0f;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            particle.gameObject.SetActive(false);
            particle.Pause();
            guitar.transform.rotation = Quaternion.Euler(0, 0, -45f);
        }
    }
}
