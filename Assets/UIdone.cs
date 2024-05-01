// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;


// public class UIdone : MonoBehaviour
// {
//     public GameObject completedPopUp;
//     // public Button back;
//     void Start()
//     {
//         completedPopUp.SetActive(false);
//     }
//     void GoToMainMenu()
//     {
//         Time.timeScale = 1f;
//         SceneManager.LoadScene("Overworld");

//     }
//     // IEnumerator DisplayAddTextCor(string add)
//     // {
//     //     addText.text = add;
//     //     addText.gameObject.SetActive(true);
//     //     yield return new WaitForSeconds(1f);
//     //     addText.gameObject.SetActive(false);
//     //     yield return new WaitForSeconds(1f);
//     //     visible = true;


//     // }
//     // public void DisplayIngredientAdded(string added)
//     // {
//     //     addText.text = add;
//     //     StartCoroutine(DisplayAddTextCor(add));
//     //     //hitText.gameObject.SetActive(true);
//     // }
//     public void finish()
//     {
//         addText.gameObject.SetActive(false);
//         completedPopUp.SetActive(true);    
//         back.onClick.AddListener(GoToMainMenu);
//     }

// }
