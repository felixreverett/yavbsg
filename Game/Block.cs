using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Game
{
    public class Block
    {
        public int id;
        public NamedBinaryTag.ObjectProperty data;
        //scripts go here

        public void Read(BinaryReader r)
        {
            id = r.ReadInt32();
            data = new NamedBinaryTag.ObjectProperty();
            data.Read(r);
        }

        public void Write(BinaryWriter w)
        {
            w.Write(id);
            data.Write(w);
        }
    }
}
