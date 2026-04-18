using System.Collections.ObjectModel;
using System.Windows.Input;

public class MainViewModel
{
    public string Imie { get; set; }
    public string Nazwisko { get; set; }
    public string Telefon { get; set; }
    public string Email { get; set; }
    public string Model { get; set; }
    public string Typ { get; set; }
    public string Marka { get; set; }
    public string Cena { get; set; }

    public ObservableCollection<string> Wyniki { get; set; } = new();

    public ICommand DodajKlientaCommand { get; }
    public ICommand DodajRowerCommand { get; }

    public MainViewModel()
    {
        DodajKlientaCommand = new RelayCommand(DodajKlienta);
        DodajRowerCommand = new RelayCommand(DodajRower);
    }

    private void DodajKlienta()
    {
        var klient = new Klient
        {
            Imie = Imie,
            Nazwisko = Nazwisko,
            Telefon = Telefon,
            Email = Email
        };

        var validator = new KlientValidator();
        var result = validator.Validate(klient);

        if (!result.IsValid)
        {
            Wyniki.Clear();
            foreach (var e in result.Errors)
                Wyniki.Add(e.ErrorMessage);
            return;
        }

        using (var context = new AppDbContext())
        {
            context.Klienci.Add(klient);
            context.SaveChanges();
        }

        Wyniki.Clear();
        Wyniki.Add("Dodano klienta!");
    }

    private void DodajRower()
    {
        decimal cenaParsed;

        if (!decimal.TryParse(Cena, out cenaParsed))
        {
            Wyniki.Clear();
            Wyniki.Add("Niepoprawna cena!");
            return;
        }

        var rower = new Rower
        {
            Model = Model,
            Typ = Typ,
            Marka = Marka,
            CenaZaDzien = cenaParsed,
            Dostepny = true
        };

        var validator = new RowerValidator();
        var result = validator.Validate(rower);

        if (!result.IsValid)
        {
            Wyniki.Clear();
            foreach (var e in result.Errors)
                Wyniki.Add(e.ErrorMessage);
            return;
        }

        using (var context = new AppDbContext())
        {
            context.Rowery.Add(rower);
            context.SaveChanges();
        }

        Wyniki.Clear();
        Wyniki.Add($"Dodano rower: {Marka} {Model} ({Typ}) - {cenaParsed} zł");
    }
}