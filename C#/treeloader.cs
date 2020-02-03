
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;
 
 
public class treeloader : MonoBehaviour
{

    [SerializeField]
    string path_to_savefile;
    [SerializeField]
    public int LEvel;
    [SerializeField]
    public int health;
    [SerializeField]
    public int mana;
    [SerializeField]
    public int stamina;
    [SerializeField]
    public int exp;
    [SerializeField]
    public int score;
    [SerializeField]
    public int gold;
    [SerializeField]
    public int experience;

    int expe;
    Player player;


    Transform transform_p;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            System.IO.FileStream fs = new System.IO.FileStream(path_to_savefile + "/Save_test",
                                                               System.IO.FileMode.Open,
                                                               System.IO.FileAccess.Read);
            System.IO.BinaryReader reader = new System.IO.BinaryReader(fs);
            Player player = other.GetComponent<Player>();
            Transform transform_p = other.GetComponent<Transform>();
            string save = "";
            while (reader.PeekChar() != -1)
            {
                save += reader.ReadChar();
            }
            /*health = 0;
            mana = 0;
            stamina = 0;
            experience = 0;
            score = 0;
            gold = 0;*/
            int counter = 0;
            string s = "";
            float x = 0;
            float y = 0;
            float z = 0;
            float rx = 0;
            float ry = 0;
            float rz = 0;
            for (int i = 1; i < save.Length; i++)
            {
                if (!(save[i].Equals('n')))
                {
                    s += save[i];
                }
                else if (s.Length != 0)
                {


                    if (counter == 0)
                    {
                        int level = LEvel;
                        for (int n = 0; n < s.Length; n++)
                        {
                            level += ((int)s[n] - 48) * (int)System.Math.Pow(10, s.Length - n - 1);
                        }
                        increase_expe(player, level);
                        counter++;
                        s = "";
                    }
                    if (counter == 1)
                    {
                        for (int n = 0; n < s.Length; n++)
                        {
                            health += ((int)s[n] - 48) * (int)System.Math.Pow(10, s.Length - n - 1);
                        }
                        player.Setlife(health);
                        counter++;
                        s = "";
                    }
                    if (counter == 2)
                    {
                        for (int n = 0; n < s.Length; n++)
                        {
                            mana += ((int)s[n] - 48) * (int)System.Math.Pow(10, s.Length - n - 1);
                        }
                        player.Setmana(mana);
                        counter++;
                        s = "";
                    }

                    if (counter == 3)
                    {
                        for (int n = 0; n < s.Length; n++)
                        {
                            stamina += ((int)s[n] - 48) * (int)System.Math.Pow(10, s.Length - n - 1);
                        }
                        player.Setstamina(stamina);
                        counter++;
                        s = "";
                    }

                    if (counter == 4)
                    {
                        for (int n = 0; n < s.Length; n++)
                        {
                            score += ((int)s[n] - 48) * (int)System.Math.Pow(10, s.Length - n - 1);
                        }
                        player.Setscore(score);
                        counter++;
                        s = "";
                    }

                    if (counter == 5)
                    {
                        for (int n = 0; n < s.Length; n++)
                        {
                            gold += ((int)s[n] - 48) * (int)System.Math.Pow(10, s.Length - n - 1);
                        }
                        player.Setgold(gold);
                        counter++;
                        s = "";
                    }

                    if (counter == 6)
                    {
                        for (int n = 0; n < s.Length; n++)
                        {
                            experience += ((int)s[n] - 48) * (int)System.Math.Pow(10, s.Length - n - 1);
                        }
                        player.Setexp(experience);
                        counter++;
                        s = "";
                    }
                    if (counter == 7)
                    {
                        bool b = false;
                        for (int n = 0; n < s.Length; n++)
                        {
                            if (s[n] == '.')
                            {
                                b = true;
                                x /= (float)System.Math.Pow(10, s.Length - n);
                            }
                            else if (b)
                            {
                                x += ((int)s[n] - 48) * (int)System.Math.Pow(10, n - s.Length);
                            }
                            else
                            {
                                x += ((int)s[n] - 48) * (int)System.Math.Pow(10, s.Length - n - 1);
                            }

                        }
                        counter++;
                        s = "";
                    }
                    if (counter == 8)
                    {
                        bool b = false;
                        for (int n = 0; n < s.Length; n++)
                        {
                            if (s[n] == '.')
                            {
                                b = true;
                                y /= (float)System.Math.Pow(10, s.Length - n);
                            }
                            else if (b)
                            {
                                y += ((int)s[n] - 48) * (int)System.Math.Pow(10, n - s.Length);
                            }
                            else
                            {
                                y += ((int)s[n] - 48) * (int)System.Math.Pow(10, s.Length - n - 1);
                            }
                            counter++;
                            s = "";
                        }
                    }
                    if (counter == 9)
                    {
                        bool b = false;
                        for (int n = 0; n < s.Length; n++)
                        {
                            if (s[n] == '.')
                            {
                                b = true;
                                z /= (float)System.Math.Pow(10, s.Length - n);
                            }
                            else if (b)
                            {
                                z += ((int)s[n] - 48) * (int)System.Math.Pow(10, n - s.Length);
                            }
                            else
                            {
                                z += ((int)s[n] - 48) * (int)System.Math.Pow(10, s.Length - n - 1);
                            }
                            counter++;
                            s = "";
                        }
                    }
                    if (counter == 10)
                    {
                        bool b = false;
                        for (int n = 0; n < s.Length; n++)
                        {
                            if (s[n] == '.')
                            {
                                b = true;
                                rx /= (float)System.Math.Pow(10, s.Length - n);
                            }
                            else if (b)
                            {
                                rx += ((int)s[n] - 48) * (int)System.Math.Pow(10, n - s.Length);
                            }
                            else
                            {
                                rx += ((int)s[n] - 48) * (int)System.Math.Pow(10, s.Length - n - 1);
                            }
                            counter++;
                            s = "";
                        }
                    }
                    if (counter == 11)
                    {
                        bool b = false;
                        for (int n = 0; n < s.Length; n++)
                        {
                            if (s[n] == '.')
                            {
                                b = true;
                                ry /= (float)System.Math.Pow(10, s.Length - n);
                            }
                            else if (b)
                            {
                                ry += ((int)s[n] - 48) * (int)System.Math.Pow(10, n - s.Length);
                            }
                            else
                            {
                                ry += ((int)s[n] - 48) * (int)System.Math.Pow(10, s.Length - n - 1);
                            }
                            counter++;
                            s = "";
                        }
                    }
                    if (counter == 12)
                    {
                        bool b = false;
                        for (int n = 0; n < s.Length; n++)
                        {
                            if (s[n] == '.')
                            {
                                b = true;
                                rz /= (float)System.Math.Pow(10, s.Length - n);
                            }
                            else if (b)
                            {
                                rz += ((int)s[n] - 48) * (int)System.Math.Pow(10, n - s.Length);
                            }
                            else
                            {
                                rz += ((int)s[n] - 48) * (int)System.Math.Pow(10, s.Length - n - 1);
                            }
                            counter++;
                            s = "";
                        }
                    }

                }
            }
            player.TP(new Vector3(50, 202, 50), Quaternion.Euler(0, ry, 0));
            //p.myTransform.position = new Vector3(x, y, z);
            //p.myTransform.rotation = Quaternion.Euler(rx, ry, rz);
            fs.Close();
        }
    }
    public void increase_expe(Player p, int level)
    {

        if (level > 1)
        {
            int exp = experience;
            exp = 20 - 110 * (level - 1);
            for (int i = 2; i <= level; i++)
            {
                exp += System.Convert.ToInt32(120 * Mathf.Pow(1.5f, (i - 1)));
            }
            p.Setexp(exp);
        }
        else
        {
            p.Setexp(0);
        }
    }

}


