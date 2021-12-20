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
    public Animator[] gemAnimators=new Animator[4]; 
    public SpriteRenderer[] gems=new SpriteRenderer[4];
    //private int activeEmo=-1;
    private float[] bars=new float[4]{0,0,0,0};
    private string[] text=new string[4]{"0","0","0","0"};
    // private Process p= new Process(); 
    dynamic IronEmo=null;
    void Start()
    {
        // run_py();
    }

    void Update()
    {
        text = System.IO.File.ReadAllText(@"Assets/Scripts/emotion.txt").Split(' ');
        //bars=new float[]{0,0,0,0};
        int maxi=0;
        for(int i=0; i<4;i++){
            bars[i]=float.Parse(text[i],CultureInfo.InvariantCulture);
            if(bars[maxi]<bars[i])
                maxi=i;
            var col=gems[i].color;
            gems[i].color=new Color(col.r,col.g,col.b,bars[i]);
        }

        if (GlobalData.activeEmo == -1){
            gemAnimators[maxi].SetBool("Active",true);
            GlobalData.activeEmo=maxi;
        }
        if(maxi!=GlobalData.activeEmo){
            gemAnimators[GlobalData.activeEmo].SetBool("Active",false);        
            gemAnimators[maxi].SetBool("Active",true);
            GlobalData.activeEmo=maxi;
        }
        
        
        
        
        
        // Temporary for not using emotions
        if (Input.GetKeyDown("[0]"))
        {
            using (FileStream fstream = new FileStream(@"Assets/Scripts/emotion.txt", FileMode.OpenOrCreate))
            {
                string txt = "1 0.1 0.1 0.1";
                fstream.Write(System.Text.Encoding.Default.GetBytes(txt), 0, txt.Length);
            }
        }
        if (Input.GetKeyDown("[1]"))
        {
            using (FileStream fstream = new FileStream(@"Assets/Scripts/emotion.txt", FileMode.OpenOrCreate))
            {
                string txt = "0.1 1 0.1 0.1";
                fstream.Write(System.Text.Encoding.Default.GetBytes(txt), 0, txt.Length);
            }
        }
        if (Input.GetKeyDown("[2]"))
        {
            using (FileStream fstream = new FileStream(@"Assets/Scripts/emotion.txt", FileMode.OpenOrCreate))
            {
                string txt = "0.1 0.1 1 0.1";
                fstream.Write(System.Text.Encoding.Default.GetBytes(txt), 0, txt.Length);
            }
        }
        if (Input.GetKeyDown("[3]"))
        {
            using (FileStream fstream = new FileStream(@"Assets/Scripts/emotion.txt", FileMode.OpenOrCreate))
            {
                string txt = "0.1 0.1 0.1 1";
                fstream.Write(System.Text.Encoding.Default.GetBytes(txt), 0, txt.Length);
            }
        }
    }

    void run_py(){
        var engine = Python.CreateEngine();
        ICollection<string> searchPaths = engine.GetSearchPaths();
        searchPaths.Add(Application.dataPath);
        searchPaths.Add(Application.dataPath + @"\Plugins\Lib\");
        engine.SetSearchPaths(searchPaths);
        dynamic py = engine.ExecuteFile(Application.dataPath + @"/Scripts/IronEmo.py");
        IronEmo = py.IronEmo();
    }

    // private async void run_cmd()
    // {
    //     p.StartInfo = new ProcessStartInfo()
    //     {
    //         FileName = @"C:\Windows\py.exe",
    //         Arguments = @"C:\Users\mssiz\Unity\Test\Assets\Scripts\test.py",
    //         RedirectStandardOutput = true,
    //         UseShellExecute = false,
    //         CreateNoWindow = false
    //     };
    //     await Task.Run(()=>{p.Start();}); 
    // }

    // private void run_py()
    // {
    //     ProcessStartInfo startInfo = new ProcessStartInfo("python");
    //     Process process = new Process();
    //     startInfo.FileName = @"Assets/Scripts/webemo.py";
    //     startInfo.UseShellExecute = false;
    //     startInfo.CreateNoWindow = true;
    //     startInfo.RedirectStandardError = true;
    //     startInfo.RedirectStandardOutput = true;
    //     StreamReader reader = process.StandardOutput;
    //     string result = reader.ReadToEnd();
    //     Debug.Log(result);
    // }
}
