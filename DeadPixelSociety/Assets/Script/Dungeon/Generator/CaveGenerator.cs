using UnityEngine;

public class CaveGenerator : Generator
{
    private Vector2 position;
    public float reductor = 0.05f;

    public float generateOnLeft = 0.1f;
    public float generateOnTop = 0.2f;
    public float generateOnRight = 0.6f;
    public float generateOnBottom = 1.0f;

    private void reduceLeftProba()
    {
        if(generateOnLeft - reductor > 0.0f && generateOnTop - reductor/2 > 0.0f && generateOnRight - reductor / 2 > 0.0f)
        {
            if(generateOnTop - reductor/2 > generateOnLeft - reductor && generateOnRight - reductor / 2 > generateOnTop - reductor/2)
            {
                generateOnLeft -= reductor; // 0.05f
                generateOnTop -= reductor / 2; // 0.175
                generateOnRight -= reductor / 2; // 0.575
            }
        }
    }

    private void reduceRightProba()
    {
        if (generateOnRight - reductor / 2 > 0.0f && generateOnTop + reductor / 2 < 1.0f)
        {
            if (generateOnRight - reductor / 2 > generateOnTop + reductor / 2)
            {
                generateOnRight -= reductor / 2; // 0.55f
                generateOnTop += reductor / 2; // 0.175
            }
        }
    }

    private void reduceTopProba()
    {
        if (generateOnTop - reductor / 2 > 0.0f && generateOnLeft + reductor / 2 < 1.0f)
        {
            if (generateOnTop - reductor / 2 > generateOnLeft + reductor / 2)
            {
                generateOnTop -= reductor / 2; // 0.05f
                generateOnLeft += reductor / 2; // 0.175
            }
        }
    }

    private void reduceBottomProba()
    {
        if (generateOnRight + reductor < 1.0f && generateOnLeft + reductor / 2 < 1.0f && generateOnTop + reductor / 2 < 1.0f)
        {
            if (generateOnTop + reductor / 2 > generateOnLeft + reductor && generateOnRight + reductor / 2 > generateOnTop + reductor / 2)
            {
                generateOnRight += reductor; // 0.575
                generateOnLeft += reductor / 2; // 0.575
                generateOnTop += reductor / 2; // 0.575
            }
        }
    }

    public override void Generate(int rooms)
    {
        int posY = (int) Random.Range(0.0f,rooms/2);
        position = new Vector2(0,posY);
        /* Array2D */patern = new Array2D(rooms, rooms);
        patern[0, posY] = 2;

        int roomsBuild = 0;
        int safeExit = 0;
        while(roomsBuild < rooms && safeExit < rooms*10)
        {
            bool build = false;
            float value = Random.Range(0.0f, 1.0f);

            if(value < generateOnLeft)
            {
                if(position.x != 0 && patern[(int)position.x - 1,(int)position.y] == 0)
                {
                    position.x --;
                    build = true;
                    reduceLeftProba();
                }
            }
            else if (value < generateOnTop)
            {
                if (position.y != 0 && patern[(int)position.x, (int)position.y - 1] == 0)
                {
                    position.y --;
                    build = true;
                    reduceRightProba();
                }
            }
            else if (value < generateOnRight)
            {
                if (position.y != rooms - 1 && patern[(int)position.x + 1, (int)position.y] == 0)
                {
                    position.x++;
                    build = true;
                    reduceTopProba();
                }
            }
            else if (value < generateOnBottom)
            {
                if (position.y != rooms - 1 && patern[(int)position.x, (int)position.y + 1] == 0)
                {
                    position.y++;
                    build = true;
                    reduceBottomProba();
                }
            }

            if (build)
            {
                roomsBuild ++;
                patern[(int)position.x, (int)position.y] = 1;
            }
            safeExit++;
        }
    }

    public override void CreateAnnexe(int roomsToAnnexe, AnnexeType type)
    {

    }

}
