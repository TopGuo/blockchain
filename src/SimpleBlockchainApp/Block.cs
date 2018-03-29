using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace SimpleBlockchainApp
{
    /// <summary>
    /// 块
    /// </summary>
    public class Block
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// 上一个块的hash
        /// </summary>
        public string PreviousHash { get; set; }

        /// <summary>
        /// 交易信息
        /// </summary>
        public IEnumerable<Transaction> Transactions { get; set; }

        /// <summary>
        /// 本块hash
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// 随机数
        /// </summary>
        public int Nonce { get; set; }

        /// <summary>
        /// 初始化一些信息
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="transactions"></param>
        /// <param name="previousHash"></param>
        public Block(DateTime timestamp, IEnumerable<Transaction> transactions, string previousHash = "")
        {
            this.Timestamp = timestamp;
            this.Transactions = transactions;
            this.PreviousHash=  previousHash;

            this.Hash = CalculateHash();

            this.Nonce = 0;
        }

        /// <summary>
        /// 算法 生成hash
        /// </summary>
        /// <returns></returns>
        public string CalculateHash()
        {
            var alg = SHA256.Create();

            var blockBits = Encoding.UTF8.GetBytes(this.ToString());
            var hashBits = alg.ComputeHash(blockBits);

            var hash = BitConverter.ToString(hashBits).Replace("-", string.Empty);

            return hash;
        }

        public void MineBlock(int difficulty)
        {
            while (this.Hash.Substring(0, difficulty) != 0.ToString($"D{difficulty}"))
            {
                this.Nonce++;
                this.Hash = this.CalculateHash();

                Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff")}] Mining: {this.Hash} nonce={this.Nonce}");
            }

            //匹配成功 则挖到矿
            Console.WriteLine($"Block mined: {this.Hash}");
        }

        /// <summary>
        /// 重写string方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{this.PreviousHash}{this.Timestamp}{JsonConvert.SerializeObject(this.Transactions)}{this.Nonce}";
        }
    }
}