using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public GameObject controller;
    public GameObject movePlate;
    private int xBoard=-1;
    private int yBoard=-1;

    public string Player;

    public Sprite light_piece,king_light_piece;
    public Sprite dark_piece,king_dark_piece;

    public void activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        SetCoords();

        switch (this.name)
        {
            case "light_piece" : this.GetComponent<SpriteRenderer>().sprite = light_piece; Player="Light"; break;
            case "king_light_piece" : this.GetComponent<SpriteRenderer>().sprite = king_light_piece;Player="Light"; break;

            case "dark_piece" : this.GetComponent<SpriteRenderer>().sprite = dark_piece;Player="Dark"; break;
            case "king_dark_piece" : this.GetComponent<SpriteRenderer>().sprite = king_dark_piece;Player="Dark"; break;
        }
    }

    public void SetCoords()
    {
        float x= xBoard;
        float y = yBoard;

        // x *=0.66f;
        // y *=0.66f;

        x += -3.5f;
        y += -3.5f;

        this.transform.position = new Vector3(x,y,-1.0f);
    }

    public int GetXBoard()
    {
        return xBoard;
    }
    
    public int GetYBoard()
    {
        return yBoard;
    }

    public void SetXBoard(int x)
    {
        xBoard= x;
    }

    public void SetYBoard(int y)
    {
        yBoard= y;
        CheckForKing();
        
    }
    public void CheckForKing()
    {
        if ((this.Player == "Light" && this.GetYBoard() == 7) || (this.Player == "Dark" && this.GetYBoard() == 0))
        {
            if (this.Player == "Light"){
                this.transform.GetComponent<SpriteRenderer>().sprite=this.king_light_piece;
                this.name="king_light_piece";
            }else{
                this.transform.GetComponent<SpriteRenderer>().sprite=this.king_dark_piece;
                this.name="king_dark_piece";
            }
        }
        
    }
    public void OnMouseUp()
    {
        if (!controller.GetComponent<Game>().IsGameOver() && controller.GetComponent<Game>().GetCurrentPlayer()==Player){
            DestroyMovePlates();
            initiateMovePlates();
        }
        
    }
    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0;i < movePlates.Length;i++)
        {
            Destroy(movePlates[i]);
        }
    }

    public void initiateMovePlates()
    {
        switch (this.name)
        {
            case "king_light_piece":
            case "king_dark_piece":
                LineMovePlate(1,1);
                LineMovePlate(-1,1);
                LineMovePlate(-1,-1);
                LineMovePlate(1,-1);
                break;
            case "dark_piece":
                DarkPieceMovePlateRight(xBoard + 1 ,yBoard -1);
                DarkPieceMovePlateLeft(xBoard - 1 ,yBoard -1);
                break;
            case "light_piece":
                LightPieceMovePlateRight(xBoard + 1 ,yBoard +1);
                LightPieceMovePlateLeft(xBoard - 1 ,yBoard +1);
                break;
        }
    }

    public void LineMovePlate(int xIncrement,int yIncrement)
    {
        Game sc = controller.GetComponent<Game>();
        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;
        int x_enemy=x;
        int y_enemy=y;
        while (sc.PositionOnBoard(x,y) && sc.GetPosition(x,y)==null)
        {
            MovePlateSpawn(x,y);
            x += xIncrement;
            y += yIncrement;
        }
        if (sc.PositionOnBoard(x,y) &&sc.GetPosition(x,y)!=null&&  sc.GetPosition(x,y).GetComponent<Piece>().Player!=Player)
        {
            if (sc.GetPosition(x+xIncrement,y+yIncrement)==null && sc.PositionOnBoard(x+xIncrement,y+yIncrement)){
                MovePlateAttackSpawn(x+ xIncrement,y+ yIncrement,x_enemy,y_enemy);
            }   
            
        }
    }

    public void LightPieceMovePlateRight(int x,int y)
    {
        Game sc=controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x,y))
        {
            if (sc.GetPosition(x,y)==null)
            {
                MovePlateSpawn(x,y);
            }
            if(sc.PositionOnBoard(x,y) && sc.GetPosition(x,y)!=null && sc.GetPosition(x,y).GetComponent<Piece>().Player!=Player)
            {
                if (sc.GetPosition(x+1,y+1)==null && sc.PositionOnBoard(x+1,y+1))
                {
                    MovePlateAttackSpawn(x+1,y+1,x,y);
                }
            }
        }
    }
    public void LightPieceMovePlateLeft(int x,int y)
    {
        Game sc=controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x,y))
        {
            if (sc.GetPosition(x,y)==null)
            {
                MovePlateSpawn(x,y);
            }
            if(sc.PositionOnBoard(x,y) && sc.GetPosition(x,y)!=null && sc.GetPosition(x,y).GetComponent<Piece>().Player!=Player)
            {
                if (sc.GetPosition(x-1,y+1)==null && sc.PositionOnBoard(x-1,y+1))
                {
                    MovePlateAttackSpawn(x-1,y+1,x,y);
                }
            }
        }
    }

    public void DarkPieceMovePlateLeft(int x,int y)
    {
        Game sc=controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x,y))
        {
            if (sc.GetPosition(x,y)==null)
            {
                MovePlateSpawn(x,y);
            }
            if(sc.PositionOnBoard(x,y) && sc.GetPosition(x,y)!=null && sc.GetPosition(x,y).GetComponent<Piece>().Player!=Player)
            {
                if (sc.GetPosition(x-1,y-1)==null && sc.PositionOnBoard(x-1,y-1))
                {
                    MovePlateAttackSpawn(x-1,y-1,x,y);
                }
            }
        }
    }
    public void DarkPieceMovePlateRight(int x,int y)
    {
        Game sc=controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x,y))
        {
            if (sc.GetPosition(x,y)==null)
            {
                MovePlateSpawn(x,y);
            }
            if(sc.PositionOnBoard(x,y) && sc.GetPosition(x,y)!=null && sc.GetPosition(x,y).GetComponent<Piece>().Player!=Player)
            {
                if (sc.GetPosition(x+1,y-1)==null && sc.PositionOnBoard(x+1,y-1))
                {
                    MovePlateAttackSpawn(x+1,y-1,x,y);
                }
            }
        }
    }

    public void MovePlateSpawn(int matrixX,int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x += -3.5f;
        y += -3.5f;

        GameObject mp=Instantiate(movePlate,new Vector3(x,y,-3.0f),Quaternion.identity);
        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX,matrixY);
    }

    public void MovePlateAttackSpawn(int matrixX,int matrixY,int x_enemy,int y_enemy)
    {
        float x = matrixX;
        float y = matrixY;

        x += -3.5f;
        y += -3.5f;

        GameObject mp=Instantiate(movePlate,new Vector3(x,y,-3.0f),Quaternion.identity);//posisi moveplate
        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.SetReference(gameObject);
        mpScript.GetEnemy(x_enemy,y_enemy);
        mpScript.SetCoords(matrixX,matrixY); //piece posisi

    }
}
