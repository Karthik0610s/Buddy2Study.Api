using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buddy2Study.Application.Dtos
{
    public class FileUploadDto
    {
        public int Id { get; set; }
        public string TypeofUser { get; set; }

        public List<IFormFile> FormFiles { get; set; }
    }
}
