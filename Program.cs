using System;

namespace BookShop
{
    class Program
    {
        /// <summary>
        /// Kitap Mağazası Uygulaması
        ///
        /// Kitap, Kasa
        ///
        /// 1 - kitap kayıt edebilmeyi
        ///     -- kayıt esnasında kitap adi, adedi, maliyet fiyati, vergisi, kazanç miktari vs.
        ///     -- ürün fiyati maliyet fiyati, vergi ve kazanç miktarina bağlı olarak hesaplanır
        /// 2 - kitap silebilme
        ///     -- kita silme fonksiyonu seçilirse girilen adet kadar kitap silinecektir
        /// 3 - kitap güncelleme
        /// 4 - kitap satış
        ///     -- satılan kitap fiyatı kasaya gelir olarak giriş yapılır
        ///     -- satılan kitap kitap listemden eksiltilir
        /// 5 - kitap listesi
        /// 6 - kitap listesinden arama kabiliyeti
        ///
        ///  Kitap -> id, adi, tür'ü (enum kullanacağız), maliyet fiyati, toplam vergi, stok adedi, kayit tarihi, güncelleme tarihi
        ///  Kasa işlemi -> id, tür (gelir , gider (enum kullanacağım)), tutar, kayit tarihi
        static void Main(string[] args)
        {
            int option=0;
            while (option != 8)
            {
                Console.WriteLine("1--Kitap Ekleme");
                Console.WriteLine("2--Kitap Silme");
                Console.WriteLine("3--Güncelleme");
                Console.WriteLine("4--Kitap Satış");
                Console.WriteLine("5--Kitap Listeleme");
                Console.WriteLine("6--Kitap Ara");
                Console.WriteLine("7--Kasa Hareketleri");
                Console.WriteLine("8--Çıkış");

                option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        BookAddCase();
                        Console.Clear();
                        BookLıst();
                        break;

                    case 2:
                        BookLıst();
                        Console.Write("Silmek İstediğiniz Kitap ID'si=?");
                        int bookId = Convert.ToInt32(Console.ReadLine());
                        Book.removeBook(bookId);
                        Console.Clear();
                        BookLıst();
                        break;

                    case 3:
                        BookLıst();
                        Console.WriteLine("Güncellemek istediğiniz kitabın ıd'sini giriniz=");
                        int bookUptatedId = Convert.ToInt32(Console.ReadLine());
                        Book.bookUpdate(bookUptatedId);
                        break;

                    case 4:
                        BookLıst();
                        Console.WriteLine("Satılan kitap ID'si?");
                        int bookSellId = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Kaç adet Kitap Satıldı Giriniz=?");
                        int bookSellQty = Convert.ToInt32(Console.ReadLine());
                        Book.bookSell(bookSellId, bookSellQty);                        
                        
                        

                        break;
                    case 5:
                        BookLıst();
                        break;

                    case 6:
                        Console.WriteLine("Aramak istediğniz kitabın adınızı giriniz:");
                        string bookName = Console.ReadLine();
                        Book.bookSearch(bookName);
                        break;
                    case 7:
                        CaseTransaction.CaseTransactionLıst();
                        break;

                    default:
                        break;
                }
            }
        
            

            foreach (CaseTransaction caseTransaction in CaseTransaction.CaseTransactions)
            {
                Console.WriteLine("Kasa Hareketleri=");
                Console.WriteLine(caseTransaction.ToString());
                Console.WriteLine("---------------------------------------------------------------------------------------------");

            }


        }
        public static void BookAddCase()
        {
            //Kitap Ekleme
            //Adı,Maliyet Fiyatı,Türü,Tax,Kazanç Miktarı,,adet

            //Kitap Adı
            Console.Write("Kitap Adı=");
            string newBook = Console.ReadLine();

            //Kitap Maliyeti
            double costPrice = 0;
            try
            {
                Console.Write("Maliyet=");
                 costPrice = Convert.ToDouble(Console.ReadLine());
            }
            catch(Exception e)
            {
                Console.WriteLine("Lütfen bir sayı giriniz");
                             
            }


            //Kitap Türü
            Console.Write("Kitap Türü(0-4)=");
            BookTypesEnum bookType = (BookTypesEnum)Convert.ToInt32(Console.ReadLine());

            //Vergi Oranı
            Console.Write("Vergi Oranı:");
            int taxPercentage = Convert.ToInt32(Console.ReadLine());

            //Kazanç Oranı
            Console.Write("Kazanç Oranı=");
            int profitMargin = Convert.ToInt32(Console.ReadLine());

            //Miktar
            Console.Write("Kitabın Adedi=");
            int qty = Convert.ToInt32(Console.ReadLine());

            Book newBook2 = new Book(newBook, costPrice, bookType, taxPercentage, profitMargin, qty);
            Book.addBook(newBook2);
        }

        public static void BookLıst()
        {
            foreach (Book item in Book.Books)

            {
                Console.WriteLine("---------------------------------------------------------------------------------------------");
                Console.WriteLine("Kitap Listesi=");
                Console.WriteLine(item.ToString());
                Console.WriteLine("---------------------------------------------------------------------------------------------");

            }
        }
    }
}
