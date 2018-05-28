using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Game
{
    public class Entity
    {
        public Vector3 Position;
        public Vector3 Size;
        public float Yaw, Pitch;
        //worldin

        public Entity()
        {
            
        }

        public Vector3 GetLookingVector()
        {
            return new Vector3((float)(Math.Sin(Yaw) * Math.Cos(Pitch)), (float)(Math.Sin(Pitch)), (float)(Math.Cos(Yaw) * Math.Cos(Pitch)));
        }

        public Vector3 GetWalkingVector()
        {
            return new Vector3((float)(Math.Sin(Yaw)), 0f, (float)(Math.Cos(Yaw)));
        }
    }
}
