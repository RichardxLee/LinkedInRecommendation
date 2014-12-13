using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LinkedInRecommendation
{
    class XMLParser
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public XMLParser () 
        {
        }

        /// <summary>
        /// Parse user profile for first and last name
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public Person parseUserProfile(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNode node = doc.DocumentElement.SelectSingleNode("/person");
            Person p = new Person();
            try     {   p.firstName = node.SelectSingleNode("first-name").InnerText;    }
            catch   {   p.firstName = "";                                               }
            try     {   p.lastName = node.SelectSingleNode("last-name").InnerText;      }
            catch   {   p.lastName = "";                                                }

            return p;
        }

        /// <summary>
        /// Parse user connection into list of persons
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public List<Person> parseUserConnection(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNodeList nodeList = doc.DocumentElement.SelectNodes("/connections/person");
            List<Person> lPersons = new List<Person>();
            foreach (XmlNode node in nodeList)
            {
                Person p = new Person();
                try { p.firstName = node.SelectSingleNode("first-name").InnerText; }
                catch { p.firstName = ""; }
                try { p.lastName = node.SelectSingleNode("last-name").InnerText; }
                catch { p.lastName = ""; }
                try { p.headline = node.SelectSingleNode("headline").InnerText; }
                catch { p.headline = ""; }
                try { p.picUrl = node.SelectSingleNode("picture-url").InnerText; }
                catch { p.picUrl = ""; }
                try { p.locationCountry = node.SelectSingleNode("location/country").InnerText; }
                catch { p.locationCountry = ""; }
                try { p.locationCity = node.SelectSingleNode("location/name").InnerText; }
                catch { p.locationCity = ""; }
                try { p.industry = node.SelectSingleNode("industry").InnerText; }
                catch { p.industry = ""; }
                lPersons.Add(p);
            }
            return lPersons;
        }

    }
}
