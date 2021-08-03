using System;
using System.Linq;
using System.Xml;

namespace XmlTools
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Program program = new Program();
                    program.Run();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private void Run()
        {
            Console.WriteLine("Please, enter path to xml file");
            var filePath = Console.ReadLine();
            var document = new XmlDocument();
            document.Load(filePath);
            var nodes = document.SelectNodes("//Trade");
            var id = 0;
            foreach (XmlNode node in nodes)
            {
                var exchangeTradeIdNode = node.ChildNodes.Cast<XmlNode>().FirstOrDefault(x => x.Name == "ExchangeTradeId");
                if (exchangeTradeIdNode == null)
                {
                    var idNode = document.CreateElement("ExchangeTradeId");
                    idNode.InnerText = id.ToString();
                    node.AppendChild(idNode);
                }
                else
                {
                    exchangeTradeIdNode.InnerText = id.ToString();
                }
                id += 1;
            }
            document.Save(filePath);
            Console.WriteLine("IDs added successfully");
        }
    }
}
