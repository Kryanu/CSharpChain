﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace CSharpChainModel
{
    class TransactionsConvertor : ConverterBase
    {
        public override object StringToField(string from)
        {
            List<Transaction> list = new List<Transaction>();
            string recieved = "";
            string sent = "";
            decimal amount = 0;
            string desc = "";
            string hash = "";

            string result = from.Replace("Transactions:", "");

            string[] resultArray = result.Split(']');

            foreach (string item in resultArray)
            {

                int minus = item.IndexOf('@');
                int plus = item.IndexOf('+');
                int times = item.IndexOf('*');
                int open = item.IndexOf('[');
                if (minus < 0)
                {
                    break;
                }
                recieved = item.Substring(0,minus);
                sent = item.Substring(minus + 1,plus - minus);
                desc = item.Substring(plus + 1,times - plus-1);
                amount = Decimal.Parse(item.Substring(times + 1,open - times -1 ));
                hash = item.Substring(open+1);
                list.Add(new Transaction(sent,recieved,amount,desc,hash));
            }

            return list;
        }

        public override string FieldToString(object from)
        {
            List<Transaction> list = (List<Transaction>)from;
            StringBuilder transactions = new StringBuilder("Transactions:");
            foreach(Transaction item in list)
            {
                transactions.Append(item.ReceiverAddress);
                transactions.Append('@');
                transactions.Append(item.SenderAddress);
                transactions.Append('+');
                transactions.Append(item.Description);
                transactions.Append('*');
                transactions.Append(item.Amount);
                transactions.Append('[');
                transactions.Append(item.hash);
                transactions.Append(']');
            }

            return transactions.ToString();
        }
    }
}
