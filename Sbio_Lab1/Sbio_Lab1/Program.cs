using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Sbio_Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream fs=new FileStream("RNA_codon_table.txt",FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            Dictionary<string,string> rna = new Dictionary<string, string>(); //diff between keyvalure pair and dict??
            //KeyValuePair<stirng, string> rna = new KeyValuePair<string, string>();
            while (sr.Peek() != -1) {
            string tmp=sr.ReadLine();
            rna.Add(tmp.Substring(0, 3), tmp.Substring(4, 1));
            //Console.WriteLine(rna.Keys.ToString(), rna.Values.ToString());
        }
            fs.Close();
            sr.Close();
            Console.WriteLine("enter protein");
            //Console.WriteLine("enter dna followed by protein");
            //string dna = Console.ReadLine(); //TO READ FROM CONSOLE
            FileStream f = new FileStream("test1.txt", FileMode.Open);
            StreamReader r = new StreamReader(f);
          
            string dna = "";
            while (r.Peek() != -1)
            {
                dna += r.ReadLine(); //read directly from file, dna too big for console
            }
            f.Close();
            r.Close();
            string protein = Console.ReadLine();
            string ac = ""; //amino acids
            string codon="";
            string answer = "";
           
            for (int i = 0; i<6; i++) //O(1) total O(M*N)
            {
                if (i < 3) { 
                    //int codon_no = (dna.Length - i) / 3;
                    for (int j = i; j+2 < dna.Length-i; j+=3) //no of divisions of sequence  O(N)
                    {
                        //if (j + 2 <= dna.Length - i)
                            codon = dna.Substring(j, 3);
                        //else
                            //codon = "";
                        //search for codon in dict;
                        if (rna.ContainsKey(codon))
                        {
                            //string.Concat(answer, rna[codon].ToString());
                            ac+= rna[codon]; //amino acids of the given dna
                        }
                        
                    }
                    
                        if (ac.Contains(protein))
                        { 
                            for (int k =0; k < ac.Length; k++)
                            { //O(M*N)
                                for (int z = 0; z < protein.Length; z++)
                                    if (ac[k] == protein[z]) { 
                                        //answer += dna.Substring(ac.IndexOf(protein) * 3, 3);
                                        answer += dna.Substring((k * 3) + i, 3);
                                    }
                            }
                        }
                    if(answer!="")
                    Console.WriteLine(answer+"\n");
                    answer = "";
                    ac = "";
                }
                else
                {
                    string Tdna = dna;
                    for (int j = 0; j < dna.Length; j++)
                    {
                        if (Tdna[j] == 'T')
                            Tdna.Replace(Tdna[j],'U'); //replace every T by U
                    }
                    Tdna.Reverse();

                    for (int j = i-3; j+2 < Tdna.Length-(i-3); j+=3) //no of divisions of sequence  O(N) //I-3 to remove the 3 times of the dna before replacing
                    {                                               //T with U ,***(fix it)***
                        codon = Tdna.Substring(j, 3);
                         if (rna.ContainsKey(codon))
                            ac += rna[codon]; //amino acids of the given dna
                        
                    }
                    if (ac.Contains(protein))
                    {
                        for (int k = 0; k < ac.Length; k++)
                        { //O(M*N)
                            for (int z = 0; z < protein.Length; z++)
                                if (ac[k] == protein[z])
                                {
                                    //answer += dna.Substring(ac.IndexOf(protein) * 3, 3);
                                    answer += dna.Substring((k * 3) + (i-3), 3);
                                }
                        }
                    }
                    if (answer != "")
                        Console.WriteLine(answer+"\n");
                    answer = "";
                    ac = "";        //kan mmkn el code yb2a a2sar men keda kteer, ***(fix it)***
                }
            }
        }
    }
}
