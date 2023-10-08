using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Conf2023.EFmodel;

public class Book
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
    public int Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public int AuthorId { get; set; }
    public Author Author { get; set; }
}
