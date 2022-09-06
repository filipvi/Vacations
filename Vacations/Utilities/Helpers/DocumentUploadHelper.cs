using System.Collections.Generic;

namespace Vacations.Utilities.Helpers
{
    // Prilikom prvog pokretanja apk, treba postojati folder Documents/Analize
    public static class DocumentUploadHelper
    {
        private static readonly List<string> MimeTypeList = new List<string>(new[]
        {
            //office mime types
            //"application/msword",   //.doc , .dot
            //"application/vnd.openxmlformats-officedocument.wordprocessingml.document",  //.docx
            //"application/vnd.openxmlformats-officedocument.wordprocessingml.template",  //.dotx
            //"application/vnd.ms-word.document.macroEnabled.12", //.docm
            //"application/vnd.ms-word.template.macroEnabled.12", //.dotm
            "application/vnd.ms-excel", //.xls , .xlt , .xla
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",    //.xlsx
            "application/vnd.openxmlformats-officedocument.spreadsheetml.template", //.xltx
            "application/vnd.ms-excel.sheet.macroEnabled.12",   //.xlsm
            "application/vnd.ms-excel.template.macroEnabled.12",    //.xltm
            "application/vnd.ms-excel.addin.macroEnabled.12",   //.xlam
            "application/vnd.ms-excel.sheet.binary.macroEnabled.12", //.xlsb
 
            //pdf mime type
            //"application/pdf",  //.pdf
 
            //image mime types
            //"image/bmp",    //.bmp , .bm
            //"image/x-windows-bmp",  //.bmp
            //"image/jpeg",   //.jpg , .jpeg
            //"image/pjpeg",  //.jpeg
            //"image/x-jps",  //.jps
            //"image/gif",    //.gif
            //"image/png" //.png
        });

        public static bool CheckMimeType(string contentType)
        {
            return MimeTypeList.Contains(contentType);
        }

        //Upload document
        //public static async Task<UploadAnalysisInquiryDocumentResult> UploadDocument(IFormFile formFile,
        //    int documentTypeId, string labAnalysisNumber, IWebHostEnvironment environment)
        //{
        //    var result = new UploadAnalysisInquiryDocumentResult();

        //    try
        //    {
        //        if (formFile != null)
        //        {
        //            var fileExtension = Path.GetExtension(formFile.FileName);
        //            var fileNameRoot = Path.GetFileNameWithoutExtension(formFile.FileName);
        //            var newFileName = fileNameRoot + "_" + DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss") + fileExtension;

        //            string path = Path.Combine(environment.ContentRootPath, "Documents/Analize");
        //            string analysisPath = Path.Combine(path, labAnalysisNumber);
        //            bool folderExists = Directory.Exists(analysisPath);

        //            if (!folderExists)
        //            {
        //                Directory.CreateDirectory(analysisPath);
        //            }

        //            string filePath = Path.Combine(analysisPath, newFileName);
        //            using Stream fileStream = new FileStream(filePath, FileMode.Create);
        //            await formFile.CopyToAsync(fileStream);

        //            result.AnalysisDocument = new AnalysisDocument
        //            {
        //                Name = formFile.FileName,
        //                Location = newFileName,
        //                AnalysisDocumentTypeId = documentTypeId,
        //                CreationDate = DateTime.Now
        //            };

        //            result.Success = true;
        //        }
        //        else
        //        {
        //            result.Success = false;
        //            result.Message = "Molimo vas priložite datoteku";
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Log.RepositoryLog("DocumentUploadHelper", "UploadDocument", e, null);

        //        result.Message = "Greška prilikom spremanja dokumenta";
        //        result.Success = false;
        //    }

        //    return result;
        //}
    }
}
