using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DegreeQuest
{
    class Projectile : Actor
    {
        Actor Creator;
        public Location Bearing;
        PType ptype;

        public Projectile(Actor c, Location b, int ms)
        {
            Creator = c;
            Bearing = b;
            MoveSpeed = ms;
            Position = Creator.Position;
            ptype = PType.Dot;
        }

        public Projectile(Actor c, Location b, int ms, PType pt)
        {
            Creator = c;
            Bearing = b;
            MoveSpeed = ms;
            Position = Creator.Position;
            ptype = pt;
        }

        public Projectile(Actor c, Location b, int ms, PType pt, Location o)
        {
            Creator = c;
            Bearing = b;
            MoveSpeed = ms;
            Position = o;
            ptype = pt;
        }

        public override AType GetAType()
        {
            return AType.Projectile;
        }

        public override Vector2 GetPos()
        {
            return Position.toVector2();
        }
    }
}
