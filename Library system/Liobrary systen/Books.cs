using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Library_sys
{
    interface operations
    {
        public int GetBook(List<Books> list, int limit);
        public int returnBook(Users user, List<Journal> journalList, List<Books> bookList);
        public Books giftbook();

        public Journal giftJournal();
    }
    //id ucun mecbr oldum basqa class yaraddim cunku basqacur mumkun deyil
 
    
    class Journal : Books
    {
        /*
        burada istedim ki books un icinde operationsdan overrirde ettiyim functionu tamamile atim
        operationsun icine ve onu umumi bir funksiya kimi istifade edib hem journal hemde books
        classinin icinde istifade edim amma ki orada bir obyektin ferqli propertylerini cagirmaq
        lazim gelirdi ve duzu onu nece edeceryimi bas aca bilmedim ona gore sade sekilde bele yaziram
        */

        // listin typeine gore elave etmek olmur
        //id ni class dan idare etmek qeyri mumkundur ne qedert ellesdimse de olmadi

        //isAvailable ni bookdan inherit etmek istedim ama strukturu tam anlaya bilmedim bookun fieldi hemde bunun fieldimi sayilir


        //isAvailable sadece ve sadece constructorun icinde teyin edende teyin olunur basqa cur deyismek olmur normalda field ile etmek isteydirdim
        
        public bool isAvailable { get; set; }
        public int JournalId { get; set; }
        public string JournalName { get; set; }

        public int yearOfPublication { get; set; }
        public string JournalPublisher { get; set; }


        public Journal()
        {
            
            
        }    
        
        public Journal(string journalName, string journalPublisher,int year,int JournalID)
        {
            JournalName = journalName;
            JournalPublisher = journalPublisher;
            yearOfPublication = year;
            JournalId = JournalID;
            isAvailable = true;

        }
        //burada deyesen generic class deye birsyeden istifasde olunmalid ama tam tutmadim duzu eslinde yuxardada dediyim kimi isteyirdim ki operators icinden her kii class a inherit edim ama list typeini ne vereceyimi bilmedim ond agore bele yaziram
        public int GetBook(List<Journal> list, int limit)
        {
            int current = -1;
                Console.WriteLine("Remaining Journals:");
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].isAvailable)
                    {
                        Console.WriteLine($"{i} {list[i].JournalName}");
                    }
                }
                Console.WriteLine("which one would you like to get?");
            try
            {
                current = int.Parse(Console.ReadLine());
                
            }
            catch (Exception ex) { Console.WriteLine("invalid input"); return current; }
        
                return current;
        }


    }
        
    


    internal class Books : operations
    {
        



        public int BookId { get; set; }
        public string BookName { get; set; }
        public string Genre { get; set; }
        public string WriterName { get; set; }
        public string Publisher { get; set; }


        public bool IsAvailable { get; set; }


        public Books(string bookName, string genre, string writerName, string publisher,int bookID)
        {
            BookId++;
            BookName = bookName;
            Genre = genre;
            WriterName = writerName;
            Publisher = publisher;
            BookId = bookID;
            IsAvailable = true;
        }

        public static void showbooks(Users user, List<Journal> journalList, List<Books> bookList)
        {
            for (int i = 0; i < user.bookdata.Count(); i++)
            {
                if (user.bookdata[i] > 20000)
                {
                    foreach (Journal jou in journalList)
                    {
                        if (jou.JournalId == user.bookdata[i])
                        {
                            Console.WriteLine($"{i}. {jou.JournalName}");
                        }
                    }
                }
                else
                {
                    foreach (Books boo in bookList)
                    {
                        if (boo.BookId == user.bookdata[i])
                        {
                            Console.WriteLine($"{i}. {boo.BookName}");
                        }
                    }
                }
                Thread.Sleep(1000);
            }

        }

        public Books() { }

        public int GetBook(List<Books> list, int limit)
        {
            if (limit < 3)
            {
                Console.WriteLine("Remaining books:");
                for (int i = 0;i<list.Count;i++)
                {
                    if (list[i].IsAvailable)
                    {
                        Console.WriteLine($"{i} {list[i].BookName}"); 
                    }
                }
                Console.WriteLine("which one would you like to get?");
                int current = int.Parse(Console.ReadLine());
                return current;

                

            }
            else
            {
                Console.WriteLine("You've reached your limit.");
                return -1;
            }
        }


        public Journal giftJournal()
        {
            Journal journal = new Journal();

            Console.Clear();

            Console.WriteLine("Name: ");
            journal.JournalName = Console.ReadLine();

            Console.WriteLine("Publisher:");
            journal.JournalPublisher = Console.ReadLine();

            Console.WriteLine("Year: ");
            journal.yearOfPublication = int.Parse(Console.ReadLine());


            return journal;
        }
        public Books giftbook()
        {

            Books new_book = new Books();
            Console.Clear();
            Console.WriteLine("Name: ");
            new_book.BookName = Console.ReadLine();

            Console.WriteLine("Genre: ");
            new_book.Genre = Console.ReadLine();

            Console.WriteLine("Writername: ");
            new_book.WriterName = Console.ReadLine();

            Console.WriteLine("Publisher: ");
            new_book.Publisher = Console.ReadLine();



            return new_book;

        }

        
        public int returnBook(Users user,List<Journal> journalList,List<Books> bookList)
        {
            Console.WriteLine("Which book would you like to return?");
            showbooks(user, journalList, bookList);
            int ans = int.Parse(Console.ReadLine());

            return ans;
            // istedim ki butun isi burdan gorum ama "database" imiz mainde oldugu ucun sadece id ni return edib oradan isimi hell edecem
            
            

            
        }

        
    }
}
