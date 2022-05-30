using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BlockchainTransactions
{
    public class Block
    {

        public string _datahash
        {
            get; set;
        }

        public string _prevdatahash
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


        public List<string> data
        {
            get; set;
        }

     

        public Block(List<string> data, long time, string prevhash)
        {
            this.data = data;
            this._currenttime = time;
            this._prevdatahash = prevhash;
            this._nonce = 0;
        }

        public Block() { }

        public string _runHash()
        {
            Hash h = new Hash();
            string stringFromList = String.Join(",", this.data.ToArray());
            string combine = String.Join(stringFromList,this._prevdatahash,this._nonce);
            _datahash = h.GetSha256FromString(combine);
            return _datahash;

        }

        public string _prevHash(BlockChain block_chain)
        {
         
                int indexval = block_chain.listofBlocks.Count - 1;
                _prevdatahash = block_chain.listofBlocks[indexval]._datahash;
                
           


            return _prevdatahash;

        }


    }
}
