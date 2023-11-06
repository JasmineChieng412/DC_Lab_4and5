using DC_LAB_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BusinessTier
{
    [ServiceContract]
    public interface BusinessInterface
    {
        [OperationContract]
        int GetNumEntries();
        [OperationContract]
        void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out string imagepath);

        [OperationContract]
        void GetValuesForSearch(string searchText, out int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out string imagepath);

        [OperationContract]
        void SearchByLastName(string lastName, out List<DatabaseStorage> results);
    }
}
