using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleFlappy : MonoBehaviour
{
    public float vitesseHorizontale;
    public float vitesseVerticale;

    //Sprite
    public Sprite flappyBlesse;
    public Sprite flappyNormale;
    //Game Object
    public GameObject objetPiece;
    public GameObject objetPackVie;
    public GameObject objetChampingon;
    //Audio
    public AudioClip sonCollision;
    public AudioClip sonPiece;
    public AudioClip sonPackVie;
    public AudioClip sonChampigon;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.RightArrow))) //Deplacement vers la droite
        {
            vitesseHorizontale = 2;
        }
        else if (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.LeftArrow))) //Deplacement vers la gauche 
        {
            vitesseHorizontale = -3;
        }
        else
        {
            vitesseHorizontale = GetComponent<Rigidbody2D>().velocity.x; //vitesse horizontale actuelle
        }

        if (Input.GetKeyDown(KeyCode.W) || (Input.GetKeyDown(KeyCode.UpArrow)))
        {
            vitesseVerticale = 5;
        }
        else
        {
            vitesseVerticale = GetComponent<Rigidbody2D>().velocity.y; //vitesse horizontale actuelle
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(vitesseHorizontale, vitesseVerticale);
    }

    //Collisions
    void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.gameObject.name);

        //Collision flappy et colonne
        if (collision.gameObject.name == "Colonne")
        {
            GetComponent<SpriteRenderer>().sprite = flappyBlesse;
            GetComponent<AudioSource>().PlayOneShot(sonCollision, 1f);
        }

        //Collision flappy piece
        else if(collision.gameObject.name == "PieceOr")
        {
            collision.gameObject.SetActive(false);
            Invoke("ActivePiece", 3f);
        }

        //Collision flappy packVie
        else if(collision.gameObject.name == "PackVie")
        {
            collision.gameObject.SetActive(false);
            Invoke("ActivePackVie", 3f);
            GetComponent<SpriteRenderer>().sprite = flappyNormale;
        }

        //Collision flappy champigon
        else if(collision.gameObject.name == "Champingon")
        {
            collision.gameObject.SetActive(false);
            Invoke("ActiveChampingon", 3f);
            transform.localScale *= 1.5f;
        }
    }


    // - - - FONCTIONS - - - //
    //Fonction pour reactiver la piece
    void ActivePiece()
    {
        objetPiece.SetActive(true);
        float deplacementAleatoire = Random.Range(-1, 1);
        objetPiece.transform.position = new Vector2(objetPiece.transform.position.x, deplacementAleatoire);	// deplacement aleatoire verticale piece
    }

    //Fonction pour reactiver le pack de vie
    void ActivePackVie()
    {
        objetPackVie.SetActive(true);
    }

    //fonction pour reactiver le champigon
    void ActiveChampingon()
    {
        objetChampingon.SetActive(true);
        transform.localScale /= 1.5f;
    }
}
