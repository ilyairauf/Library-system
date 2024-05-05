using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace Library_sys
{
    
    internal class Program
    {
        //eslinde user.bookdata icine direk obyektleri atamq daha duzgun olardi ama bur userin hem jurnali hemde kitabi ola biler ve list de bir cur type qebul edir deye mecbur bele yazmali oldum
        
        static void Main(string[] args)
        {

            Books book = new Books();
            Journal journal = new Journal();

            int journalID = 20006;
            int bookID = 10003;
            List<Users> userdata = new List<Users>();
            List<Books> bookList = new List<Books>()
            {
            new Books("Hərb və Sülh", "Novella", "Lev Tolstoy", "Qanun nəşriyyatı",10001),
            new Books("Harry Potter", "Fantastik", "J.K.Rowling", "Qanun nəşriyyatı",10002),
            };
            List<Journal> journalList = new List<Journal>()
            {
                new Journal("New York Times", "The New York Times Company", 2006,20001),
                new Journal("New Yorker", "Conde Nast", 1925, 20002),
                new Journal("Wall Street Journal", "Dow Jones & Company", 1889,20003),
                new Journal("National Geographic", "National Geographic Society", 1888,20004),
                new Journal("Scientific American", "Springer Nature", 1845,20005)
            };

            
            while (true)
            {
                Console.WriteLine("Welcome to Paris library!");
                Console.WriteLine(@"1.Login
2.sign up
3.Exit");
                string input = Console.ReadLine();
                if (input == "1")
                {
                    Console.WriteLine("Nickname: ");
                    string nick = Console.ReadLine();
                    Console.WriteLine("Password: ");
                    dynamic pass = Console.ReadLine();
                    Users user = userdata.Where(x => x.UserName.Contains(nick)).FirstOrDefault();
                    if (user != null)
                    {
                        if (user.Password == pass)
                        {
                            Console.WriteLine("Login successful!");
                            while (true)
                            {
                                int exit = 0;


                                Thread.Sleep(1000);
                                Console.Clear();
                                Console.WriteLine(@"1.return book
2.check out books
3.get new book or journal
4.gift book
5.Exit");

                                string operation = Console.ReadLine();
                                void give(int num)
                                {
                                    if (user.bookdata[num] > 20000)
                                    {
                                        foreach (Journal jo in journalList)
                                        {
                                            if (jo.JournalId == user.bookdata[num])
                                            {
                                                jo.isAvailable = true;
                                                Console.WriteLine($"{jo.JournalName} has been deleted from your system thank you!");
                                            }
                                        }
                                        user.bookdata.RemoveAt(num);

                                    }
                                    else
                                    {
                                        foreach (Books jo in journalList)
                                        {
                                            if (jo.BookId == user.bookdata[num])
                                            {
                                                jo.IsAvailable = true;
                                                Console.WriteLine($"{jo.BookName} has been deleted from your system thank you!");
                                            }
                                        }
                                        user.bookdata.RemoveAt(num);
                                        foreach (int i in user.bookdata)
                                        {
                                            Console.WriteLine(i);
                                        }


                                    }
                                }

                                switch (operation)
                                {
                                    case "1":
                                        if (user.bookdata.Count > 0)
                                        {
                                            int answ = book.returnBook(user, journalList, bookList);
                                            switch (answ)
                                            {
                                                case 0:
                                                    give(0);
                                                    break;
                                                case 1:
                                                    give(1);
                                                    break;
                                                case 2:
                                                    give(2);
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("You have no book to return, enjoy our library!");
                                        }

                                        break;
                                    case "2":
                                        if(user.bookdata.Count > 0)
                                        {
                                            Console.WriteLine("Your Books:");
                                            Books.showbooks(user, journalList,bookList);
                                        }
                                        else
                                        {
                                            Console.WriteLine("You have no books.");
                                            Thread.Sleep(1000);
                                            Console.Clear();
                                        }
                                        break;
                                    case "3":
                                        if (user.bookdata.Count < 3)
                                        {
                                            Console.Write("Would you like to read here?(Y/N)");
                                            string ans = Console.ReadLine();
                                            Console.Clear();
                                            if (ans == "Y")
                                            {
                                                Console.WriteLine("Would you like 1.journal or a 2.book");
                                                Console.Write("Enter number: ");
                                                ans = Console.ReadLine();
                                                //burada eyni bir funksiyadan cox istifade edirem ama basqa cur nece edeceyimi bilmedim cunki her defe feqli bir obyektden aliram datani
                                                if (ans == "1")
                                                {
                                                    try
                                                    {
                                                        int curr = journal.GetBook(journalList, user.bookdata.Count);
                                                        if (journalList[curr].isAvailable == false)
                                                        {
                                                            Console.WriteLine("Sorry, this book is unavailable ");
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            journalList[curr].isAvailable = false;
                                                            user.bookdata.Add(journalList[curr].JournalId);
                                                            Console.WriteLine(" ");
                                                            Console.WriteLine($"Congratulations! you've acquired {journalList[curr].JournalName}");
                                                            Console.WriteLine("Keep in mind that you can read this only in here!");
                                                            Console.WriteLine(" ");
                                                            Console.WriteLine("Your books and journals: ");
                                                            Books.showbooks(user, journalList, bookList);
                                                            Thread.Sleep(3000);
                                                        }
                                                    }
                                                    catch (Exception ex) { Console.WriteLine("invalid input"); }
                                                    break;

                                                }
                                                else if (ans == "2")
                                                {
                                                    int curr = book.GetBook(bookList, user.bookdata.Count);
                                                    if (bookList[curr].IsAvailable == false)
                                                    {
                                                        Console.WriteLine("Sorry, this book is unavailable ");
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        bookList[curr].IsAvailable = false;
                                                        user.bookdata.Add(bookList[curr].BookId);
                                                        Console.WriteLine(" ");
                                                        Console.WriteLine($"Congratulations! you've acquired {bookList[curr].BookName}");
                                                        Console.WriteLine("Your books and journals: ");
                                                        Console.WriteLine(" ");
                                                        Books.showbooks(user, journalList, bookList);
                                                        Thread.Sleep(3000);
                                                        break;
                                                    }


                                                }
                                                else { Console.WriteLine("Invalid input!"); break; }

                                            }
                                            else if (ans == "N")
                                            {
                                                int curr = book.GetBook(bookList, user.bookdata.Count);
                                                try
                                                {
                                                    if (bookList[curr].IsAvailable == false)
                                                    {
                                                        Console.WriteLine("Sorry, this book is unavailable ");
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        bookList[curr].IsAvailable = false;
                                                        user.bookdata.Add(bookList[curr].BookId);
                                                        Console.WriteLine($"Congratulations! you've acquired {bookList[curr].BookName}");
                                                        Console.WriteLine("Your books and journals: ");
                                                        Books.showbooks(user, journalList, bookList);
                                                    }
                                                }
                                                catch (Exception ex) { Console.WriteLine("invalid input"); }
                                                break;
                                            }
                                            else { Console.WriteLine("Invalid input!"); break; }
                                        }
                                        else { Console.WriteLine("You've reached your limit you can't get books or journals anymore :( "); break; }
                                    case "4":
                                        Console.WriteLine("Is it a 1.journal or 2.Book?");
                                        string t = Console.ReadLine();
                                        switch (t)
                                        {
                                            case "1":
                                                Journal new_journal = book.giftJournal();
                                                new_journal.JournalId = journalID++;
                                                new_journal.isAvailable = true;
                                                journalList.Add(new_journal);

                                                break;
                                            case "2":
                                                Books new_b = book.giftbook();
                                                new_b.BookId = bookID++;
                                                new_b.IsAvailable = true;
                                                bookList.Add(new_b);
                                                break;
                                            default:
                                                Console.WriteLine("Invalid input");
                                                break;

                                        }
                                        break;
                                    case "5":
                                        Console.Clear();
                                        exit = 1;
                                        break;





                                }
                                if (exit == 1)
                                {
                                    break;
                                }


                            }


                        }


                    }
                    else { Console.Clear(); Console.WriteLine("nickname doesn't exist or wrong password");  Thread.Sleep(2000); Console.Clear(); }

                }
                else if (input == "2")
                {
                    Users user = new Users();
                    Console.WriteLine($@"Please enter your credentials");

                    Console.Write("nickname: ");
                    user.UserName = Console.ReadLine();

                    Console.Write("Fullname: ");
                    user.Fullname = Console.ReadLine();

                    Console.Write("Password: ");
                    user.Password = Console.ReadLine();

                    userdata.Add(user);
                    Console.Clear();
                    Console.WriteLine("Congratulations! You've successfully signed up");
                    Thread.Sleep(3000);
                    Console.Clear();
                    


                }
                else if (input == "3") { break; }
                else { Console.WriteLine("Invalid input "); }

            }
        }
    }


}