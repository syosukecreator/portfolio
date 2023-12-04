using ClosedXML.Excel;
using static CSharpCodeGenerator.EnumDefineStructure;

namespace CSharpCodeGenerator
{
    class EnumXlsxReader
    {
        public enum ColumnNum
        {
            NamespaceName = 1,
            EnumName = 2,
            EnumElementName = 3,
            DataType = 4,
            Value = 5,
            Comment = 6
        }

        static readonly XLColor s_headerColor = XLColor.FromHtml("#FF20124D");
        static readonly string s_ignoredSheetName = "Ignored_";

        public BookData GetBookData()
        {
            var bookData = new BookData();
            var path = GetEnumDefinePath();
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

                var enumData = new EnumData();

                foreach (var row in sheet.Rows())
                {
                    var namespaceName = row.Cell((int)ColumnNum.NamespaceName).Value.ToString();
                    var enumName = row.Cell((int)ColumnNum.EnumName).Value.ToString();
                    var enumElementName = row.Cell((int)ColumnNum.EnumElementName).Value.
                        ToString();
                    var dataType = row.Cell((int)ColumnNum.DataType).Value.ToString();
                    var value = row.Cell((int)ColumnNum.Value).Value.ToString();
                    var comment = row.Cell((int)ColumnNum.Comment).Value.ToString();

                    // ヘッダー部分を除外する
                    if (row.FirstCell().Style.Fill.BackgroundColor == s_headerColor)
                    {
                        continue;
                    }

                    // Enum要素名が無かったら終了する
                    if (string.IsNullOrEmpty(enumElementName))
                    {
                        break;
                    }

                    if (!string.IsNullOrEmpty(namespaceName))
                    {
                        sheetData.NamespaceName = namespaceName;
                    }

                    if (!string.IsNullOrEmpty(enumName) && enumData.EnumName != null &&
                        enumName != enumData.EnumName)
                    {
                        sheetData.EnumDatas.Add(enumData);
                        enumData = new EnumData();
                    }

                    if (!string.IsNullOrEmpty(enumName))
                    {
                        enumData.EnumName = enumName;
                    }

                    var rowData = new RowData
                    {
                        EnumElementName = enumElementName,
                        DataType = dataType,
                        Value = value,
                        Comment = comment
                    };

                    enumData.RowDatas.Add(rowData);
                }

                sheetData.EnumDatas.Add(enumData);
                bookData.SheetDatas.Add(sheetData);
            }

            return bookData;
        }

        string GetEnumDefinePath()
        {
            var directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
            var path = Path.Combine(directoryInfo.Parent.Parent.Parent.FullName,
                "EnumDefine.xlsx");
            return path;
        }
    }
}
