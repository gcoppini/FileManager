using System;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using Atento.FileManager.Web.Api.Models;

namespace Atento.FileManager.Web.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class FileStorageController: Controller
    {
        private readonly IStoredFileRepository _repo;

        public FileStorageController(IStoredFileRepository repo)
        {
            _repo = repo;
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
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
        
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                    using (var stream = new MemoryStream())
                    {
                        file.CopyTo(stream);

                        var options = new GridFSUploadOptions
                        {
                            Metadata = new BsonDocument
                            {
                                {"author", "Gabriel Abner Copini"},
                                {"contentType", file.ContentType},
                                { "copyrighted", true }
                            } 
                        };  

                        

                        var id = _repo.bucket.UploadFromBytes(fileName, stream.ToArray(),options);
                        return Ok(new { id });
                    }
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
            
                var fileBytes = await _repo.bucket.DownloadAsBytesByNameAsync(filename);

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
    }
}