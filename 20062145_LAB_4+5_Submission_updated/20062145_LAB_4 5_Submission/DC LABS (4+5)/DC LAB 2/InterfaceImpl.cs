using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
//using DC_LAB_2;

namespace DC_LAB_2 //ConsoleApp1
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    public class InterfaceImpl : Interface1
    {
        DatabaseClass myObject = new DatabaseClass();

        public CommunicationState State { get; set; }


        public int GetNumEntries()
            {
            // Implement logic to return the number of entries in the database
            return myObject.GetNumRecords();

            }

        public void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal,
                out string fName, out string lName, out string imagepath)
        {
                imagepath = myObject.GetImageByIndex(index);
                acctNo = myObject.GetAcctNoByIndex(index);
                pin = myObject.GetPINByIndex(index);
                bal = myObject.GetBalanceByIndex(index);
                fName = myObject.GetFirstNameByIndex(index);
                lName = myObject.GetLastNameByIndex(index);
                
        }

     /*   int Interface1.GetNumEntries()
        {
            throw new NotImplementedException();
        }*/
    }
}
