using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;


public enum Estagios
{
    Memorizar,
    Cronometro,
    Acertar,
    Fim
}

public class Main : MonoBehaviour
{

    bool play = true;
    public bool chave_cronos = false;
    int vezes = 0;
    public  Button start , menu, vermelho, verde, amarelo, azul;
    public  Button menu_play, menu_config , menu_sair, config_back, config_creditos, config_records,config_creditos_back;
    public  Button continuar, desistir, back, voltar_game, sair_game, fim;
    public  Image tela_menu, tela_game, tela_config, tela_credito, tela_records,tela_crono,tela_continue, nivel, tela_game_op;
    public  TextMeshProUGUI cronos, txt_recorde,level;
    public  float velocidade  =0.7f;

    public Color [] btnEstado;
    public  Toggle lento, normal, rapido;
    public  int score = 0 ; 
    float t = 0.0f ; 
    int l = 0 ;

    public static int index = 0;
    public Estagios estagios;
        
    Config config = new Config();


    public static List<int> cores = new List<int>();
    Color apagar = new Color(72f, 72f, 72f);
    Color acender = new Color(255f, 255f, 255f);


    public List<Sprite> niveis = new List<Sprite>();


    void Start()
    {
            //GET RECORD 
            score = PlayerPrefs.GetInt("recorde") != null ? PlayerPrefs.GetInt("recorde") : 0  ;
            if(score > 1 ){
               txt_recorde.text = "Record " + score.ToString() + " sequences";
                nivel.GetComponent<Image>().sprite =  
                score < 10 ?  niveis[0] :
                score >= 10 && score < 15 ?  niveis[1] :
                score >= 15 && score < 25 ?  niveis[2] :
                score >= 25 ?  niveis[3] : null;
            }else{
                txt_recorde.text = "Record  00 sequences";
            }

           //LISTA DE 3 CORES INICIAIS
           listaInicial();
    
            // CLICK NO BOTÃO START PARA INICIAR JOGO
           start.onClick.AddListener(() => { 
             if(estagios == Estagios.Memorizar ){
                 chave_cronos = true;
                 StartCoroutine(memorizarCores(cores ,vermelho ,amarelo, verde ,azul ,btnEstado[0] ,btnEstado[1] ,velocidade));
                 start.gameObject.SetActive(false);
                 menu.gameObject.SetActive(true);
            }});


            // CLICK NO BOTÃO CONTINUAR PARA PROSSEGUIR NO JOGO
           continuar.onClick.AddListener(() => { 
                 tela_continue.gameObject.SetActive(false);
                 chave_cronos = true;
                 Main.index = 0 ;
                 level.text = "01";
                 Main.cores.Clear();
                 estagios = Estagios.Memorizar;
                 listaInicial();
                 StartCoroutine(memorizarCores(cores ,vermelho ,amarelo, verde ,azul ,btnEstado[0] ,btnEstado[1] ,velocidade));
           });

            // CLICK NO BOTÃO DESISTIR PARA SAIR NO JOGO
            desistir.onClick.AddListener(() => {
                 tela_continue.gameObject.SetActive(false);

                 Main.index = 0 ;
                 level.text = "01";
                 Main.cores.Clear();
                 estagios = Estagios.Memorizar;
                 listaInicial();

                 tela_game.gameObject.SetActive(false);
                 tela_menu.gameObject.SetActive(true);
                 start.gameObject.SetActive(true);
                 menu.gameObject.SetActive(false);
            });

            // CLICK NO VOLTAR 
             back.onClick.AddListener(() => {
               
                 Main.index = 0 ;
                 level.text = "01";
                 Main.cores.Clear();
                 estagios = Estagios.Memorizar;
                 listaInicial();

                 tela_game.gameObject.SetActive(false);
                 tela_menu.gameObject.SetActive(true);
                 start.gameObject.SetActive(true);
                 menu.gameObject.SetActive(false);
            });

                       
            // SAIR DO JOGO
            sair_game.onClick.AddListener(() => {
                 Application.Quit();
           });

           //SAIR DO JOGO
            fim.onClick.AddListener(() => {
                 Application.Quit();
           });

             
             // BLOCO DE BOTÕES DE CORES 
            vermelho.onClick.AddListener(() => { 
                  if(estagios == Estagios.Acertar){
                StartCoroutine(acertandoCores(cores,vermelho )); }
                });
            amarelo.onClick.AddListener(() => { 
                 if(estagios == Estagios.Acertar){

                StartCoroutine(acertandoCores(cores,amarelo )); }
                });
            verde.onClick.AddListener(() => { 
                if(estagios == Estagios.Acertar){

                StartCoroutine(acertandoCores(cores,verde )); }
                });
            azul.onClick.AddListener(() => { 
                if(estagios == Estagios.Acertar){

                StartCoroutine(acertandoCores(cores,azul )); }
                });

               
               //MENU INICIAL
         
              menu_play.onClick.AddListener(() => {config.goBack(tela_game, tela_menu); estagios = Estagios.Memorizar;});
              menu_config.onClick.AddListener(() => {config.goBack(tela_config, tela_menu);});
              menu_sair.onClick.AddListener(() => {Application.Quit(); });

              // CONFIGURE

               config_back.onClick.AddListener(() => {config.goBack(tela_menu, tela_config);});
               config_creditos.onClick.AddListener(() => {config.goBack(tela_credito, tela_config);});
               config_creditos_back.onClick.AddListener(() => {config.goBack(tela_config, tela_credito);});

   }


