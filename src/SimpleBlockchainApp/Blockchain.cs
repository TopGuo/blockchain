using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SimpleBlockchainApp
{
    /// <summary>
    /// 区块链
    /// </summary>
    public class Blockchain : IEnumerable<Block>
    {
        /// <summary>
        /// 块链
        /// </summary>
        public List<Block> Chain { get; private set; }

        /// <summary>
        /// 困难程度（等级1-10）
        /// </summary>
        public int Difficulty { get; set; }

        /// <summary>
        /// 暂且将其命名为 待交易链
        /// </summary>
        public List<Transaction> PendingTransactions { get; private set; }

        /// <summary>
        /// 奖励 消费差价 作为（矿工）服务奖励
        /// </summary>
        public double MineReward { get; set; }
    
        public Blockchain(int difficulty = 5, double mineReward = 100.0)
        {
            this.Chain = new List<Block>();
            this.PendingTransactions = new List<Transaction>();
            //将区块加入链
            this.Chain.Add(CreateGenesisBlock());

            this.Difficulty = difficulty;
            this.MineReward = mineReward;
        }

        /// <summary>
        /// 生成一个区块
        /// </summary>
        /// <returns></returns>

        private Block CreateGenesisBlock()
        {
            return new Block(DateTime.Parse("2018-03-24 14:14:14"), new List<Transaction>());
        }

        /// <summary>
        /// 生成交易
        /// </summary>
        /// <param name="transaction"></param>
        public void CreateTransaction(Transaction transaction)
        {
            this.PendingTransactions.Add(transaction);
        }

        /// <summary>
        /// 矿工挖到矿 写入一条预交易信息 获取奖励
        /// </summary>
        /// <param name="mineRewardAddress">矿工hash地址</param>
        public void MinePendingTransactions(string mineRewardAddress)
        {
            var block = new Block(DateTime.Now, this.PendingTransactions);
            //
            block.MineBlock(this.Difficulty);

            System.Console.WriteLine($"Block successfully mined: {block.Hash}");

            this.Chain.Add(block);
            //fromAdress 为null 表示没有发起者，该矿是矿工挖的 并标记上 奖励
            this.PendingTransactions = new List<Transaction>(){
                new Transaction(null, mineRewardAddress, this.MineReward)
            };
        }

        /// <summary>
        /// 获取矿工的余额
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