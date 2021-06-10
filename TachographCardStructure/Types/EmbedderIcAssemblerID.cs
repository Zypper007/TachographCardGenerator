using System;
using System.Collections.Generic;
using System.Text;
using TachographCardStructure.Types;
using System.Linq;

namespace TachographCardStructure.Types
{
    // 5 bytes
    public class EmbedderIcAssemblerID: IToByte, IGetLength
    {
        public IA5String CountryCode { get; set; } = new IA5String(2); // 2bytes
        public BCDString ModuleEmbedder { get; set; } = new BCDString(2); // 2bytes
        public byte ManufacturerInformatin { get; set; } // 1byte

        public EmbedderIcAssemblerID(string countryCode, string moduleEmbedder , byte manufacturerInformation)
        {
            CountryCode.SetText(countryCode);
            ModuleEmbedder.SetNumber(moduleEmbedder);
            ManufacturerInformatin = manufacturerInformation;
        }

        public byte[] ToByte()
        {
            var bytes = new List<byte>(5);
            var cc_bytes = CountryCode.ToByte();
            var me_bytes = ModuleEmbedder.ToByte();

            return bytes.Concat(cc_bytes)
                .Concat(me_bytes)
                .Append(ManufacturerInformatin).ToArray();
        }

        public ushort GetLength()
        {
            return (ushort)(CountryCode.GetLength() + ModuleEmbedder.GetLength() + 1);
        }
    }
}
