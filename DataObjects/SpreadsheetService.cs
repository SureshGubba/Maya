using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO.Packaging;
using System.Data;
using System.Drawing;
using SchneiderMilkManagement.DataLayer.DataObjects;
using System.Threading.Tasks;
namespace SchneiderMilkManagement.DataLayer.DataObjects
{
    /// <summary>
    public class SpreadsheetService
    {
        public SpreadsheetService()
        {
        }
        public void CreateSpreadsheetWorkbook(string filename, DataTable excelData, string fromDate, string todate)
        {
            string fileName = @filename;
            uint sheetId = 1; //Start at the first sheet in the Excel workbook.
            SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook);
            WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();
            Sheets sheets;

            WorkbookStylesPart stylesPart = spreadsheetDocument.WorkbookPart.AddNewPart<WorkbookStylesPart>();
            sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

            var distinctdatesforsheetnames = (from data in excelData.AsEnumerable()
                                              select data.Field<DateTime>("Date").ToString("yyyy-MM-dd")).Distinct();


            stylesPart.Stylesheet = GenerateStyleSheet();

            UInt32 sheetCounter = 0;
            try
            {

                Parallel.ForEach(distinctdatesforsheetnames, oneDate =>
                {


                    //distinctdatesforsheetnames.ToList().ForEach(oneDate =>
                    //{

                    var starttime = DateTime.Now;
                    //sheetdata creation
                    var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                    var sheetData = new SheetData();
                    worksheetPart.Worksheet = new Worksheet(sheetData);
                    //Converting to the month,Date and year format
                    var sheetname = Convert.ToDateTime(oneDate).ToString("MMMM dd, yyyy");
                    //Giving sheet names
                    var sheet = new Sheet()
                    {
                        Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                        SheetId = sheetCounter++,
                        Name = sheetname
                    };
                    var counter = 1;
                    CreateColumnHeaders(excelData, sheetData);//Creating header columns
                    var excelRowIndex = 1;//starting row index in excel 
                    // var excelDataForOneDay = excelData.AsEnumerable().Where(x => x.Field<string>("OnlyDate") == oneDate).ToList();
                    var excelDataForOneDay = excelData.AsEnumerable().Where(x => x.Field<DateTime>("Date").ToString("yyyy-MM-dd") == oneDate).ToList();

                    excelDataForOneDay.ToList().ForEach(y =>
                    {
                        Row contentRow = CreateContentRow(++counter, y, excelData.Columns.Count, excelData, spreadsheetDocument, workbookpart, stylesPart.Stylesheet);
                        excelRowIndex++;
                        sheetData.AppendChild(contentRow);
                    });


                    sheets.Append(sheet);
                    workbookpart.Workbook.Save();
                    Console.WriteLine("took for " + oneDate + "   " + DateTime.Now.Subtract(starttime).TotalSeconds.ToString());

                });



                spreadsheetDocument.Close();
            }
            catch (Exception ex)
            {
            }

        }
        private void CreateColumnHeaders(DataTable excelData, SheetData sheetData)//Header column creation
        {

            var headerColumn = new Row();
            foreach (DataColumn column in excelData.Columns)
            {
                var cell = new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(column.ColumnName)
                };
                headerColumn.AppendChild(cell);
            }
            sheetData.AppendChild(headerColumn);

        }
        public static Row CreateContentRow(int index, DataRow rowdata, int headerColumnsCount, DataTable excelData, SpreadsheetDocument spreadsheetDocument, WorkbookPart workbookpart, Stylesheet stylesheet)//Row creation
        {
            Row newrow = new Row();
            newrow.RowIndex = (UInt32)index;
            CellFormat celform = new CellFormat();
            var rowexceldataindex = excelData.Rows.IndexOf(rowdata);
            try
            {
                for (int i = 0; i < headerColumnsCount; i++)
                {
                    var status = rowdata["Status"].ToString();

                    Cell cell = CreateTextCell(excelData.Columns[i].ToString(), rowdata[i].ToString(), index, excelData);

                    newrow.AppendChild(cell);


                    var columnName = excelData.Columns[i].ToString();
                    cell.StyleIndex = (UInt32Value)4U;

                    if (i == 0 && status == "Normal")
                    {
                        cell.StyleIndex = (UInt32Value)3U;
                    }


                    if (columnName == "Status")
                    {
                        switch (status)
                        {
                            case "Normal":
                                cell.StyleIndex = (UInt32Value)2U;
                                break;

                            case "Attention":
                                cell.StyleIndex = (UInt32Value)1U;
                                break;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
            }
            return newrow;

        }
        static void AddCellFormat(CellFormats cf, Fills fills1)
        {
            CellFormat cellFormat2 = new CellFormat() { NumberFormatId = 0, FontId = 0, FillId = fills1.Count - 1, BorderId = 0, FormatId = 0, ApplyFill = true };
            cf.Append(cellFormat2);
        }
        public static Fills AddFill(Fills fills1)
        {
            Fill fill1 = new Fill();

            PatternFill patternFill1 = new PatternFill() { PatternType = PatternValues.Solid };
            ForegroundColor foregroundColor1 = new ForegroundColor() { Rgb = "FFFFFF00" };
            BackgroundColor backgroundColor1 = new BackgroundColor() { Indexed = (UInt32Value)64U };

            patternFill1.Append(foregroundColor1);
            patternFill1.Append(backgroundColor1);

            fill1.Append(patternFill1);
            fills1.Append(fill1);
            return fills1;
        }
        public static Cell CreateTextCell(string header, string text, int index, DataTable excelData)//Adding text
        {
            Cell c = new Cell();


            c.DataType = CellValues.InlineString;
            InlineString inlineString = new InlineString();
            Text t = new Text();
            t.Text = text;



            if (t.Text == "1" && excelData.Rows[index - 2]["ParameterName"].ToString() == "LidStatus" && header == "CapturedValue")
            {
                t.Text = "closed";
            }
            else if (t.Text == "0" && excelData.Rows[index - 2]["ParameterName"].ToString() == "LidStatus" && header == "CapturedValue")
            {
                t.Text = "Open";
            }
            if (excelData.Rows[index - 2]["ParameterName"].ToString() == "LidStatus" && (header == "MinValue" || header == "MaxValue"))
            {
                t.Text = " ";
            }
           
            if (excelData.Rows[index - 2]["ParameterName"].ToString().Trim().Equals("Level(cm)", StringComparison.InvariantCultureIgnoreCase) && header == "CapturedValue")
            {
                t.Text = String.Format("{0} Liters ({1} cms)", new BMCMonitorDao().GetCapacityinLitersFromMM(Convert.ToDecimal(t.Text)).CapacityInLiters, Convert.ToDecimal(t.Text));
            }
            inlineString.AppendChild(t);
            c.AppendChild(inlineString);

            return c;
        }

        public static Stylesheet GenerateStyleSheet()
        {

            Stylesheet stylesheet1 = new Stylesheet();
            //stylesheet1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            //stylesheet1.AddNamespaceDeclaration("x14ac", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac");

            Fonts fonts1 = new Fonts() { Count = (UInt32Value)1U, KnownFonts = true };

            Font font1 = new Font();
            FontSize fontSize1 = new FontSize() { Val = 11D };
            Color color1 = new Color() { Theme = (UInt32Value)1U };
            FontName fontName1 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering1 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme1 = new FontScheme() { Val = FontSchemeValues.Minor };

            font1.Append(fontSize1);
            font1.Append(color1);
            font1.Append(fontName1);
            font1.Append(fontFamilyNumbering1);
            font1.Append(fontScheme1);

            fonts1.Append(font1);

            Fills fills1 = new Fills() { Count = (UInt32Value)6U };

            // FillId = 0
            Fill nocolor = new Fill();
            PatternFill patternnocolor = new PatternFill() { PatternType = PatternValues.None };
            nocolor.Append(patternnocolor);

            // FillId = 1
            Fill defaultcolor = new Fill();
            PatternFill patterndefaultcolor = new PatternFill() { PatternType = PatternValues.Gray125 };
            defaultcolor.Append(patterndefaultcolor);

            // FillId = 2,RED
            Fill redcolr = new Fill();
            PatternFill patternredcolr = new PatternFill() { PatternType = PatternValues.Solid };
            ForegroundColor foregroundColor1 = new ForegroundColor() { Rgb = "FFFF0000" };
            BackgroundColor backgroundColor1 = new BackgroundColor() { Indexed = (UInt32Value)64U };
            patternredcolr.Append(foregroundColor1);
            patternredcolr.Append(backgroundColor1);
            redcolr.Append(patternredcolr);

            // FillId = 3,GREEN
            Fill greencolor = new Fill();
            PatternFill patterngreencolor = new PatternFill() { PatternType = PatternValues.Solid };
            ForegroundColor foregroundColor2 = new ForegroundColor() { Rgb = "FF00FF00" };
            BackgroundColor backgroundColor2 = new BackgroundColor() { Indexed = (UInt32Value)64U };
            patterngreencolor.Append(foregroundColor2);
            patterngreencolor.Append(backgroundColor2);
            greencolor.Append(patterngreencolor);

            // FillId = 4,ORANGE
            Fill orangecolor = new Fill();
            PatternFill patternorangecolor = new PatternFill() { PatternType = PatternValues.Solid };
            ForegroundColor foregroundColor3 = new ForegroundColor() { Rgb = "FFFF6600" };
            BackgroundColor backgroundColor3 = new BackgroundColor() { Indexed = (UInt32Value)64U };
            patternorangecolor.Append(foregroundColor3);
            patternorangecolor.Append(backgroundColor3);
            orangecolor.Append(patternorangecolor);

            //FillId=5,BLUE
            Fill lighgreencolor = new Fill();
            PatternFill patternlighgreencolor = new PatternFill() { PatternType = PatternValues.Solid };
            ForegroundColor foregroundColor6 = new ForegroundColor() { Rgb = "FFCCFFCC" };
            BackgroundColor backgroundColor6 = new BackgroundColor() { Indexed = (UInt32Value)64U };
            patternlighgreencolor.Append(foregroundColor6);
            patternlighgreencolor.Append(backgroundColor6);
            lighgreencolor.Append(patternlighgreencolor);

            fills1.Append(nocolor);
            fills1.Append(defaultcolor);
            fills1.Append(redcolr);
            fills1.Append(greencolor);
            fills1.Append(orangecolor);
            fills1.Append(lighgreencolor);

            //Borders borders1 = new Borders() { Count = (UInt32Value)1U };

            //Border border1 = new Border();
            //LeftBorder leftBorder1 = new LeftBorder();
            //RightBorder rightBorder1 = new RightBorder();
            //TopBorder topBorder1 = new TopBorder();
            //BottomBorder bottomBorder1 = new BottomBorder();
            //DiagonalBorder diagonalBorder1 = new DiagonalBorder();

            //border1.Append(leftBorder1);
            //border1.Append(rightBorder1);
            //border1.Append(topBorder1);
            //border1.Append(bottomBorder1);
            //border1.Append(diagonalBorder1);

            //borders1.Append(border1);

            CellStyleFormats cellStyleFormats1 = new CellStyleFormats() { Count = (UInt32Value)1U };
            CellFormat cellFormat1 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U };

            cellStyleFormats1.Append(cellFormat1);

            CellFormats cellFormats1 = new CellFormats() { Count = (UInt32Value)2U };
            CellFormat cellFormat2 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U };
            CellFormat cellFormat3 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFill = true };
            CellFormat cellFormat4 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)3U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFill = true };
            CellFormat cellFormat5 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)4U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFill = true };
            CellFormat cellFormat6 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)5U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFill = true };
            cellFormats1.Append(cellFormat2);
            cellFormats1.Append(cellFormat3);
            cellFormats1.Append(cellFormat4);
            cellFormats1.Append(cellFormat5);
            cellFormats1.Append(cellFormat6);

            CellStyles cellStyles1 = new CellStyles() { Count = (UInt32Value)1U };
            CellStyle cellStyle1 = new CellStyle() { Name = "Normal", FormatId = (UInt32Value)0U, BuiltinId = (UInt32Value)0U };

            cellStyles1.Append(cellStyle1);
            DifferentialFormats differentialFormats1 = new DifferentialFormats() { Count = (UInt32Value)0U };
            TableStyles tableStyles1 = new TableStyles() { Count = (UInt32Value)0U, DefaultTableStyle = "TableStyleMedium2", DefaultPivotStyle = "PivotStyleMedium9" };

            StylesheetExtensionList stylesheetExtensionList1 = new StylesheetExtensionList();

            StylesheetExtension stylesheetExtension1 = new StylesheetExtension() { Uri = "{EB79DEF2-80B8-43e5-95BD-54CBDDF9020C}" };
            stylesheetExtension1.AddNamespaceDeclaration("x14", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
            //X14.SlicerStyles slicerStyles1 = new X14.SlicerStyles() { DefaultSlicerStyle = "SlicerStyleLight1" };

            // stylesheetExtension1.Append(slicerStyles1);

            stylesheetExtensionList1.Append(stylesheetExtension1);

            //stylesheet1.Append(fonts1);
            stylesheet1.Append(fills1);
            //stylesheet1.Append(borders1);
            stylesheet1.Append(cellStyleFormats1);
            stylesheet1.Append(cellFormats1);
            stylesheet1.Append(cellStyles1);
            stylesheet1.Append(differentialFormats1);
            stylesheet1.Append(tableStyles1);
            stylesheet1.Append(stylesheetExtensionList1);
            return stylesheet1;

        }
        public static void setStyleSheet(SpreadsheetDocument spreadsheetDocument)
        {

            Stylesheet ss = new Stylesheet();

            ss = spreadsheetDocument.WorkbookPart.WorkbookStylesPart.Stylesheet;
            ForegroundColor fc1 = new ForegroundColor();
            fc1.Rgb = HexBinaryValue.FromString("FFFFFF");

            ForegroundColor fc2 = new ForegroundColor();
            fc2.Rgb = HexBinaryValue.FromString("A2CD5A"); // Orange

            ForegroundColor fc3 = new ForegroundColor();
            fc3.Rgb = HexBinaryValue.FromString("3D59AB"); // Blue
            Fonts fonts1 = new Fonts();

            Font f1 = new Font(new FontSize() { Val = 11 }, new Color() { Rgb = "FFFFFF" }, new FontName() { Val = "Arial" });
            fonts1.AppendChild(f1);
            ss.Fonts.AppendChild(fonts1);
            //new Font(new FontSize() { Val = 11 }, new Color() { Rgb = "FFFFFF" }, new FontName() { Val = "Arial" }));

            // Index 0
            ss.Fills.AppendChild(new Fill()
            {
                PatternFill = new PatternFill()
                {
                    PatternType = PatternValues.Solid,
                    BackgroundColor = new BackgroundColor() { Rgb = fc1.Rgb }
                }
            });

            // Index 1
            ss.Fills.AppendChild(new Fill()
            {
                PatternFill = new PatternFill()
                {
                    PatternType = PatternValues.Solid,
                    BackgroundColor = new BackgroundColor() { Rgb = fc2.Rgb }
                }
            });

            // Index 2
            ss.Fills.AppendChild(new Fill()
            {
                PatternFill = new PatternFill()
                {
                    PatternType = PatternValues.Solid,
                    BackgroundColor = new BackgroundColor() { Rgb = fc3.Rgb }
                }
            });

            CellFormat cf = new CellFormat();
            //cf.NumberFormatId = 0;
            //cf.FontId = 0;
            //cf.FillId = 0;
            //cf.BorderId = 0;
            //csfs.Append(cf);
            //csfs.Count = UInt32Value.FromUInt32((uint)csfs.ChildElements.Count);

            ////uint iExcelIndex = 164;
            //NumberFormats nfs = new NumberFormats();
            CellFormats cfs = new CellFormats();

            // index 0
            cf = new CellFormat();
            cf.NumberFormatId = 0;
            cf.FontId = 0;
            cf.FillId = 0;
            cf.BorderId = 0;
            cf.FormatId = 0;
            cfs.Append(cf);

            // index 1
            // coloured orange column text
            cf = new CellFormat();
            cf.NumberFormatId = 1;
            cf.FontId = 0;
            cf.FillId = 1;
            cf.BorderId = 1;
            cf.FormatId = 0;
            cf.ApplyNumberFormat = BooleanValue.FromBoolean(false);
            cfs.Append(cf);

            // index 2
            // coloured blue column text
            cf = new CellFormat();
            cf.NumberFormatId = 2;
            cf.FontId = 0;
            cf.FillId = 2;
            cf.BorderId = 1;
            cf.FormatId = 0;
            cf.ApplyNumberFormat = BooleanValue.FromBoolean(false);
            cfs.Append(cf);

            // nfs.Count = UInt32Value.FromUInt32((uint)nfs.ChildElements.Count);
            cfs.Count = UInt32Value.FromUInt32((uint)cfs.ChildElements.Count);

            ss.CellFormats.Append(cfs);


            spreadsheetDocument.WorkbookPart.WorkbookStylesPart.Stylesheet.Save();
        }
    }

}



