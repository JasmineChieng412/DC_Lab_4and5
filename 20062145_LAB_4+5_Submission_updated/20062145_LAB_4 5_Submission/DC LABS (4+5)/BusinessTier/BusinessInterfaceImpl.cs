using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseServer;
using System.Threading;
using System.ServiceModel;
using DC_LAB_2;

namespace BusinessTier
{

    internal class BusinessInterfaceImpl : BusinessInterface
    {
        private ServerInterface foob;

        public BusinessInterfaceImpl()
        {

            ChannelFactory<ServerInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            //Set the URL and create the connection!
            string URL = "net.tcp://localhost:8100/DataService";
            foobFactory = new ChannelFactory<ServerInterface>(tcp, URL);
            foob = foobFactory.CreateChannel();


        }
        public int GetNumEntries()
        {
            return foob.GetNumEntries();
        }

        public void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out string imagepath)
        {
            foob.GetValuesForEntry(index, out acctNo, out pin, out bal,
                            out fName, out lName, out imagepath);
        }

        public void GetValuesForSearch(string searchText, out int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out string imagepath)
        {
            index = 0;
            bal = 0;
            pin = 0;
            acctNo = 0;
            lName = "";
            fName = "";
            imagepath = "";

            int numEntry = foob.GetNumEntries();

            for (int i = 1; i <= numEntry; i++)
            {
                int dbal;
                uint dpin = 0;
                uint dacctNo = 0;
                string dlName = "";
                string dfName = "";
                string dimagepath = "";

                foob.GetValuesForEntry(i, out dacctNo, out dpin, out dbal, out dfName, out dlName, out dimagepath);
                if (dlName.ToLower().Contains(searchText.ToLower()))
                {
                    fName = dfName;

                    lName = dlName;
                    pin = dpin;
                    acctNo = dacctNo;
                    bal = dbal;
                    imagepath = dimagepath;
                    break;
                }
            }
            Thread.Sleep(5000); //Forced sleep for two seconds        }
        }

        public void SearchByLastName(string lastName, out List<DatabaseStorage> results)
        {
            results = new List<DatabaseStorage>();

            int numEntries = foob.GetNumEntries();

            for (int i = 1; i <= numEntries; i++)
            {
                uint acctNo;
                uint pin;
                int bal;
                string fName;
                string lName;
                string imagepath;

                foob.GetValuesForEntry(i, out acctNo, out pin, out bal, out fName, out lName, out imagepath);

                if (lName != null && lName.ToLower().Contains(lastName.ToLower()))
                {
                    // Found a match based on last name, add it to the results list
                    DatabaseStorage storage = new DatabaseStorage
                    {
                        acctNo = acctNo,
                        pin = pin,
                        balance = bal,
                        firstName = fName,
                        lastName = lName,
                        imagepath = imagepath
                    };

                    results.Add(storage);
                }
            }
        }


    }
}
