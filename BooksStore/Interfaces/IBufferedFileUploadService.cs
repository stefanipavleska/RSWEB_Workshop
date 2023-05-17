﻿namespace BooksStore.Interfaces
{
    public interface IBufferedFileUploadService
    {
        Task<string> UploadFile(IFormFile file, IWebHostEnvironment webHostEnvironment);
    }
}
