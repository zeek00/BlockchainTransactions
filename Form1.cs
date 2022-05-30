using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlockchainTransactions
{
    public partial class Form1 : Form
    {
        //List<Block> blockchain = new List<Block>();
        public BlockChain block_chain = new BlockChain();

        public Form1()
        {
            InitializeComponent();
            block_chain._genesisBlock(block_chain);
            //foreach(Block b in block_chain.listofBlocks)
            //{
            //    //LogThis(b._datahash);
            //    //LogThis(b._prevdatahash);

            //}
            //LogThis(String.Join(",", block_chain.listofBlocks));
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public void LogThis(string logString)
        {
            // Logging to the Log Text Box
            _logbox.AppendText("[" + DateTime.Now + "] " + logString + "\n");
            
          
        }
        public void ledgerLog(string logString)
        {
            // Logging to the Ledger Box
            _ledger.AppendText(logString + "\n");
       
        } 
        
        public void keyLog()
        {
            // Logging to the Key Box
            string data = block_chain._publicKey;
            
           
               
                _keybox.AppendText("[ Key No For current transaction: "  + "]" + 
                    "\n"+ data + "\n-------------------------------------------------------------------------------------------\n");
            

        }



        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void _submit_Click(object sender, EventArgs e)
        {
            string Name = _name.Text;
            string Item = _itemlist.Text;
            string Price = _price.Text;
            long Time = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));




            if (Item == "Antique box set" && Price == "1500")
            {
                _item1.Text = Item + " " + "sold";
            }
            else if (Item == "Antique box set" && Price != "1500")
            {
                int x;
                Int32.TryParse(_price.Text, out x);
                int newprice = 1500-x;
                string myString = newprice.ToString();

                _item1.Text = "ANTIQUE BOX SET \n PRICE LEFT:" + myString;
                
            }
            else if (Item == "Antique clock" && Price == "1000")
            {
   
                _item2.Text = Item + " " + "sold";
            }
            else if (Item == "Antique clock" && Price != "1000")
            {
                int x;
                Int32.TryParse(_price.Text, out x);
                int newprice = 1000 - x;
                string myString = newprice.ToString();

                _item2.Text = "ANTIQUE CLOCK \n PRICE LEFT:" + myString;
            }
            else if (Item == "Antique gramophone" && Price == "2500")
            {
                _item3.Text = Item + " " + "sold";
            }
            else if (Item == "Antique gramophone" && Price != "2500")
            {
                int x;
                Int32.TryParse(_price.Text, out x);
                int newprice = 2500 - x;
                string myString = newprice.ToString();

                _item3.Text = "ANTIQUE GRAMOPHONE \n PRICE LEFT:" + myString;
            }

            block_chain._pendingTransaction(Name, Item, Price, Time);
            int orderNo = 0;
            
            List<Block> l = block_chain.mining(block_chain);
           
            foreach (Block bl in l)
            {
                orderNo++;
                ledgerLog("[ Block No: " + orderNo +"]"  + 
                    "\n" +"block hash: \n" + bl._datahash + 
                    "\n" + "previous block hash: \n" + 
                    bl._prevdatahash +
                    "\n ------------------------------------------------------------------------------------------------------------" );
                

            }
            keyLog();

            BlockChain bchain = new BlockChain();
           
            string[] info = { "Name:", "Item:", "Price:"};
            LogThis(info[0] + " " + Name + " " + info[1] + " " + Item + " " + info[2] + " " + Price);

            


            
            // _logbox.Text = DateTime.Now + " " + info[0] + " " + Name + " " + info[1] + " " + Item + " " + info[2] + " " + Price; 

        }

      

        private void _itemlist_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void _clear_Click(object sender, EventArgs e)
        {
            _name.Text = " " ;
            _itemlist.Text = " ";
            _price.Text = " ";

        }

        private void _name_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
