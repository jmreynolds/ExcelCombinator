using System;
using System.Linq;
using NetOffice.ExcelApi.Enums;

namespace DataAccess
{
    public abstract class ExcelBase
    {
        protected NetOffice.ExcelApi.Application Application;
        protected NetOffice.ExcelApi.Workbook Workbook;
        protected NetOffice.ExcelApi.Worksheet Worksheet;
        protected NetOffice.ExcelApi.Range Cells;
        protected NetOffice.ExcelApi.Range Range;
        private string _outputPath;
        private string _inputFile;

        public string OutputPath
        {
            get { return _outputPath; }
            set
            {
                _outputPath = value; 
                OutputPathChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public string InputFile
        {
            get { return _inputFile; }
            set
            {
                _inputFile = value;
                InputFileChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        protected void KillExcel()
        {
            Workbook.Close();
            Application.DisplayAlerts = true;
            Application.Quit();
            Helpers.Release(Cells);
            Helpers.Release(Worksheet);
            Helpers.Release(Workbook);
            Helpers.Release(Application);
            Helpers.Release(Range);
            Cells = null;
            Range = null;
            Worksheet = null;
            Workbook = null;
            Application = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public event EventHandler OutputPathChanged;

        protected void SaveRangeToExcelFile(object[,] range)
        {
            OpenWorksheet();
            var rowCount = range.GetLength(0);
            var colCount = range.GetLength(1);
            Range =  Worksheet.Cells[1,1];
            Range = Range.get_Resize(rowCount, colCount);
            Range.set_Value(XlRangeValueDataType.xlRangeValueDefault, range);
            Worksheet.Name = "Output";
            Application.DisplayAlerts = false;
            Workbook.SaveAs(OutputPath);
            KillExcel();

        }

        protected void OpenWorksheet(string fileName ="")
        {
            Application = new NetOffice.ExcelApi.Application();
            Workbook = (string.IsNullOrWhiteSpace(fileName)) 
                ? Application.Workbooks.Add() 
                : Application.Workbooks.Open(fileName);
            Worksheet = (NetOffice.ExcelApi.Worksheet) Workbook.Sheets.First();
        }

        public event EventHandler InputFileChanged;

        protected object[,] ReadRange()
        {
            object[,] valueArray;
            try
            {
                OpenWorksheet(InputFile);
                Range = Worksheet.UsedRange;
                valueArray =
                    (object[,])Range.get_Value(XlRangeValueDataType.xlRangeValueDefault);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message,e.InnerException);
            }
            finally
            {
                KillExcel();
            }
            return valueArray;
        }
    }
}