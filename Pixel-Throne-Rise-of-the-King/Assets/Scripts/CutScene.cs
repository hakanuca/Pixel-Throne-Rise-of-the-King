using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CutScene : MonoBehaviour
{

   public void Skip()
   {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }


   public void MainMenu () 
   {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

   }
   
   public void DemoEnd () 
   {
         SceneManager.LoadScene(0);

   }
   public void Restart () 
   {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

   }
   public void RestartCave () 
   {
         SceneManager.LoadScene(3);

   }
}
