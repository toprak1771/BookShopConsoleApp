using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop
{
    class Book : BaseClass
    {
        public static List<Book> Books = new List<Book>();

        
        public string Name { get; set; } // kitabın adı
        public BookTypesEnum BookType { get; set; } // kitap türü
        public double CostPrice { get; set; } // maliyet fiyati
        public int TaxPercantage { get; set; } // toplam vergiyi belirtir
        public int ProfitMargin { get; set; } // kazanç yüzedi ör. 15 => %18
        public double Price { get; set; } // ürün fiyatı
        public int QTY { get; set; } // quantity qty -> stokdaki adedi gösterir
        
        
	

	    
        public Book(string _name,double _costPrice,BookTypesEnum _bookTypeEnum, int _taxPercantage=1, int _profitMargin=10,int _qty=1)
        {
            Name = _name;
            CostPrice = _costPrice;
            BookType = _bookTypeEnum;
            TaxPercantage = _taxPercantage;
            ProfitMargin = _profitMargin;
            QTY = _qty;

          

           Price = calculatedPrice(_costPrice, _taxPercantage, _profitMargin);

        }
        public static double calculatedPrice(double costPrice,int tax,int profitMargin)
        {
            double taxPrice = (costPrice * tax) / 100;
            double profitPrice = (costPrice * profitMargin) / 100;
            double price = costPrice + taxPrice + profitPrice;
            return price;
        }
        public static void addBook(Book book2)
        {
            try
            {
                //Kitap oluşturuldu.
                Books.Add(book2);

                //Ürün maliyeti hesaplanan metot sayesinde tutar hesaplandı.
                double amount=CaseTransaction.calculatedAmount(book2.CostPrice, book2.QTY);

                //Kasa hareketleri nesnesi oluşturuldu.
                CaseTransaction caseTransaction = new CaseTransaction(amount,TransactionTypeEnums.EXPENSE);

                //Kasa hareketini kaydetmek için CaseTransaction sınıfındaki save metodu
                CaseTransaction.saveCaseTransaction(caseTransaction);



            }
            catch(Exception ex)
            {
                Console.WriteLine("Hata Oluştu=" + ex.Message);
            }

        }

        public static void removeBook(int ıd)
        {
            foreach (Book book in Books)
            {
                if (book.Id==ıd)
                {
                    Books.Remove(book);
                    break;
                }
            }
        }

        // Satılacak kitabın idsini ve kaç kitap olduğu bilgisi alınır.
        public static void bookSell(int bookId,int bookQty)
        {
            foreach (Book book in Books)
            {
                if (book.Id==bookId)
                {
                    if (book.QTY >= bookQty)
                    {
                        //Satılan Kitap Miktarı Kitap Listesindeki kitap bilgisinden çıkarıldı.
                        book.QTY = book.QTY - bookQty;
                        //Satışı kasaya kaydetme işlemleri
                        //Satış Tutarı Hesaplandı
                        double profitAmount = CaseTransaction.calculatedAmount(book.Price, bookQty);
                        //Kasa hareketi işlendi.
                        CaseTransaction caseTransaction = new CaseTransaction(profitAmount, TransactionTypeEnums.INCOMING);
                        CaseTransaction.saveCaseTransaction(caseTransaction);
                        Console.WriteLine("----------------------------------------------");
                        Console.WriteLine(book.ToString());
                        Console.WriteLine("----------------------------------------------");
                    }
                    else
                    {
                        Console.WriteLine("----------------------------------------------");
                        Console.WriteLine("Stokta bu kadar adet kitap bulunamadı.");
                        Console.WriteLine("----------------------------------------------");
                        //Console.WriteLine(book.ToString());
                        Console.WriteLine("----------------------------------------------");
                    }
                    
                    
                }
            }
        }

        public static void bookSearch(string bookName)
        {
            foreach  (Book book in Books)
            {
                if (book.Name.Contains(bookName))
                {
                    Console.WriteLine(book.ToString());
                    Console.WriteLine("*******************");
                }
                else 
                {
                    Console.WriteLine("Aradığınız kitap bulunamadı.");
                    Console.WriteLine("*******************");
                }
            }
        }

        public static void bookUpdate(int bookID)
        {
            int option2 = 0;
            foreach (Book book in Books)
            {
                if (book.Id==bookID)
                {
                    Console.WriteLine(book.ToString());
                    Console.WriteLine("***********************");
                    Console.WriteLine("Değiştirmek istediğiniz bilgiyi giriniz:");
                    Console.WriteLine("Kitap Adı=1 -- Maliyet=2 -- Kitap Türü=3 -- Vergi Oranı=4 -- Kazanç Oranı=5 -- Kitabın Adedi=6 ");
                    option2 = Convert.ToInt32(Console.ReadLine());
                    switch (option2)
                    {
                        case 1:
                            Console.WriteLine("Yeni ismi giriniz=");
                            string newName = Console.ReadLine();
                            book.Name = newName;
                            Console.WriteLine(book.ToString());
                            break;
                        case 2:
                            Console.WriteLine("Yeni maliyeti giriniz=");
                            int newCost = Convert.ToInt32(Console.ReadLine());
                            book.CostPrice = newCost;
                            double newPrice=calculatedPrice(book.CostPrice,book.TaxPercantage,book.ProfitMargin);
                            book.Price = newPrice;
                            Console.WriteLine(book.ToString());
                            break;
                        case 3:
                            Console.WriteLine("Yeni türü giriniz(0-4)=");
                            int newType = Convert.ToInt32(Console.ReadLine());
                            BookTypesEnum typesEnum = (BookTypesEnum)newType;                                                         
                            Console.WriteLine(book.ToString());
                            break; 
                        case 4:
                            Console.WriteLine("Yeni vergi oranını giriniz=");
                            int newTaxPercantage = Convert.ToInt32(Console.ReadLine());
                            book.TaxPercantage=newTaxPercantage;
                            double newPrice2 = calculatedPrice(book.CostPrice, book.TaxPercantage, book.ProfitMargin);
                            book.Price = newPrice2;
                            Console.WriteLine(book.ToString());
                            break;
                        case 5:
                            Console.WriteLine("Yeni kazanç oranını giriniz=");
                            int newProfitMargin = Convert.ToInt32(Console.ReadLine());
                            book.ProfitMargin = newProfitMargin;
                            double newPrice3 = calculatedPrice(book.CostPrice, book.TaxPercantage, book.ProfitMargin);
                            Console.WriteLine(book.ToString());
                            break;
                        case 6:
                            Console.WriteLine("Yeni kitap adedini giriniz=");
                            int newBookQTY = Convert.ToInt32(Console.ReadLine());
                            book.QTY=newBookQTY;
                            Console.WriteLine(book.ToString());
                            break;

                        default:
                            break;
                    }

                }
            }
        }

        public override string ToString()
        {
            return string.Format("Id={0} --" + "Name={1} --" +"Type={2} --"+ "Cost Price={3} --" + "Price={4} --"+ "Miktarı={5}", Id,Name,BookType,CostPrice,Price,QTY);
        }

    }
}
