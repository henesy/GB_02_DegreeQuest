using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Specialized;
using System.IO;

namespace DegreeQuest
{
    public class NPCTemplate
    {
        public static readonly String NPC_FILE =  @"..\..\..\..\Resources\NPC.txt";
        public static String[] columns = { "Name", "HP", "EP", "Subjects" };
        public String name;
        public Dice HP, EP;
        public BitVector32 subjects;
        public Texture2D texture;

        NPCTemplate(String name,Dice HP, Dice EP, BitVector32 subjects)
        {
            this.name = name;
            this.HP = HP;
            this.EP = EP;
            this.subjects = subjects;
            this.texture = null;
        }

        NPCTemplate(String name, Dice HP, Dice EP, int subjects): this(name,HP,EP,new BitVector32(subjects)) {}

        NPC create()
        {
            return new NPC(this);
        }

        public static void update()
        {
            String name;
            Dice HP, EP;
            BitVector32 subjects;
            Console.WriteLine(Path.GetFullPath(NPC_FILE));
            String[] lines = File.ReadAllLines(NPC_FILE, Encoding.UTF8);
            if (lines[0] != "Version 1.0.0")
                throw new Exception();
            int i = 1;
            while (i < lines.Length)
            {
                if (lines[i].Length<7 || lines[i].Substring(0,5) != "Name:")
                {
                    i++; continue;
                }
                else
                {
                    name = lines[i].Substring(6);
                    HP = new Dice(lines[i+1].Substring(4));
                    EP = new Dice(lines[i + 2].Substring(4));
                    subjects = Subject.listToVect(lines[i + 3].Substring(10));
                    String[] values = { name, HP.ToString(), EP.ToString(), subjects.ToString() };
                    DBReader.insertRecord("NPCTemplate",columns, values);
                    i = i+5;
                }
            }
        }

    }
}
