using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControleFlappy : MonoBehaviour
{
    //Vitesse
    public float vitesseHorizontale;
    public float vitesseVerticale;

    //Sprite Falppy
    public Sprite flappyNormaleBas;
    public Sprite flappyNormaleHaut;
    public Sprite flappyBlesseBas;
    public Sprite flappyBlesseHaut;

    //Game Object
    public GameObject objetPiece;
    public GameObject objetPackVie;
    public GameObject objetChampingon;
    public GameObject objetGrille;
    
    //Audio
    public AudioClip sonCollision;
    public AudioClip sonPiece;
    public AudioClip sonPackVie;
    public AudioClip sonChampingon;
    public AudioClip sonFinPartie;

    //FinPartie
    public bool flappyTouchee = false;
    public bool partieTerminee = false;

    //Texte
    public TextMeshProUGUI pointage;
    public TextMeshProUGUI messageFin;

    public int score;

    void Start()
    {
        //score a 0
        score = 0;
        //message fin disparait
        messageFin.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        //Affichage du pointage
        pointage.text = "pointage:" + score;

        if (partieTerminee == false)
        {
            //touche D
            if (Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.RightArrow))) //Deplacement vers la droite
            {
                vitesseHorizontale = 2;
            }
            //touche A
            else if (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.LeftArrow))) //Deplacement vers la gauche 
            {
                vitesseHorizontale = -3;
            }
            else
            {
                vitesseHorizontale = GetComponent<Rigidbody2D>().velocity.x; //vitesse horizontale actuelle
            }

            //touche W
            if (Input.GetKeyDown(KeyCode.W) || (Input.GetKeyDown(KeyCode.UpArrow)))
            {
                vitesseVerticale = 5;
                if (flappyTouchee == false)
                {
                    GetComponent<SpriteRenderer>().sprite = flappyNormaleBas;
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = flappyBlesseBas;
                }
            }
            else
            {
                vitesseVerticale = GetComponent<Rigidbody2D>().velocity.y; //vitesse horizontale actuelle
            }

            GetComponent<Rigidbody2D>().velocity = new Vector2(vitesseHorizontale, vitesseVerticale);


            if (Input.GetKeyUp(KeyCode.W) || (Input.GetKeyUp(KeyCode.UpArrow)))
            {
                if (flappyTouchee == false)
                {
                    GetComponent<SpriteRenderer>().sprite = flappyNormaleHaut;
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = flappyBlesseHaut;
                }
            }
        }

    }

    //Collisions
    void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.gameObject.name);

        //Collision flappy colonne ou limite
        if ((collision.gameObject.name == "Colonne") || (collision.gameObject.name == "Decor"))
        {
            GetComponent<AudioSource>().PlayOneShot(sonCollision, 1f);
            //
            if (flappyTouchee == false)
            {
                GetComponent<SpriteRenderer>().sprite = flappyBlesseBas;
                flappyTouchee = true;
            } 
            else
            {
                partieTerminee = true;
                //Activer rotation
                GetComponent<Rigidbody2D>().freezeRotation = false;
                //Desactive collider
                GetComponent<Collider2D>().enabled = false;
                //Angular velocity
                GetComponent<Rigidbody2D>().angularVelocity = 100;
                //Son finPartie
                GetComponent<AudioSource>().PlayOneShot(sonFinPartie, 1f);
                //message fin partie apparait
                messageFin.enabled = true;
                //Relancement
                Invoke("RelancerPartie", 3f);
            }
            //score -5
            score -= 5;
        }

        //Collision flappy piece
        else if(collision.gameObject.name == "PieceOr")
        {
            collision.gameObject.SetActive(false);
            Invoke("ActivePiece", 3f);
            GetComponent<AudioSource>().PlayOneShot(sonPiece, 1f);
            //score +5
            score += 5;
            //Animation grille
            objetGrille.GetComponent<Animator>().enabled = true; //Activer
            Invoke("DesactiverAnim", 4f);
        }

        //Collision flappy packVie
        else if(collision.gameObject.name == "PackVie")
        {
            flappyTouchee = false;
            collision.gameObject.SetActive(false);
            Invoke("ActivePackVie", 3f);
            GetComponent<SpriteRenderer>().sprite = flappyNormaleBas;
            GetComponent<AudioSource>().PlayOneShot(sonPackVie, 1f);
            //score +5
            score += 5;
        }

        //Collision flappy champingon
        else if(collision.gameObject.name == "Champingon")
        {
            collision.gameObject.SetActive(false);
            Invoke("ActiveChampingon", 3f);
            transform.localScale *= 1.5f;
            GetComponent<AudioSource>().PlayOneShot(sonChampingon, 1f);
            //score +10
            score += 10;
        }
    }


    // - - - FONCTIONS - - - //
    //Fonction pour reactiver la piece
    void ActivePiece()
    {
        objetPiece.SetActive(true);
        float deplacementAleatoire = Random.Range(-2, 2);
        objetPiece.transform.position = new Vector2(objetPiece.transform.position.x, deplacementAleatoire);	// deplacement aleatoire verticale piece
    }

    //Fonction pour reactiver le pack de vie
    void ActivePackVie()
    {
        objetPackVie.SetActive(true);
        float deplacementAleatoire = Random.Range(-1, 1);
        objetPackVie.transform.position = new Vector2(objetPackVie.transform.position.x, deplacementAleatoire);	// deplacement aleatoire verticale pack vie
    }

    //fonction pour reactiver le champigon
    void ActiveChampingon()
    {
        objetChampingon.SetActive(true);
        transform.localScale /= 1.5f;
        float deplacementAleatoire = Random.Range(-1, 1);
        objetChampingon.transform.position = new Vector2(objetChampingon.transform.position.x, deplacementAleatoire);	// deplacement aleatoire verticale Champingon
    }

    //Fonction pour redemarer le jeu
    void RelancerPartie()
    {
        SceneManager.LoadScene(0);
    }

    //Fonction pour desctiver an8imation grille après 4 seconde
    void DesactiverAnim()
    {
        objetGrille.GetComponent<Animator>().enabled = false; //Deactiver
    }
}
