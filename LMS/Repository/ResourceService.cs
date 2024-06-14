using LMS.DTOs;
using LMS.Helpers;
using LMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace LMS.Repository
{
    public class ResourceService : IResourceService
    {

        private readonly DataContext _Context;
        private readonly JWTService _jWTService;

        //Contructor of the ResourceService
        public ResourceService(DataContext Context,JWTService jWTService)
        {
            _Context = Context;
            _jWTService = jWTService;
        }

        public async Task<AddBookResponseDto> AddResource(AddBookRequestDto book,HttpContext httpContext)
        {
            var addedby = _jWTService.GetUsername(httpContext);
            var resource = await _Context.Resources.FirstOrDefaultAsync(u => u.ISBN == book.ISBN); 

            if (resource == null)// Check resource already in DB
            {
                if (await _Context.Author.FirstOrDefaultAsync(u => u.AuthorName == book.Author) == null) //Check is he new author
                {
                    var auth = new Author
                    {
                        AuthorName = book.Author
                    };
                    _Context.Author.Add(auth);   //Add the Author
                    await _Context.SaveChangesAsync();
                }
                

                var reso = new Resource //Make Resource
                {
                    ISBN = book.ISBN,
                    Title = book.Title,
                    AuthorName = book.Author,
                    Type = book.Type,
                    Quantity = book.Quantity,
                    Borrowed = 0,
                   //year=book.Year,
                    Description = book.Description,
                    Price = book.Price,
                    PageCount = book.Pages,
                    AddedOn = DateTime.UtcNow,
                    ImageURL = book.ImagePath,
                    AddedByID = addedby,
                    BookLocation =  book.CupboardId+"-"+book.ShelfNo,
                };
                _Context.Resources.Add(reso); //Add the Resource
                await _Context.SaveChangesAsync();

                var response = new AddBookResponseDto //Make Response
                {
                    ISBN = reso.ISBN,
                    Title = reso.Title
                };

                return response; //Return Response

            }
            else
            {
                throw new Exception("Book Already in the library");
            }
        }

        public async Task<bool> DeleteResource(string isbn)
        {
            var reso = await _Context.Resources.FirstOrDefaultAsync(u => u.ISBN == isbn);
            if (reso != null)
            {
                _Context.Resources.Remove(reso);
                await _Context.SaveChangesAsync();
                return true;
            }
            else
            {
                throw new Exception("No Resource Found");
            }
        }

        public async Task<List<ResourceListDto>> SearchResources(SearchbookDto searchbookDto)
        {
            var records = new List<Resource>();
            List<ResourceListDto> reso = new List<ResourceListDto>();


            if (searchbookDto.keyword == "")
            {
                records = _Context.Resources.ToList();
            }
            if (searchbookDto.tag == "all")
            {
                records = _Context.Resources.Where(e =>
                    e.Title.ToLower().Contains(searchbookDto.keyword.ToLower())||
                   e.ISBN.ToLower().Contains(searchbookDto.keyword.ToLower()) ||
                   e.AuthorName.ToLower().Contains(searchbookDto.keyword.ToLower()) 
                   ).ToList();
            }
            else if (searchbookDto.tag == "title")
            {
                records = _Context.Resources.Where(e => e.Title.ToLower().Contains(searchbookDto.keyword.ToLower())).ToList();
            }
            else if (searchbookDto.tag == "isbn")
            {
                records = _Context.Resources.Where(e => e.ISBN.ToLower().Contains(searchbookDto.keyword.ToLower())).ToList();
            }
            else if (searchbookDto.tag == "author")
            {
                records = _Context.Resources.Where(e => e.AuthorName.ToLower().Contains(searchbookDto.keyword.ToLower())).ToList();
            }

            if (searchbookDto.type != "all")
            {
                records = records.Where(e => e.Type.ToLower() == searchbookDto.type.ToLower()).ToList();
            }


            foreach (var x in records)
            {
                int count= _Context.Reservations.Where(e => e.ResourceId == x.ISBN).Count();
                var y = new ResourceListDto
                {
                    isbn = x.ISBN,
                    title = x.Title,
                    noOfBooks = x.Quantity+x.Borrowed,
                    url = x.ImageURL,
                    type = x.Type,
                    remain=x.Quantity,
                    dateadded=x.AddedOn,
                    noOfRes=count,
                    author = x.AuthorName,
                    location = x.BookLocation
                };

                reso.Add(y);
            }
            return (reso);
        }


        public async Task<bool> EditResource(AddBookRequestDto book)
        {
            var resource = await _Context.Resources.FirstOrDefaultAsync(u => u.ISBN == book.ISBN);

            if (resource != null)
            {
                
                    resource.Title = book.Title;
                
                    resource.Price = book.Price;
                
                    resource.Quantity = book.Quantity;
                
                    resource.BookLocation =book.CupboardId.ToString()+"-"+book.ShelfNo.ToString();
                
                    resource.PageCount = book.Pages;
               
                    resource.ImageURL = book.ImagePath;
               
                    resource.Type = book.Type;

                    resource.ImageURL = book.ImagePath;

                    resource.Description = book.Description;

               
                    var author = await _Context.Author.FirstOrDefaultAsync(u => u.AuthorName == book.Author);
                    if (author == null)
                    {
                        var newauthor = new Author();
                        newauthor.AuthorName = book.Author;
                        _Context.Author.Add(newauthor);
                    }
                    resource.AuthorName = book.Author;

              
                await _Context.SaveChangesAsync();
                return true;
            }
            else
            {
                throw new Exception("Resource not found");
            }
        }

        public async Task<AboutResourceDto> AboutResource(string isbn)
        {
            var resource = await _Context.Resources.FirstOrDefaultAsync(u => u.ISBN == isbn);
            if(resource == null)
            {
                throw new Exception("Book Not Found");
            }
            else
            {
                var location= await _Context.Locations.FirstOrDefaultAsync(u => u.LocationNo == resource.BookLocation);
                var cupboard = await _Context.Cupboard.FirstOrDefaultAsync(u => u.cupboardID == location.CupboardId);
                var res = new AboutResourceDto
                {
                        ISBN=resource.ISBN,
                        Type=resource.Type,
                        Title=resource.Title,
                        Author=resource.AuthorName,
                        Remain=resource.Quantity,
                        borrowed=resource.Borrowed,
                        total=resource.Quantity+resource.Borrowed,
                        CupboardId=cupboard.cupboardID.ToString(), 
                        CupboardName=cupboard.name,
                        ShelfId=location.ShelfNo.ToString(),
                        Description=resource.Description,
                        pages=resource.PageCount,
                        price=resource.Price,
                        addedon=resource.AddedOn,
                        Imagepath=resource.ImageURL
                };
                return res;
            }
        }

    }
}
