using System.ComponentModel.DataAnnotations;

public class Rower
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(10)]
    public string Model { get; set; }

    [Required]
    [MaxLength(30)]
    public string Typ { get; set; }

    [Required]
    [MaxLength(30)]
    public string Marka { get; set; }

    public decimal CenaZaDzien { get; set; }

    public bool Dostepny { get; set; }

    public List<Wypozyczenie> Wypozyczenia {  get; set; }
}