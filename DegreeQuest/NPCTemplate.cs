using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DegreeQuest
{
    public class NPCTemplate
    {
        public Dice HP, EP;
        public int[] subjects;
        public Texture2D texture;

        NPCTemplate(Dice HP, Dice EP, int[] subjects,Texture2D texture)
        {
            this.HP = HP;
            this.EP = EP;
            this.subjects = subjects;
            this.texture = texture;
        }

        NPC create()
        {
            return new NPC(this);
        }
    }
}
