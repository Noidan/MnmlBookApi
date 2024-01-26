var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var books = new List<Book> {
    new Book { Id = 1, Title = "Мир смерти", Author="Гарри Гаррисон" },
    new Book { Id = 2, Title = "1984", Author="Джордж Оруэлл" },
    new Book { Id = 3, Title = "Обитаемый остров", Author="Аркадий и Борис Стругацкие" },
    new Book { Id = 4, Title = "Дюна", Author="Фрэнк Герберт" },
};

app.MapGet("/book", () =>
{
    return books;
});

app.MapGet("/book/{id}", (int id) =>
{
    var book = books.Find(b => b.Id == id);
    if (book == null)
        return Results.NotFound("Этой книги нет в списке");

    return Results.Ok(book);
});

app.MapPost("/book", (Book book) => {
    books.Add(book);
    return books;
    });

app.MapPut("/book/{id}", ( Book updatedBook, int id) =>
{
    var book = books.Find(b => b.Id == id);
    if (book == null)
        return Results.NotFound("Этой книги нет в списке");

    book.Title = updatedBook.Title;
    book.Author = updatedBook.Author;

    return Results.Ok(books);
});

app.MapDelete("/book/{id}", (int id) =>
{
    var book = books.Find(b => b.Id == id);
    if (book == null)
        return Results.NotFound("Этой книги нет в списке");

    books.Remove(book);

    return Results.Ok(books);
});

app.Run();

class Book
{
    public int Id { get; set; } 
    public required string Title { get; set; }  
    public required string Author { get; set; }
}
