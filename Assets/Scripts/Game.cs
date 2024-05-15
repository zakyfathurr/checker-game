using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Game : MonoBehaviour
{
    public GameObject pieces;

    private GameObject[,] positions= new GameObject[8,8];
    private GameObject[] LightPlayer = new GameObject[16];
    private GameObject[] DarkPlayer = new GameObject[16];

    private string currentPlayer = "Light";

    private bool GameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        
        LightPlayer= new GameObject [] {
            Create("light_piece",0,0), Create("light_piece",2,0), Create("light_piece",4,0), Create("light_piece",6,0), 
            Create("light_piece",1,1), Create("light_piece",3,1), Create("light_piece",5,1), Create("light_piece",7,1), 
            Create("light_piece",0,2), Create("light_piece",2,2), Create("light_piece",4,2), Create("light_piece",6,2)
        };
        DarkPlayer= new GameObject []{
            Create("dark_piece",1,7), Create("dark_piece",3,7), Create("dark_piece",5,7), Create("dark_piece",7,7), 
            Create("dark_piece",0,6), Create("dark_piece",2,6), Create("dark_piece",4,6), Create("dark_piece",6,6), 
            Create("dark_piece",1,5), Create("dark_piece",3,5), Create("dark_piece",5,5), Create("dark_piece",7,5)
        };

        for ( int i = 0;i < LightPlayer.Length; i++)
        {
            SetPosition(LightPlayer[i]);
            SetPosition(DarkPlayer[i]);
        }
    }

    public GameObject Create(string name ,int x,int y)
    {
        GameObject obj = Instantiate(pieces,new Vector3(0,0,-1),Quaternion.identity);
        Piece P = obj.GetComponent<Piece>();
        P.name = name;
        P.SetXBoard(x);
        P.SetYBoard(y);
        P.activate();
        return obj;
    }

    public void SetPosition(GameObject obj)
    {
        Piece P=obj.GetComponent<Piece>();
        positions[P.GetXBoard(),P.GetYBoard()] = obj;
    }


    public void SetPositionEmpty(int x,int y)
    {
        positions[x,y]=null;
    }

    public GameObject GetPosition(int x,int y)
    {
        return positions[x,y];
    }

    public bool PositionOnBoard(int x,int y)
    {
        if (x < 0 || y<0 || x >= positions.GetLength(0) || y>= positions.GetLength(1)) return false;
        return true;
    }

    public string GetCurrentPlayer(){
        return currentPlayer;
    }
    public bool IsGameOver(){
        return GameOver;
    }
    public void NextTurn(){
        if (currentPlayer =="Light"){
            currentPlayer = "Dark";
        }else{
            currentPlayer="Light";
        }
    }
    
}
