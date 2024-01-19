using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordInput3 : MonoBehaviour
{
    public WordManager3 wordManager;
    // Update is called once per frame
    void Update()
    {
        foreach (char letter in Input.inputString){
            // Debug.Log(letter);
            wordManager.TypeLetter(letter);
        }
        
    }
}