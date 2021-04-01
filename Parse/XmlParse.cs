using SoftxPansion1.Models;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SoftxPansion1.Parse
{
    public static class XmlParse
    {
        public static List<Unit> Parse(string url)
        {
            List<Unit> Units = new List<Unit>();
            int id = 0;
                XDocument xdoc = XDocument.Load(url);
                foreach (XElement unit in xdoc.Element("Units").Elements("Unit"))
                {
                    XAttribute title = unit.Attribute("Title");
                    Unit un = new Unit();
                    un.Id = id;
                    string NewTitle = title.ToString().Remove(0, 7).TrimEnd('"');
                    un.Title = NewTitle;
                     //добавляем название подразделения в список подразделений 
                    foreach(XElement person in unit.Elements("Employee"))
                    {
                        XAttribute position = person.Attribute("Position");
                        XAttribute name = person.Attribute("Name");
                        XAttribute hireDate = person.Attribute("HireDate");
                        string NewName = name.ToString().Remove(0, 6).TrimEnd('"');
                        string NewPosition = position.ToString().Remove(0,10).TrimEnd('"');
                        string NewHireDate = hireDate.ToString().Remove(0, 10).TrimEnd('"');
                        Employee p = new Employee { Name = NewName, HireDate = NewHireDate, Position = NewPosition };

                        un.Employees.Add(p);
                    }
                    Units.Add(un);
                id++;
                //добавляем данные работников в список их подразделения
                }

            return Units;

        }
    }
}
