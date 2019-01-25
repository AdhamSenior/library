using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Library.Models
{
    public class Kitob
    {
        public int KitobId { get; set; }
        public string nomi { get; set; }
        public DateTime chop_etilg_yil { get; set; }
        public string avtor { get; set; }
        public int JanrId { get; set; }
        public Janr janr { get; set; }
        public string nashriyot { get; set; }
        public int betlar_soni { get; set; }
        public int soni { get; set; }
        public string rasmi { get; set; }
        public string qisqacha_malumot { get; set; }
        IEnumerable<Foyd_ber_kitoblar> foyd_ber_kitoblar { get; set; }
    }
    public class Kitobxon
    {
        public int KitobxonId { get; set; }
        public string familiya { get; set; }
        public string ismi { get; set; }
        public string sharifi { get; set; }
        public DateTime tugilgan_yili { get; set; }
        public string millati { get; set; }
        public string passport_seriya { get; set; }
        public int passport_nomer { get; set; }
        public string adress { get; set; }
        public int telefon { get; set; }
        IEnumerable<Foyd_ber_kitoblar> foyd_ber_kitoblar { get; set; }
    }
    public class Xodim
    {
        public int XodimId { get; set; }
        public string familiya { get; set; }
        public string ismi { get; set; }
        public string sharifi { get; set; }
        public string login { get; set; }
        public string parol { get; set; }
        IEnumerable<Foyd_ber_kitoblar> foyd_ber_kitoblar { get; set; }
    }
    public class Foyd_ber_kitoblar
    {
        public int Foyd_ber_kitoblarId { get; set; }
        public int XodimId { get; set; }
        public Xodim xodim { get; set; }
        public int KitobxonId { get; set; }
        public Kitobxon kitobxon { get; set; }
        public int KitobId { get; set; }
        public Kitob kitob { get; set; }
        public DateTime berilgan_vaqti { get; set; }
        public DateTime qaytarilish_vaqti { get; set; }

    }
    public class Janr
    {
        public int JanrId { get; set; }
        public string janr_nomi { get; set; }
        IEnumerable<Kitob> kitob { get; set; }
    }
    public class BazaContext : DbContext
    {
        public DbSet<Kitob> Kitobs { get; set; }
        public DbSet<Kitobxon> Kitobxons { get; set; }
        public DbSet<Xodim> Xodims { get; set; }
        public DbSet<Foyd_ber_kitoblar> Foyd_ber_kitoblars { get; set; }
        public DbSet<Janr> Janrs { get; set; }
    }
    public class BazaInit : DropCreateDatabaseAlways<BazaContext>
    {
        protected override void Seed(BazaContext context)
        {
            context.Janrs.Add(new Janr { janr_nomi = "Boevik" });
            context.Janrs.Add(new Janr { janr_nomi = "Detektiv" });
            context.Janrs.Add(new Janr { janr_nomi = "Detskie" });
            context.Janrs.Add(new Janr { janr_nomi = "Dokumentalnoe" });
            context.Janrs.Add(new Janr { janr_nomi = "Kompyuter_i_internet" });
            context.Janrs.Add(new Janr { janr_nomi = "Lyubovnie" });
            context.Janrs.Add(new Janr { janr_nomi = "Fantastika" });
            context.Janrs.Add(new Janr { janr_nomi = "Yumor" });
            base.Seed(context);
        }
    }
}