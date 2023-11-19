using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public UpdateGenreModel Model { get; set; }
        public int GenreId { get; set; }
        private readonly IBookStoreDbContext _context;

        public UpdateGenreCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
             var genre=_context.Genres.SingleOrDefault(x=>x.Id==GenreId);
             if (genre is null)
             {
                throw new InvalidOperationException("Kitap türü Bulunamadı");
             }
            if (_context.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId))
            {
                throw new InvalidOperationException("Aynı isimli bir kitap türü mevcut");
            }
            genre.Name=string.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name: Model.Name;
            genre.IsActive=Model.IsActive;
            _context.SaveChanges();
        }
    }
    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } =true;
    }
}