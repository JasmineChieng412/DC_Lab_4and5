using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC_LAB_2
{
    public class DatabaseClass
    {
        List<DatabaseStorage> dataStorage;
        public DatabaseClass()
        {

            dataStorage = new List<DC_LAB_2.DatabaseStorage>();
            InitializeData(); // Call a method to load data into the list
        }
        public List<DatabaseStorage> GetStorages()
        {
            return dataStorage;
        }

        private void InitializeData()
        {
            DatabaseGenerator dataGenerator = new DatabaseGenerator();
            int i=0, numRecords = 1000; // You can change this number as needed

            for (i = 0; i < numRecords; i++)
            {
                uint pin, acctNo;
                string firstName, lastName;
                int balance;
                string imagepath;

                dataGenerator.GetNextAccount(out pin, out acctNo, out firstName, out lastName, out balance, out imagepath);

                dataStorage.Add(new DatabaseStorage
                {
                    acctNo = acctNo,
                    pin = pin,
                    balance = balance,
                    firstName = firstName,
                    lastName = lastName,
                    imagepath = imagepath
                });
            }
            if(i == numRecords)
            {
                numRecords = numRecords - 1;    
            }
        }

        private bool IsIndexValid(int index)
        {
            return index >= 0 && index < dataStorage.Count;
        }

        public uint GetAcctNoByIndex(int index)
        {
            uint acc = 0;
            try
            {
                // Your code that might cause an index out of bounds error
                if (IsIndexValid(index))
                {
                    acc = dataStorage[index].acctNo;
                }
                else
                {
                    throw new IndexOutOfRangeException("Index is out of range.");
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                // Handle the index out of bounds error here
                Console.WriteLine("Index out of bounds error occurred: " + ex.Message);
            }
            // return dataStorage[index].acctNo;
            return acc;

        }

        public uint GetPINByIndex(int index)
        {
            uint pin=0;

            try
            {
                // Your code that might cause an index out of bounds error

                if (IsIndexValid(index))
                {
                    pin= dataStorage[index].pin;
                }
                else
                {
                    throw new IndexOutOfRangeException("Index is out of range.");

                }
            }
            catch (IndexOutOfRangeException ex)
            {
                // Handle the index out of bounds error here
                Console.WriteLine("Index out of bounds error occurred: " + ex.Message);
            }
            return pin;
        }

        public string GetFirstNameByIndex(int index)
        {
            string fname = "";

            try
            {
                // Your code that might cause an index out of bounds error

                if (IsIndexValid(index))
                {
                    fname= dataStorage[index].firstName;
                }
                else
                {
                    throw new IndexOutOfRangeException("Index is out of range.");

                }
            }
            catch (IndexOutOfRangeException ex)
            {
                // Handle the index out of bounds error here
                Console.WriteLine("Index out of bounds error occurred: " + ex.Message);
            }
            return fname;
        }

        public string GetLastNameByIndex(int index)
        {
            string lname = "";

            try
            {
                // Your code that might cause an index out of bounds error

                if (IsIndexValid(index))
                {
                    lname= dataStorage[index].lastName;
                }
                else
                {
                    throw new IndexOutOfRangeException("Index is out of range.");

                }
            }
            catch (IndexOutOfRangeException ex)
            {
                // Handle the index out of bounds error here
                Console.WriteLine("Index out of bounds error occurred: " + ex.Message);
            }
            return lname;
        }

        public int GetBalanceByIndex(int index)
        {
            int bal = 0;

            try
            {
                // Your code that might cause an index out of bounds error

                if (IsIndexValid(index))
                {
                    bal= dataStorage[index].balance;
                }
                else
                {
                    throw new IndexOutOfRangeException("Index is out of range.");

                }
            }
            catch (IndexOutOfRangeException ex)
            {
                // Handle the index out of bounds error here
                Console.WriteLine("Index out of bounds error occurred: " + ex.Message);
            }
            return bal;
        }
        public string GetImageByIndex(int index)
        {
            string img = "";
            try
            {
                if (index >= 0 && index < dataStorage.Count)
                {
                    img = dataStorage[index].imagepath;
                }
                else
                {
                    throw new IndexOutOfRangeException("Index is out of range.");

                }
            }
            catch (IndexOutOfRangeException ex)
            {
                // Handle the index out of bounds error here
                Console.WriteLine("Index out of bounds error occurred: " + ex.Message);
            }
            return img;

           // return null; // Return null if index is out of bounds
        }

        public int GetNumRecords()
        {
            return dataStorage.Count;
        }

        /*   internal class DataStorage : DatabaseStorage
           {
               public uint acctNo { get; set; }
               public uint pin { get; set; }
               public int balance { get; set; }
               public string firstName { get; set; }
               public string lastName { get; set; }
           }*/
    }
}
