using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Wypozyczenia")]
public class Wypozyczenie
{
    [Key]
    public int Id { get; set; }

    public DateTime DataWyp { get; set; }
    public DateTime DataZwr { get; set; }

    public int KlientId { get; set; }
    public Klient Klient { get; set; }

    public int RowerId { get; set; }
    public Rower Rower { get; set; }
}