   void Update(){

         // CRONOMETRANDO ANTES DO START           
        if(chave_cronos == true){
             Cronometrando(ref chave_cronos);
          }

         // PAUSANDO GAME           
        menu.onClick.AddListener(() => {   
               tela_game_op.gameObject.SetActive(true);
                Pause();
         });

        voltar_game.onClick.AddListener(() => {
                 tela_game_op.gameObject.SetActive(false);
                Unpause();
        });

    }



      // FUNÇÃO DA LISTA INICIAL 
  void listaInicial(){
         System.Random r = new System.Random();
         for(int i = 0 ; i <= 2; i++){
         int index = r.Next(1,5);

         cores.Add(index);
         Debug.Log(cores[i]);
        }     
    }
  
        // SORTEANDO COR
   void IncorporandoNovaCor(){
         System.Random r = new System.Random();
         int index = r.Next(1,5);
         Main.cores.Add(index);
         
    }


    // MOSTRANDO SEQUÊNCIA DE CORES PARA MEMORIZAÇÃO 
public IEnumerator memorizarCores(List<int> listaCores ,  Button vermelhox,   Button amarelox ,  Button verdex,  Button azulx, Color apagar, Color acender,float velocidade ){

    //verificando velocidade do game       
    velocidade = config.valocidadeGame(velocidade,lento, normal, rapido);


    //verificando se deve abrir cronometragem ou não     
     if( chave_cronos == true){
     yield return new WaitForSecondsRealtime (3.5f);
     }else{
     yield return new WaitForSecondsRealtime (1.0f);
    }

            
   //sinalizando cores e elaborando intervalo  
   for(int i = 0 ; i < listaCores.Count; i++){

      switch(listaCores[i]){
        //vermelha    

        case 1 : 
            yield return new WaitForSecondsRealtime (velocidade);
            vermelhox.GetComponent<Image>().color = Color.white;
             yield return new WaitForSecondsRealtime (velocidade);
            vermelhox.GetComponent<Image>().color  = Color.grey;
        break;

        //amarela
        case 2 :
            yield return new WaitForSecondsRealtime (velocidade);
            amarelox.GetComponent<Image>().color =Color.white;
            yield return new WaitForSecondsRealtime (velocidade);
            amarelox.GetComponent<Image>().color  =  Color.grey;

        break;
        
        //verde
        case 3 : 
            yield return new WaitForSecondsRealtime (velocidade);
            verdex.GetComponent<Image>().color = Color.white;
            yield return new WaitForSecondsRealtime (velocidade);
            verdex.GetComponent<Image>().color  =  Color.grey;

        break;

        //azul
        case 4 :
            yield return new WaitForSecondsRealtime (velocidade);
            azulx.GetComponent<Image>().color  = Color.white ; 
            yield return new WaitForSecondsRealtime (velocidade);
            azulx.GetComponent<Image>().color  =   Color.grey; 
        break;
}//SWITCH
}//FOR
estagios = Estagios.Acertar;
}//IENUMERATOR



// CLICANDO PARA ACERTO DAS CORES
public  IEnumerator acertandoCores (List<int> listaCores ,  Button btn){

    btn.GetComponent<Image>().color = Color.white;
    yield return new WaitForSecondsRealtime (0.5f);
    btn.GetComponent<Image>().color  = Color.grey;

    int numBtn = 888;

    switch(btn.name){
        case "vermelho" :numBtn = 1; break;
        case "amarelo" :numBtn = 2;  break;
        case "verde" : numBtn = 3; break;
        case "azul" :numBtn = 4; break;
    }

   if(listaCores[Main.index] == numBtn){

     Main.index += 1;
     Debug.Log("TESTE" + Main.index);
     Debug.Log("ACERTOU");

      if( Main.index == listaCores.Count){

         IncorporandoNovaCor();
         Main.index = 0;
         StartCoroutine(memorizarCores(cores ,vermelho ,amarelo, verde ,azul ,btnEstado[0] ,btnEstado[1] ,velocidade));
          l = Main.cores.Count - 2;
         level.text =  l < 10  ? "0" + l.ToString() : l.ToString();
         estagios = Estagios.Memorizar;
      }
    }
   
   else{
    tela_continue.gameObject.SetActive(true);
       
      if(  l > score ){
        PlayerPrefs.SetInt("recorde", l);
      }

    yield return new WaitForSecondsRealtime (1f);
   } 


}//IENUMERATOR


// FUNÇÃO CRONOMETRO
public void Cronometrando( ref bool chave_cronos ){
estagios = Estagios.Cronometro;
   
 tela_crono.gameObject.SetActive(true);
 t += Time.deltaTime;
 float limite = 3f;
 int c =  Mathf.RoundToInt( limite - t  );

 cronos.text = c.ToString();
 if ( c < 0){
 tela_crono.gameObject.SetActive(false);
 chave_cronos = false;
 t = 0.0f;
 estagios = Estagios.Memorizar;

 }

}



// PAUSE GAME
void Pause()
{
	Time.timeScale = 0;
}

void Unpause()
{
    Time.timeScale = 1;
}
   
}
