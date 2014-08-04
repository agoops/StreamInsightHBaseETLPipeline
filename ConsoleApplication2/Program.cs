using System;
using System.ServiceModel;
using Microsoft.ComplexEventProcessing;
using Microsoft.ComplexEventProcessing.Linq;
using Microsoft.ComplexEventProcessing.ManagementService;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Data.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;


namespace ConsoleApplication2

    /* This example:
     * creates an embedded server instance and makes it available to other clients
     * defines, deploys, binds, and runs a simple source, query, and sink
     * waits for the user to stop the server
     */
{
  
   
    class Program
    {



        static void Main(string[] args)
        {
            // Create an embedded StreamInsight server
            using (Server server = Server.Create("streaminsightreportinginstance"))
            {

                // Create a local end point for the server embedded in this program
                var host = new ServiceHost(server.CreateManagementService());
                host.AddServiceEndpoint(typeof(IManagementService), new WSHttpBinding(SecurityMode.Message), "http://localhost/MyStreamInsightServer");
                host.Open();

                /* The following entities will be defined and available in the server for other clients:
                 * serverApp
                 * serverSource
                 * serverSink
                 * serverProcess
                 */
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\t-ankigu\Documents\output.txt"))
                {
                    AccountBaseEventSrc a = new AccountBaseEventSrc();
                    foreach (PropertyInfo pi in a.GetType().GetProperties())
                    {
                        file.Write("cf:"+pi.Name + ", ");
                    }
                }
                // CREATE a StreamInsight APPLICATION in the server
                var myApp = server.CreateApplication("serverApp");



                /**
                 * 
                 *      SETUP database Stuff
                 */
                 
                //SetUpDatabase<AccountBaseEventSrc> setup = new SetUpDatabase<AccountBaseEventSrc>("AccountBase", "AccountId", SQL_COMMANDS.TIGER_REPL_CONN, SQL_COMMANDS.ACCOUNT_BASE);
                //SetUpDatabase<SourceDatabaseEvent> setup2 = new SetUpDatabase<SourceDatabaseEvent>("SalesHistory", "SaleID", SQL_COMMANDS.CAPTURE_CHANGES_CONN, SQL_COMMANDS.SALES_HISTORY);
                //SetUpDatabase<PhoneBaseEventSrc> phoneSetup = new SetUpDatabase<PhoneBaseEventSrc>("PhoneCallBase", "ActivityId", SQL_COMMANDS.TIGER_REPL_CONN, SQL_COMMANDS.PHONE_CALL_BASE);
                SetUpDatabase<ActivityPointerBaseEventSrc> activitySetup = new SetUpDatabase<ActivityPointerBaseEventSrc>("ActivityPointerBase", "ActivityId", SQL_COMMANDS.TIGER_REPL_CONN, SQL_COMMANDS.ACTIVITY_BASE);
                //SetUpDatabase<OpportunityBaseEventSrc> opportunitySetup = new SetUpDatabase<OpportunityBaseEventSrc>("OpportunityBase", "OpportunityId", SQL_COMMANDS.TIGER_REPL_CONN, SQL_COMMANDS.OPPORTUNITY_BASE);

                //SetUpDatabase<OwnerBaseEventSrc> ownerSetup = new SetUpDatabase<OwnerBaseEventSrc>("OwnerBase", "OwnerId", SQL_COMMANDS.TIGER_REPL_CONN, SQL_COMMANDS.OWNER_BASE);

                //Action<object> phoneAction = (object obj) =>
                //{
                //    phoneSetup.begin(myApp, 2000, "phoneProcess");
                //};
                //Task phone = new Task(phoneAction, "alpha");

                Action<object> activityAction = (object obj) =>
                {
                    activitySetup.begin(myApp, 200, "activityProcess");
                };
                Task activity = new Task(activityAction, "alpha");

                //Action<object> ownerAction = (object obj) =>
                //{
                //    ownerSetup.begin(myApp, 1652, "ownerProcess");
                //};
                //Task owner = new Task(ownerAction, "alpha");


                //phone.Start();
                activity.Start();
                //owner.Start();

                //Action<object> opportunityAction = (object obj) =>
                //{
                //    opportunitySetup.begin(myApp, 2000, "phoneProcess");
                //};
                //Task t1 = new Task(opportunityAction, "alpha");
                //t1.Start();

                //Action<object> activityAction = (object obj) =>
                //{
                //    activitySetup.begin(myApp, 200, "activityProcess");
                //};
                //Task t2 = new Task(activityAction, "alpha");
                //t2.Start();



                /**
                 *     Change Database Stuff
                 */

                //ChangeDatabase<PhoneBaseEventChange> phoneChange = new ChangeDatabase<PhoneBaseEventChange>("PhoneCallBase", "ActivityId", SQL_COMMANDS.TIGER_REPL_CONN, SQL_COMMANDS.PHONE_CALL_BASE_CHANGE);

                //Action<object> phoneAction = (object obj) =>
                //{
                //    phoneChange.begin(myApp, "phoneChangeProcess");
                //};
                //Task t2 = new Task(phoneAction, "alpha");
                //t2.Start();

                Console.WriteLine("Done. Press enter to quit.");
                Console.ReadLine();
               
                host.Close();
            }

        }

        
    }
}