using Credo.ClassPrinter.BusinessLogic.Interfaces.Services;
using Credo.ClassPrinter.DataContracts.Constants;
using System.Reflection;
using System.Text;

namespace Credo.ClassPrinter.BusinessLogic.Services
{
    public class RecursiveObjectPrinter : IObjectPrinter
    {
        private const string IndentString = "    ";
        private readonly StringBuilder _output;
        private HashSet<Type> _visitedTypes;

        public RecursiveObjectPrinter()
        {
            _output = new StringBuilder();
            _visitedTypes = new HashSet<Type>();
        }


        public string Print(Type type)
        {
            _output.Clear();
            PrintRecursively(type, 0);
            return _output.ToString();
        }

        private void PrintRecursively(Type type, int indentLevel)
        {
            if (_visitedTypes.Contains(type))
            {
                _output.AppendLine($"{GetIndent(indentLevel)}{MetadataConstants.ClassLabel}: {type.Name} ({BusinessErrorMessages.CirucalReferenceDetected})");
                return;
            }

            _visitedTypes.Add(type);

            _output.AppendLine($"{GetIndent(indentLevel)}{MetadataConstants.ClassLabel}: {type.Name}");

            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
            {
                _output.AppendLine($"{GetIndent(indentLevel + 1)}{MetadataConstants.PropertyLabel}: {property.Name} ({MetadataConstants.TypeLabel}: {property.PropertyType.Name})");

                if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                {
                    PrintRecursively(property.PropertyType, indentLevel + 2);
                }
            }

            _visitedTypes.Remove(type);
        }

        private string GetIndent(int level)
        {
            return new string(' ', level * IndentString.Length);
        }

        //azurabiani@credo.ge
        private string GetFriendlyTypeName(Type type)
        {
            if (!type.IsGenericType)
                return type.Name;

            var genericArgs = type.GetGenericArguments()
                .Select(t => GetFriendlyTypeName(t));

            var baseName = type.Name.Split('`')[0];
            return $"{baseName}<{string.Join(", ", genericArgs)}>";
        }
    }
}
