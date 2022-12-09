using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Entities.DTC
{
    public class BankAccountColumn
    {
        public static string id = "Id";
        public static string code = "Code";
        public static string name = "Name";
        public static string balance = "Balance";
        public static string isActive = "isActive";
        public static string currencyId = "CurrencyId";
        public static string clientId = "clientId";
        public static string actions = null;

        public static string GetPropertyDescription(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(id):
                    return id;
                case nameof(name):
                    return name;
                case nameof(code):
                    return code;
                case nameof(balance):
                    return balance;
                case nameof(isActive):
                    return isActive;
                case nameof(currencyId):
                    return currencyId;
                case nameof(clientId):
                    return clientId;
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
                case nameof(code):
                case nameof(balance):
                case nameof(isActive):
                case nameof(currencyId):
                case nameof(clientId):
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
                case nameof(code):
                case nameof(balance):
                case nameof(isActive):
                case nameof(currencyId):
                case nameof(clientId):
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
                case nameof(name):
                case nameof(code):
                case nameof(balance):
                case nameof(isActive):
                case nameof(currencyId):
                case nameof(clientId):
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
                case nameof(code):
                case nameof(balance):
                case nameof(isActive):
                case nameof(currencyId):
                case nameof(clientId):
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
                case nameof(code):
                    return DataType.String;
                case nameof(balance):
                    return DataType.Decimal;
                case nameof(isActive):
                    return DataType.Boolean;
                case nameof(currencyId):
                case nameof(clientId):
                    return DataType.Number;
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
                case nameof(code):
                case nameof(balance):
                case nameof(isActive):
                case nameof(currencyId):
                case nameof(clientId):
                    return null;
                case nameof(actions):
                    return actionsData;
                default:
                    return actionsData;
            }
        }
    }
}
