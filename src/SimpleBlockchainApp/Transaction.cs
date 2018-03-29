using System;

namespace SimpleBlockchainApp
{
    /// <summary>
    /// 交易
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// 消费者hash地址 这笔交易的发起者
        /// </summary>
        public string FromAddress { get; set; }
        /// <summary>
        /// 生产者hash地址 
        /// </summary>
        public string ToAddress { get; set; }
        /// <summary>
        /// 交易金额
        /// </summary>
        public double Amount { get; set; }

        public Transaction(string fromAddress, string toAddress, double amount)
        {
            this.FromAddress = fromAddress;
            this.ToAddress = toAddress;
            this.Amount = amount;
        }
    }
}