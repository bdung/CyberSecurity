using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordInput : MonoBehaviour
{
    // public WordManager wordManager;
    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update called");
        foreach (char letter in Input.inputString)
        {
            Debug.Log(letter);
            FindAnyObjectByType<WordManager>().TypeLetter(letter);
        }

    }
}
