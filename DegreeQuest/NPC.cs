using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeQuest
{
    /* Generic NPC class type */
    class NPC : Actor
    {
        Vector2 Position;

        /* As per Actor */
        public AType GetAType()
        { return AType.NPC; }

        public Vector2 GetPos()
        { return Position; }
    }
}
