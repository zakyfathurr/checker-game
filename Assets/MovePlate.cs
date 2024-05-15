using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;
    GameObject reference = null;

    int matrixX;
    int matrixY;
    int x_enemy;
    int y_enemy;

    public bool attack = false;

    public void Start()
    {
        if (attack)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f,0.0f,0.0f,1.0f);
        }
    }

    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        if (attack)
        {
            GameObject p=controller.GetComponent<Game>().GetPosition(x_enemy,y_enemy);//hapus piece
            Destroy(p);
        }
        controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Piece>().GetXBoard(),reference.GetComponent<Piece>().GetYBoard());
        
        reference.GetComponent<Piece>().SetXBoard(matrixX);
        reference.GetComponent<Piece>().SetYBoard(matrixY);
        reference.GetComponent<Piece>().SetCoords();

        controller.GetComponent<Game>().SetPosition(reference);

        controller.GetComponent<Game>().NextTurn();

        reference.GetComponent<Piece>().DestroyMovePlates();
    }

    public void GetEnemy(int x,int y){
        x_enemy=x;
        y_enemy=y;
    }
    public void SetCoords(int x,int y)
    {
        matrixX=x;
        matrixY=y;
    }

    public void SetReference(GameObject obj)
    {
        reference= obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }
}
