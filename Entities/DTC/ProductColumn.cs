using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTC
{
    public class ProductColumn
    {
        public static string id = "Id";
        public static string name = "Name";
        public static string shortDescription = "Short Description";
        public static string longDescription = "Long Description";
        public static string price = "Price";
        public static string image = "Image";
        public static string sasia = "Quantity";
        public static string actions = null;

        public static string GetPropertyDescription(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(id):
                    return id;
                case nameof(name):
                    return name;
                case nameof(shortDescription):
                    return shortDescription;
                case nameof(longDescription):
                    return longDescription;
                case nameof(price):
                    return price;
                case nameof(image):
                    return image;
                case nameof(sasia):
                    return sasia;
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
                case nameof(shortDescription):
                case nameof(longDescription):
                case nameof(price):
                case nameof(image):
                case nameof(sasia):
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
                case nameof(shortDescription):
                case nameof(longDescription):
                case nameof(price):
                case nameof(image):
                case nameof(sasia):
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
                case nameof(shortDescription):
                case nameof(longDescription):
                case nameof(price):
                case nameof(image):
                case nameof(sasia):
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
                case nameof(shortDescription):
                case nameof(longDescription):
                case nameof(price):
                case nameof(image):
                case nameof(sasia):
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
                case nameof(shortDescription):
                case nameof(longDescription):
                    return DataType.String;
                case nameof(price):
                    return DataType.Decimal;
                case nameof(image):
                    return DataType.Custom;
                case nameof(sasia):
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
                case nameof(shortDescription):
                case nameof(longDescription):
                case nameof(price):
                case nameof(image):
                case nameof(sasia):
                    return null;
                case nameof(actions):
                    return actionsData;
                default:
                    return actionsData;
            }
        }
    }
}
