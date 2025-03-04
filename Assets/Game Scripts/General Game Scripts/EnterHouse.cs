using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterHouse : MonoBehaviour
{
    [SerializeField] private SceneController sceneController;
    [SerializeField] private GameObject houseInteriorEntrance;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            sceneController.EnterExitHouse(houseInteriorEntrance.transform.position);
        }
    }
}