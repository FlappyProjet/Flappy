using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementObjets : MonoBehaviour
{
    public float vitesse;
    public float positionFin;
    public float positionDebut;
    public float deplacementAleatoire;


    // Start is called before the first frame update
    void Start()
    {
        float valeurAleatoireY = Random.Range(-deplacementAleatoire, deplacementAleatoire);  // Génère une valeur aléatoire
        transform.position = new Vector2(transform.position.x, valeurAleatoireY);	// positionnement vertical de l'objet
    }

    // Update is called once per frame
    void Update()
    {
        transform­.Translate(vitesse, 0f, 0f);

        if (transform.position.x < positionFin)
        {
            transform.position = new Vector3(positionDebut, transform.position.y, 0);
            float valeurAleatoireY = Random.Range(-deplacementAleatoire, deplacementAleatoire);  // Génère une valeur aléatoire
            transform.position = new Vector2(positionDebut, valeurAleatoireY);	// positionnement vertical de l'objet
        }

    }

}
