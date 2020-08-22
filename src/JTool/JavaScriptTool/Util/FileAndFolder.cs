// ***********************************************************************
// Product          : CommonLibs
// Unit             : Manage 
// Version          : 1.0.0
// Created          : 2020-02-19
// Last Update      : 2020-5-25
// Copyright (c) 2020 JANSOFT. All Rights Reserved
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CoreWeb.Handlers.Helper
{
    public static class FileAndFolder
    {
        /// <summary>
        /// Kiểm tra hình ảnh or file tồn tại
        /// </summary>
        /// <param name="url"></param>
        /// <param name="contenTypeResponse"></param>
        /// <returns></returns>
        public static bool CheckImageExit(string url, params string[] contenTypeResponse)
        {
            // return (!string.IsNullOrEmpty(CheckUrlResponse(url, contenTypeResponse)) || new Uri(url).IsFile) ? true : false;
            bool ok = false;
            if (!string.IsNullOrEmpty(CheckUrlResponse(url, contenTypeResponse)))
            {
                ok = true;
            }
            var uri = new Uri(url);
            if (uri.IsFile)
            {
                ok = true;
            }
            return ok;
        }
        /// <summary>
        ///  Kiểm tra file tồn tại trong đường dẫn trên các trang web ngoài thông qua respon
        /// </summary>
        /// <param name="uriToImage"></param>
        /// <param name="contenTypeResponse"></param>
        /// <returns></returns>
        public static string CheckUrlResponse(string uriToImage, params string[] contenTypeResponse)
        {

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uriToImage);
                request.Method = "HEAD";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                // true nếu stt = OK và tồn tại content type trong tham số truyền vào
                if (response.StatusCode == HttpStatusCode.OK && contenTypeResponse.Any(a => a == response.ContentType))
                {
                    return response.ContentType;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return string.Empty;
        }
        /// <summary>
        ///  // create folder not exit
        /// </summary>
        /// <param name="pathFolder"></param>
        public static void CreateFolderNoExit(string pathFolder)
        {
            if (!Directory.Exists(pathFolder))
            {
                Directory.CreateDirectory(pathFolder);
            }
        }
        /// <summary>
        /// sao chép file
        /// </summary>
        /// <param name="fileSource"></param>
        /// <param name="fileTarget"></param>
        /// <returns></returns>
        public static async Task CoppyFileAsync(string fileSource, string fileTarget)
        {
            string p = Path.GetDirectoryName(fileTarget);
            CreateFolderNoExit(p);
            using (FileStream filestream = File.Create(fileTarget))
            {
                using (FileStream file = File.OpenRead(fileSource))
                {
                    await file.CopyToAsync(filestream);
                    await filestream.FlushAsync();
                }
            }
        }
        /// <summary>
        /// Ghi text 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="fileTarget"></param>
        /// <returns></returns>
        public static async Task WriteFileAsync(this string content, string fileTarget)
        {
            string p = Path.GetDirectoryName(fileTarget);
            CreateFolderNoExit(p);
            using (StreamWriter writer = File.AppendText(fileTarget))
            {
                await writer.WriteLineAsync(content);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>Exception</returns>
        public static void DeleteFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException("fileName");
            try
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchFolder"></param>
        /// <param name="isRecursive"></param>
        /// <param name="filters">jpg, png...</param>
        /// <returns></returns>
        public static List<string> GetFiles(string searchFolder, bool isRecursive = false, params string[] filters)
        {
            List<string> filesFound = new List<string>();
            var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            foreach (var filter in filters)
            {
                filesFound.AddRange(Directory.GetFiles(searchFolder, string.Format("*.{0}", filter), searchOption));
            }
            return filesFound;
        }
    }
}
