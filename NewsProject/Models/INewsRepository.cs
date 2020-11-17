using NewsProject.ViewModels;
using System.Collections.Generic;

namespace NewsProject.Models
{
    public interface INewsRepository
    {
        IEnumerable<News> GetAllNews();
        News GetNews(int id);
        News Create(NewsCreateDTO news);
        NewsUpdateDTO GetUpdate(int id);
        News Update(NewsUpdateDTO news);
        News Delete(int id);
    }
}
