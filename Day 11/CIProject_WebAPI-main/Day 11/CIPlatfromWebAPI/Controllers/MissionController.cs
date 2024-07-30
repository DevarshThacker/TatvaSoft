using Business_logic_Layer;
using Data_Access_Layer;
using Data_Access_Layer.Repository.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionController : ControllerBase
    {
        private readonly BALMission _balMission;
        private readonly IWebHostEnvironment _hostingEnvironment;
        ResponseResult result = new ResponseResult();

        public MissionController(BALMission balMission, IWebHostEnvironment hostingEnvironment)
        {
            _balMission = balMission;
            _hostingEnvironment = hostingEnvironment;
        }

        #region Mission
        [HttpGet]
        [Route("GetMissionThemeList")]
        [Authorize]
        public ResponseResult GetMissionThemeList()
        {
            try
            {
                result.Data = _balMission.GetMissionThemeList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpGet]
        [Route("GetMissionSkillList")]
        [Authorize]
        public ResponseResult GetMissionSkillList()
        {
            try
            {
                result.Data = _balMission.GetMissionSkillList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpGet]
        [Route("MissionList")]
        [Authorize]
        public ResponseResult MissionList()
        {
            try
            {
                result.Data = _balMission.MissionList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpPost]
        [Route("AddMission")]
        [Authorize]
        public ResponseResult AddMission(Missions mission)
        {
            try
            {
                result.Data = _balMission.AddMission(mission);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpGet]
        [Route("MissionDetailById/{id}")]
        [Authorize]
        public ResponseResult MissionDetailById(int id)
        {
            try
            {
                result.Data = _balMission.MissionDetailById(id);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpPost]
        [Route("UpdateMission")]
        [Authorize]
        public ResponseResult UpdateMission(Missions mission)
        {
            try
            {
                result.Data = _balMission.UpdateMission(mission);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpDelete]
        [Route("DeleteMission/{id}")]
        [Authorize]
        public ResponseResult DeleteMission(int id)
        {
            try
            {
                result.Data = _balMission.DeleteMission(id);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpPost]
        [Route("UploadImage")]
        public async Task<IActionResult> UploadImage([FromForm] UploadFile upload)
        {
            string filePath = "";
            List<string> fileList = new List<string>();

            var files = Request.Form.Files;
                if (files != null && files.Count > 0)
    {
        foreach (var file in files)
        {
            if (!string.IsNullOrEmpty(file.ContentDisposition))
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                filePath = Path.Combine("UploadMissionImage", upload.ModuleName);
                string fileRootPath = Path.Combine(_hostingEnvironment.WebRootPath, filePath);

                if (!Directory.Exists(fileRootPath))
                {
                    Directory.CreateDirectory(fileRootPath);
                }

                string name = Path.GetFileNameWithoutExtension(fileName);
                string extension = Path.GetExtension(fileName);
                string fullFileName = $"{name}_{DateTime.Now:yyyyMMddHHmmss}{extension}";
                string fullPath = Path.Combine(filePath, fullFileName);
                string fullRootPath = Path.Combine(fileRootPath, fullFileName);

                using (var stream = new FileStream(fullRootPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                fileList.Add(fullPath);
            }
            else
            {
                return BadRequest(new { success = false, message = "Invalid file content disposition." });
            }
        }
    }
    else
    {
        return BadRequest(new { success = false, message = "No files uploaded." });
    }

    return Ok(new { success = true, data = fileList });
}

        #endregion
    }
}
