using System;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using Atento.FileManager.Web.Api.Models;
using Atento.FileManager.Services;


/*
   CQS
 - Quais métodos são comandos e quais são querys?
*/
namespace Atento.FileManager.Web.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class FileStorageController: Controller
    {
        private readonly IStoredFileRepository _repo;
        private readonly IFileStorageService _fileService;


        public FileStorageController(IStoredFileRepository repo,
                                     IFileStorageService fileService)
        {
            _repo = repo;
            _fileService = fileService;
        }

        // GET api/todos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoredFile>>> Get()
        {
            return new ObjectResult(await _repo.GetAllFiles());
        }

        // GET api/todos/1
        [HttpGet("{id}")]
        public async Task<ActionResult<StoredFile>> Get(string id)
        {
            var todo = await _repo.GetFile(id);

            if (todo == null)
                return new NotFoundResult();
            
            return new ObjectResult(todo);
        }

        // POST api/todos
        [HttpPost]
        public async Task<ActionResult<StoredFile>> Post([FromBody] StoredFile file)
        {
            await _repo.Create(file);
            return new OkObjectResult(file);
        }

        // PUT api/todos/1
        [HttpPut("{id}")]
        public async Task<ActionResult<StoredFile>> Put(string id, [FromBody] StoredFile file)
        {
            var todoFromDb = await _repo.GetFile(id);

            if (todoFromDb == null)
                return new NotFoundResult();

            file.Id = todoFromDb.Id;
            

            await _repo.Update(file);

            return new OkObjectResult(file);
        }

        // DELETE api/todos/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var post = await _repo.GetFile(id);

            if (post == null)
                return new NotFoundResult();

            await _repo.Delete(id);

            return new OkResult();
        }
        
        [HttpPost("upload"), DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var result = await _fileService.Save(file);

                if(result)
                {
                    return Ok(new { result });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        
        //GET api/download?filename=foo.bar
        [HttpGet("download")]
        public async Task<IActionResult> Download(string filename) //case SenSiTive
        {
            try
            {
                if (string.IsNullOrEmpty(filename)) return BadRequest();
            
                var fileBytes = await _fileService.Read(filename);

                if(fileBytes.Length == 0) return NotFound();

                return new FileContentResult(fileBytes, "application/octet-stream")
                {
                    FileDownloadName = filename
                };
            }
            catch(MongoDB.Driver.GridFS.GridFSFileNotFoundException ex)
            {
                System.Diagnostics.Debugger.Log(1,"Info",ex.Message);
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        } 


        //GET api/copy?filename=foo.bar&destinationPath=pathOnServerOrNetwork
        [HttpGet("copy")]
        public async Task<IActionResult> Copy(string filename, string destinationPath) //case SenSiTive
        {
            try
            {
                if (string.IsNullOrEmpty(filename)) return BadRequest();
                if(string.IsNullOrEmpty(destinationPath)) return BadRequest();

                var success = await _fileService.CopyTo(filename, destinationPath);

                if(!success) return StatusCode(500, $"Internal server error: Error {success}");

                return new OkResult();
            }
            catch(MongoDB.Driver.GridFS.GridFSFileNotFoundException ex)
            {
                System.Diagnostics.Debugger.Log(1,"Info",ex.Message);
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }     
    
        [HttpGet("remove")]
        public async Task<IActionResult> Remove(string filename)
        {
            if (string.IsNullOrEmpty(filename)) return BadRequest();

            var result = await _fileService.Remove(filename);

            if (!result)
                return new NotFoundResult();

            return new OkResult();
        }

    }
}