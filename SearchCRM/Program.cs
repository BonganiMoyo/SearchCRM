using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchCRM
{
    class Program
    {
        static void Main(string[] args)
        {
            StartLoop();
        }

        private static void StartLoop()
        {
            Console.WriteLine("Press 1 to search for javascript files containing specific text.");
            Console.WriteLine("Press any other key to Exit!!!");

            ConsoleKeyInfo key = Console.ReadKey();
            string action = key.Key.ToString();

            if (action == "NumPad1" || action == "D1")
            {
                Console.WriteLine();
                Console.WriteLine("Enter text to search for in javascript and Html files and press enter");
                string searchString = Console.ReadLine();
                PerformSearch(searchString);

            }

        }

        private static void PerformSearch(string searchString)
        {
            //Crm_MSCRMEntities =
            Crm_MSCRMEntities ctx = new Crm_MSCRMEntities();
            string decryptedConted = string.Empty;
            //string searchString = "OrganizationData.svc";
            Console.WriteLine(String.Format("Searching for {0}", searchString.ToLower()));

            foreach (var item in ctx.WebResources)
            {
                decryptedConted = GetContent(item.Content);
                if (decryptedConted.ToLower().Contains(searchString.ToLower()))
                {
                    Console.WriteLine(String.Format("Resource {0} contains {1}", item.Name, searchString.ToLower()));

                }
            }

            Console.WriteLine("Completed");
            Console.WriteLine();
            StartLoop();
        }

        public static string GetContent(string encryptedContent)
        {
            if (encryptedContent == null)
            {
                return string.Empty;
            }
            byte[] binaryData = System.Convert.FromBase64String(encryptedContent);
            string unencryptedContent = System.Text.Encoding.ASCII.GetString(binaryData);
            return unencryptedContent;
        }
    }
}
