using AutoMapper;
using System.Windows;
using WypozyczalniaRowerow.Mapping;


namespace WypozyczalniaRowerow
{
    public partial class MainWindow : Window
    {
        private MainViewModel VM => (MainViewModel)DataContext;
        private IMapper _mapper;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = config.CreateMapper();
        }

        

        private void SzukajKlienta_Click(object sender, RoutedEventArgs e)
        {
            var nazwisko = txtSzukajKlienta.Text;

            using (var context = new AppDbContext())
            {
                var wyniki = context.Klienci
                    .Where(k => k.Nazwisko.Contains(nazwisko))
                    .ToList()
                    .Select(k => _mapper.Map<KlientDto>(k).PelnaNazwa)
                    .ToList();

                if (wyniki.Count == 0)
                {
                    VM.Wyniki.Clear();
                    VM.Wyniki.Add("Brak klientów spełniających kryteria");
                }
                else
                {
                    VM.Wyniki.Clear();

                    foreach (var w in wyniki)
                    {
                        VM.Wyniki.Add(w);
                    }
                }
            }
        }

        private void SzukajRower_Click(object sender, RoutedEventArgs e)
        {
            var typ = txtSzukajRower.Text;

            using (var context = new AppDbContext())
            {
                var wyniki = context.Rowery
                    .Where(r => r.Typ.Contains(typ))
                    .ToList()
                    .Select(r => _mapper.Map<RowerDto>(r).Opis)
                    .ToList();

                if (wyniki.Count == 0)
                {
                    VM.Wyniki.Clear();
                    VM.Wyniki.Add("Brak rowerów spełniających kryteria");
                }
                else
                {
                    VM.Wyniki.Clear();

                    foreach (var w in wyniki)
                    {
                        VM.Wyniki.Add(w);
                    }
                }
            }
        }

        private void Wypozycz_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtIdKlienta.Text, out int idKlienta) ||
                !int.TryParse(txtIdRoweru.Text, out int idRoweru) ||
                !int.TryParse(txtDni.Text, out int dni))
            {
                VM.Wyniki.Clear();
                VM.Wyniki.Add("Błędne ID lub ilość dni");
                return;
            }

            using (var context = new AppDbContext())
            {
                var klient = context.Klienci.Find(idKlienta);
                var rower = context.Rowery.Find(idRoweru);

                if (klient == null)
                {
                    VM.Wyniki.Clear();
                    VM.Wyniki.Add("Klient nie istnieje");
                    return;
                }

                if (rower == null)
                {
                    VM.Wyniki.Clear();
                    VM.Wyniki.Add("Rower nie istnieje");
                    return;
                }

                var czyZajety = context.Wypozyczenia
                    .Any(w => w.RowerId == idRoweru && w.DataZwr > DateTime.Now);

                if (czyZajety)
                {
                    VM.Wyniki.Clear();
                    VM.Wyniki.Add("Rower jest już wypożyczony");
                    return;
                }

                var wypozyczenie = new Wypozyczenie
                {
                    KlientId = idKlienta,
                    RowerId = idRoweru,
                    DataWyp = DateTime.Now,
                    DataZwr = DateTime.Now.AddDays(dni)
                };

                context.Wypozyczenia.Add(wypozyczenie);
                context.SaveChanges();

                VM.Wyniki.Clear();
                VM.Wyniki.Add($"Wypożyczono {rower.Marka} {rower.Model} dla {klient.Imie} {klient.Nazwisko} na {dni} dni");
            }
        }


    }
}