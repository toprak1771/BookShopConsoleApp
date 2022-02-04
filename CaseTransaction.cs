using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop
{
    class CaseTransaction : BaseClass
    {
        public static List<CaseTransaction> CaseTransactions = new List<CaseTransaction>();
        
        public double Amount { get; set; }
        public TransactionTypeEnums TransactionType { get; set; }

        public CaseTransaction(double _amount, TransactionTypeEnums _transactionType)
        {
            Amount = _amount;
            TransactionType = _transactionType;
        }

        //Kasa İşlemleri Kaydetme Metodu
        public static void saveCaseTransaction(CaseTransaction casetransaction)
        {
            CaseTransactions.Add(casetransaction);
        }

        public static double calculatedAmount(double price, int qty)
        {
            return price * qty;
        }


        public static void CaseTransactionLıst()
        {
            Console.WriteLine("KASA HAREKETLERİ");
            double caseSum = 0;
            foreach  (CaseTransaction caseTransaction in CaseTransactions)
            {
                Console.WriteLine("----------------------------");
                if (caseTransaction.TransactionType==TransactionTypeEnums.EXPENSE)
                {
                    caseSum -= caseTransaction.Amount;
                }
                else
                {
                    caseSum += caseTransaction.Amount;
                }
                Console.WriteLine(caseTransaction.ToString());
            }
            Console.WriteLine("Kasa Toplam Tutarı=" + caseSum);
            Console.WriteLine("----------------------------");

        } 


        public override string ToString()
        {
            return string.Format("Id={0} -- Type={1} -- Amount={2} -- Created Time={3}",Id,TransactionType,Amount,CreatedTime);
        }




    }
}
