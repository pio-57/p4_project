using System.ComponentModel.DataAnnotations;

public class Klient
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string Imie { get; set; }

    [Required]
    [MaxLength(30)]
    public string Nazwisko { get; set; }

    [MaxLength(50)]
    public string Telefon { get; set; }

    [MaxLength(50)]
    public string Email { get; set; }

    public List<Wypozyczenie> Wypozyczenia { get; set; }
}