using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Config : MonoBehaviour
{


  // FUNÇÃO PARA SETAR DIFICULDADE DO JOGO - VELOCIDADE DE GAME
  public  float valocidadeGame ( float vel, Toggle lento, Toggle normal, Toggle rapido ){
   vel =  rapido.isOn == true ? 0.5f : lento.isOn == true ? 1.8f  : 0.8f ;
   return vel;
  }


  // PROSSEGUIR E VOLTAR TELA 
  public void goBack ( Image abre, Image fecha ){
     fecha.gameObject.SetActive(false);
     abre.gameObject.SetActive(true);
  }



}
