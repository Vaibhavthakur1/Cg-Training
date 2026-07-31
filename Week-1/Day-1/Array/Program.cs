using System;
using System.Transactions;
//using System.Runtime.InteropServices;
using DSA_Array;
class Program
{
    static void Main(string[] args)
    {
        int[] arrayVariable = { 1, 2, 3, 4, 5 };
        //int sum = 0;
        //Array.Reverse(arrayVariable);
        //int max = arrayVariable[0];
        //int OddCount = 0;
        //int EvenCount = 0;
        //for (int i = 0; i < arrayVariable.Length; i++)
        //{





        //sum += arrayVariable[i];
        //Console.WriteLine(arrayVariable[i]);
        //if (arrayVariable[i] > max)
        //{
        //    max = arrayVariable[i];
        //}

        //if (arrayVariable[i] % 2 == 0)
        //{
        //    Console.WriteLine("Even numbers are-> " + arrayVariable[i]);
        //    EvenCount++;
        //}
        //else
        //{
        //    Console.WriteLine("Odd numbers are-> " + arrayVariable[i]);
        //    OddCount++;
        //}

        //}
        //Console.WriteLine("total even numbers -> "+EvenCount);
        //Console.WriteLine("Total odd numbers ->"+ OddCount);
        //Console.WriteLine(max);
        //Console.WriteLine(sum);


        //Print second largest element

        //int[] number = { 1, 2, 3, 4, 5 };
        //int largest = number[0];
        //int secondLargest = number[0];

        //for(int i = 1; i < number.Length; i++)
        //{
        //    if (number[i] > largest)
        //    {
        //        secondLargest = largest;
        //        largest = number[i];
        //    }
        //}
        //Console.WriteLine(secondLargest);


        //merge two arrays
        //int[] arrayFirst = { 5, 10, 15, 20, 25 };
        //int[] arraySecond = { 30, 35, 40, 45, 50 };

        //int[] mergedArray = new int[arrayFirst.Length + arraySecond.Length];

        //for (int i = 0; i < arrayFirst.Length; i++)
        //{
        //    mergedArray[i] = arrayFirst[i];

        //}
        //for (int i = 0; i < arraySecond.Length; i++)
        //{
        //    mergedArray[arrayFirst.Length + i] = arraySecond[i];
        //}

        //for (int i = 0; i < mergedArray.Length; i++)
        //{
        //    Console.WriteLine(mergedArray[i]);
        //}



        //duplicate element in an array
        //int[] arrDuplicate = { 5, 10, 15, 10, 25 ,10,15,5};

        //for(int i = 0; i < arrDuplicate.Length; i++)
        //{
        //    bool isDuplicate = false;

        //    for(int j = 0; j < i; j++)
        //    {
        //        if (arrDuplicate[i] == arrDuplicate[j])
        //        {
        //            isDuplicate = true;
        //            break;

        //        }

        //    }
        //    if (!isDuplicate)
        //    {
        //        Console.Write(arrDuplicate[i] + " ");
        //    }
        //}


        //reverse string
        //string text = "VaibhavSingh";
        //char[] arr = text.ToCharArray();

        //Array.Reverse(arr);

        //string reverse = new string(arr);
        //Console.WriteLine(reverse);



        BankTransaction[] transactions = new BankTransaction[]
        {
            new BankTransaction {AccountId= "100", TransactionAmount = 1500, Timestamp = new DateTime(2026, 7, 28), MerchantName = "BookStore"},
            new BankTransaction {AccountId= "101", TransactionAmount = 6500, Timestamp = new DateTime(2026, 7, 26), MerchantName = "FurnitureStore"},
            new BankTransaction {AccountId= "102", TransactionAmount = 105000, Timestamp = new DateTime(2026, 7, 24), MerchantName = "BikeShowroom"},
            new BankTransaction {AccountId= "103", TransactionAmount = 7000, Timestamp = new DateTime(2026, 7, 27), MerchantName = "Electronic Shop"},
        };



        static void DisplayTransactions(BankTransaction[] transactions)
        {
            foreach(BankTransaction t in transactions)
            {
                Console.WriteLine($"AccountId-> {t.AccountId} TransactionAmount-> {t.TransactionAmount} TimeStamp-> {t.Timestamp} MerchantName-> {t.MerchantName}");
            }
        }

        static void CheckSusTransaction(BankTransaction[] transactions)
        {

            bool found = false;

            foreach(BankTransaction t in transactions)
            {
                if (t.TransactionAmount > 100000)
                {
                    Console.WriteLine("Suspicious transaction: ");
                    Console.WriteLine($"Account Id: {t.AccountId}");
                    Console.WriteLine($"Amount: {t.TransactionAmount}");
                    Console.WriteLine($"TimeStamp: {t.Timestamp}");
                    Console.WriteLine($"Merchant: {t.MerchantName}");
                    found = true;
                }
            }
            if (!found)
            {
                Console.WriteLine("No Suspicious transaction found.");
            }
        }

        Console.WriteLine("Task 1: Display Data");
        DisplayTransactions(transactions);
        CheckSusTransaction(transactions);



        //bool running = true;
        //while (running)
        //{
        //    Console.WriteLine("---Bank fraud detection----");
        //    Console.WriteLine("Display Transaction");
        //    Console.WriteLine("Add Transaction");
        //    Console.WriteLine("CheckSuspicious transaction");
        //    Console.WriteLine("Exit the application");

        //}


    }
}