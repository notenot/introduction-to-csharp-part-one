using System;
using System.Collections.Generic;
using System.Text;

namespace Profiling
{
    class Generator
    {
        public static string GenerateDeclarations()
        {
            var result = new StringBuilder();

            foreach (var fieldCount in Constants.FieldCounts)
                result
                    .Append(GenerateDeclaration("struct", "S", fieldCount))
                    .Append(GenerateDeclaration("class", "C", fieldCount));
            return result.ToString();
        }

        private static string GenerateDeclaration(
            string type, string name, int fieldCount)
        {
            var result = new StringBuilder();
            result.AppendFormat("{0} {1}{2}", type, name, fieldCount);
            result.AppendLine();
            result.AppendLine("{");

            for (var i = 0; i < fieldCount; ++i)
                result.AppendFormat("byte Value{0}; ", i);

            result.AppendLine();
            result.AppendLine("}");
            return result.ToString();
        }

        public static string GenerateArrayRunner()
        {
            var result =
                new StringBuilder("public class ArrayRunner : IRunner");
            result.AppendLine();
            result.AppendLine("{");

            foreach (var index in Constants.FieldCounts)
                result.Append(GenerateMethodsPcAndPs(index));

            result.Append(GenerateMethodCall(Constants.FieldCounts));
            result.AppendLine("}");

            return result.ToString();
        }

        private static string GenerateMethodsPcAndPs(int index)
        {
            var result = new StringBuilder();

            result.AppendFormat("void PC{0}()", index);
            result.AppendLine();
            result.AppendLine("{");
            result.AppendFormat("var array = new C{0}[Constants.ArraySize];", index);
            result.AppendLine();
            result.AppendFormat(
                "for (int i = 0; i < Constants.ArraySize; i++) array[i] = new C{0}();", index);
            result.AppendLine();
            result.AppendLine("}");

            result.AppendFormat("void PS{0}()", index);
            result.AppendLine();
            result.AppendLine("{");
            result.AppendFormat("var array = new S{0}[Constants.ArraySize];", index);
            result.AppendLine();
            result.AppendLine("}");

            return result.ToString();
        }

        private static string GenerateMethodCall(IReadOnlyCollection<int> indices)
        {
            var result = 
                new StringBuilder("public void Call(bool isClass, int size, int count)");
            result.AppendLine();
            result.AppendLine("{");

            foreach (var index in indices)
                result
                    .Append(GenerateBlockFromMethodCall(true, index))
                    .Append(GenerateBlockFromMethodCall(false, index));

            result.AppendLine("throw new ArgumentException();");
            result.AppendLine("}");

            return result.ToString();
        }

        private static string GenerateBlockFromMethodCall(bool isClass, int index)
        {
            var result = new StringBuilder();
            
            result.AppendFormat(
                "if ({0}isClass && size == {1})", isClass ? "" : "!", index);
            result.AppendLine();
            result.AppendLine("{");
            result.AppendFormat(
                "for (int i = 0; i < count; i++) {0}{1}();", isClass ? "PC" : "PS", index);
            result.AppendLine();
            result.AppendLine("return;");
            result.AppendLine("}");

            return result.ToString();
        }

        public static string GenerateCallRunner()
        {
            throw new NotImplementedException();
        }
    }
}
