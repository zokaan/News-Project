using Microsoft.AspNetCore.Hosting;
using NewsProject.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NewsProject.Models
{
    public class NewsRepository : INewsRepository
    {
        private readonly NewsDbContext _dbContext;
        private IHostingEnvironment _hostingEnvironment;

        public NewsRepository(NewsDbContext dbContext,
                              IHostingEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _hostingEnvironment = hostingEnvironment;
        }
        public News Create(NewsCreateDTO news)
        {
            string fileName = UploadedImage(news);
            News addedNews = new News
            {
                Title = news.Title,
                ShortDescription = news.ShortDescription,
                Content = news.Content,
                Image = fileName
            };

            _dbContext.News.Add(addedNews);
            _dbContext.SaveChanges();
            return addedNews;
        }

        public NewsUpdateDTO GetUpdate(int id)
        {
            News news = _dbContext.News.Where(n => n.Id == id).FirstOrDefault();

            NewsUpdateDTO updateNews = new NewsUpdateDTO
            {
                Id = news.Id,
                Title = news.Title,
                ShortDescription = news.ShortDescription,
                Content = news.Content,
            };

            return updateNews;
        }

        public News Delete(int id)
        {
            News news = _dbContext.News.Where(n => n.Id == id).FirstOrDefault();
            if(news != null)
            {
                _dbContext.News.Remove(news);
                _dbContext.SaveChanges();
            }
            return news;
        }

        public IEnumerable<News> GetAllNews()
        {
            return _dbContext.News;
        }

        public News GetNews(int id)
        {
            News news = _dbContext.News.Where(n => n.Id == id).FirstOrDefault();
            return news;
        }

        public News Update(NewsUpdateDTO news)
        {
            News editNews = _dbContext.News.Where(n => n.Id == news.Id).FirstOrDefault();
            if (editNews != null)
            {
                editNews.Title = news.Title;
                editNews.ShortDescription = news.ShortDescription;
                editNews.Content = news.Content;
                if (news.ImageFile != null)
                {
                    editNews.Image = UploadedImage(news);
                }
                _dbContext.SaveChanges();
            }

            return editNews;
        }

        private string UploadedImage(NewsCreateDTO news)
        {
            string fileName = null;
            if (news.ImageFile != null)
            {
                string imagesFolder = Path.Combine(_hostingEnvironment.WebRootPath, "pictures");
                fileName = Guid.NewGuid().ToString() + "_" + news.ImageFile.FileName;
                string filePath = Path.Combine(imagesFolder, fileName);
                news.ImageFile.CopyTo(new FileStream(filePath, FileMode.Create));
            }

            return fileName;
        }

    }
}
