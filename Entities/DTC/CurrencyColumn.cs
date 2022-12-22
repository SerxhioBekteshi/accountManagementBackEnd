using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTC
{
    public class CurrencyColumn
    {
        public static string id = "Id";
        public static string code = "Code";
        public static string description = "Description";
        public static string exchangeRate = "ExchangeRate";
        public static string actions = null;

        public static string GetPropertyDescription(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(id):
                    return id;
                case nameof(code):
                    return code;
                case nameof(description):
                    return description;
                case nameof(exchangeRate):
                    return exchangeRate;
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
                case nameof(code):
                case nameof(description):
                case nameof(exchangeRate):
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
                case nameof(code):
                case nameof(description):
                case nameof(exchangeRate):
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
                case nameof(code):
                case nameof(description):
                case nameof(exchangeRate):
                default:
                    return 50;
            }
        }

        public static bool GetPropertyFilterable(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(id):
                case nameof(code):
                case nameof(description):
                case nameof(exchangeRate):
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
                case nameof(exchangeRate):
                    return DataType.Decimal;
                case nameof(code):
                case nameof(description):
                    return DataType.String;
                case nameof(actions):
                    return DataType.Actions;
                default:
                    return DataType.String;
            }
        }

        public static object[] GetPropertyActions(string propertyName)
        {

            object[] actionsData =
            {
                new { name = "add", icon= "fa-solid fa-plus", color = "lightblue"},
                new { name = "edit", icon= "fa-regular fa-pen-to-square", color = "blue"},
                new { name = "delete", icon= "fa-regular fa-trash-can", color = "red" },
            };

            switch (propertyName)
            {
                case nameof(id):
                case nameof(code):
                case nameof(description):
                case nameof(exchangeRate):
                    return null;
                case nameof(actions):
                    return actionsData;
                default:
                    return actionsData;
            }
        }
    }
}
