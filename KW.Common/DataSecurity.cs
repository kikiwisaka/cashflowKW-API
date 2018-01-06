using BlowfishNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Common
{
    public static class DataSecurity
    {
        private const string saltKey = "Qm84#hx83E76ild(ndkNXC BM<28dl290anmdj3kf903mnDSJK83234@2";
        public static string Encrypt(string field)
        {
            AHCBlowfish BF = new AHCBlowfish();
            if (!string.IsNullOrWhiteSpace(field))
                return BF.Encrypt(saltKey, field).ToString();
            return string.Empty;
        }

        public static string Decrypt(string field)
        {
            AHCBlowfish BF = new AHCBlowfish();
            if (!string.IsNullOrWhiteSpace(field))
                return BF.Decrypt(saltKey, field).ToString();

            return string.Empty;
        }
    }
}
