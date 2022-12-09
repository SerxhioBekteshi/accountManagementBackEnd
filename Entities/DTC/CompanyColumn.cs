using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTC
{
    public class CompanyColumn
    {
        public static string id = "Id";
        public static string name = "Name";
        public static string address = "Address";
        public static string country = "Country";
        public static string managerAccountActivated = "ManagerAccountActivated";
        public static string actions = null;

        public static string GetPropertyDescription(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(id):
                    return id;
                case nameof(name):
                    return name;
                case nameof(address):
                    return address;
                case nameof(country):
                    return country;
                case nameof(managerAccountActivated):
                    return managerAccountActivated;
                case nameof(actions):
                    return null;
                default:
                    return "";
            }
        }

        public static bool GetPropertyIsHidden(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(id):
                    return true;
                case nameof(name):
                case nameof(address):
                case nameof(country):
                case nameof(managerAccountActivated):

                    return false;
                case nameof(actions):
                    return false;
                default:
                    return true;
            }
        }

        public static bool GetPropertyHideable(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(id):
                    return true;
                case nameof(name):
                case nameof(address):
                case nameof(country):
                case nameof(managerAccountActivated):

                case nameof(actions):
                    return false;
                default:
                    return true;
            }
        }

        public static int GetPropertyMinWidth(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(id):
                case nameof(name):
                case nameof(address):

                case nameof(country):
                case nameof(managerAccountActivated):

                default:
                    return 50;
            }
        }

        public static bool GetPropertyFilterable(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(id):
                case nameof(name):
                case nameof(address):
                case nameof(country):
                case nameof(managerAccountActivated):

                    return true;
                case nameof(actions):
                    return false;
                default:
                    return true;
            }
        }

        public static DataType GetPropertyDataType(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(id):
                    return DataType.Number;
                case nameof(name):
                case nameof(address):
                case nameof(country):
                case nameof(managerAccountActivated):

                    return DataType.String;
                case nameof(actions):
                    return DataType.Actions;
                default:
                    return DataType.String;
            }
        }

        public static object[] GetPropertyActions(string propertyName)
        {

            //List<DataTableIcons> actionsData = new List<DataTableIcons>();
            //actionsData.Add(new DataTableIcons("edit", "fa-regular fa-pencil"));
            //actionsData.Add(new DataTableIcons("delete", "fa-regular fa-trash"));

            object[] actionsData = 
            {
                new { name = "insert", icon= "fa-solid fa-plus", color = "lightblue" },
                new { name = "edit", icon= "fa-regular fa-pen-to-square", color = "blue"},
                new { name = "delete", icon= "fa-regular fa-trash-can", color = "red" },

            };

            switch (propertyName)
            {
                case nameof(id):
                case nameof(name):
                case nameof(address):
                case nameof(country):
                case nameof(managerAccountActivated):

                    return null;
                case nameof(actions):
                    return actionsData;
                default:
                    return actionsData;
            }
        }
    }


}
