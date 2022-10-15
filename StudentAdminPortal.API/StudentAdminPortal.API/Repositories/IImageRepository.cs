using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.Repositories
{
    public interface IImageRepository
    {
       Task<String> Upload(IFormFile file, string fileName);
    }
}
