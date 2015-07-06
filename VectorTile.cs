namespace Mapbox.Vectors
{
    namespace mapnik.vector
    {
        [System.Serializable, ProtoBuf.ProtoContract(Name = @"tile")]
        public class tile : ProtoBuf.IExtensible
        {
            private readonly System.Collections.Generic.List<layer> _layers = new System.Collections.Generic.List<mapnik.vector.tile.layer>();
            [ProtoBuf.ProtoMember(3, Name = @"layers", DataFormat = ProtoBuf.DataFormat.Default)]
            public System.Collections.Generic.List<layer> layers
            {
                get { return _layers; }
            }

            [System.Serializable, ProtoBuf.ProtoContract(Name = @"value")]
            public class value : ProtoBuf.IExtensible
            {
                public value() { }

                private string _string_value = "";

                public bool HasStringValue { get; set; }
                public bool HasFloatValue { get; set; }
                public bool HasDoubleValue { get; set; }
                public bool HasIntValue { get; set; }
                public bool HasUIntValue { get; set; }
                public bool HasSIntValue { get; set; }
                public bool HasBoolValue { get; set; }

                [ProtoBuf.ProtoMember(1, IsRequired = false, Name = @"string_value", DataFormat = ProtoBuf.DataFormat.Default)]
                [System.ComponentModel.DefaultValue("")]
                public string string_value
                {
                    get { return _string_value; }
                    set
                    {
                        HasStringValue = true;
                        _string_value = value;
                    }
                }
                private float _float_value = default(float);
                [ProtoBuf.ProtoMember(2, IsRequired = false, Name = @"float_value", DataFormat = ProtoBuf.DataFormat.FixedSize)]
                [System.ComponentModel.DefaultValue(default(float))]
                public float float_value
                {
                    get
                    {
                        return _float_value;
                    }
                    set
                    {
                        _float_value = value;
                        HasFloatValue = true;

                    }
                }
                private double _double_value = default(double);
                [ProtoBuf.ProtoMember(3, IsRequired = false, Name = @"double_value", DataFormat = ProtoBuf.DataFormat.TwosComplement)]
                [System.ComponentModel.DefaultValue(default(double))]
                public double double_value
                {
                    get { return _double_value; }
                    set
                    {
                        _double_value = value;
                        HasDoubleValue = true;
                    }
                }
                private long _int_value = default(long);
                [ProtoBuf.ProtoMember(4, IsRequired = false, Name = @"int_value", DataFormat = ProtoBuf.DataFormat.TwosComplement)]
                [System.ComponentModel.DefaultValue(default(long))]
                public long int_value
                {
                    get { return _int_value; }
                    set
                    {
                        _int_value = value;
                        HasIntValue = true;
                    }
                }
                private ulong _uint_value = default(ulong);
                [ProtoBuf.ProtoMember(5, IsRequired = false, Name = @"uint_value", DataFormat = ProtoBuf.DataFormat.TwosComplement)]
                [System.ComponentModel.DefaultValue(default(ulong))]
                public ulong uint_value
                {
                    get { return _uint_value; }
                    set
                    {
                        _uint_value = value;
                        HasUIntValue = true;
                    }
                }
                private long _sint_value = default(long);
                [ProtoBuf.ProtoMember(6, IsRequired = false, Name = @"sint_value", DataFormat = ProtoBuf.DataFormat.ZigZag)]
                [System.ComponentModel.DefaultValue(default(long))]
                public long sint_value
                {
                    get { return _sint_value; }
                    set
                    {
                        _sint_value = value;
                        HasSIntValue = true;
                    }
                }
                private bool _bool_value = default(bool);
                [ProtoBuf.ProtoMember(7, IsRequired = false, Name = @"bool_value", DataFormat = ProtoBuf.DataFormat.Default)]
                [System.ComponentModel.DefaultValue(default(bool))]
                public bool bool_value
                {
                    get { return _bool_value; }
                    set
                    {
                        _bool_value = value;
                        HasBoolValue = true;
                    }
                }
                private  ProtoBuf.IExtension extensionObject;
                 ProtoBuf.IExtension  ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                { return  ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
            }

            [ System.Serializable,  ProtoBuf.ProtoContract(Name = @"feature")]
            public partial class feature :  ProtoBuf.IExtensible
            {
                public feature() { }

                private ulong _id = default(ulong);
                [ProtoBuf.ProtoMember(1, IsRequired = false, Name = @"id", DataFormat =  ProtoBuf.DataFormat.TwosComplement)]
                [System.ComponentModel.DefaultValue(default(ulong))]
                public ulong id
                {
                    get { return _id; }
                    set { _id = value; }
                }
                private readonly  System.Collections.Generic.List<uint> _tags = new  System.Collections.Generic.List<uint>();
                [ProtoBuf.ProtoMember(2, Name = @"tags", DataFormat =  ProtoBuf.DataFormat.TwosComplement, Options =  ProtoBuf.MemberSerializationOptions.Packed)]
                public  System.Collections.Generic.List<uint> tags
                {
                    get { return _tags; }
                }

                private GeomType _type = GeomType.Unknown;
                [ProtoBuf.ProtoMember(3, IsRequired = false, Name = @"type", DataFormat = ProtoBuf.DataFormat.TwosComplement)]
                [System.ComponentModel.DefaultValue(GeomType.Unknown)]
                public GeomType type
                {
                    get { return _type; }
                    set { _type = value; }
                }
                private readonly  System.Collections.Generic.List<uint> _geometry = new System.Collections.Generic.List<uint>();
                [ProtoBuf.ProtoMember(4, Name = @"geometry", DataFormat = ProtoBuf.DataFormat.TwosComplement, Options =  ProtoBuf.MemberSerializationOptions.Packed)]
                public System.Collections.Generic.List<uint> geometry
                {
                    get { return _geometry; }
                }

                private ProtoBuf.IExtension extensionObject;
                ProtoBuf.IExtension ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                { return ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
            }

            [System.Serializable, ProtoBuf.ProtoContract(Name = @"layer")]
            public class layer : ProtoBuf.IExtensible
            {
                private uint _version;
                [ProtoBuf.ProtoMember(15, IsRequired = true, Name = @"version", DataFormat = ProtoBuf.DataFormat.TwosComplement)]
                public uint version
                {
                    get { return _version; }
                    set { _version = value; }
                }
                private string _name;
                [ProtoBuf.ProtoMember(1, IsRequired = true, Name = @"name", DataFormat = ProtoBuf.DataFormat.Default)]
                public string name
                {
                    get { return _name; }
                    set { _name = value; }
                }
                private readonly System.Collections.Generic.List<feature> _features = new System.Collections.Generic.List<mapnik.vector.tile.feature>();
                [ProtoBuf.ProtoMember(2, Name = @"features", DataFormat = ProtoBuf.DataFormat.Default)]
                public System.Collections.Generic.List<feature> features
                {
                    get { return _features; }
                }

                private readonly System.Collections.Generic.List<string> _keys = new System.Collections.Generic.List<string>();
                [ProtoBuf.ProtoMember(3, Name = @"keys", DataFormat = ProtoBuf.DataFormat.Default)]
                public System.Collections.Generic.List<string> keys
                {
                    get { return _keys; }
                }

                private readonly System.Collections.Generic.List<value> _values = new System.Collections.Generic.List<mapnik.vector.tile.value>();
                [ProtoBuf.ProtoMember(4, Name = @"values", DataFormat = ProtoBuf.DataFormat.Default)]
                public System.Collections.Generic.List<value> values
                {
                    get { return _values; }
                }

                private uint _extent = (uint)4096;
                [ProtoBuf.ProtoMember(5, IsRequired = false, Name = @"extent", DataFormat = ProtoBuf.DataFormat.TwosComplement)]
                [System.ComponentModel.DefaultValue((uint)4096)]
                public uint extent
                {
                    get { return _extent; }
                    set { _extent = value; }
                }
                private ProtoBuf.IExtension extensionObject;
                ProtoBuf.IExtension ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                { return ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
            }

            [ProtoBuf.ProtoContract(Name = @"GeomType")]
            public enum GeomType
            {

                [ProtoBuf.ProtoEnum(Name = @"Unknown", Value = 0)]
                Unknown = 0,

                [ProtoBuf.ProtoEnum(Name = @"Point", Value = 1)]
                Point = 1,

                [ProtoBuf.ProtoEnum(Name = @"LineString", Value = 2)]
                LineString = 2,

                [ProtoBuf.ProtoEnum(Name = @"Polygon", Value = 3)]
                Polygon = 3
            }

            private ProtoBuf.IExtension extensionObject;
            ProtoBuf.IExtension  ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            { return ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
        }

    }
}
