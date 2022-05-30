using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainTransactions
{
    public class BlockChain
    {
        //Form1 f = new Form1();
        public List<Block> listofBlocks = new List<Block>();
        public List<string> pendingTransactions = new List<string>();
       

        public string _datahash
        {
            get; set;
        }

        public string _prevdatahash
        {
            get; set;
        }

        //public Block _prevBlock
        //{
        //    get;set;
        //}

        public string _data
        {
            get; set;
        }

        public long _currenttime
        {
            get; set;
        }

        public long _nonce
        {
            get; set;
        }

        public string _publicKey;
        public string _privateKey;


        //public void _keyGeneration(byte[] data)
        //{
            
        //    RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
        //    string str = RSA.ToXmlString(true);
        //    StringBuilder data = new StringBuilder();
        //    for (int i = 0; i < bytes1; i++)
        //    { data.Append("a"); }
        //    byte[] buffer = Encoding.ASCII.GetBytes(data.ToString());
        //    Console.WriteLine(str);


        //}


        

        public void _genesisBlock(BlockChain block_chain)
        {
            long Time = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
            Block genesisBlock = new Block(new List<string>(), Time, "00000000000000000000000000000000000000000000000000000000000000000");
            genesisBlock._datahash = genesisBlock._runHash();
            proofOfWork(genesisBlock);
            listofBlocks.Add(genesisBlock);
        }



        public Block lastBlock()
        {
            foreach (Block bs in listofBlocks)
            {
                
                Console.WriteLine("Hash: \n"+ bs._datahash);
                Console.WriteLine("Previous Hash: \n" + bs._prevdatahash);
            }
            return this.listofBlocks.Last();
        }

        public string proofOfWork(Block b)
        {
            Hash h = new Hash();
            int no = 4;
            string value = new string(new char[no]).Replace('\0', '0');
            string combine = string.Join(this._prevdatahash, this._nonce, b.data);
            byte[] databytes = Encoding.ASCII.GetBytes(combine);
            _keyGeneration(databytes);

            while (!b._datahash.Substring(0, no).Equals(value))
            {

                _nonce++;
                b._datahash = h.GetSha256FromString(b._datahash);

            }
            return b._datahash;

            
        }


        //public void _addToChain(Block b, string proof, BlockChain block_chain)
        //{
        //    string prevHash = this.lastBlock()._prevdatahash;
        //    if (prevHash != b._prevdatahash)
        //    {
        //        return;
        //    }

        //    //if (proof == b._runHash(block_chain) && proof.StartsWith("0000"))
        //    //{
        //        b._datahash = proof;
        //        listofBlocks.Add(b);
        //    //}

        //}

        public void _addToChain(Block b, BlockChain block_chain)
        {
            //string prevHash = this.lastBlock()._prevdatahash;
            //if (prevHash != b._prevdatahash)
            //{
            //    return;
            //}
            this.listofBlocks.Add(b);
        }

        public void _pendingTransaction(string name, string item, string amount, long time)
        {
            string data = name + " " + item + " " + amount + " " + time;
            byte[] databytes = Encoding.ASCII.GetBytes(data);
            foreach (byte b in databytes)
            {
                Console.WriteLine(b);
            }
            pendingTransactions.Add(data);
        }


        public void _keyGeneration(byte[] data)
        {

            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            _privateKey = RSA.ToXmlString(true);
            byte[] byt = RSA.Encrypt(data, true);
            StringBuilder stringdata = new StringBuilder();
            for (int i = 0; i < byt.Length; i++)
            {
                stringdata.Append(byt[i].ToString("0"));
            }
            _publicKey = stringdata.ToString();
            
            Console.WriteLine(_publicKey);


        }



        public List<Block> mining(BlockChain block_chain)
        {
           
          
            if(pendingTransactions.Count == 0)
            {
                Console.WriteLine("No data available");
            }
            else
            {

                
                Block lBlock = lastBlock();
                string lastHash = lBlock._datahash;
                long Time = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
                Block b = new Block(pendingTransactions, Time, lastHash);
                //string _hash = b._runHash();
                //string _prevhash = b._prevHash();
                //string proof = proofOfWork(b);
                b._datahash = b._runHash();
                proofOfWork(b);
               
                _addToChain(b, block_chain);
                pendingTransactions.Clear();
                //f.ledgerLog("block hash: \n" + _datahash + "\n" + "previous block hash: \n" + _prevdatahash);
            }

            return listofBlocks;
        }

            
        

            

    }
}
