using System;

namespace SimpleBlockchainApp
{
    /// <summary>
    /// ����
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// ������hash��ַ ��ʽ��׵ķ�����
        /// </summary>
        public string FromAddress { get; set; }
        /// <summary>
        /// ������hash��ַ 
        /// </summary>
        public string ToAddress { get; set; }
        /// <summary>
        /// ���׽��
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