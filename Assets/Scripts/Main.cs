using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using Debug = UnityEngine.Debug;
using IronPython.Hosting;
using System.Threading.Tasks;
using DefaultNamespace;

public class Main : MonoBehaviour
{
    public Transform[] respawns;
    public SpriteRenderer heart;
    public Sprite[] heartColors;

    // public SpriteRenderer[] gems = new SpriteRenderer[4];
    
    // private float[] bars = new float[4] {0, 0, 0, 0};
    //
    // private string[] text = new string[4] {"0", "0", "0", "0"};


    void Start()
    {
        GlobalData.respawns = respawns;
        GlobalData.checkRoom();
    }

    void Update()
    {
        try
        {
            // text = System.IO.File.ReadAllText(@"Assets/Scripts/emotion.txt").Split(' ');
            //
            // int maxi = 0;
            // for (int i = 0; i < 4; i++)
            // {
            //     bars[i] = float.Parse(text[i], CultureInfo.InvariantCulture);
            //     if (bars[maxi] < bars[i])
            //         maxi = i;
            //     var col = gems[i].color;
            //     gems[i].color = new Color(col.r, col.g, col.b, bars[i]);
            // }
            //
            // if (GlobalData.activeEmo == -1)
            // {
            //     gemAnimators[maxi].SetBool("Active", true);
            //     GlobalData.activeEmo = maxi;
            // }
            //
            // if (maxi != GlobalData.activeEmo)
            // {
            //     gemAnimators[GlobalData.activeEmo].SetBool("Active", false);
            //     gemAnimators[maxi].SetBool("Active", true);
            //     GlobalData.activeEmo = maxi;
            // }
            int em = int.Parse(System.IO.File.ReadAllText(@"Assets/Scripts/emotion.txt"));
            heart.sprite = heartColors[em];
            GlobalData.activeEmo = em;
        }
        catch{ }


        // Temporary for not using emotions
        // if (Input.GetKeyDown("[0]"))
        // {
            // using (FileStream fstream = new FileStream(@"Assets/Scripts/emotion.txt", FileMode.OpenOrCreate))
            // {
            //     string txt = "1 0.1 0.1 0.1";
            //     fstream.Write(System.Text.Encoding.Default.GetBytes(txt), 0, txt.Length);
            // }
        // }
        //
        // if (Input.GetKeyDown("[1]"))
        // {
        //     using (FileStream fstream = new FileStream(@"Assets/Scripts/emotion.txt", FileMode.OpenOrCreate))
        //     {
        //         string txt = "0.1 1 0.1 0.1";
        //         fstream.Write(System.Text.Encoding.Default.GetBytes(txt), 0, txt.Length);
        //     }
        // }
        //
        // if (Input.GetKeyDown("[2]"))
        // {
        //     using (FileStream fstream = new FileStream(@"Assets/Scripts/emotion.txt", FileMode.OpenOrCreate))
        //     {
        //         string txt = "0.1 0.1 1 0.1";
        //         fstream.Write(System.Text.Encoding.Default.GetBytes(txt), 0, txt.Length);
        //     }
        // }
        //
        // if (Input.GetKeyDown("[3]"))
        // {
        //     using (FileStream fstream = new FileStream(@"Assets/Scripts/emotion.txt", FileMode.OpenOrCreate))
        //     {
        //         string txt = "0.1 0.1 0.1 1";
        //         fstream.Write(System.Text.Encoding.Default.GetBytes(txt), 0, txt.Length);
        //     }
        // }
        
        if (Input.GetKeyDown("[0]"))
        {
            using (FileStream fstream = new FileStream(@"Assets/Scripts/emotion.txt", FileMode.OpenOrCreate))
            {
                string txt = "0";
                fstream.Write(System.Text.Encoding.Default.GetBytes(txt), 0, txt.Length);
            }
        }
        
        if (Input.GetKeyDown("[1]"))
        {
            using (FileStream fstream = new FileStream(@"Assets/Scripts/emotion.txt", FileMode.OpenOrCreate))
            {
                string txt = "1";
                fstream.Write(System.Text.Encoding.Default.GetBytes(txt), 0, txt.Length);
            }
        }
        
        if (Input.GetKeyDown("[2]"))
        {
            using (FileStream fstream = new FileStream(@"Assets/Scripts/emotion.txt", FileMode.OpenOrCreate))
            {
                string txt = "2";
                fstream.Write(System.Text.Encoding.Default.GetBytes(txt), 0, txt.Length);
            }
        }
        
        if (Input.GetKeyDown("[3]"))
        {
            using (FileStream fstream = new FileStream(@"Assets/Scripts/emotion.txt", FileMode.OpenOrCreate))
            {
                string txt = "3";
                fstream.Write(System.Text.Encoding.Default.GetBytes(txt), 0, txt.Length);
            }
        }
    }
    
}