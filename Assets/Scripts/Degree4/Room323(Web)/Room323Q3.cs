using System.Collections;
using TMPro;
using UnityEngine;

public class Room323Q3 : MonoBehaviour
{
   public TMP_InputField q3;
   public GameObject right;
   public GameObject wrong;
   public GameObject ques3;
   public void CheckQ2()
   {
      if (q3.text == "B" || q3.text == "b")
      {
         ques3.SetActive(false);
         right.SetActive(true);
         FindAnyObjectByType<EndTalking>().countCorrect++;

      }
      else
      {
         wrong.SetActive(true);
      }
   }

}