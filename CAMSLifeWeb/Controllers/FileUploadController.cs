using CaliphWeb.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CaliphWeb.Controllers
{
    public class FileUploadController : Controller
    {
        // GET: FileUpload
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> UploadFile(string tempDataName)
        {
            List<FileUploadViewModel> uploads = new List<FileUploadViewModel>();
            
            if (TempData[tempDataName] != null)
            {
                uploads = TempData[tempDataName] as List<FileUploadViewModel>;
            }

            if (Request.Files != null)
            {
                try
                {
                    foreach (string file in Request.Files)
                    {
                        var fileContent = Request.Files[file];
                        if (fileContent != null && fileContent.ContentLength > 0)
                        {
                            // get a stream
                            var stream = fileContent.InputStream;
                            // and optionally write the file to disk
                            string ext = Path.GetExtension(fileContent.FileName);


                            string path = "/FileUpload/"+ tempDataName+"/";

                            if (!Directory.Exists(Server.MapPath("~" + path)))
                                Directory.CreateDirectory(Server.MapPath("~" + path));


                            var tempFileName = $@"{DateTime.Now.Ticks}{ext}";
                            var serverPath = Path.Combine(Server.MapPath("~" + path) + tempFileName);
                            using (var fileStream = System.IO.File.Create(serverPath))
                            {
                                await stream.CopyToAsync(fileStream);
                            }

                            uploads.Add(new  FileUploadViewModel
                            {
                                Id= Guid.NewGuid().ToString(),
                                Name = tempFileName,
                                SavePath = path,
                            });
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            TempData[tempDataName] = uploads;

            return Json(uploads);
        }



        [HttpPost]
        public async Task<ActionResult> UploadSingleFile(string tempDataName)
        {
            var uploadedFile = new FileUploadViewModel();
            if (Request.Files != null)
            {
                try
                {
                    foreach (string file in Request.Files)
                    {
                        var fileContent = Request.Files[file];
                        if (fileContent != null && fileContent.ContentLength > 0)
                        {
                            // get a stream
                            var stream = fileContent.InputStream;
                            // and optionally write the file to disk
                            string ext = Path.GetExtension(fileContent.FileName);
                            string path = "/FileUpload/" + tempDataName + "/";


                            if (!Directory.Exists(Server.MapPath("~" + path)))
                                Directory.CreateDirectory(Server.MapPath("~" + path));


                            var tempFileName = $@"{DateTime.Now.Ticks}{ext}";
                            var serverPath = Path.Combine(Server.MapPath("~" + path) + tempFileName);
                            using (var fileStream = System.IO.File.Create(serverPath))
                            {
                                await stream.CopyToAsync(fileStream);
                            }


                            uploadedFile = new FileUploadViewModel
                            {
                                Id = Guid.NewGuid().ToString(),
                                Name = tempFileName,
                                SavePath = path,
                            };
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }


            return Json(uploadedFile);
        }


        [HttpPost]
        public async Task<ActionResult> UploadSummerNoteImageAsync(HttpPostedFileBase file)
        {
            if (file.ContentLength == 0)
                return Json("");
            try
            {
                var stream = file.InputStream;
                // and optionally write the file to disk
                string ext = Path.GetExtension(file.FileName);
                string path = "/FileUpload/summernote/";


                if (!Directory.Exists(Server.MapPath("~" + path)))
                    Directory.CreateDirectory(Server.MapPath("~" + path));


                var tempFileName = $@"{DateTime.Now.Ticks}{ext}";
                var serverPath = Path.Combine(Server.MapPath("~" + path) + tempFileName);
                using (var fileStream = System.IO.File.Create(serverPath))
                {
                    await stream.CopyToAsync(fileStream);
                }


              var  uploadedFile = new FileUploadViewModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = tempFileName,
                    SavePath = path,
                };
                return Json(uploadedFile.FullPath);

            }
            catch (Exception ex)
            {

            }
            return Json("");
        }

    }
}