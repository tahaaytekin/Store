using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBook
    {
         private readonly IBookStoreDbContext _context;
         public int BookId { get; set; }

         public DeleteBook(IBookStoreDbContext context)
         {
            _context=context;
         }
         public void Handle()
         {
             var book =_context.Books.SingleOrDefault(x => x.Id ==BookId);
            if(book==null) throw new InvalidOperationException("Silinecek Kitap Bulunamadı");
            _context.Books.Remove(book);
            _context.SaveChanges();
         }
    }
}