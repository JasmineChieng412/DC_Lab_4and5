using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DC_LAB_2;
using System.ServiceModel;

namespace DatabaseServer
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]

    internal class ServerInterfaceImpl : ServerInterface
    {
        DatabaseClass myObject = new DatabaseClass();

        public int GetNumEntries()
        {
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
    }
}
