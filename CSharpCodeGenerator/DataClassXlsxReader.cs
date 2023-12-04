using ClosedXML.Excel;
using static CSharpCodeGenerator.DataClassDefineStructure;

namespace CSharpCodeGenerator
{
    class DataClassXlsxReader
    {
        public enum ColumnNum
        {
            NamespaceName = 1,
            ClassName = 2,
            Inheritance = 3,
            DataType = 4,
            VariableName = 5,
            Comment = 6,
            UsingName = 7,
        }

        static readonly XLColor s_headerColor = XLColor.FromHtml("#FF20124D");
        static readonly string s_ignoredSheetName = "Ignored_";

        public BookData GetBookData()
        {
            var bookData = new BookData();
            var path = GetDataClassDefinePath();
            using var book = new XLWorkbook(path);

            foreach (var sheet in book.Worksheets)
            {
                // 特定の接頭語がついているシートは無視する
                if (sheet.Name.StartsWith(s_ignoredSheetName))
                {
                    continue;
                }

                var sheetData = new SheetData
                {
                    SheetName = sheet.Name
                };

                var dataClassData = new DataClassData();

                foreach (var row in sheet.Rows())
                {
                    var namespaceName = row.Cell((int)ColumnNum.NamespaceName).Value.ToString();
                    var className = row.Cell((int)ColumnNum.ClassName).Value.ToString();
                    var inheritance = row.Cell((int)ColumnNum.Inheritance).Value.ToString();
                    var dataType = row.Cell((int)ColumnNum.DataType).Value.ToString();
                    var variableName = row.Cell((int)ColumnNum.VariableName).Value.ToString();
                    var comment = row.Cell((int)ColumnNum.Comment).Value.ToString();
                    var usingName = row.Cell((int)ColumnNum.UsingName).Value.ToString();

                    // ヘッダー部分を除外する
                    if (row.FirstCell().Style.Fill.BackgroundColor == s_headerColor)
                    {
                        continue;
                    }

                    // 変数名が無かったら終了する
                    if (string.IsNullOrEmpty(variableName))
                    {
                        break;
                    }

                    if (!string.IsNullOrEmpty(usingName) && !sheetData.UsingNames.Contains(usingName))
                    {
                        sheetData.UsingNames.Add(usingName);
                    }

                    if (!string.IsNullOrEmpty(namespaceName))
                    {
                        sheetData.NamespaceName = namespaceName;
                    }

                    if (!string.IsNullOrEmpty(className) && dataClassData.ClassName != null &&
                        className != dataClassData.ClassName)
                    {
                        sheetData.DataClassDatas.Add(dataClassData);
                        dataClassData = new DataClassData();
                    }

                    if (!string.IsNullOrEmpty(className))
                    {
                        dataClassData.ClassName = className;
                    }

                    var rowData = new RowData
                    {
                        DataType = dataType,
                        VariableName = variableName,
                        Comment = comment
                    };

                    dataClassData.RowDatas.Add(rowData);
                }

                sheetData.DataClassDatas.Add(dataClassData);
                bookData.SheetDatas.Add(sheetData);
            }

            return bookData;
        }

        string GetDataClassDefinePath()
        {
            var directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
            var path = Path.Combine(directoryInfo.Parent.Parent.Parent.FullName,
                "DataClassDefine.xlsx");
            return path;
        }
    }
}
