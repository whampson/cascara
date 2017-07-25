﻿#region License
/* Copyright (c) 2017 Wes Hampson
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */
#endregion

using System;
using System.Runtime.InteropServices;

namespace WHampson.Cascara.Types
{
    /// <summary>
    /// A 64-bit signed integer.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CascaraInt64 : ICascaraType,
        IComparable, IComparable<CascaraInt64>, IEquatable<CascaraInt64>
    {
        private const int Size = 8;

        private long m_value;

        private CascaraInt64(long value)
        {
            m_value = value;
        }

        public int CompareTo(CascaraInt64 other)
        {
            if (m_value < other.m_value)
            {
                return -1;
            }
            else if (m_value > other.m_value)
            {
                return 1;
            }

            return 0;
        }

        public bool Equals(CascaraInt64 other)
        {
            return m_value == other.m_value;
        }

        int IComparable.CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            if (!(obj is CascaraInt64))
            {
                string fmt = "Object is not an instance of {0}.";
                string msg = string.Format(fmt, GetType().Name);

                throw new ArgumentException(msg, "obj");
            }

            return CompareTo((CascaraInt64) obj);
        }

        byte[] ICascaraType.GetBytes()
        {
            return BitConverter.GetBytes(m_value);
        }

        int ICascaraType.GetSize()
        {
            return Size;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is CascaraInt64))
            {
                return false;
            }

            return Equals((CascaraInt64) obj);
        }

        public override int GetHashCode()
        {
            // Lower 32 bits XORed with upper 32 bits
            return unchecked((int) m_value) ^ ((int) (m_value >> 32));
        }

        public override string ToString()
        {
            return m_value.ToString();
        }

        public static implicit operator CascaraInt64(long value)
        {
            return new CascaraInt64(value);
        }

        public static explicit operator long(CascaraInt64 value)
        {
            return value.m_value;
        }
    }
}