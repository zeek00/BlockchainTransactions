using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace BlockchainTransactions
{
   public class Hash
    {
        //public string _publicKey;
        //public string _privateKey;

        public string GetSha256FromString(string Data)
        {
            var message = Encoding.ASCII.GetBytes(Data);
            SHA256Managed hashString = new SHA256Managed();
            string hex = "";

            var hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }

    }
}
