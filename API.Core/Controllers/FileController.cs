﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API.Core.IServices;
using API.Core.Model;
using API.Core.Model.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {



        private readonly IBinFileServices _binFileServices;
        public FileController(IBinFileServices BinFileServices)
        {

            this._binFileServices = BinFileServices;
        }





        /// <summary>
        /// 上传图片,多文件，可以使用 postman 测试，
        /// 如果是单文件，可以 参数写 IFormFile file1
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/images/Upload/Pic")]
        public async Task<MessageModel<string>> InsertPicture([FromServices]IWebHostEnvironment environment)
        {
            var data = new MessageModel<string>();
            string path = string.Empty;
            string foldername = "images";
            IFormFileCollection files = Request.Form.Files;

            if (files == null || !files.Any()) { data.Msg = "请选择上传的文件。"; return data; }

            if (files.Sum(c => c.Length) <= 1024 * 1024 * 4)
            {
                string strpath = string.Empty;
             
                foreach (var file in files)
                {
                    strpath = Path.Combine(foldername, DateTime.Now.ToString("MMddHHmmss") + file.FileName);
                    path = Path.Combine(environment.WebRootPath, strpath);

                    byte[] bytea = null;
                    using (var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        byte[] bytes = new byte[stream.Length];
                        stream.Read(bytes, 0, bytes.Length);
                        // 设置当前流的位置为流的开始 
                        stream.Seek(0, SeekOrigin.Begin);
                        bytea = bytes;
                    };
                    
                   
                    BinFiles binFiles = new BinFiles
                    {
                        FileByte = bytea
                    };
                    await _binFileServices.Add(binFiles);
                   
                }


                data = new MessageModel<string>()
                {
                    Response = strpath,
                    Msg = "上传成功",
                    Success = true,
                };
                return data;
            }
            else
            {
                data.Msg = "图片过大";
                return data;
            }

        }
    }
}
