using System;
using System.Collections.Generic;
using System.Text;

namespace TachographCardStructure.Types
{
    public class ControlType : IToByte, IGetLength
    {
        public enum C_PARAMETER
        {
            NOT_DOWNLOADED_CARD = 0b_0000_0000,
            DOWNLOADED_CARD = 0b_1000_0000
        }

        public enum V_PARAMETER
        {
            NOT_DOWLOADED_VU = 0b_0000_0000,
            DOWNLOADED_VU = 0b_0100_0000
        }

        public enum P_PARAMETER
        {
            NOT_PRINTED = 0b_0000_0000,
            PRINTED = 0b_0010_0000
        }

        public enum D_PARAMETER
        {
            NOT_USED_DISPLAYER = 0b_0000_0000,
            USED_DISPLAYER = 0b_0001_0000,
        }

        public C_PARAMETER C { get; internal set; }
        public V_PARAMETER V { get; internal set; }
        public P_PARAMETER P { get; internal set; }
        public D_PARAMETER D { get; internal set; }

        public ControlType()
        {
            C = C_PARAMETER.NOT_DOWNLOADED_CARD;
            V = V_PARAMETER.NOT_DOWLOADED_VU;
            P = P_PARAMETER.NOT_PRINTED;
            D = D_PARAMETER.NOT_USED_DISPLAYER;
        }

        public ControlType(C_PARAMETER C, V_PARAMETER V, P_PARAMETER P, D_PARAMETER D)
        {
            this.C = C;
            this.V = V;
            this.P = P;
            this.D = D;
        }

        public ushort GetLength()
        {
            return 1;
        }

        public byte[] ToByte()
        {
            return new byte[] { (byte)((byte)C | (byte)V | (byte)P | (byte)D) };
        }
    }
}
