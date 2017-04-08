namespace MassDefect.Import.Xml
{
    using MassDefect.Data.Stores;
    using System;
    using System.IO;
    using System.Xml.Linq;
    using System.Xml.XPath;

    public static class XmlImport
    {
        private const string NewAnomaliesPath = "../../../datasets/new-anomalies.xml";

        public static void ImportNewAnomalies()
        {
            var xml = XDocument.Load(NewAnomaliesPath);
            var anomalies = xml.XPathSelectElements("anomalies/anomaly");
            AnomalyStore.AddAnomaliesAndVictimsFromXml(anomalies);            
        }
    }
}
