using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Storage.V1;
using Google.Apis.Storage.v1.Data;
using System;
using Google.Apis.Auth.OAuth2;
using WebApplication3.Data;
using WebApplication3.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeechToTextController : ControllerBase
    {

        public static GoogleCredential credential = GoogleCredential.FromFile("C:/Users/92475/OneDrive/桌面/WebApplication3/WebApplication3/Controllers/oceanic-bindery-356922-6601a97c89b6.json");
        //public static GoogleCredential credential = GoogleCredential.GetApplicationDefault();
        public static StorageClient storage = StorageClient.Create(credential);
        private readonly FileDBContext _context;

        public SpeechToTextController(FileDBContext context)
        {
            _context = context;
        }


        /// <summary> 
        /// Get all file names from the storage bucket
        /// </summary>
        [HttpGet]
        public string Get()
        {
            
            // Make an authenticated API request.
            string str = "";
            foreach (var obj in _context.SpeechFiles)
            {
                str += (obj.Name+"\n");
            }
            return str;
        }

        /// <summary> 
        /// Upload a file to the storage bucket
        /// </summary>
        [HttpPost]
        public async void UploadFile(IFormFile file)
        {

            var ms = new MemoryStream();
            
            file.CopyTo(ms);
         
            // Make an authenticated API request.
            var buckets = storage.ListBuckets("oceanic-bindery-356922");
            var obj = storage.UploadObject("msa-speech-to-text", file.FileName, null, ms);
            var data = new SpeechFile
            {
                Name = file.FileName,
                isVIP = false,
                Type = "audio/mpeg3"
            };
            Console.WriteLine("===================================");
            _context.Add(data);
            _context.SaveChanges();
        }

        /// <summary> 
        /// PUT method - change File's accessibility on membership
        /// </summary>
        [HttpPut("{n}")]
        public void Put(string n)
        {
            var obj = _context.SpeechFiles.Single(f => f.Name == n);
            obj.isVIP = !obj.isVIP;
            _context.Update(obj);
            _context.SaveChanges();

        }

        /// <summary> 
        /// Delete a file by name
        /// </summary>
        [HttpDelete("{name}")]
        public void Delete(string name)
        {
            // Make an authenticated API request.
            var buckets = storage.ListBuckets("oceanic-bindery-356922");
            storage.DeleteObjectAsync("msa-speech-to-text", name);
            _context.Remove(_context.SpeechFiles.Single(f => f.Name==name));
            _context.SaveChanges();
        }
    
    }
}
