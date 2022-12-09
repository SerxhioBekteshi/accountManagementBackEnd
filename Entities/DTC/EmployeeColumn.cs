using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTC
{
    public class EmployeeColumn
    {
        public static string id = "Id";
        public static string name = "Name";
        public static string age = "Age";
        public static string position = "Position";
        public static string actions = null;

        public static string GetPropertyDescription(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(id):
                    return id;
                case nameof(name):
                    return name;
                case nameof(age):
                    return age;
                case nameof(position):
                    return position;
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
                case nameof(age):
                case nameof(position):
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
                case nameof(age):
                case nameof(position):
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
                case nameof(age):
                case nameof(position):

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
                case nameof(age):
                case nameof(position):
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
                case nameof(age):
                case nameof(position):
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
                new { name = "edit", icon= "fa-regular fa-pen-to-square", color = "blue"},
                new { name = "delete", icon= "fa-regular fa-trash-can", color = "red" },

            };

            switch (propertyName)
            {
                case nameof(id):
                case nameof(name):
                case nameof(age):
                case nameof(position):
                    return null;
                case nameof(actions):
                    return actionsData;
                default:
                    return actionsData;
            }
        }
    }
}
