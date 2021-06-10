using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TachographCardStructure.Types
{

    // reprezentuje zapis liczby 10 w systemie 2
    // możliwe wartości 0 .. 9
    // w jednym byte mieścią się 2 cyfry 
    public class BCDString : IToByte, IGetLength
    {
        private byte[] bytes;


        public BCDString(uint size)
        {
            bytes = new byte[size];
        }

        public int Length => bytes.Length;

        public int this[int key]
        {
            get
            {
                var a = readA(bytes[key]);
                var b = readB(bytes[key]);
                return a * 10 + b;
            }
            set
            {
                if (value > 99 || value < 0) throw new Exception("[BCDString::this[] set] number over size");
                var a = value / 10;
                var b = value - a * 10;
                bytes[key] = Compress(a, b);
            }
        } 

        public void SetNumber(string number)
        {
            if (number.Length % 2 != 0) number = "0" + number;
            var partial = number.Length / bytes.Length;

            for (int i = 0; i < bytes.Length; i++)
            {
                var num = number.Substring(partial * i, partial);
                this[i] = Convert.ToInt32(num);
            }
        }

        public void SetNumber(int number)
        {
            SetNumber(number.ToString());
        }

        public override string ToString()
        {
            var s = "";
            for(int i =0; i< Length; i++)
            {
                var b = this[i];
                var sb = b.ToString();
                if (sb.Length == 1) sb = "0" + sb;
                s += sb;
            }
            return s;
        }

        public static implicit operator int(BCDString self)
        {
            return Convert.ToInt32(self.ToString());
        }

        // zapisuje dwie cyfry w zapisie binarnym na 8 bitach
        private byte Compress(int a, int b)
        {
            return (byte)(a << 4 | b);
        }

        // zwraca liczbę stworzonych z bitów 7,6,5,4
        private byte readA(byte b)
        {
            return (byte)(b >> 4);
        }
        // zwraca liczbę stworzoną z bitów 3,2,1,0
        private byte readB(byte b)
        {
            b = (byte)(b << 4);
            return readA(b);
        }

        // zwraca bity
        public byte[] ToByte()
        {
            return bytes;
        }

        public ushort GetLength()
        {
            return (ushort)bytes.Length; 
        }
    }
}
