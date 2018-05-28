using System;
using System.IO;
using System.Collections.Generic;

namespace Game
{
    public class NamedBinaryTag
    {
        static readonly Type[] TYPES = new[] { null, typeof(IntProperty), typeof(BoolProperty), typeof(StringProperty), typeof(FloatProperty), typeof(ObjectProperty) };

        #region Definitions
        public class Property<T>
        {
            public T value;

            public virtual void Read(BinaryReader r) { }

            public virtual void Write(BinaryWriter w) { }
        }

        public class IntProperty : Property<int> //integer property
        {
            public override void Read(BinaryReader r)
            {
                value = r.ReadInt32();
            }

            public override void Write(BinaryWriter w)
            {
                w.Write(value);
            }
        }

        public class BoolProperty : Property<bool> //boolean property
        {
            public override void Read(BinaryReader r)
            {
                value = r.ReadBoolean();
            }

            public override void Write(BinaryWriter w)
            {
                w.Write(value);
            }
        }

        public class StringProperty : Property<string> //text property
        {
            public override void Read(BinaryReader r)
            {
                value = r.ReadString();
            }

            public override void Write(BinaryWriter w)
            {
                w.Write(value);
            }
        }

        public class FloatProperty : Property<float> //float property
        {
            public override void Read(BinaryReader r)
            {
                value = r.ReadSingle();
            }

            public override void Write(BinaryWriter w)
            {
                w.Write(value);
            }
        }

        --public class ObjectProperty : Property<Dictionary<string, object>>
        {
            public override void Read(BinaryReader r)
            {
                value = new Dictionary<string, object>();
                byte type = r.ReadByte();
                string tag;
                while (type > 0)
                {
                    tag = r.ReadString();
                    dynamic v = Activator.CreateInstance(TYPES[type]);
                    v.Read(r);
                    value.Add(tag, v);
                    type = r.ReadByte();
                }
            }

            public override void Write(BinaryWriter w)
            {
                byte type = 0;
                foreach (string t in value.Keys)
                {
                    type = (byte)Array.IndexOf(TYPES, value[t].GetType());
                    w.Write(type);
                    w.Write(t);
                    //Type newtype = typeof(Property<>).MakeGenericType(TYPES[type]); //wtf mad hacks
                    dynamic o = Convert.ChangeType(value[t], value[t].GetType());
                    o.Write(w);
                }
                w.Write((byte)0); //without (byte) it will write a 4-byte signed integer version of 0
            }
            #region Getters
            public string GetString(string tag, string def)
            {
                try
                {
                    return ((StringProperty)value[tag]).value;
                }
                catch
                {
                    return def;
                }
            }

            public int GetInt(string tag, int def)
            {
                try
                {
                    return ((IntProperty)value[tag]).value;
                }
                catch
                {
                    return def;
                }
            }

            public float GetFloat(string tag, float def)
            {
                try
                {
                    return ((FloatProperty)value[tag]).value;
                }
                catch
                {
                    return def;
                }
            }

            public bool GetBool(string tag, bool def)
            {
                try
                {
                    return ((BoolProperty)value[tag]).value;
                }
                catch
                {
                    return def;
                }
            }

            public ObjectProperty GetObject(string tag)
            {
                try
                {
                    return (ObjectProperty)value[tag];
                }
                catch
                {
                    return new ObjectProperty() { value = new Dictionary<string, object>() };
                }
            }
            #endregion
            #region Setters
            public void SetString(string tag, string val)
            {
                value[tag] = new StringProperty() { value = val };
            }

            public void SetInt(string tag, int val)
            {
                value[tag] = new IntProperty() { value = val };
            }

            public void SetFloat(string tag, float val)
            {
                value[tag] = new FloatProperty() { value = val };
            }

            public void SetBool(string tag, bool val)
            {
                value[tag] = new BoolProperty() { value = val };
            }

            public void SetObject(string tag, ObjectProperty val)
            {
                value[tag] = val;
            }
            #endregion
        }
        #endregion
    }
}