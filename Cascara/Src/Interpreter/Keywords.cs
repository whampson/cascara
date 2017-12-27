﻿using System;
using System.Collections.Generic;

namespace WHampson.Cascara
{
    internal static partial class ReservedWords
    {
        public static class Keywords
        {
            // XML root element
            public const string DocumentRoot = "cascaraLayout";

            // Data types
            public const string Bool = "bool";
            public const string Bool8 = "bool8";
            public const string Bool16 = "bool16";
            public const string Bool32 = "bool32";
            public const string Bool64 = "bool64";
            public const string Byte = "byte";
            public const string Char = "char";
            public const string Char8 = "char8";
            public const string Char16 = "char16";
            public const string Double = "double";
            public const string Float = "float";
            public const string Int = "int";
            public const string Int8 = "int8";
            public const string Int16 = "int16";
            public const string Int32 = "int32";
            public const string Int64 = "int64";
            public const string Long = "long";
            public const string Short = "short";
            public const string Single = "single";
            public const string Struct = "struct";
            public const string UInt = "uint";
            public const string UInt8 = "uint8";
            public const string UInt16 = "uint16";
            public const string UInt32 = "uint32";
            public const string UInt64 = "uint64";
            public const string ULong = "ulong";
            public const string Union = "union";
            public const string UShort = "ushort";

            // Imperatives
            public const string Align = "align";
            public const string Echo = "echo";
            public const string Include = "include";
            public const string Local = "local";
            public const string Typedef = "typedef";

            public static readonly HashSet<string> AllKeywords = new HashSet<string>(
                typeof(Keywords).GetPublicConstants<string>());
        }
    }
}