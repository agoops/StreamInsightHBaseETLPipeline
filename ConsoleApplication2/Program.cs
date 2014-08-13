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

             
                // CREATE a StreamInsight APPLICATION in the server
                var myApp = server.CreateApplication("serverApp");



                /**
                 *      SetUp database Examples
                 */
                 
                //SetUpDatabase<PhoneBaseEventSrc> phoneSetup = new SetUpDatabase<PhoneBaseEventSrc>("PhoneCallBase", "ActivityId", SQL_COMMANDS.TIGER_REPL_CONN, SQL_COMMANDS.PHONE_CALL_BASE);

                //Action<object> phoneAction = (object obj) =>
                //{
                //    phoneSetup.begin(myApp, 2000, "phoneProcess");
                //};
                //Task phone = new Task(phoneAction, "alpha");

                SetUpDatabase<ActivityPointerBaseEventSrc> activitySetup = new SetUpDatabase<ActivityPointerBaseEventSrc>("ActivityPointerBase", "ActivityId", SQL_COMMANDS.TIGER_REPL_CONN, SQL_COMMANDS.ACTIVITY_BASE);

                Action<object> activityAction = (object obj) =>
                {
                    activitySetup.begin(myApp, 200, "activityProcess");
                };
                Task activity = new Task(activityAction, "thisStringDoesntMatter");

                activity.Start();

                /**
                 *     Change Database Examples
                 */

                ChangeDatabase<PhoneBaseEventChange> phoneChange = new ChangeDatabase<PhoneBaseEventChange>("PhoneCallBase", "ActivityId", SQL_COMMANDS.TIGER_REPL_CONN, SQL_COMMANDS.PHONE_CALL_BASE_CHANGE);

                Action<object> phoneAction = (object obj) =>
                {
                    phoneChange.begin(myApp, 200, "phoneChangeProcess");
                };
                Task phone = new Task(phoneAction, "alpha");
                phone.Start();

                Console.WriteLine("Processes begun. Press enter to quit.");
                Console.ReadLine();
               
                host.Close();
            }

        }

        
    }
}