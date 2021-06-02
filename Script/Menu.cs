using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Menu : MonoBehaviour
{


public Button play ;
public Button sair ;
public Image menu;
public Image game;



 void Start(){

     play.onClick.AddListener(() => { 


     menu.gameObject.SetActive(false);
     game.gameObject.SetActive(true);
     });

     sair.onClick.AddListener(() => { 
         Application.Quit();
    });

 }


}
