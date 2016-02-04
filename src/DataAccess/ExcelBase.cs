using System;
using System.Linq;

namespace DataAccess
{
    public class ExcelBase
    {
        protected NetOffice.ExcelApi.Application Application;
        protected NetOffice.ExcelApi.Workbook Workbook;
        protected NetOffice.ExcelApi.Worksheet Worksheet;
        protected NetOffice.ExcelApi.Range Cells;

        protected void KillExcel()
        {
            Workbook.Close();
            Application.DisplayAlerts = true;
            Application.Quit();
            Helpers.Release(Cells);
            Helpers.Release(Worksheet);
            Helpers.Release(Workbook);
            Helpers.Release(Application);
            Cells = null;
            Worksheet = null;
            Workbook = null;
            Application = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}