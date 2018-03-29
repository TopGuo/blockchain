using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SimpleBlockchainApp
{
    /// <summary>
    /// ������
    /// </summary>
    public class Blockchain : IEnumerable<Block>
    {
        /// <summary>
        /// ����
        /// </summary>
        public List<Block> Chain { get; private set; }

        /// <summary>
        /// ���ѳ̶ȣ��ȼ�1-10��
        /// </summary>
        public int Difficulty { get; set; }

        /// <summary>
        /// ���ҽ�������Ϊ ��������
        /// </summary>
        public List<Transaction> PendingTransactions { get; private set; }

        /// <summary>
        /// ���� ���Ѳ�� ��Ϊ���󹤣�������
        /// </summary>
        public double MineReward { get; set; }
    
        public Blockchain(int difficulty = 5, double mineReward = 100.0)
        {
            this.Chain = new List<Block>();
            this.PendingTransactions = new List<Transaction>();
            //�����������
            this.Chain.Add(CreateGenesisBlock());

            this.Difficulty = difficulty;
            this.MineReward = mineReward;
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        /// <returns></returns>

        private Block CreateGenesisBlock()
        {
            return new Block(DateTime.Parse("2018-03-24 14:14:14"), new List<Transaction>());
        }

        /// <summary>
        /// ���ɽ���
        /// </summary>
        /// <param name="transaction"></param>
        public void CreateTransaction(Transaction transaction)
        {
            this.PendingTransactions.Add(transaction);
        }

        /// <summary>
        /// ���ڵ��� д��һ��Ԥ������Ϣ ��ȡ����
        /// </summary>
        /// <param name="mineRewardAddress">��hash��ַ</param>
        public void MinePendingTransactions(string mineRewardAddress)
        {
            var block = new Block(DateTime.Now, this.PendingTransactions);
            //
            block.MineBlock(this.Difficulty);

            System.Console.WriteLine($"Block successfully mined: {block.Hash}");

            this.Chain.Add(block);
            //fromAdress Ϊnull ��ʾû�з����ߣ��ÿ��ǿ��ڵ� ������� ����
            this.PendingTransactions = new List<Transaction>(){
                new Transaction(null, mineRewardAddress, this.MineReward)
            };
        }

        /// <summary>
        /// ��ȡ�󹤵����
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public double GetBalanceOfAddress(string address)
        {
            var balance = 0.0;

            foreach (var block in this.Chain)
            {
                foreach (var transaction in block.Transactions)
                {
                    if(transaction.FromAddress == address)
                    {
                        balance -= transaction.Amount;
                    }

                    if(transaction.ToAddress == address)
                    {
                        balance += transaction.Amount;
                    }
                }
            }

            return balance;
        }

        public bool IsChainValid()
        {
            for (int i = 1; i < this.Chain.Count; i++)
            {
                var currentBlock = this.Chain[i];
                var previousBlock = this.Chain[i - 1];

                if (currentBlock.Hash != currentBlock.CalculateHash())
                {
                    return false;
                }

                if (currentBlock.PreviousHash != previousBlock.Hash)
                {
                    return false;
                }
            }

            return true;
        }

        public IEnumerator<Block> GetEnumerator()
        {
            return this.Chain.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}