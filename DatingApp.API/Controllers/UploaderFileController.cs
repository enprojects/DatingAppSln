using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace DatingApp.API.Controllers
{
    public static class Helper
    {
        public static IEnumerable<T> ConvertSheetToObjects<T>(this ExcelWorksheet worksheet) where T : new()
        {
            //DateTime Conversion
            var convertDateTime = new Func<double, DateTime>(excelDate =>
            {
                if (excelDate < 1)
                    throw new ArgumentException("Excel dates cannot be smaller than 0.");

                var dateOfReference = new DateTime(1900, 1, 1);

                if (excelDate > 60d)
                    excelDate = excelDate - 2;
                else
                    excelDate = excelDate - 1;
                return dateOfReference.AddDays(excelDate);
            });

            //Get the properties of T
            var tprops = (new T())
                .GetType()
                .GetProperties()
                .ToList();

            //Cells only contains references to cells with actual data
            var groups = worksheet.Cells
                .GroupBy(cell => cell.Start.Row)
                .ToList();

            //Assume the second row represents column data types (big assumption!)
            var types = groups
                .Skip(1)
                .First()
                .Select(rcell => rcell.Value.GetType())
                .ToList();

            //Assume first row has the column names
            var colnames = groups
                .First()
                .Select((hcell, idx) => new { Name = hcell.Value.ToString(), index = idx })
                .Where(o => tprops.Select(p => p.Name).Contains(o.Name))
                .ToList();

            //Everything after the header is data
            var rowvalues = groups
                .Skip(1) //Exclude header
                .Select(cg => cg.Select(c => c.Value).ToList());


            //Create the collection container
            var collection = rowvalues
                .Select(row =>
                {
                    var tnew = new T();
                    colnames.ForEach(colname =>
                    {
                //This is the real wrinkle to using reflection - Excel stores all numbers as double including int
                var val = row[colname.index];
                        var type = types[colname.index];
                        var prop = tprops.First(p => p.Name == colname.Name);

                //If it is numeric it is a double since that is how excel stores all numbers
                if (type == typeof(double))
                        {
                    //Unbox it
                    var unboxedVal = (double)val;

                    //FAR FROM A COMPLETE LIST!!!
                    if (prop.PropertyType == typeof(Int32))
                                prop.SetValue(tnew, (int)unboxedVal);
                            else if (prop.PropertyType == typeof(double))
                                prop.SetValue(tnew, unboxedVal);
                            else if (prop.PropertyType == typeof(DateTime))
                                prop.SetValue(tnew, convertDateTime(unboxedVal));
                            else
                                throw new NotImplementedException(String.Format("Type '{0}' not implemented yet!", prop.PropertyType.Name));
                        }
                        else
                        {
                    //Its a string
                    prop.SetValue(tnew, val);
                        }
                    });

                    return tnew;
                });


            //Send it back
            return collection;
        }
    }


    public class TestClass
    {

        public string Col1 { get; set; }
        public string Col2 { get; set; }
        public string Col3 { get; set; }

    }

    [Route("api/[controller]")]
    public class UploaderFileController : Controller
    {

        private IHostingEnvironment _hostingEnvironment;
        public UploaderFileController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("Test")]
        public IActionResult Test()
        {

            return Ok();
        }

        [HttpPost("do")]
        public IActionResult UploadFile()
        {
            //Install-Package EPPlus.Core
            var username = "currentUser_" + DateTime.Now.ToString("dd.MM.yyyy hhmm");

            try
            {
                var file = Request.Form.Files[0];
                string folderName = "Upload";
                string webRootPath = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {

                    string fileName = $"{username}{Path.GetExtension(file.FileName)}";
                    string fullPath = Path.Combine(newPath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                return Json("Upload Successful.");
            }



            catch (System.Exception ex)
            {
                return Json("Upload Failed: " + ex.Message);
            }
        }

        public void ReadFile()
        {

            var fileName = "currentUser_25.09.2018 0828.xlsx";
            string fileFullPath = $"{_hostingEnvironment.WebRootPath}\\Upload\\{fileName}";
            var fi = new FileInfo(fileFullPath);
            using (ExcelPackage package = new ExcelPackage(fi))
            {
                //get the first worksheet in the workbook

                try
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.First();
                    int colCount = worksheet.Dimension.End.Column;  //get Column Count
                    int rowCount = worksheet.Dimension.End.Row;     //get row count

                    var newcollection = worksheet.ConvertSheetToObjects<TestClass>();

                    var testList = new List<TestClass>();



                }
                catch (Exception ex)
                {

                    throw;
                }

            }


        }

    }
}